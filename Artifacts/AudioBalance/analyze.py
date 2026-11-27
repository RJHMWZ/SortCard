import wave, array, math, pathlib, json
root=pathlib.Path(r'D:/workspace/SortCard/Assets/_Project/Audio/SFX')
db=lambda v:round(20*math.log10(max(v,1e-9)),2)
rows=[]
for f in sorted(root.glob('*.wav')):
 with wave.open(str(f)) as w:
  rate=w.getframerate();ch=w.getnchannels();width=w.getsampwidth();raw=w.readframes(w.getnframes())
 if width!=2: print(f.name,'unsupported width',width);continue
 vals=[x/32768 for x in array.array('h',raw)]
 peak=max(abs(x) for x in vals); rms=math.sqrt(sum(x*x for x in vals)/len(vals))
 block=max(1,rate*ch//50)
 energies=[sum(x*x for x in vals[i:i+block])/len(vals[i:i+block]) for i in range(0,len(vals),block)]
 active=[e for e in energies if e>max(energies)*.01]
 act=math.sqrt(sum(active)/len(active))
 row=dict(file=f.name,seconds=round(len(vals)/rate/ch,2),peak_db=db(peak),rms_db=db(rms),active_rms_db=db(act));rows.append(row)
 print(f'{f.name:18} {row["seconds"]:4} s  peak {db(peak):6}  rms {db(rms):6}  active {db(act):6}')
pathlib.Path(r'D:/workspace/SortCard/Artifacts/AudioBalance/source-levels.json').write_text(json.dumps(rows,indent=2))