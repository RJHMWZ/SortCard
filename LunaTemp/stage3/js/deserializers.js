var Deserializers = {}
Deserializers["UnityEngine.JointSpring"] = function (request, data, root) {
  var i960 = root || request.c( 'UnityEngine.JointSpring' )
  var i961 = data
  i960.spring = i961[0]
  i960.damper = i961[1]
  i960.targetPosition = i961[2]
  return i960
}

Deserializers["UnityEngine.JointMotor"] = function (request, data, root) {
  var i962 = root || request.c( 'UnityEngine.JointMotor' )
  var i963 = data
  i962.m_TargetVelocity = i963[0]
  i962.m_Force = i963[1]
  i962.m_FreeSpin = i963[2]
  return i962
}

Deserializers["UnityEngine.JointLimits"] = function (request, data, root) {
  var i964 = root || request.c( 'UnityEngine.JointLimits' )
  var i965 = data
  i964.m_Min = i965[0]
  i964.m_Max = i965[1]
  i964.m_Bounciness = i965[2]
  i964.m_BounceMinVelocity = i965[3]
  i964.m_ContactDistance = i965[4]
  i964.minBounce = i965[5]
  i964.maxBounce = i965[6]
  return i964
}

Deserializers["UnityEngine.JointDrive"] = function (request, data, root) {
  var i966 = root || request.c( 'UnityEngine.JointDrive' )
  var i967 = data
  i966.m_PositionSpring = i967[0]
  i966.m_PositionDamper = i967[1]
  i966.m_MaximumForce = i967[2]
  i966.m_UseAcceleration = i967[3]
  return i966
}

Deserializers["UnityEngine.SoftJointLimitSpring"] = function (request, data, root) {
  var i968 = root || request.c( 'UnityEngine.SoftJointLimitSpring' )
  var i969 = data
  i968.m_Spring = i969[0]
  i968.m_Damper = i969[1]
  return i968
}

Deserializers["UnityEngine.SoftJointLimit"] = function (request, data, root) {
  var i970 = root || request.c( 'UnityEngine.SoftJointLimit' )
  var i971 = data
  i970.m_Limit = i971[0]
  i970.m_Bounciness = i971[1]
  i970.m_ContactDistance = i971[2]
  return i970
}

Deserializers["UnityEngine.WheelFrictionCurve"] = function (request, data, root) {
  var i972 = root || request.c( 'UnityEngine.WheelFrictionCurve' )
  var i973 = data
  i972.m_ExtremumSlip = i973[0]
  i972.m_ExtremumValue = i973[1]
  i972.m_AsymptoteSlip = i973[2]
  i972.m_AsymptoteValue = i973[3]
  i972.m_Stiffness = i973[4]
  return i972
}

Deserializers["UnityEngine.JointAngleLimits2D"] = function (request, data, root) {
  var i974 = root || request.c( 'UnityEngine.JointAngleLimits2D' )
  var i975 = data
  i974.m_LowerAngle = i975[0]
  i974.m_UpperAngle = i975[1]
  return i974
}

Deserializers["UnityEngine.JointMotor2D"] = function (request, data, root) {
  var i976 = root || request.c( 'UnityEngine.JointMotor2D' )
  var i977 = data
  i976.m_MotorSpeed = i977[0]
  i976.m_MaximumMotorTorque = i977[1]
  return i976
}

Deserializers["UnityEngine.JointSuspension2D"] = function (request, data, root) {
  var i978 = root || request.c( 'UnityEngine.JointSuspension2D' )
  var i979 = data
  i978.m_DampingRatio = i979[0]
  i978.m_Frequency = i979[1]
  i978.m_Angle = i979[2]
  return i978
}

Deserializers["UnityEngine.JointTranslationLimits2D"] = function (request, data, root) {
  var i980 = root || request.c( 'UnityEngine.JointTranslationLimits2D' )
  var i981 = data
  i980.m_LowerTranslation = i981[0]
  i980.m_UpperTranslation = i981[1]
  return i980
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Texture2D"] = function (request, data, root) {
  var i982 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Texture2D' )
  var i983 = data
  i982.name = i983[0]
  i982.width = i983[1]
  i982.height = i983[2]
  i982.mipmapCount = i983[3]
  i982.anisoLevel = i983[4]
  i982.filterMode = i983[5]
  i982.hdr = !!i983[6]
  i982.format = i983[7]
  i982.wrapMode = i983[8]
  i982.alphaIsTransparency = !!i983[9]
  i982.alphaSource = i983[10]
  i982.graphicsFormat = i983[11]
  i982.sRGBTexture = !!i983[12]
  i982.desiredColorSpace = i983[13]
  i982.wrapU = i983[14]
  i982.wrapV = i983[15]
  return i982
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.Scene"] = function (request, data, root) {
  var i984 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.Scene' )
  var i985 = data
  i984.name = i985[0]
  i984.index = i985[1]
  i984.startup = !!i985[2]
  return i984
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Camera"] = function (request, data, root) {
  var i986 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Camera' )
  var i987 = data
  i986.aspect = i987[0]
  i986.orthographic = !!i987[1]
  i986.orthographicSize = i987[2]
  i986.backgroundColor = new pc.Color(i987[3], i987[4], i987[5], i987[6])
  i986.nearClipPlane = i987[7]
  i986.farClipPlane = i987[8]
  i986.fieldOfView = i987[9]
  i986.depth = i987[10]
  i986.clearFlags = i987[11]
  i986.cullingMask = i987[12]
  i986.rect = i987[13]
  request.r(i987[14], i987[15], 0, i986, 'targetTexture')
  i986.usePhysicalProperties = !!i987[16]
  i986.focalLength = i987[17]
  i986.sensorSize = new pc.Vec2( i987[18], i987[19] )
  i986.lensShift = new pc.Vec2( i987[20], i987[21] )
  i986.gateFit = i987[22]
  i986.commandBufferCount = i987[23]
  i986.cameraType = i987[24]
  i986.enabled = !!i987[25]
  return i986
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.GameObject"] = function (request, data, root) {
  var i988 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.GameObject' )
  var i989 = data
  i988.name = i989[0]
  i988.tagId = i989[1]
  i988.enabled = !!i989[2]
  i988.isStatic = !!i989[3]
  i988.layer = i989[4]
  return i988
}

Deserializers["LittleFinds.GameManager"] = function (request, data, root) {
  var i990 = root || request.c( 'LittleFinds.GameManager' )
  var i991 = data
  request.r(i991[0], i991[1], 0, i990, 'Risk')
  request.r(i991[2], i991[3], 0, i990, 'Tutorial')
  request.r(i991[4], i991[5], 0, i990, 'Cards')
  request.r(i991[6], i991[7], 0, i990, 'Collections')
  request.r(i991[8], i991[9], 0, i990, 'Shuffle')
  request.r(i991[10], i991[11], 0, i990, 'Timer')
  request.r(i991[12], i991[13], 0, i990, 'UI')
  request.r(i991[14], i991[15], 0, i990, 'Feedback')
  request.r(i991[16], i991[17], 0, i990, 'Trash')
  return i990
}

Deserializers["LittleFinds.GameTimer"] = function (request, data, root) {
  var i992 = root || request.c( 'LittleFinds.GameTimer' )
  var i993 = data
  i992.Running = !!i993[0]
  i992.Expired = request.d('System.Action', i993[1], i992.Expired)
  return i992
}

Deserializers["System.Action"] = function (request, data, root) {
  var i994 = root || request.c( 'System.Action' )
  var i995 = data
  return i994
}

Deserializers["LittleFinds.CardManager"] = function (request, data, root) {
  var i996 = root || request.c( 'LittleFinds.CardManager' )
  var i997 = data
  var i999 = i997[0]
  var i998 = []
  for(var i = 0; i < i999.length; i += 2) {
  request.r(i999[i + 0], i999[i + 1], 2, i998, '')
  }
  i996.cards = i998
  var i1001 = i997[1]
  var i1000 = []
  for(var i = 0; i < i1001.length; i += 2) {
  request.r(i1001[i + 0], i1001[i + 1], 2, i1000, '')
  }
  i996.art = i1000
  request.r(i997[2], i997[3], 0, i996, 'game')
  return i996
}

Deserializers["LittleFinds.CollectionManager"] = function (request, data, root) {
  var i1006 = root || request.c( 'LittleFinds.CollectionManager' )
  var i1007 = data
  var i1009 = i1007[0]
  var i1008 = []
  for(var i = 0; i < i1009.length; i += 2) {
  request.r(i1009[i + 0], i1009[i + 1], 2, i1008, '')
  }
  i1006.pools = i1008
  var i1011 = i1007[1]
  var i1010 = []
  for(var i = 0; i < i1011.length; i += 2) {
  request.r(i1011[i + 0], i1011[i + 1], 2, i1010, '')
  }
  i1006.boxes = i1010
  return i1006
}

Deserializers["LittleFinds.ShuffleController"] = function (request, data, root) {
  var i1016 = root || request.c( 'LittleFinds.ShuffleController' )
  var i1017 = data
  request.r(i1017[0], i1017[1], 0, i1016, 'game')
  request.r(i1017[2], i1017[3], 0, i1016, 'center')
  return i1016
}

Deserializers["LittleFinds.GameUIController"] = function (request, data, root) {
  var i1018 = root || request.c( 'LittleFinds.GameUIController' )
  var i1019 = data
  request.r(i1019[0], i1019[1], 0, i1018, 'game')
  request.r(i1019[2], i1019[3], 0, i1018, 'helpButton')
  request.r(i1019[4], i1019[5], 0, i1018, 'lives')
  request.r(i1019[6], i1019[7], 0, i1018, 'time')
  request.r(i1019[8], i1019[9], 0, i1018, 'message')
  request.r(i1019[10], i1019[11], 0, i1018, 'modalTitle')
  request.r(i1019[12], i1019[13], 0, i1018, 'modalDescription')
  request.r(i1019[14], i1019[15], 0, i1018, 'modalButtonText')
  request.r(i1019[16], i1019[17], 0, i1018, 'soundLabel')
  request.r(i1019[18], i1019[19], 0, i1018, 'hapticLabel')
  request.r(i1019[20], i1019[21], 0, i1018, 'modal')
  request.r(i1019[22], i1019[23], 0, i1018, 'overlay')
  request.r(i1019[24], i1019[25], 0, i1018, 'startButton')
  request.r(i1019[26], i1019[27], 0, i1018, 'freshButton')
  request.r(i1019[28], i1019[29], 0, i1018, 'settingsRoot')
  request.r(i1019[30], i1019[31], 0, i1018, 'resultRoot')
  var i1021 = i1019[32]
  var i1020 = []
  for(var i = 0; i < i1021.length; i += 2) {
  request.r(i1021[i + 0], i1021[i + 1], 2, i1020, '')
  }
  i1018.resultBoxes = i1020
  return i1018
}

Deserializers["LittleFinds.FeedbackController"] = function (request, data, root) {
  var i1022 = root || request.c( 'LittleFinds.FeedbackController' )
  var i1023 = data
  request.r(i1023[0], i1023[1], 0, i1022, 'layer')
  request.r(i1023[2], i1023[3], 0, i1022, 'round')
  request.r(i1023[4], i1023[5], 0, i1022, 'burst')
  request.r(i1023[6], i1023[7], 0, i1022, 'ring')
  request.r(i1023[8], i1023[9], 0, i1022, 'star')
  request.r(i1023[10], i1023[11], 0, i1022, 'stage')
  request.r(i1023[12], i1023[13], 0, i1022, 'audio')
  request.r(i1023[14], i1023[15], 0, i1022, 'font')
  i1022.SoundOn = !!i1023[16]
  i1022.Haptics = !!i1023[17]
  return i1022
}

Deserializers["LittleFinds.AudioDirector"] = function (request, data, root) {
  var i1024 = root || request.c( 'LittleFinds.AudioDirector' )
  var i1025 = data
  var i1027 = i1025[0]
  var i1026 = []
  for(var i = 0; i < i1027.length; i += 2) {
  request.r(i1027[i + 0], i1027[i + 1], 2, i1026, '')
  }
  i1024.clips = i1026
  var i1029 = i1025[1]
  var i1028 = []
  for(var i = 0; i < i1029.length; i += 2) {
  request.r(i1029[i + 0], i1029[i + 1], 2, i1028, '')
  }
  i1024.flipVariants = i1028
  var i1031 = i1025[2]
  var i1030 = []
  for(var i = 0; i < i1031.length; i += 2) {
  request.r(i1031[i + 0], i1031[i + 1], 2, i1030, '')
  }
  i1024.impactVariants = i1030
  i1024.masterVolume = i1025[3]
  i1024.victoryGain = i1025[4]
  i1024.damageGain = i1025[5]
  i1024.failureGain = i1025[6]
  request.r(i1025[7], i1025[8], 0, i1024, 'damageClip')
  request.r(i1025[9], i1025[10], 0, i1024, 'failureClip')
  return i1024
}

Deserializers["LittleFinds.TutorialController"] = function (request, data, root) {
  var i1034 = root || request.c( 'LittleFinds.TutorialController' )
  var i1035 = data
  i1034.isTutorialLevel = !!i1035[0]
  request.r(i1035[1], i1035[2], 0, i1034, 'game')
  request.r(i1035[3], i1035[4], 0, i1034, 'coachRoot')
  request.r(i1035[5], i1035[6], 0, i1034, 'coach')
  request.r(i1035[7], i1035[8], 0, i1034, 'focus')
  request.r(i1035[9], i1035[10], 0, i1034, 'hand')
  request.r(i1035[11], i1035[12], 0, i1034, 'heading')
  request.r(i1035[13], i1035[14], 0, i1034, 'nextButton')
  request.r(i1035[15], i1035[16], 0, i1034, 'nextLabel')
  return i1034
}

Deserializers["LittleFinds.RoundRisk"] = function (request, data, root) {
  var i1036 = root || request.c( 'LittleFinds.RoundRisk' )
  var i1037 = data
  i1036.maximumLives = i1037[0]
  i1036.shuffleSeconds = i1037[1]
  i1036.simultaneousMistakeGrace = i1037[2]
  request.r(i1037[3], i1037[4], 0, i1036, 'game')
  return i1036
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.RectTransform"] = function (request, data, root) {
  var i1038 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.RectTransform' )
  var i1039 = data
  i1038.pivot = new pc.Vec2( i1039[0], i1039[1] )
  i1038.anchorMin = new pc.Vec2( i1039[2], i1039[3] )
  i1038.anchorMax = new pc.Vec2( i1039[4], i1039[5] )
  i1038.sizeDelta = new pc.Vec2( i1039[6], i1039[7] )
  i1038.anchoredPosition3D = new pc.Vec3( i1039[8], i1039[9], i1039[10] )
  i1038.rotation = new pc.Quat(i1039[11], i1039[12], i1039[13], i1039[14])
  i1038.scale = new pc.Vec3( i1039[15], i1039[16], i1039[17] )
  return i1038
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Canvas"] = function (request, data, root) {
  var i1040 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Canvas' )
  var i1041 = data
  i1040.planeDistance = i1041[0]
  i1040.referencePixelsPerUnit = i1041[1]
  i1040.isFallbackOverlay = !!i1041[2]
  i1040.renderMode = i1041[3]
  i1040.renderOrder = i1041[4]
  i1040.sortingLayerName = i1041[5]
  i1040.sortingOrder = i1041[6]
  i1040.scaleFactor = i1041[7]
  request.r(i1041[8], i1041[9], 0, i1040, 'worldCamera')
  i1040.overrideSorting = !!i1041[10]
  i1040.pixelPerfect = !!i1041[11]
  i1040.targetDisplay = i1041[12]
  i1040.overridePixelPerfect = !!i1041[13]
  i1040.enabled = !!i1041[14]
  return i1040
}

Deserializers["UnityEngine.UI.CanvasScaler"] = function (request, data, root) {
  var i1042 = root || request.c( 'UnityEngine.UI.CanvasScaler' )
  var i1043 = data
  i1042.m_UiScaleMode = i1043[0]
  i1042.m_ReferencePixelsPerUnit = i1043[1]
  i1042.m_ScaleFactor = i1043[2]
  i1042.m_ReferenceResolution = new pc.Vec2( i1043[3], i1043[4] )
  i1042.m_ScreenMatchMode = i1043[5]
  i1042.m_MatchWidthOrHeight = i1043[6]
  i1042.m_PhysicalUnit = i1043[7]
  i1042.m_FallbackScreenDPI = i1043[8]
  i1042.m_DefaultSpriteDPI = i1043[9]
  i1042.m_DynamicPixelsPerUnit = i1043[10]
  i1042.m_PresetInfoIsWorld = !!i1043[11]
  return i1042
}

Deserializers["UnityEngine.UI.GraphicRaycaster"] = function (request, data, root) {
  var i1044 = root || request.c( 'UnityEngine.UI.GraphicRaycaster' )
  var i1045 = data
  i1044.m_IgnoreReversedGraphics = !!i1045[0]
  i1044.m_BlockingObjects = i1045[1]
  i1044.m_BlockingMask = UnityEngine.LayerMask.FromIntegerValue( i1045[2] )
  return i1044
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer"] = function (request, data, root) {
  var i1046 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer' )
  var i1047 = data
  i1046.cullTransparentMesh = !!i1047[0]
  return i1046
}

Deserializers["UnityEngine.UI.Image"] = function (request, data, root) {
  var i1048 = root || request.c( 'UnityEngine.UI.Image' )
  var i1049 = data
  request.r(i1049[0], i1049[1], 0, i1048, 'm_Sprite')
  i1048.m_Type = i1049[2]
  i1048.m_PreserveAspect = !!i1049[3]
  i1048.m_FillCenter = !!i1049[4]
  i1048.m_FillMethod = i1049[5]
  i1048.m_FillAmount = i1049[6]
  i1048.m_FillClockwise = !!i1049[7]
  i1048.m_FillOrigin = i1049[8]
  i1048.m_UseSpriteMesh = !!i1049[9]
  i1048.m_PixelsPerUnitMultiplier = i1049[10]
  request.r(i1049[11], i1049[12], 0, i1048, 'm_Material')
  i1048.m_Maskable = !!i1049[13]
  i1048.m_Color = new pc.Color(i1049[14], i1049[15], i1049[16], i1049[17])
  i1048.m_RaycastTarget = !!i1049[18]
  i1048.m_RaycastPadding = new pc.Vec4( i1049[19], i1049[20], i1049[21], i1049[22] )
  return i1048
}

Deserializers["LittleFinds.SafeArea"] = function (request, data, root) {
  var i1050 = root || request.c( 'LittleFinds.SafeArea' )
  var i1051 = data
  request.r(i1051[0], i1051[1], 0, i1050, 'content')
  return i1050
}

Deserializers["LittleFinds.MobileGameLayout"] = function (request, data, root) {
  var i1052 = root || request.c( 'LittleFinds.MobileGameLayout' )
  var i1053 = data
  request.r(i1053[0], i1053[1], 0, i1052, 'board')
  request.r(i1053[2], i1053[3], 0, i1052, 'grid')
  request.r(i1053[4], i1053[5], 0, i1052, 'footer')
  request.r(i1053[6], i1053[7], 0, i1052, 'hint')
  request.r(i1053[8], i1053[9], 0, i1052, 'effects')
  request.r(i1053[10], i1053[11], 0, i1052, 'trash')
  request.r(i1053[12], i1053[13], 0, i1052, 'tutorial')
  var i1055 = i1053[14]
  var i1054 = []
  for(var i = 0; i < i1055.length; i += 2) {
  request.r(i1055[i + 0], i1055[i + 1], 2, i1054, '')
  }
  i1052.slots = i1054
  return i1052
}

Deserializers["UnityEngine.UI.Button"] = function (request, data, root) {
  var i1056 = root || request.c( 'UnityEngine.UI.Button' )
  var i1057 = data
  i1056.m_OnClick = request.d('UnityEngine.UI.Button+ButtonClickedEvent', i1057[0], i1056.m_OnClick)
  i1056.m_Navigation = request.d('UnityEngine.UI.Navigation', i1057[1], i1056.m_Navigation)
  i1056.m_Transition = i1057[2]
  i1056.m_Colors = request.d('UnityEngine.UI.ColorBlock', i1057[3], i1056.m_Colors)
  i1056.m_SpriteState = request.d('UnityEngine.UI.SpriteState', i1057[4], i1056.m_SpriteState)
  i1056.m_AnimationTriggers = request.d('UnityEngine.UI.AnimationTriggers', i1057[5], i1056.m_AnimationTriggers)
  i1056.m_Interactable = !!i1057[6]
  request.r(i1057[7], i1057[8], 0, i1056, 'm_TargetGraphic')
  return i1056
}

Deserializers["UnityEngine.UI.Button+ButtonClickedEvent"] = function (request, data, root) {
  var i1058 = root || request.c( 'UnityEngine.UI.Button+ButtonClickedEvent' )
  var i1059 = data
  i1058.m_PersistentCalls = request.d('UnityEngine.Events.PersistentCallGroup', i1059[0], i1058.m_PersistentCalls)
  return i1058
}

Deserializers["UnityEngine.Events.PersistentCallGroup"] = function (request, data, root) {
  var i1060 = root || request.c( 'UnityEngine.Events.PersistentCallGroup' )
  var i1061 = data
  var i1063 = i1061[0]
  var i1062 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Events.PersistentCall')))
  for(var i = 0; i < i1063.length; i += 1) {
    i1062.add(request.d('UnityEngine.Events.PersistentCall', i1063[i + 0]));
  }
  i1060.m_Calls = i1062
  return i1060
}

Deserializers["UnityEngine.Events.PersistentCall"] = function (request, data, root) {
  var i1066 = root || request.c( 'UnityEngine.Events.PersistentCall' )
  var i1067 = data
  request.r(i1067[0], i1067[1], 0, i1066, 'm_Target')
  i1066.m_TargetAssemblyTypeName = i1067[2]
  i1066.m_MethodName = i1067[3]
  i1066.m_Mode = i1067[4]
  i1066.m_Arguments = request.d('UnityEngine.Events.ArgumentCache', i1067[5], i1066.m_Arguments)
  i1066.m_CallState = i1067[6]
  return i1066
}

Deserializers["UnityEngine.Events.ArgumentCache"] = function (request, data, root) {
  var i1068 = root || request.c( 'UnityEngine.Events.ArgumentCache' )
  var i1069 = data
  request.r(i1069[0], i1069[1], 0, i1068, 'm_ObjectArgument')
  i1068.m_ObjectArgumentAssemblyTypeName = i1069[2]
  i1068.m_IntArgument = i1069[3]
  i1068.m_FloatArgument = i1069[4]
  i1068.m_StringArgument = i1069[5]
  i1068.m_BoolArgument = !!i1069[6]
  return i1068
}

Deserializers["UnityEngine.UI.Navigation"] = function (request, data, root) {
  var i1070 = root || request.c( 'UnityEngine.UI.Navigation' )
  var i1071 = data
  i1070.m_Mode = i1071[0]
  i1070.m_WrapAround = !!i1071[1]
  request.r(i1071[2], i1071[3], 0, i1070, 'm_SelectOnUp')
  request.r(i1071[4], i1071[5], 0, i1070, 'm_SelectOnDown')
  request.r(i1071[6], i1071[7], 0, i1070, 'm_SelectOnLeft')
  request.r(i1071[8], i1071[9], 0, i1070, 'm_SelectOnRight')
  return i1070
}

Deserializers["UnityEngine.UI.ColorBlock"] = function (request, data, root) {
  var i1072 = root || request.c( 'UnityEngine.UI.ColorBlock' )
  var i1073 = data
  i1072.m_NormalColor = new pc.Color(i1073[0], i1073[1], i1073[2], i1073[3])
  i1072.m_HighlightedColor = new pc.Color(i1073[4], i1073[5], i1073[6], i1073[7])
  i1072.m_PressedColor = new pc.Color(i1073[8], i1073[9], i1073[10], i1073[11])
  i1072.m_SelectedColor = new pc.Color(i1073[12], i1073[13], i1073[14], i1073[15])
  i1072.m_DisabledColor = new pc.Color(i1073[16], i1073[17], i1073[18], i1073[19])
  i1072.m_ColorMultiplier = i1073[20]
  i1072.m_FadeDuration = i1073[21]
  return i1072
}

Deserializers["UnityEngine.UI.SpriteState"] = function (request, data, root) {
  var i1074 = root || request.c( 'UnityEngine.UI.SpriteState' )
  var i1075 = data
  request.r(i1075[0], i1075[1], 0, i1074, 'm_HighlightedSprite')
  request.r(i1075[2], i1075[3], 0, i1074, 'm_PressedSprite')
  request.r(i1075[4], i1075[5], 0, i1074, 'm_SelectedSprite')
  request.r(i1075[6], i1075[7], 0, i1074, 'm_DisabledSprite')
  return i1074
}

Deserializers["UnityEngine.UI.AnimationTriggers"] = function (request, data, root) {
  var i1076 = root || request.c( 'UnityEngine.UI.AnimationTriggers' )
  var i1077 = data
  i1076.m_NormalTrigger = i1077[0]
  i1076.m_HighlightedTrigger = i1077[1]
  i1076.m_PressedTrigger = i1077[2]
  i1076.m_SelectedTrigger = i1077[3]
  i1076.m_DisabledTrigger = i1077[4]
  return i1076
}

Deserializers["LittleFinds.ButtonFeel"] = function (request, data, root) {
  var i1078 = root || request.c( 'LittleFinds.ButtonFeel' )
  var i1079 = data
  return i1078
}

Deserializers["UnityEngine.UI.Text"] = function (request, data, root) {
  var i1080 = root || request.c( 'UnityEngine.UI.Text' )
  var i1081 = data
  i1080.m_FontData = request.d('UnityEngine.UI.FontData', i1081[0], i1080.m_FontData)
  i1080.m_Text = i1081[1]
  request.r(i1081[2], i1081[3], 0, i1080, 'm_Material')
  i1080.m_Maskable = !!i1081[4]
  i1080.m_Color = new pc.Color(i1081[5], i1081[6], i1081[7], i1081[8])
  i1080.m_RaycastTarget = !!i1081[9]
  i1080.m_RaycastPadding = new pc.Vec4( i1081[10], i1081[11], i1081[12], i1081[13] )
  return i1080
}

Deserializers["UnityEngine.UI.FontData"] = function (request, data, root) {
  var i1082 = root || request.c( 'UnityEngine.UI.FontData' )
  var i1083 = data
  request.r(i1083[0], i1083[1], 0, i1082, 'm_Font')
  i1082.m_FontSize = i1083[2]
  i1082.m_FontStyle = i1083[3]
  i1082.m_BestFit = !!i1083[4]
  i1082.m_MinSize = i1083[5]
  i1082.m_MaxSize = i1083[6]
  i1082.m_Alignment = i1083[7]
  i1082.m_AlignByGeometry = !!i1083[8]
  i1082.m_RichText = !!i1083[9]
  i1082.m_HorizontalOverflow = i1083[10]
  i1082.m_VerticalOverflow = i1083[11]
  i1082.m_LineSpacing = i1083[12]
  return i1082
}

Deserializers["UnityEngine.UI.Shadow"] = function (request, data, root) {
  var i1084 = root || request.c( 'UnityEngine.UI.Shadow' )
  var i1085 = data
  i1084.m_EffectColor = new pc.Color(i1085[0], i1085[1], i1085[2], i1085[3])
  i1084.m_EffectDistance = new pc.Vec2( i1085[4], i1085[5] )
  i1084.m_UseGraphicAlpha = !!i1085[6]
  return i1084
}

Deserializers["LittleFinds.CollectionBox"] = function (request, data, root) {
  var i1086 = root || request.c( 'LittleFinds.CollectionBox' )
  var i1087 = data
  i1086.category = i1087[0]
  request.r(i1087[1], i1087[2], 0, i1086, 'mouth')
  request.r(i1087[3], i1087[4], 0, i1086, 'insertionLayer')
  request.r(i1087[5], i1087[6], 0, i1086, 'storedPoint')
  request.r(i1087[7], i1087[8], 0, i1086, 'frontLip')
  request.r(i1087[9], i1087[10], 0, i1086, 'progressFill')
  request.r(i1087[11], i1087[12], 0, i1086, 'check')
  request.r(i1087[13], i1087[14], 0, i1086, 'categoryIcon')
  var i1089 = i1087[15]
  var i1088 = []
  for(var i = 0; i < i1089.length; i += 2) {
  request.r(i1089[i + 0], i1089[i + 1], 2, i1088, '')
  }
  i1086.stamps = i1088
  request.r(i1087[16], i1087[17], 0, i1086, 'title')
  return i1086
}

Deserializers["UnityEngine.UI.RectMask2D"] = function (request, data, root) {
  var i1092 = root || request.c( 'UnityEngine.UI.RectMask2D' )
  var i1093 = data
  i1092.m_Padding = new pc.Vec4( i1093[0], i1093[1], i1093[2], i1093[3] )
  i1092.m_Softness = new pc.Vec2( i1093[4], i1093[5] )
  return i1092
}

Deserializers["LittleFinds.CardController"] = function (request, data, root) {
  var i1094 = root || request.c( 'LittleFinds.CardController' )
  var i1095 = data
  request.r(i1095[0], i1095[1], 0, i1094, 'visual')
  request.r(i1095[2], i1095[3], 0, i1094, 'rotator')
  request.r(i1095[4], i1095[5], 0, i1094, 'front')
  request.r(i1095[6], i1095[7], 0, i1094, 'back')
  request.r(i1095[8], i1095[9], 0, i1094, 'illustration')
  request.r(i1095[10], i1095[11], 0, i1094, 'frontPaper')
  request.r(i1095[12], i1095[13], 0, i1094, 'shine')
  request.r(i1095[14], i1095[15], 0, i1094, 'title')
  request.r(i1095[16], i1095[17], 0, i1094, 'subtitle')
  request.r(i1095[18], i1095[19], 0, i1094, 'number')
  request.r(i1095[20], i1095[21], 0, i1094, 'group')
  return i1094
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasGroup"] = function (request, data, root) {
  var i1096 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasGroup' )
  var i1097 = data
  i1096.m_Alpha = i1097[0]
  i1096.m_Interactable = !!i1097[1]
  i1096.m_BlocksRaycasts = !!i1097[2]
  i1096.m_IgnoreParentGroups = !!i1097[3]
  i1096.enabled = !!i1097[4]
  return i1096
}

Deserializers["UnityEngine.EventSystems.EventSystem"] = function (request, data, root) {
  var i1098 = root || request.c( 'UnityEngine.EventSystems.EventSystem' )
  var i1099 = data
  request.r(i1099[0], i1099[1], 0, i1098, 'm_FirstSelected')
  i1098.m_sendNavigationEvents = !!i1099[2]
  i1098.m_DragThreshold = i1099[3]
  return i1098
}

Deserializers["UnityEngine.EventSystems.StandaloneInputModule"] = function (request, data, root) {
  var i1100 = root || request.c( 'UnityEngine.EventSystems.StandaloneInputModule' )
  var i1101 = data
  i1100.m_HorizontalAxis = i1101[0]
  i1100.m_VerticalAxis = i1101[1]
  i1100.m_SubmitButton = i1101[2]
  i1100.m_CancelButton = i1101[3]
  i1100.m_InputActionsPerSecond = i1101[4]
  i1100.m_RepeatDelay = i1101[5]
  i1100.m_ForceModuleActive = !!i1101[6]
  i1100.m_SendPointerHoverToParent = !!i1101[7]
  return i1100
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings"] = function (request, data, root) {
  var i1102 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings' )
  var i1103 = data
  i1102.ambientIntensity = i1103[0]
  i1102.reflectionIntensity = i1103[1]
  i1102.ambientMode = i1103[2]
  i1102.ambientLight = new pc.Color(i1103[3], i1103[4], i1103[5], i1103[6])
  i1102.ambientSkyColor = new pc.Color(i1103[7], i1103[8], i1103[9], i1103[10])
  i1102.ambientGroundColor = new pc.Color(i1103[11], i1103[12], i1103[13], i1103[14])
  i1102.ambientEquatorColor = new pc.Color(i1103[15], i1103[16], i1103[17], i1103[18])
  i1102.fogColor = new pc.Color(i1103[19], i1103[20], i1103[21], i1103[22])
  i1102.fogEndDistance = i1103[23]
  i1102.fogStartDistance = i1103[24]
  i1102.fogDensity = i1103[25]
  i1102.fog = !!i1103[26]
  request.r(i1103[27], i1103[28], 0, i1102, 'skybox')
  i1102.fogMode = i1103[29]
  var i1105 = i1103[30]
  var i1104 = []
  for(var i = 0; i < i1105.length; i += 1) {
    i1104.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap', i1105[i + 0]) );
  }
  i1102.lightmaps = i1104
  i1102.lightProbes = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes', i1103[31], i1102.lightProbes)
  i1102.lightmapsMode = i1103[32]
  i1102.mixedBakeMode = i1103[33]
  i1102.environmentLightingMode = i1103[34]
  i1102.ambientProbe = new pc.SphericalHarmonicsL2(i1103[35])
  i1102.referenceAmbientProbe = new pc.SphericalHarmonicsL2(i1103[36])
  i1102.useReferenceAmbientProbe = !!i1103[37]
  request.r(i1103[38], i1103[39], 0, i1102, 'customReflection')
  request.r(i1103[40], i1103[41], 0, i1102, 'defaultReflection')
  i1102.defaultReflectionMode = i1103[42]
  i1102.defaultReflectionResolution = i1103[43]
  i1102.sunLightObjectId = i1103[44]
  i1102.pixelLightCount = i1103[45]
  i1102.defaultReflectionHDR = !!i1103[46]
  i1102.hasLightDataAsset = !!i1103[47]
  i1102.hasManualGenerate = !!i1103[48]
  return i1102
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap"] = function (request, data, root) {
  var i1108 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap' )
  var i1109 = data
  request.r(i1109[0], i1109[1], 0, i1108, 'lightmapColor')
  request.r(i1109[2], i1109[3], 0, i1108, 'lightmapDirection')
  request.r(i1109[4], i1109[5], 0, i1108, 'shadowMask')
  return i1108
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes"] = function (request, data, root) {
  var i1110 = root || new UnityEngine.LightProbes()
  var i1111 = data
  return i1110
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material"] = function (request, data, root) {
  var i1118 = root || new pc.UnityMaterial()
  var i1119 = data
  i1118.name = i1119[0]
  request.r(i1119[1], i1119[2], 0, i1118, 'shader')
  i1118.renderQueue = i1119[3]
  i1118.enableInstancing = !!i1119[4]
  var i1121 = i1119[5]
  var i1120 = []
  for(var i = 0; i < i1121.length; i += 1) {
    i1120.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter', i1121[i + 0]) );
  }
  i1118.floatParameters = i1120
  var i1123 = i1119[6]
  var i1122 = []
  for(var i = 0; i < i1123.length; i += 1) {
    i1122.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter', i1123[i + 0]) );
  }
  i1118.colorParameters = i1122
  var i1125 = i1119[7]
  var i1124 = []
  for(var i = 0; i < i1125.length; i += 1) {
    i1124.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter', i1125[i + 0]) );
  }
  i1118.vectorParameters = i1124
  var i1127 = i1119[8]
  var i1126 = []
  for(var i = 0; i < i1127.length; i += 1) {
    i1126.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter', i1127[i + 0]) );
  }
  i1118.textureParameters = i1126
  var i1129 = i1119[9]
  var i1128 = []
  for(var i = 0; i < i1129.length; i += 1) {
    i1128.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag', i1129[i + 0]) );
  }
  i1118.materialFlags = i1128
  return i1118
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter"] = function (request, data, root) {
  var i1132 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter' )
  var i1133 = data
  i1132.name = i1133[0]
  i1132.value = i1133[1]
  return i1132
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter"] = function (request, data, root) {
  var i1136 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter' )
  var i1137 = data
  i1136.name = i1137[0]
  i1136.value = new pc.Color(i1137[1], i1137[2], i1137[3], i1137[4])
  return i1136
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter"] = function (request, data, root) {
  var i1140 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter' )
  var i1141 = data
  i1140.name = i1141[0]
  i1140.value = new pc.Vec4( i1141[1], i1141[2], i1141[3], i1141[4] )
  return i1140
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter"] = function (request, data, root) {
  var i1144 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter' )
  var i1145 = data
  i1144.name = i1145[0]
  request.r(i1145[1], i1145[2], 0, i1144, 'value')
  return i1144
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag"] = function (request, data, root) {
  var i1148 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag' )
  var i1149 = data
  i1148.name = i1149[0]
  i1148.enabled = !!i1149[1]
  return i1148
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader"] = function (request, data, root) {
  var i1150 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader' )
  var i1151 = data
  var i1153 = i1151[0]
  var i1152 = new (System.Collections.Generic.List$1(Bridge.ns('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError')))
  for(var i = 0; i < i1153.length; i += 1) {
    i1152.add(request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError', i1153[i + 0]));
  }
  i1150.ShaderCompilationErrors = i1152
  i1150.name = i1151[1]
  i1150.guid = i1151[2]
  var i1155 = i1151[3]
  var i1154 = []
  for(var i = 0; i < i1155.length; i += 1) {
    i1154.push( i1155[i + 0] );
  }
  i1150.shaderDefinedKeywords = i1154
  var i1157 = i1151[4]
  var i1156 = []
  for(var i = 0; i < i1157.length; i += 1) {
    i1156.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass', i1157[i + 0]) );
  }
  i1150.passes = i1156
  var i1159 = i1151[5]
  var i1158 = []
  for(var i = 0; i < i1159.length; i += 1) {
    i1158.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass', i1159[i + 0]) );
  }
  i1150.usePasses = i1158
  var i1161 = i1151[6]
  var i1160 = []
  for(var i = 0; i < i1161.length; i += 1) {
    i1160.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue', i1161[i + 0]) );
  }
  i1150.defaultParameterValues = i1160
  request.r(i1151[7], i1151[8], 0, i1150, 'unityFallbackShader')
  i1150.readDepth = !!i1151[9]
  i1150.hasDepthOnlyPass = !!i1151[10]
  i1150.isCreatedByShaderGraph = !!i1151[11]
  i1150.disableBatching = !!i1151[12]
  i1150.compiled = !!i1151[13]
  return i1150
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError"] = function (request, data, root) {
  var i1164 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError' )
  var i1165 = data
  i1164.shaderName = i1165[0]
  i1164.errorMessage = i1165[1]
  return i1164
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass"] = function (request, data, root) {
  var i1170 = root || new pc.UnityShaderPass()
  var i1171 = data
  i1170.id = i1171[0]
  i1170.subShaderIndex = i1171[1]
  i1170.name = i1171[2]
  i1170.passType = i1171[3]
  i1170.grabPassTextureName = i1171[4]
  i1170.usePass = !!i1171[5]
  i1170.zTest = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1171[6], i1170.zTest)
  i1170.zWrite = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1171[7], i1170.zWrite)
  i1170.culling = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1171[8], i1170.culling)
  i1170.blending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i1171[9], i1170.blending)
  i1170.alphaBlending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i1171[10], i1170.alphaBlending)
  i1170.colorWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1171[11], i1170.colorWriteMask)
  i1170.offsetUnits = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1171[12], i1170.offsetUnits)
  i1170.offsetFactor = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1171[13], i1170.offsetFactor)
  i1170.stencilRef = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1171[14], i1170.stencilRef)
  i1170.stencilReadMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1171[15], i1170.stencilReadMask)
  i1170.stencilWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1171[16], i1170.stencilWriteMask)
  i1170.stencilOp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i1171[17], i1170.stencilOp)
  i1170.stencilOpFront = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i1171[18], i1170.stencilOpFront)
  i1170.stencilOpBack = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i1171[19], i1170.stencilOpBack)
  var i1173 = i1171[20]
  var i1172 = []
  for(var i = 0; i < i1173.length; i += 1) {
    i1172.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag', i1173[i + 0]) );
  }
  i1170.tags = i1172
  var i1175 = i1171[21]
  var i1174 = []
  for(var i = 0; i < i1175.length; i += 1) {
    i1174.push( i1175[i + 0] );
  }
  i1170.passDefinedKeywords = i1174
  var i1177 = i1171[22]
  var i1176 = []
  for(var i = 0; i < i1177.length; i += 1) {
    i1176.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup', i1177[i + 0]) );
  }
  i1170.passDefinedKeywordGroups = i1176
  var i1179 = i1171[23]
  var i1178 = []
  for(var i = 0; i < i1179.length; i += 1) {
    i1178.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i1179[i + 0]) );
  }
  i1170.variants = i1178
  var i1181 = i1171[24]
  var i1180 = []
  for(var i = 0; i < i1181.length; i += 1) {
    i1180.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i1181[i + 0]) );
  }
  i1170.excludedVariants = i1180
  i1170.hasDepthReader = !!i1171[25]
  return i1170
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value"] = function (request, data, root) {
  var i1182 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value' )
  var i1183 = data
  i1182.val = i1183[0]
  i1182.name = i1183[1]
  return i1182
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending"] = function (request, data, root) {
  var i1184 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending' )
  var i1185 = data
  i1184.src = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1185[0], i1184.src)
  i1184.dst = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1185[1], i1184.dst)
  i1184.op = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1185[2], i1184.op)
  return i1184
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp"] = function (request, data, root) {
  var i1186 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp' )
  var i1187 = data
  i1186.pass = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1187[0], i1186.pass)
  i1186.fail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1187[1], i1186.fail)
  i1186.zFail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1187[2], i1186.zFail)
  i1186.comp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1187[3], i1186.comp)
  return i1186
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag"] = function (request, data, root) {
  var i1190 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag' )
  var i1191 = data
  i1190.name = i1191[0]
  i1190.value = i1191[1]
  return i1190
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup"] = function (request, data, root) {
  var i1194 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup' )
  var i1195 = data
  var i1197 = i1195[0]
  var i1196 = []
  for(var i = 0; i < i1197.length; i += 1) {
    i1196.push( i1197[i + 0] );
  }
  i1194.keywords = i1196
  i1194.hasDiscard = !!i1195[1]
  return i1194
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant"] = function (request, data, root) {
  var i1200 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant' )
  var i1201 = data
  i1200.passId = i1201[0]
  i1200.subShaderIndex = i1201[1]
  var i1203 = i1201[2]
  var i1202 = []
  for(var i = 0; i < i1203.length; i += 1) {
    i1202.push( i1203[i + 0] );
  }
  i1200.keywords = i1202
  i1200.vertexProgram = i1201[3]
  i1200.fragmentProgram = i1201[4]
  i1200.exportedForWebGl2 = !!i1201[5]
  i1200.readDepth = !!i1201[6]
  return i1200
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass"] = function (request, data, root) {
  var i1206 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass' )
  var i1207 = data
  request.r(i1207[0], i1207[1], 0, i1206, 'shader')
  i1206.pass = i1207[2]
  return i1206
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue"] = function (request, data, root) {
  var i1210 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue' )
  var i1211 = data
  i1210.name = i1211[0]
  i1210.type = i1211[1]
  i1210.value = new pc.Vec4( i1211[2], i1211[3], i1211[4], i1211[5] )
  i1210.textureValue = i1211[6]
  i1210.shaderPropertyFlag = i1211[7]
  return i1210
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Sprite"] = function (request, data, root) {
  var i1212 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Sprite' )
  var i1213 = data
  i1212.name = i1213[0]
  request.r(i1213[1], i1213[2], 0, i1212, 'texture')
  i1212.aabb = i1213[3]
  i1212.vertices = i1213[4]
  i1212.triangles = i1213[5]
  i1212.textureRect = UnityEngine.Rect.MinMaxRect(i1213[6], i1213[7], i1213[8], i1213[9])
  i1212.packedRect = UnityEngine.Rect.MinMaxRect(i1213[10], i1213[11], i1213[12], i1213[13])
  i1212.border = new pc.Vec4( i1213[14], i1213[15], i1213[16], i1213[17] )
  i1212.transparency = i1213[18]
  i1212.bounds = i1213[19]
  i1212.pixelsPerUnit = i1213[20]
  i1212.textureWidth = i1213[21]
  i1212.textureHeight = i1213[22]
  i1212.nativeSize = new pc.Vec2( i1213[23], i1213[24] )
  i1212.pivot = new pc.Vec2( i1213[25], i1213[26] )
  i1212.textureRectOffset = new pc.Vec2( i1213[27], i1213[28] )
  return i1212
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.AudioClip"] = function (request, data, root) {
  var i1214 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.AudioClip' )
  var i1215 = data
  i1214.name = i1215[0]
  return i1214
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font"] = function (request, data, root) {
  var i1216 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font' )
  var i1217 = data
  i1216.name = i1217[0]
  i1216.ascent = i1217[1]
  i1216.originalLineHeight = i1217[2]
  i1216.fontSize = i1217[3]
  var i1219 = i1217[4]
  var i1218 = []
  for(var i = 0; i < i1219.length; i += 1) {
    i1218.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo', i1219[i + 0]) );
  }
  i1216.characterInfo = i1218
  request.r(i1217[5], i1217[6], 0, i1216, 'texture')
  i1216.originalFontSize = i1217[7]
  return i1216
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo"] = function (request, data, root) {
  var i1222 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo' )
  var i1223 = data
  i1222.index = i1223[0]
  i1222.advance = i1223[1]
  i1222.bearing = i1223[2]
  i1222.glyphWidth = i1223[3]
  i1222.glyphHeight = i1223[4]
  i1222.minX = i1223[5]
  i1222.maxX = i1223[6]
  i1222.minY = i1223[7]
  i1222.maxY = i1223[8]
  i1222.uvBottomLeftX = i1223[9]
  i1222.uvBottomLeftY = i1223[10]
  i1222.uvBottomRightX = i1223[11]
  i1222.uvBottomRightY = i1223[12]
  i1222.uvTopLeftX = i1223[13]
  i1222.uvTopLeftY = i1223[14]
  i1222.uvTopRightX = i1223[15]
  i1222.uvTopRightY = i1223[16]
  return i1222
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources"] = function (request, data, root) {
  var i1224 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources' )
  var i1225 = data
  var i1227 = i1225[0]
  var i1226 = []
  for(var i = 0; i < i1227.length; i += 1) {
    i1226.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Resources+File', i1227[i + 0]) );
  }
  i1224.files = i1226
  i1224.componentToPrefabIds = i1225[1]
  return i1224
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources+File"] = function (request, data, root) {
  var i1230 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources+File' )
  var i1231 = data
  i1230.path = i1231[0]
  request.r(i1231[1], i1231[2], 0, i1230, 'unityObject')
  return i1230
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings"] = function (request, data, root) {
  var i1232 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings' )
  var i1233 = data
  var i1235 = i1233[0]
  var i1234 = []
  for(var i = 0; i < i1235.length; i += 1) {
    i1234.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder', i1235[i + 0]) );
  }
  i1232.scriptsExecutionOrder = i1234
  var i1237 = i1233[1]
  var i1236 = []
  for(var i = 0; i < i1237.length; i += 1) {
    i1236.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer', i1237[i + 0]) );
  }
  i1232.sortingLayers = i1236
  var i1239 = i1233[2]
  var i1238 = []
  for(var i = 0; i < i1239.length; i += 1) {
    i1238.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer', i1239[i + 0]) );
  }
  i1232.cullingLayers = i1238
  i1232.timeSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings', i1233[3], i1232.timeSettings)
  i1232.physicsSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings', i1233[4], i1232.physicsSettings)
  i1232.physics2DSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings', i1233[5], i1232.physics2DSettings)
  i1232.qualitySettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i1233[6], i1232.qualitySettings)
  i1232.enableRealtimeShadows = !!i1233[7]
  i1232.enableAutoInstancing = !!i1233[8]
  i1232.enableStaticBatching = !!i1233[9]
  i1232.enableDynamicBatching = !!i1233[10]
  i1232.lightmapEncodingQuality = i1233[11]
  i1232.desiredColorSpace = i1233[12]
  var i1241 = i1233[13]
  var i1240 = []
  for(var i = 0; i < i1241.length; i += 1) {
    i1240.push( i1241[i + 0] );
  }
  i1232.allTags = i1240
  return i1232
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder"] = function (request, data, root) {
  var i1244 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder' )
  var i1245 = data
  i1244.name = i1245[0]
  i1244.value = i1245[1]
  return i1244
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer"] = function (request, data, root) {
  var i1248 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer' )
  var i1249 = data
  i1248.id = i1249[0]
  i1248.name = i1249[1]
  i1248.value = i1249[2]
  return i1248
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer"] = function (request, data, root) {
  var i1252 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer' )
  var i1253 = data
  i1252.id = i1253[0]
  i1252.name = i1253[1]
  return i1252
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings"] = function (request, data, root) {
  var i1254 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings' )
  var i1255 = data
  i1254.fixedDeltaTime = i1255[0]
  i1254.maximumDeltaTime = i1255[1]
  i1254.timeScale = i1255[2]
  i1254.maximumParticleTimestep = i1255[3]
  return i1254
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings"] = function (request, data, root) {
  var i1256 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings' )
  var i1257 = data
  i1256.gravity = new pc.Vec3( i1257[0], i1257[1], i1257[2] )
  i1256.defaultSolverIterations = i1257[3]
  i1256.bounceThreshold = i1257[4]
  i1256.autoSyncTransforms = !!i1257[5]
  i1256.autoSimulation = !!i1257[6]
  var i1259 = i1257[7]
  var i1258 = []
  for(var i = 0; i < i1259.length; i += 1) {
    i1258.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask', i1259[i + 0]) );
  }
  i1256.collisionMatrix = i1258
  return i1256
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask"] = function (request, data, root) {
  var i1262 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask' )
  var i1263 = data
  i1262.enabled = !!i1263[0]
  i1262.layerId = i1263[1]
  i1262.otherLayerId = i1263[2]
  return i1262
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings"] = function (request, data, root) {
  var i1264 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings' )
  var i1265 = data
  request.r(i1265[0], i1265[1], 0, i1264, 'material')
  i1264.gravity = new pc.Vec2( i1265[2], i1265[3] )
  i1264.positionIterations = i1265[4]
  i1264.velocityIterations = i1265[5]
  i1264.velocityThreshold = i1265[6]
  i1264.maxLinearCorrection = i1265[7]
  i1264.maxAngularCorrection = i1265[8]
  i1264.maxTranslationSpeed = i1265[9]
  i1264.maxRotationSpeed = i1265[10]
  i1264.baumgarteScale = i1265[11]
  i1264.baumgarteTOIScale = i1265[12]
  i1264.timeToSleep = i1265[13]
  i1264.linearSleepTolerance = i1265[14]
  i1264.angularSleepTolerance = i1265[15]
  i1264.defaultContactOffset = i1265[16]
  i1264.autoSimulation = !!i1265[17]
  i1264.queriesHitTriggers = !!i1265[18]
  i1264.queriesStartInColliders = !!i1265[19]
  i1264.callbacksOnDisable = !!i1265[20]
  i1264.reuseCollisionCallbacks = !!i1265[21]
  i1264.autoSyncTransforms = !!i1265[22]
  var i1267 = i1265[23]
  var i1266 = []
  for(var i = 0; i < i1267.length; i += 1) {
    i1266.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask', i1267[i + 0]) );
  }
  i1264.collisionMatrix = i1266
  return i1264
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask"] = function (request, data, root) {
  var i1270 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask' )
  var i1271 = data
  i1270.enabled = !!i1271[0]
  i1270.layerId = i1271[1]
  i1270.otherLayerId = i1271[2]
  return i1270
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.QualitySettings"] = function (request, data, root) {
  var i1272 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.QualitySettings' )
  var i1273 = data
  var i1275 = i1273[0]
  var i1274 = []
  for(var i = 0; i < i1275.length; i += 1) {
    i1274.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i1275[i + 0]) );
  }
  i1272.qualityLevels = i1274
  var i1277 = i1273[1]
  var i1276 = []
  for(var i = 0; i < i1277.length; i += 1) {
    i1276.push( i1277[i + 0] );
  }
  i1272.names = i1276
  i1272.shadows = i1273[2]
  i1272.anisotropicFiltering = i1273[3]
  i1272.antiAliasing = i1273[4]
  i1272.lodBias = i1273[5]
  i1272.shadowCascades = i1273[6]
  i1272.shadowDistance = i1273[7]
  i1272.shadowmaskMode = i1273[8]
  i1272.shadowProjection = i1273[9]
  i1272.shadowResolution = i1273[10]
  i1272.softParticles = !!i1273[11]
  i1272.softVegetation = !!i1273[12]
  i1272.activeColorSpace = i1273[13]
  i1272.desiredColorSpace = i1273[14]
  i1272.masterTextureLimit = i1273[15]
  i1272.maxQueuedFrames = i1273[16]
  i1272.particleRaycastBudget = i1273[17]
  i1272.pixelLightCount = i1273[18]
  i1272.realtimeReflectionProbes = !!i1273[19]
  i1272.shadowCascade2Split = i1273[20]
  i1272.shadowCascade4Split = new pc.Vec3( i1273[21], i1273[22], i1273[23] )
  i1272.streamingMipmapsActive = !!i1273[24]
  i1272.vSyncCount = i1273[25]
  i1272.asyncUploadBufferSize = i1273[26]
  i1272.asyncUploadTimeSlice = i1273[27]
  i1272.billboardsFaceCameraPosition = !!i1273[28]
  i1272.shadowNearPlaneOffset = i1273[29]
  i1272.streamingMipmapsMemoryBudget = i1273[30]
  i1272.maximumLODLevel = i1273[31]
  i1272.streamingMipmapsAddAllCameras = !!i1273[32]
  i1272.streamingMipmapsMaxLevelReduction = i1273[33]
  i1272.streamingMipmapsRenderersPerFrame = i1273[34]
  i1272.resolutionScalingFixedDPIFactor = i1273[35]
  i1272.streamingMipmapsMaxFileIORequests = i1273[36]
  i1272.currentQualityLevel = i1273[37]
  return i1272
}

Deserializers.fields = {"Luna.Unity.DTO.UnityEngine.Textures.Texture2D":{"name":0,"width":1,"height":2,"mipmapCount":3,"anisoLevel":4,"filterMode":5,"hdr":6,"format":7,"wrapMode":8,"alphaIsTransparency":9,"alphaSource":10,"graphicsFormat":11,"sRGBTexture":12,"desiredColorSpace":13,"wrapU":14,"wrapV":15},"Luna.Unity.DTO.UnityEngine.Scene.Scene":{"name":0,"index":1,"startup":2},"Luna.Unity.DTO.UnityEngine.Components.Camera":{"aspect":0,"orthographic":1,"orthographicSize":2,"backgroundColor":3,"nearClipPlane":7,"farClipPlane":8,"fieldOfView":9,"depth":10,"clearFlags":11,"cullingMask":12,"rect":13,"targetTexture":14,"usePhysicalProperties":16,"focalLength":17,"sensorSize":18,"lensShift":20,"gateFit":22,"commandBufferCount":23,"cameraType":24,"enabled":25},"Luna.Unity.DTO.UnityEngine.Scene.GameObject":{"name":0,"tagId":1,"enabled":2,"isStatic":3,"layer":4},"Luna.Unity.DTO.UnityEngine.Components.RectTransform":{"pivot":0,"anchorMin":2,"anchorMax":4,"sizeDelta":6,"anchoredPosition3D":8,"rotation":11,"scale":15},"Luna.Unity.DTO.UnityEngine.Components.Canvas":{"planeDistance":0,"referencePixelsPerUnit":1,"isFallbackOverlay":2,"renderMode":3,"renderOrder":4,"sortingLayerName":5,"sortingOrder":6,"scaleFactor":7,"worldCamera":8,"overrideSorting":10,"pixelPerfect":11,"targetDisplay":12,"overridePixelPerfect":13,"enabled":14},"Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer":{"cullTransparentMesh":0},"Luna.Unity.DTO.UnityEngine.Components.CanvasGroup":{"m_Alpha":0,"m_Interactable":1,"m_BlocksRaycasts":2,"m_IgnoreParentGroups":3,"enabled":4},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings":{"ambientIntensity":0,"reflectionIntensity":1,"ambientMode":2,"ambientLight":3,"ambientSkyColor":7,"ambientGroundColor":11,"ambientEquatorColor":15,"fogColor":19,"fogEndDistance":23,"fogStartDistance":24,"fogDensity":25,"fog":26,"skybox":27,"fogMode":29,"lightmaps":30,"lightProbes":31,"lightmapsMode":32,"mixedBakeMode":33,"environmentLightingMode":34,"ambientProbe":35,"referenceAmbientProbe":36,"useReferenceAmbientProbe":37,"customReflection":38,"defaultReflection":40,"defaultReflectionMode":42,"defaultReflectionResolution":43,"sunLightObjectId":44,"pixelLightCount":45,"defaultReflectionHDR":46,"hasLightDataAsset":47,"hasManualGenerate":48},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap":{"lightmapColor":0,"lightmapDirection":2,"shadowMask":4},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes":{"bakedProbes":0,"positions":1,"hullRays":2,"tetrahedra":3,"neighbours":4,"matrices":5},"Luna.Unity.DTO.UnityEngine.Assets.Material":{"name":0,"shader":1,"renderQueue":3,"enableInstancing":4,"floatParameters":5,"colorParameters":6,"vectorParameters":7,"textureParameters":8,"materialFlags":9},"Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag":{"name":0,"enabled":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader":{"ShaderCompilationErrors":0,"name":1,"guid":2,"shaderDefinedKeywords":3,"passes":4,"usePasses":5,"defaultParameterValues":6,"unityFallbackShader":7,"readDepth":9,"hasDepthOnlyPass":10,"isCreatedByShaderGraph":11,"disableBatching":12,"compiled":13},"Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError":{"shaderName":0,"errorMessage":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass":{"id":0,"subShaderIndex":1,"name":2,"passType":3,"grabPassTextureName":4,"usePass":5,"zTest":6,"zWrite":7,"culling":8,"blending":9,"alphaBlending":10,"colorWriteMask":11,"offsetUnits":12,"offsetFactor":13,"stencilRef":14,"stencilReadMask":15,"stencilWriteMask":16,"stencilOp":17,"stencilOpFront":18,"stencilOpBack":19,"tags":20,"passDefinedKeywords":21,"passDefinedKeywordGroups":22,"variants":23,"excludedVariants":24,"hasDepthReader":25},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value":{"val":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending":{"src":0,"dst":1,"op":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp":{"pass":0,"fail":1,"zFail":2,"comp":3},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup":{"keywords":0,"hasDiscard":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant":{"passId":0,"subShaderIndex":1,"keywords":2,"vertexProgram":3,"fragmentProgram":4,"exportedForWebGl2":5,"readDepth":6},"Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass":{"shader":0,"pass":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue":{"name":0,"type":1,"value":2,"textureValue":6,"shaderPropertyFlag":7},"Luna.Unity.DTO.UnityEngine.Textures.Sprite":{"name":0,"texture":1,"aabb":3,"vertices":4,"triangles":5,"textureRect":6,"packedRect":10,"border":14,"transparency":18,"bounds":19,"pixelsPerUnit":20,"textureWidth":21,"textureHeight":22,"nativeSize":23,"pivot":25,"textureRectOffset":27},"Luna.Unity.DTO.UnityEngine.Assets.AudioClip":{"name":0},"Luna.Unity.DTO.UnityEngine.Assets.Font":{"name":0,"ascent":1,"originalLineHeight":2,"fontSize":3,"characterInfo":4,"texture":5,"originalFontSize":7},"Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo":{"index":0,"advance":1,"bearing":2,"glyphWidth":3,"glyphHeight":4,"minX":5,"maxX":6,"minY":7,"maxY":8,"uvBottomLeftX":9,"uvBottomLeftY":10,"uvBottomRightX":11,"uvBottomRightY":12,"uvTopLeftX":13,"uvTopLeftY":14,"uvTopRightX":15,"uvTopRightY":16},"Luna.Unity.DTO.UnityEngine.Assets.Resources":{"files":0,"componentToPrefabIds":1},"Luna.Unity.DTO.UnityEngine.Assets.Resources+File":{"path":0,"unityObject":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings":{"scriptsExecutionOrder":0,"sortingLayers":1,"cullingLayers":2,"timeSettings":3,"physicsSettings":4,"physics2DSettings":5,"qualitySettings":6,"enableRealtimeShadows":7,"enableAutoInstancing":8,"enableStaticBatching":9,"enableDynamicBatching":10,"lightmapEncodingQuality":11,"desiredColorSpace":12,"allTags":13},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer":{"id":0,"name":1,"value":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer":{"id":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings":{"fixedDeltaTime":0,"maximumDeltaTime":1,"timeScale":2,"maximumParticleTimestep":3},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings":{"gravity":0,"defaultSolverIterations":3,"bounceThreshold":4,"autoSyncTransforms":5,"autoSimulation":6,"collisionMatrix":7},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings":{"material":0,"gravity":2,"positionIterations":4,"velocityIterations":5,"velocityThreshold":6,"maxLinearCorrection":7,"maxAngularCorrection":8,"maxTranslationSpeed":9,"maxRotationSpeed":10,"baumgarteScale":11,"baumgarteTOIScale":12,"timeToSleep":13,"linearSleepTolerance":14,"angularSleepTolerance":15,"defaultContactOffset":16,"autoSimulation":17,"queriesHitTriggers":18,"queriesStartInColliders":19,"callbacksOnDisable":20,"reuseCollisionCallbacks":21,"autoSyncTransforms":22,"collisionMatrix":23},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.QualitySettings":{"qualityLevels":0,"names":1,"shadows":2,"anisotropicFiltering":3,"antiAliasing":4,"lodBias":5,"shadowCascades":6,"shadowDistance":7,"shadowmaskMode":8,"shadowProjection":9,"shadowResolution":10,"softParticles":11,"softVegetation":12,"activeColorSpace":13,"desiredColorSpace":14,"masterTextureLimit":15,"maxQueuedFrames":16,"particleRaycastBudget":17,"pixelLightCount":18,"realtimeReflectionProbes":19,"shadowCascade2Split":20,"shadowCascade4Split":21,"streamingMipmapsActive":24,"vSyncCount":25,"asyncUploadBufferSize":26,"asyncUploadTimeSlice":27,"billboardsFaceCameraPosition":28,"shadowNearPlaneOffset":29,"streamingMipmapsMemoryBudget":30,"maximumLODLevel":31,"streamingMipmapsAddAllCameras":32,"streamingMipmapsMaxLevelReduction":33,"streamingMipmapsRenderersPerFrame":34,"resolutionScalingFixedDPIFactor":35,"streamingMipmapsMaxFileIORequests":36,"currentQualityLevel":37}}

Deserializers.requiredComponents = {"38":[39],"40":[39],"41":[39],"42":[39],"43":[39],"44":[39],"45":[46],"47":[0],"48":[49],"50":[49],"51":[49],"52":[49],"53":[49],"54":[49],"55":[49],"56":[57],"58":[57],"59":[57],"60":[57],"61":[57],"62":[57],"63":[57],"64":[57],"65":[57],"66":[57],"67":[57],"68":[57],"69":[57],"70":[0],"71":[72],"73":[74],"75":[74],"22":[14],"76":[14],"77":[14],"78":[14],"79":[14],"80":[14],"81":[14],"82":[14],"83":[14],"25":[22],"27":[26,14],"84":[14],"24":[22],"85":[14],"86":[14],"87":[14],"88":[14],"89":[14],"90":[14],"91":[14],"92":[14],"93":[14],"94":[26,14],"32":[14],"95":[14],"96":[14],"97":[14],"17":[26,14],"98":[14],"99":[34],"100":[34],"35":[34],"101":[34],"102":[0],"103":[0]}

Deserializers.types = ["UnityEngine.Camera","UnityEngine.AudioListener","UnityEngine.MonoBehaviour","LittleFinds.GameManager","LittleFinds.RoundRisk","LittleFinds.TutorialController","LittleFinds.CardManager","LittleFinds.CollectionManager","LittleFinds.ShuffleController","LittleFinds.GameTimer","LittleFinds.GameUIController","LittleFinds.FeedbackController","LittleFinds.CardController","UnityEngine.Sprite","UnityEngine.RectTransform","LittleFinds.CollectionBox","UnityEngine.UI.Button","UnityEngine.UI.Text","UnityEngine.GameObject","LittleFinds.AudioDirector","UnityEngine.Font","UnityEngine.AudioClip","UnityEngine.Canvas","UnityEngine.EventSystems.UIBehaviour","UnityEngine.UI.CanvasScaler","UnityEngine.UI.GraphicRaycaster","UnityEngine.CanvasRenderer","UnityEngine.UI.Image","LittleFinds.SafeArea","LittleFinds.MobileGameLayout","LittleFinds.ButtonFeel","UnityEngine.UI.Shadow","UnityEngine.UI.RectMask2D","UnityEngine.CanvasGroup","UnityEngine.EventSystems.EventSystem","UnityEngine.EventSystems.StandaloneInputModule","UnityEngine.Shader","UnityEngine.Texture2D","UnityEngine.AudioLowPassFilter","UnityEngine.AudioBehaviour","UnityEngine.AudioHighPassFilter","UnityEngine.AudioReverbFilter","UnityEngine.AudioDistortionFilter","UnityEngine.AudioEchoFilter","UnityEngine.AudioChorusFilter","UnityEngine.Cloth","UnityEngine.SkinnedMeshRenderer","UnityEngine.FlareLayer","UnityEngine.ConstantForce","UnityEngine.Rigidbody","UnityEngine.Joint","UnityEngine.HingeJoint","UnityEngine.SpringJoint","UnityEngine.FixedJoint","UnityEngine.CharacterJoint","UnityEngine.ConfigurableJoint","UnityEngine.CompositeCollider2D","UnityEngine.Rigidbody2D","UnityEngine.Joint2D","UnityEngine.AnchoredJoint2D","UnityEngine.SpringJoint2D","UnityEngine.DistanceJoint2D","UnityEngine.FrictionJoint2D","UnityEngine.HingeJoint2D","UnityEngine.RelativeJoint2D","UnityEngine.SliderJoint2D","UnityEngine.TargetJoint2D","UnityEngine.FixedJoint2D","UnityEngine.WheelJoint2D","UnityEngine.ConstantForce2D","UnityEngine.StreamingController","UnityEngine.TextMesh","UnityEngine.MeshRenderer","UnityEngine.Tilemaps.TilemapRenderer","UnityEngine.Tilemaps.Tilemap","UnityEngine.Tilemaps.TilemapCollider2D","LittleFinds.CardShardGraphic","LittleFinds.ComicVictory","LittleFinds.DamageFlashGraphic","LittleFinds.ResultSticker","LittleFinds.TrashCanGraphic","LittleFinds.WarningStripes","UnityEngine.UI.Dropdown","UnityEngine.UI.Graphic","UnityEngine.UI.AspectRatioFitter","UnityEngine.UI.ContentSizeFitter","UnityEngine.UI.GridLayoutGroup","UnityEngine.UI.HorizontalLayoutGroup","UnityEngine.UI.HorizontalOrVerticalLayoutGroup","UnityEngine.UI.LayoutElement","UnityEngine.UI.LayoutGroup","UnityEngine.UI.VerticalLayoutGroup","UnityEngine.UI.Mask","UnityEngine.UI.MaskableGraphic","UnityEngine.UI.RawImage","UnityEngine.UI.Scrollbar","UnityEngine.UI.ScrollRect","UnityEngine.UI.Slider","UnityEngine.UI.Toggle","UnityEngine.EventSystems.BaseInputModule","UnityEngine.EventSystems.PointerInputModule","UnityEngine.EventSystems.TouchInputModule","UnityEngine.EventSystems.Physics2DRaycaster","UnityEngine.EventSystems.PhysicsRaycaster"]

Deserializers.unityVersion = "2022.3.62f2";

Deserializers.productName = "SortCard";

Deserializers.lunaInitializationTime = "11/25/2026 02:10:35";

Deserializers.lunaDaysRunning = "0.3";

Deserializers.lunaVersion = "7.1.0";

Deserializers.lunaSHA = "cf93782349542fe0b84ad13951a26809f8419628";

Deserializers.creativeName = "Card2";

Deserializers.lunaAppID = "35754";

Deserializers.projectId = "5c95f280842c24d4da20222024853a01";

Deserializers.packagesInfo = "com.unity.nuget.newtonsoft-json: 3.2.2\ncom.unity.ugui: 1.0.0";

Deserializers.externalJsLibraries = "";

Deserializers.androidLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.androidLink?window.$environment.packageConfig.androidLink:'Empty';

Deserializers.iosLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.iosLink?window.$environment.packageConfig.iosLink:'Empty';

Deserializers.base64Enabled = "True";

Deserializers.minifyEnabled = "True";

Deserializers.isForceUncompressed = "False";

Deserializers.isAntiAliasingEnabled = "False";

Deserializers.isRuntimeAnalysisEnabledForCode = "False";

Deserializers.runtimeAnalysisExcludedClassesCount = "1660";

Deserializers.runtimeAnalysisExcludedMethodsCount = "2817";

Deserializers.runtimeAnalysisExcludedModules = "physics3d, physics2d, particle-system, reflection, mecanim-wasm";

Deserializers.isRuntimeAnalysisEnabledForShaders = "True";

Deserializers.isRealtimeShadowsEnabled = "True";

Deserializers.isReferenceAmbientProbeBaked = "True";

Deserializers.isLunaCompilerV2Used = "True";

Deserializers.companyName = "DefaultCompany";

Deserializers.buildPlatform = "StandaloneWindows64";

Deserializers.applicationIdentifier = "com.DefaultCompany.SortCard";

Deserializers.disableAntiAliasing = true;

Deserializers.graphicsConstraint = 28;

Deserializers.linearColorSpace = true;

Deserializers.buildID = "991fd5af-b90b-43ba-8aa5-9227ecc75faa";

Deserializers.runtimeInitializeOnLoadInfos = [[["UnityEngine","Experimental","Rendering","ScriptableRuntimeReflectionSystemSettings","ScriptingDirtyReflectionSystemInstance"]],[],[],[],[]];

Deserializers.typeNameToIdMap = function(){ var i = 0; return Deserializers.types.reduce( function( res, item ) { res[ item ] = i++; return res; }, {} ) }()

