import numpy as np, wave, pathlib, re, math, json
root=pathlib.Path(r'D:/workspace/SortCard/Assets/_Project')
out=pathlib.Path(r'D:/workspace/SortCard/Artifacts/AudioBalance')
source=(root/'Scripts/Effects/AudioDirector.cs').read_text()
nums=lambda text:[float(x.strip().rstrip('f')) for x in text.split(',')]
gains=nums(re.search(r'float\[\] gains=\{([^}]+)',source)[1])
fg=nums(re.search(r'flipGains=\{([^}]+)',source)[1]);ig=nums(re.search(r'impactGains=\{([^}]+)',source)[1])
special=[float(re.search(name+r'=([.\d]+)f',source)[1]) for name in ['victoryGain','damageGain','failureGain']]
files=['CardFlip','CardCollect','PoolFill','Combo','Shuffle','MistClear','Victory','ButtonClick','CardImpact','CardReturn','TutorialStep']
clips={}
for path in (root/'Audio/SFX').glob('*.wav'):
 with wave.open(str(path)) as w:
  a=np.frombuffer(w.readframes(w.getnframes()),dtype='<i2').astype(float)/32768
  a=a.reshape(-1,w.getnchannels()).mean(axis=1)
  if w.getframerate()!=44100:a=np.interp(np.arange(int(len(a)*44100/w.getframerate()))*w.getframerate()/44100,np.arange(len(a)),a)
  clips[path.stem]=a
rows=json.loads((out/'source-levels.json').read_text()); levels={r['file'][:-4]:r for r in rows}
db=lambda n:20*math.log10(max(n,1e-9))
lines=['Audio balance: source-file measurements plus playback gain (master 0.8).','No source WAV files modified.','Audible RMS excludes blocks >20 dB below the loudest 20 ms block.','']
old=[.62,.38,.62,.44,.6,.5,.65,.48,.74,.38,.45]
for i,name in enumerate(files):
 if i in [0,8]:continue
 before=.9 if i==6 else old[i];after=special[0] if i==6 else gains[i]
 lines.append(f'{name:16} before {levels[name]["active_rms_db"]+db(.8*before):6.1f} dBFS   after {levels[name]["active_rms_db"]+db(.8*after):6.1f} dBFS')
for cue,prefix,arr in [(0,'CardFlip',fg),(8,'CardImpact',ig)]:
 for i,mult in enumerate(arr):
  name=prefix+str(i);lines.append(f'{name:16} before {levels[name]["active_rms_db"]+db(.8*old[cue]):6.1f} dBFS   after {levels[name]["active_rms_db"]+db(.8*gains[cue]*mult):6.1f} dBFS')

def render(events,master=.8,before=False):
 voices=[(0,np.zeros(0)) for _ in range(8)];specials=[(0,np.zeros(0)) for _ in range(2)];mix=np.zeros(44100*15);last={};fi=ii=next_voice=0
 for t,cue,chain in events:
  pos=int(t*44100)
  if cue>=0 and t-last.get(cue,-10)<.035:continue
  last[cue]=t
  playing=sum(end>pos for end,_ in voices)
  pitch=1;vg=1
  if cue==0:
   i=fi%3;fi+=1;name='CardFlip'+str(i);vg=1 if before else fg[i]
  elif cue==8:
   i=ii%3;ii+=1;name='CardImpact'+str(i);vg=1 if before else ig[i];pitch=min(1.22,1+chain*.025)
  elif cue==-1:name='LifeLoss'
  elif cue==-2:name='Failure'
  else:
   name=files[cue]
   if cue==3:pitch=min(1.18,.96+chain*.025)
  a=clips[name];a=np.interp(np.arange(0,len(a),pitch),np.arange(len(a)),a)
  if cue in [6,-2]:
   for end,buf in voices+specials:
    if end>pos:mix[pos:end]-=buf[-(end-pos):]
   voices=[(0,np.zeros(0)) for _ in voices];specials=[(0,np.zeros(0)) for _ in specials]
   gain=(.9 if cue==6 else .85) if before else special[0 if cue==6 else 2];idx=1;bank=specials
  elif cue==-1:
   gain=1 if before else special[1];idx=0;bank=specials
  else:
   overlap=1/math.sqrt(max(1,playing*.55));overlap=overlap if before else max(.8,overlap)
   gain=(old[cue] if before else gains[cue])*vg*overlap;bank=voices
   idx=next((i for i,(end,_) in enumerate(voices) if end<=pos),None)
   if idx is None:idx=next_voice%8;next_voice+=1
  end,buf=bank[idx]
  if end>pos:mix[pos:end]-=buf[-(end-pos):]
  buf=a*min(1,master*gain);end=pos+len(buf);mix[pos:end]+=buf;bank[idx]=(end,buf)
 return mix

events=[]
for i in range(6):
 t=.3+i*.45;events.extend([(t,0,i+1),(t+.28,1,i+1),(t+.28,8,i+1),(t+.28,3,i+1)])
events += [(3.4,2,6),(4.3,7,0),(4.6,4,0),(5.5,10,0),(6,-1,0),(7,6,0),(9,-2,0)]
for before in [True,False]:
 a=render(events,before=before);name='before-preview.wav' if before else 'balanced-preview.wav'
 with wave.open(str(out/name),'wb') as w:w.setparams((1,2,44100,0,'NONE','not compressed'));w.writeframes((np.clip(a,-1,1)*32767).astype('<i2').tobytes())
 lines.append(f'{name}: mixed peak {db(np.max(np.abs(a))):.2f} dBFS, clipped samples {np.sum(np.abs(a)>=1)}')
stress=[]
for i in range(24):
 t=.1+i*.10;stress.extend([(t,1,i+1),(t,8,i+1),(t,3,i+1)])
for master in [.8,1]:
 a=render(stress,master=master);peak=np.max(np.abs(a));lines.append(f'Rapid combo stress (10 collections/s), master {master}: peak {db(peak):.2f} dBFS, clipped samples {np.sum(np.abs(a)>=1)}');assert peak<1
report='\n'.join(lines)+'\n';(out/'balance-report.txt').write_text(report);print(report)