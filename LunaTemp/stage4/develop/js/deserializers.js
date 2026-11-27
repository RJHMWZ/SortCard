var Deserializers = {}
Deserializers["UnityEngine.JointSpring"] = function (request, data, root) {
  var i320 = root || request.c( 'UnityEngine.JointSpring' )
  var i321 = data
  i320.spring = i321[0]
  i320.damper = i321[1]
  i320.targetPosition = i321[2]
  return i320
}

Deserializers["UnityEngine.JointMotor"] = function (request, data, root) {
  var i322 = root || request.c( 'UnityEngine.JointMotor' )
  var i323 = data
  i322.m_TargetVelocity = i323[0]
  i322.m_Force = i323[1]
  i322.m_FreeSpin = i323[2]
  return i322
}

Deserializers["UnityEngine.JointLimits"] = function (request, data, root) {
  var i324 = root || request.c( 'UnityEngine.JointLimits' )
  var i325 = data
  i324.m_Min = i325[0]
  i324.m_Max = i325[1]
  i324.m_Bounciness = i325[2]
  i324.m_BounceMinVelocity = i325[3]
  i324.m_ContactDistance = i325[4]
  i324.minBounce = i325[5]
  i324.maxBounce = i325[6]
  return i324
}

Deserializers["UnityEngine.JointDrive"] = function (request, data, root) {
  var i326 = root || request.c( 'UnityEngine.JointDrive' )
  var i327 = data
  i326.m_PositionSpring = i327[0]
  i326.m_PositionDamper = i327[1]
  i326.m_MaximumForce = i327[2]
  i326.m_UseAcceleration = i327[3]
  return i326
}

Deserializers["UnityEngine.SoftJointLimitSpring"] = function (request, data, root) {
  var i328 = root || request.c( 'UnityEngine.SoftJointLimitSpring' )
  var i329 = data
  i328.m_Spring = i329[0]
  i328.m_Damper = i329[1]
  return i328
}

Deserializers["UnityEngine.SoftJointLimit"] = function (request, data, root) {
  var i330 = root || request.c( 'UnityEngine.SoftJointLimit' )
  var i331 = data
  i330.m_Limit = i331[0]
  i330.m_Bounciness = i331[1]
  i330.m_ContactDistance = i331[2]
  return i330
}

Deserializers["UnityEngine.WheelFrictionCurve"] = function (request, data, root) {
  var i332 = root || request.c( 'UnityEngine.WheelFrictionCurve' )
  var i333 = data
  i332.m_ExtremumSlip = i333[0]
  i332.m_ExtremumValue = i333[1]
  i332.m_AsymptoteSlip = i333[2]
  i332.m_AsymptoteValue = i333[3]
  i332.m_Stiffness = i333[4]
  return i332
}

Deserializers["UnityEngine.JointAngleLimits2D"] = function (request, data, root) {
  var i334 = root || request.c( 'UnityEngine.JointAngleLimits2D' )
  var i335 = data
  i334.m_LowerAngle = i335[0]
  i334.m_UpperAngle = i335[1]
  return i334
}

Deserializers["UnityEngine.JointMotor2D"] = function (request, data, root) {
  var i336 = root || request.c( 'UnityEngine.JointMotor2D' )
  var i337 = data
  i336.m_MotorSpeed = i337[0]
  i336.m_MaximumMotorTorque = i337[1]
  return i336
}

Deserializers["UnityEngine.JointSuspension2D"] = function (request, data, root) {
  var i338 = root || request.c( 'UnityEngine.JointSuspension2D' )
  var i339 = data
  i338.m_DampingRatio = i339[0]
  i338.m_Frequency = i339[1]
  i338.m_Angle = i339[2]
  return i338
}

Deserializers["UnityEngine.JointTranslationLimits2D"] = function (request, data, root) {
  var i340 = root || request.c( 'UnityEngine.JointTranslationLimits2D' )
  var i341 = data
  i340.m_LowerTranslation = i341[0]
  i340.m_UpperTranslation = i341[1]
  return i340
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Texture2D"] = function (request, data, root) {
  var i342 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Texture2D' )
  var i343 = data
  i342.name = i343[0]
  i342.width = i343[1]
  i342.height = i343[2]
  i342.mipmapCount = i343[3]
  i342.anisoLevel = i343[4]
  i342.filterMode = i343[5]
  i342.hdr = !!i343[6]
  i342.format = i343[7]
  i342.wrapMode = i343[8]
  i342.alphaIsTransparency = !!i343[9]
  i342.alphaSource = i343[10]
  i342.graphicsFormat = i343[11]
  i342.sRGBTexture = !!i343[12]
  i342.desiredColorSpace = i343[13]
  i342.wrapU = i343[14]
  i342.wrapV = i343[15]
  return i342
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.Scene"] = function (request, data, root) {
  var i344 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.Scene' )
  var i345 = data
  i344.name = i345[0]
  i344.index = i345[1]
  i344.startup = !!i345[2]
  return i344
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Camera"] = function (request, data, root) {
  var i346 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Camera' )
  var i347 = data
  i346.aspect = i347[0]
  i346.orthographic = !!i347[1]
  i346.orthographicSize = i347[2]
  i346.backgroundColor = new pc.Color(i347[3], i347[4], i347[5], i347[6])
  i346.nearClipPlane = i347[7]
  i346.farClipPlane = i347[8]
  i346.fieldOfView = i347[9]
  i346.depth = i347[10]
  i346.clearFlags = i347[11]
  i346.cullingMask = i347[12]
  i346.rect = i347[13]
  request.r(i347[14], i347[15], 0, i346, 'targetTexture')
  i346.usePhysicalProperties = !!i347[16]
  i346.focalLength = i347[17]
  i346.sensorSize = new pc.Vec2( i347[18], i347[19] )
  i346.lensShift = new pc.Vec2( i347[20], i347[21] )
  i346.gateFit = i347[22]
  i346.commandBufferCount = i347[23]
  i346.cameraType = i347[24]
  i346.enabled = !!i347[25]
  return i346
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.GameObject"] = function (request, data, root) {
  var i348 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.GameObject' )
  var i349 = data
  i348.name = i349[0]
  i348.tagId = i349[1]
  i348.enabled = !!i349[2]
  i348.isStatic = !!i349[3]
  i348.layer = i349[4]
  return i348
}

Deserializers["LittleFinds.GameManager"] = function (request, data, root) {
  var i350 = root || request.c( 'LittleFinds.GameManager' )
  var i351 = data
  request.r(i351[0], i351[1], 0, i350, 'Risk')
  request.r(i351[2], i351[3], 0, i350, 'Tutorial')
  request.r(i351[4], i351[5], 0, i350, 'Cards')
  request.r(i351[6], i351[7], 0, i350, 'Collections')
  request.r(i351[8], i351[9], 0, i350, 'Shuffle')
  request.r(i351[10], i351[11], 0, i350, 'Timer')
  request.r(i351[12], i351[13], 0, i350, 'UI')
  request.r(i351[14], i351[15], 0, i350, 'Feedback')
  request.r(i351[16], i351[17], 0, i350, 'Trash')
  return i350
}

Deserializers["LittleFinds.GameTimer"] = function (request, data, root) {
  var i352 = root || request.c( 'LittleFinds.GameTimer' )
  var i353 = data
  i352.Running = !!i353[0]
  i352.Expired = request.d('System.Action', i353[1], i352.Expired)
  return i352
}

Deserializers["System.Action"] = function (request, data, root) {
  var i354 = root || request.c( 'System.Action' )
  var i355 = data
  return i354
}

Deserializers["LittleFinds.CardManager"] = function (request, data, root) {
  var i356 = root || request.c( 'LittleFinds.CardManager' )
  var i357 = data
  var i359 = i357[0]
  var i358 = []
  for(var i = 0; i < i359.length; i += 2) {
  request.r(i359[i + 0], i359[i + 1], 2, i358, '')
  }
  i356.cards = i358
  var i361 = i357[1]
  var i360 = []
  for(var i = 0; i < i361.length; i += 2) {
  request.r(i361[i + 0], i361[i + 1], 2, i360, '')
  }
  i356.art = i360
  request.r(i357[2], i357[3], 0, i356, 'game')
  return i356
}

Deserializers["LittleFinds.CollectionManager"] = function (request, data, root) {
  var i366 = root || request.c( 'LittleFinds.CollectionManager' )
  var i367 = data
  var i369 = i367[0]
  var i368 = []
  for(var i = 0; i < i369.length; i += 2) {
  request.r(i369[i + 0], i369[i + 1], 2, i368, '')
  }
  i366.pools = i368
  var i371 = i367[1]
  var i370 = []
  for(var i = 0; i < i371.length; i += 2) {
  request.r(i371[i + 0], i371[i + 1], 2, i370, '')
  }
  i366.boxes = i370
  return i366
}

Deserializers["LittleFinds.ShuffleController"] = function (request, data, root) {
  var i376 = root || request.c( 'LittleFinds.ShuffleController' )
  var i377 = data
  request.r(i377[0], i377[1], 0, i376, 'game')
  request.r(i377[2], i377[3], 0, i376, 'center')
  return i376
}

Deserializers["LittleFinds.GameUIController"] = function (request, data, root) {
  var i378 = root || request.c( 'LittleFinds.GameUIController' )
  var i379 = data
  request.r(i379[0], i379[1], 0, i378, 'game')
  request.r(i379[2], i379[3], 0, i378, 'helpButton')
  request.r(i379[4], i379[5], 0, i378, 'lives')
  request.r(i379[6], i379[7], 0, i378, 'time')
  request.r(i379[8], i379[9], 0, i378, 'message')
  request.r(i379[10], i379[11], 0, i378, 'modalTitle')
  request.r(i379[12], i379[13], 0, i378, 'modalDescription')
  request.r(i379[14], i379[15], 0, i378, 'modalButtonText')
  request.r(i379[16], i379[17], 0, i378, 'soundLabel')
  request.r(i379[18], i379[19], 0, i378, 'hapticLabel')
  request.r(i379[20], i379[21], 0, i378, 'modal')
  request.r(i379[22], i379[23], 0, i378, 'overlay')
  request.r(i379[24], i379[25], 0, i378, 'startButton')
  request.r(i379[26], i379[27], 0, i378, 'freshButton')
  request.r(i379[28], i379[29], 0, i378, 'settingsRoot')
  request.r(i379[30], i379[31], 0, i378, 'resultRoot')
  var i381 = i379[32]
  var i380 = []
  for(var i = 0; i < i381.length; i += 2) {
  request.r(i381[i + 0], i381[i + 1], 2, i380, '')
  }
  i378.resultBoxes = i380
  return i378
}

Deserializers["LittleFinds.FeedbackController"] = function (request, data, root) {
  var i382 = root || request.c( 'LittleFinds.FeedbackController' )
  var i383 = data
  request.r(i383[0], i383[1], 0, i382, 'layer')
  request.r(i383[2], i383[3], 0, i382, 'round')
  request.r(i383[4], i383[5], 0, i382, 'burst')
  request.r(i383[6], i383[7], 0, i382, 'ring')
  request.r(i383[8], i383[9], 0, i382, 'star')
  request.r(i383[10], i383[11], 0, i382, 'stage')
  request.r(i383[12], i383[13], 0, i382, 'audio')
  request.r(i383[14], i383[15], 0, i382, 'font')
  i382.SoundOn = !!i383[16]
  i382.Haptics = !!i383[17]
  return i382
}

Deserializers["LittleFinds.AudioDirector"] = function (request, data, root) {
  var i384 = root || request.c( 'LittleFinds.AudioDirector' )
  var i385 = data
  var i387 = i385[0]
  var i386 = []
  for(var i = 0; i < i387.length; i += 2) {
  request.r(i387[i + 0], i387[i + 1], 2, i386, '')
  }
  i384.clips = i386
  var i389 = i385[1]
  var i388 = []
  for(var i = 0; i < i389.length; i += 2) {
  request.r(i389[i + 0], i389[i + 1], 2, i388, '')
  }
  i384.flipVariants = i388
  var i391 = i385[2]
  var i390 = []
  for(var i = 0; i < i391.length; i += 2) {
  request.r(i391[i + 0], i391[i + 1], 2, i390, '')
  }
  i384.impactVariants = i390
  i384.masterVolume = i385[3]
  i384.victoryGain = i385[4]
  i384.damageGain = i385[5]
  i384.failureGain = i385[6]
  request.r(i385[7], i385[8], 0, i384, 'damageClip')
  request.r(i385[9], i385[10], 0, i384, 'failureClip')
  return i384
}

Deserializers["LittleFinds.TutorialController"] = function (request, data, root) {
  var i394 = root || request.c( 'LittleFinds.TutorialController' )
  var i395 = data
  i394.isTutorialLevel = !!i395[0]
  request.r(i395[1], i395[2], 0, i394, 'game')
  request.r(i395[3], i395[4], 0, i394, 'coachRoot')
  request.r(i395[5], i395[6], 0, i394, 'coach')
  request.r(i395[7], i395[8], 0, i394, 'focus')
  request.r(i395[9], i395[10], 0, i394, 'hand')
  request.r(i395[11], i395[12], 0, i394, 'heading')
  request.r(i395[13], i395[14], 0, i394, 'nextButton')
  request.r(i395[15], i395[16], 0, i394, 'nextLabel')
  return i394
}

Deserializers["LittleFinds.RoundRisk"] = function (request, data, root) {
  var i396 = root || request.c( 'LittleFinds.RoundRisk' )
  var i397 = data
  i396.maximumLives = i397[0]
  i396.shuffleSeconds = i397[1]
  i396.simultaneousMistakeGrace = i397[2]
  request.r(i397[3], i397[4], 0, i396, 'game')
  return i396
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.RectTransform"] = function (request, data, root) {
  var i398 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.RectTransform' )
  var i399 = data
  i398.pivot = new pc.Vec2( i399[0], i399[1] )
  i398.anchorMin = new pc.Vec2( i399[2], i399[3] )
  i398.anchorMax = new pc.Vec2( i399[4], i399[5] )
  i398.sizeDelta = new pc.Vec2( i399[6], i399[7] )
  i398.anchoredPosition3D = new pc.Vec3( i399[8], i399[9], i399[10] )
  i398.rotation = new pc.Quat(i399[11], i399[12], i399[13], i399[14])
  i398.scale = new pc.Vec3( i399[15], i399[16], i399[17] )
  return i398
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Canvas"] = function (request, data, root) {
  var i400 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Canvas' )
  var i401 = data
  i400.planeDistance = i401[0]
  i400.referencePixelsPerUnit = i401[1]
  i400.isFallbackOverlay = !!i401[2]
  i400.renderMode = i401[3]
  i400.renderOrder = i401[4]
  i400.sortingLayerName = i401[5]
  i400.sortingOrder = i401[6]
  i400.scaleFactor = i401[7]
  request.r(i401[8], i401[9], 0, i400, 'worldCamera')
  i400.overrideSorting = !!i401[10]
  i400.pixelPerfect = !!i401[11]
  i400.targetDisplay = i401[12]
  i400.overridePixelPerfect = !!i401[13]
  i400.enabled = !!i401[14]
  return i400
}

Deserializers["UnityEngine.UI.CanvasScaler"] = function (request, data, root) {
  var i402 = root || request.c( 'UnityEngine.UI.CanvasScaler' )
  var i403 = data
  i402.m_UiScaleMode = i403[0]
  i402.m_ReferencePixelsPerUnit = i403[1]
  i402.m_ScaleFactor = i403[2]
  i402.m_ReferenceResolution = new pc.Vec2( i403[3], i403[4] )
  i402.m_ScreenMatchMode = i403[5]
  i402.m_MatchWidthOrHeight = i403[6]
  i402.m_PhysicalUnit = i403[7]
  i402.m_FallbackScreenDPI = i403[8]
  i402.m_DefaultSpriteDPI = i403[9]
  i402.m_DynamicPixelsPerUnit = i403[10]
  i402.m_PresetInfoIsWorld = !!i403[11]
  return i402
}

Deserializers["UnityEngine.UI.GraphicRaycaster"] = function (request, data, root) {
  var i404 = root || request.c( 'UnityEngine.UI.GraphicRaycaster' )
  var i405 = data
  i404.m_IgnoreReversedGraphics = !!i405[0]
  i404.m_BlockingObjects = i405[1]
  i404.m_BlockingMask = UnityEngine.LayerMask.FromIntegerValue( i405[2] )
  return i404
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer"] = function (request, data, root) {
  var i406 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer' )
  var i407 = data
  i406.cullTransparentMesh = !!i407[0]
  return i406
}

Deserializers["UnityEngine.UI.Image"] = function (request, data, root) {
  var i408 = root || request.c( 'UnityEngine.UI.Image' )
  var i409 = data
  request.r(i409[0], i409[1], 0, i408, 'm_Sprite')
  i408.m_Type = i409[2]
  i408.m_PreserveAspect = !!i409[3]
  i408.m_FillCenter = !!i409[4]
  i408.m_FillMethod = i409[5]
  i408.m_FillAmount = i409[6]
  i408.m_FillClockwise = !!i409[7]
  i408.m_FillOrigin = i409[8]
  i408.m_UseSpriteMesh = !!i409[9]
  i408.m_PixelsPerUnitMultiplier = i409[10]
  request.r(i409[11], i409[12], 0, i408, 'm_Material')
  i408.m_Maskable = !!i409[13]
  i408.m_Color = new pc.Color(i409[14], i409[15], i409[16], i409[17])
  i408.m_RaycastTarget = !!i409[18]
  i408.m_RaycastPadding = new pc.Vec4( i409[19], i409[20], i409[21], i409[22] )
  return i408
}

Deserializers["LittleFinds.SafeArea"] = function (request, data, root) {
  var i410 = root || request.c( 'LittleFinds.SafeArea' )
  var i411 = data
  request.r(i411[0], i411[1], 0, i410, 'content')
  return i410
}

Deserializers["LittleFinds.MobileGameLayout"] = function (request, data, root) {
  var i412 = root || request.c( 'LittleFinds.MobileGameLayout' )
  var i413 = data
  request.r(i413[0], i413[1], 0, i412, 'board')
  request.r(i413[2], i413[3], 0, i412, 'grid')
  request.r(i413[4], i413[5], 0, i412, 'footer')
  request.r(i413[6], i413[7], 0, i412, 'hint')
  request.r(i413[8], i413[9], 0, i412, 'effects')
  request.r(i413[10], i413[11], 0, i412, 'trash')
  request.r(i413[12], i413[13], 0, i412, 'tutorial')
  var i415 = i413[14]
  var i414 = []
  for(var i = 0; i < i415.length; i += 2) {
  request.r(i415[i + 0], i415[i + 1], 2, i414, '')
  }
  i412.slots = i414
  return i412
}

Deserializers["UnityEngine.UI.Button"] = function (request, data, root) {
  var i416 = root || request.c( 'UnityEngine.UI.Button' )
  var i417 = data
  i416.m_OnClick = request.d('UnityEngine.UI.Button+ButtonClickedEvent', i417[0], i416.m_OnClick)
  i416.m_Navigation = request.d('UnityEngine.UI.Navigation', i417[1], i416.m_Navigation)
  i416.m_Transition = i417[2]
  i416.m_Colors = request.d('UnityEngine.UI.ColorBlock', i417[3], i416.m_Colors)
  i416.m_SpriteState = request.d('UnityEngine.UI.SpriteState', i417[4], i416.m_SpriteState)
  i416.m_AnimationTriggers = request.d('UnityEngine.UI.AnimationTriggers', i417[5], i416.m_AnimationTriggers)
  i416.m_Interactable = !!i417[6]
  request.r(i417[7], i417[8], 0, i416, 'm_TargetGraphic')
  return i416
}

Deserializers["UnityEngine.UI.Button+ButtonClickedEvent"] = function (request, data, root) {
  var i418 = root || request.c( 'UnityEngine.UI.Button+ButtonClickedEvent' )
  var i419 = data
  i418.m_PersistentCalls = request.d('UnityEngine.Events.PersistentCallGroup', i419[0], i418.m_PersistentCalls)
  return i418
}

Deserializers["UnityEngine.Events.PersistentCallGroup"] = function (request, data, root) {
  var i420 = root || request.c( 'UnityEngine.Events.PersistentCallGroup' )
  var i421 = data
  var i423 = i421[0]
  var i422 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Events.PersistentCall')))
  for(var i = 0; i < i423.length; i += 1) {
    i422.add(request.d('UnityEngine.Events.PersistentCall', i423[i + 0]));
  }
  i420.m_Calls = i422
  return i420
}

Deserializers["UnityEngine.Events.PersistentCall"] = function (request, data, root) {
  var i426 = root || request.c( 'UnityEngine.Events.PersistentCall' )
  var i427 = data
  request.r(i427[0], i427[1], 0, i426, 'm_Target')
  i426.m_TargetAssemblyTypeName = i427[2]
  i426.m_MethodName = i427[3]
  i426.m_Mode = i427[4]
  i426.m_Arguments = request.d('UnityEngine.Events.ArgumentCache', i427[5], i426.m_Arguments)
  i426.m_CallState = i427[6]
  return i426
}

Deserializers["UnityEngine.Events.ArgumentCache"] = function (request, data, root) {
  var i428 = root || request.c( 'UnityEngine.Events.ArgumentCache' )
  var i429 = data
  request.r(i429[0], i429[1], 0, i428, 'm_ObjectArgument')
  i428.m_ObjectArgumentAssemblyTypeName = i429[2]
  i428.m_IntArgument = i429[3]
  i428.m_FloatArgument = i429[4]
  i428.m_StringArgument = i429[5]
  i428.m_BoolArgument = !!i429[6]
  return i428
}

Deserializers["UnityEngine.UI.Navigation"] = function (request, data, root) {
  var i430 = root || request.c( 'UnityEngine.UI.Navigation' )
  var i431 = data
  i430.m_Mode = i431[0]
  i430.m_WrapAround = !!i431[1]
  request.r(i431[2], i431[3], 0, i430, 'm_SelectOnUp')
  request.r(i431[4], i431[5], 0, i430, 'm_SelectOnDown')
  request.r(i431[6], i431[7], 0, i430, 'm_SelectOnLeft')
  request.r(i431[8], i431[9], 0, i430, 'm_SelectOnRight')
  return i430
}

Deserializers["UnityEngine.UI.ColorBlock"] = function (request, data, root) {
  var i432 = root || request.c( 'UnityEngine.UI.ColorBlock' )
  var i433 = data
  i432.m_NormalColor = new pc.Color(i433[0], i433[1], i433[2], i433[3])
  i432.m_HighlightedColor = new pc.Color(i433[4], i433[5], i433[6], i433[7])
  i432.m_PressedColor = new pc.Color(i433[8], i433[9], i433[10], i433[11])
  i432.m_SelectedColor = new pc.Color(i433[12], i433[13], i433[14], i433[15])
  i432.m_DisabledColor = new pc.Color(i433[16], i433[17], i433[18], i433[19])
  i432.m_ColorMultiplier = i433[20]
  i432.m_FadeDuration = i433[21]
  return i432
}

Deserializers["UnityEngine.UI.SpriteState"] = function (request, data, root) {
  var i434 = root || request.c( 'UnityEngine.UI.SpriteState' )
  var i435 = data
  request.r(i435[0], i435[1], 0, i434, 'm_HighlightedSprite')
  request.r(i435[2], i435[3], 0, i434, 'm_PressedSprite')
  request.r(i435[4], i435[5], 0, i434, 'm_SelectedSprite')
  request.r(i435[6], i435[7], 0, i434, 'm_DisabledSprite')
  return i434
}

Deserializers["UnityEngine.UI.AnimationTriggers"] = function (request, data, root) {
  var i436 = root || request.c( 'UnityEngine.UI.AnimationTriggers' )
  var i437 = data
  i436.m_NormalTrigger = i437[0]
  i436.m_HighlightedTrigger = i437[1]
  i436.m_PressedTrigger = i437[2]
  i436.m_SelectedTrigger = i437[3]
  i436.m_DisabledTrigger = i437[4]
  return i436
}

Deserializers["LittleFinds.ButtonFeel"] = function (request, data, root) {
  var i438 = root || request.c( 'LittleFinds.ButtonFeel' )
  var i439 = data
  return i438
}

Deserializers["UnityEngine.UI.Text"] = function (request, data, root) {
  var i440 = root || request.c( 'UnityEngine.UI.Text' )
  var i441 = data
  i440.m_FontData = request.d('UnityEngine.UI.FontData', i441[0], i440.m_FontData)
  i440.m_Text = i441[1]
  request.r(i441[2], i441[3], 0, i440, 'm_Material')
  i440.m_Maskable = !!i441[4]
  i440.m_Color = new pc.Color(i441[5], i441[6], i441[7], i441[8])
  i440.m_RaycastTarget = !!i441[9]
  i440.m_RaycastPadding = new pc.Vec4( i441[10], i441[11], i441[12], i441[13] )
  return i440
}

Deserializers["UnityEngine.UI.FontData"] = function (request, data, root) {
  var i442 = root || request.c( 'UnityEngine.UI.FontData' )
  var i443 = data
  request.r(i443[0], i443[1], 0, i442, 'm_Font')
  i442.m_FontSize = i443[2]
  i442.m_FontStyle = i443[3]
  i442.m_BestFit = !!i443[4]
  i442.m_MinSize = i443[5]
  i442.m_MaxSize = i443[6]
  i442.m_Alignment = i443[7]
  i442.m_AlignByGeometry = !!i443[8]
  i442.m_RichText = !!i443[9]
  i442.m_HorizontalOverflow = i443[10]
  i442.m_VerticalOverflow = i443[11]
  i442.m_LineSpacing = i443[12]
  return i442
}

Deserializers["UnityEngine.UI.Shadow"] = function (request, data, root) {
  var i444 = root || request.c( 'UnityEngine.UI.Shadow' )
  var i445 = data
  i444.m_EffectColor = new pc.Color(i445[0], i445[1], i445[2], i445[3])
  i444.m_EffectDistance = new pc.Vec2( i445[4], i445[5] )
  i444.m_UseGraphicAlpha = !!i445[6]
  return i444
}

Deserializers["LittleFinds.CollectionBox"] = function (request, data, root) {
  var i446 = root || request.c( 'LittleFinds.CollectionBox' )
  var i447 = data
  i446.category = i447[0]
  request.r(i447[1], i447[2], 0, i446, 'mouth')
  request.r(i447[3], i447[4], 0, i446, 'insertionLayer')
  request.r(i447[5], i447[6], 0, i446, 'storedPoint')
  request.r(i447[7], i447[8], 0, i446, 'frontLip')
  request.r(i447[9], i447[10], 0, i446, 'progressFill')
  request.r(i447[11], i447[12], 0, i446, 'check')
  request.r(i447[13], i447[14], 0, i446, 'categoryIcon')
  var i449 = i447[15]
  var i448 = []
  for(var i = 0; i < i449.length; i += 2) {
  request.r(i449[i + 0], i449[i + 1], 2, i448, '')
  }
  i446.stamps = i448
  request.r(i447[16], i447[17], 0, i446, 'title')
  return i446
}

Deserializers["UnityEngine.UI.RectMask2D"] = function (request, data, root) {
  var i452 = root || request.c( 'UnityEngine.UI.RectMask2D' )
  var i453 = data
  i452.m_Padding = new pc.Vec4( i453[0], i453[1], i453[2], i453[3] )
  i452.m_Softness = new pc.Vec2( i453[4], i453[5] )
  return i452
}

Deserializers["LittleFinds.CardController"] = function (request, data, root) {
  var i454 = root || request.c( 'LittleFinds.CardController' )
  var i455 = data
  request.r(i455[0], i455[1], 0, i454, 'visual')
  request.r(i455[2], i455[3], 0, i454, 'rotator')
  request.r(i455[4], i455[5], 0, i454, 'front')
  request.r(i455[6], i455[7], 0, i454, 'back')
  request.r(i455[8], i455[9], 0, i454, 'illustration')
  request.r(i455[10], i455[11], 0, i454, 'frontPaper')
  request.r(i455[12], i455[13], 0, i454, 'shine')
  request.r(i455[14], i455[15], 0, i454, 'title')
  request.r(i455[16], i455[17], 0, i454, 'subtitle')
  request.r(i455[18], i455[19], 0, i454, 'number')
  request.r(i455[20], i455[21], 0, i454, 'group')
  return i454
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasGroup"] = function (request, data, root) {
  var i456 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasGroup' )
  var i457 = data
  i456.m_Alpha = i457[0]
  i456.m_Interactable = !!i457[1]
  i456.m_BlocksRaycasts = !!i457[2]
  i456.m_IgnoreParentGroups = !!i457[3]
  i456.enabled = !!i457[4]
  return i456
}

Deserializers["UnityEngine.EventSystems.EventSystem"] = function (request, data, root) {
  var i458 = root || request.c( 'UnityEngine.EventSystems.EventSystem' )
  var i459 = data
  request.r(i459[0], i459[1], 0, i458, 'm_FirstSelected')
  i458.m_sendNavigationEvents = !!i459[2]
  i458.m_DragThreshold = i459[3]
  return i458
}

Deserializers["UnityEngine.EventSystems.StandaloneInputModule"] = function (request, data, root) {
  var i460 = root || request.c( 'UnityEngine.EventSystems.StandaloneInputModule' )
  var i461 = data
  i460.m_HorizontalAxis = i461[0]
  i460.m_VerticalAxis = i461[1]
  i460.m_SubmitButton = i461[2]
  i460.m_CancelButton = i461[3]
  i460.m_InputActionsPerSecond = i461[4]
  i460.m_RepeatDelay = i461[5]
  i460.m_ForceModuleActive = !!i461[6]
  i460.m_SendPointerHoverToParent = !!i461[7]
  return i460
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings"] = function (request, data, root) {
  var i462 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings' )
  var i463 = data
  i462.ambientIntensity = i463[0]
  i462.reflectionIntensity = i463[1]
  i462.ambientMode = i463[2]
  i462.ambientLight = new pc.Color(i463[3], i463[4], i463[5], i463[6])
  i462.ambientSkyColor = new pc.Color(i463[7], i463[8], i463[9], i463[10])
  i462.ambientGroundColor = new pc.Color(i463[11], i463[12], i463[13], i463[14])
  i462.ambientEquatorColor = new pc.Color(i463[15], i463[16], i463[17], i463[18])
  i462.fogColor = new pc.Color(i463[19], i463[20], i463[21], i463[22])
  i462.fogEndDistance = i463[23]
  i462.fogStartDistance = i463[24]
  i462.fogDensity = i463[25]
  i462.fog = !!i463[26]
  request.r(i463[27], i463[28], 0, i462, 'skybox')
  i462.fogMode = i463[29]
  var i465 = i463[30]
  var i464 = []
  for(var i = 0; i < i465.length; i += 1) {
    i464.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap', i465[i + 0]) );
  }
  i462.lightmaps = i464
  i462.lightProbes = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes', i463[31], i462.lightProbes)
  i462.lightmapsMode = i463[32]
  i462.mixedBakeMode = i463[33]
  i462.environmentLightingMode = i463[34]
  i462.ambientProbe = new pc.SphericalHarmonicsL2(i463[35])
  i462.referenceAmbientProbe = new pc.SphericalHarmonicsL2(i463[36])
  i462.useReferenceAmbientProbe = !!i463[37]
  request.r(i463[38], i463[39], 0, i462, 'customReflection')
  request.r(i463[40], i463[41], 0, i462, 'defaultReflection')
  i462.defaultReflectionMode = i463[42]
  i462.defaultReflectionResolution = i463[43]
  i462.sunLightObjectId = i463[44]
  i462.pixelLightCount = i463[45]
  i462.defaultReflectionHDR = !!i463[46]
  i462.hasLightDataAsset = !!i463[47]
  i462.hasManualGenerate = !!i463[48]
  return i462
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap"] = function (request, data, root) {
  var i468 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap' )
  var i469 = data
  request.r(i469[0], i469[1], 0, i468, 'lightmapColor')
  request.r(i469[2], i469[3], 0, i468, 'lightmapDirection')
  request.r(i469[4], i469[5], 0, i468, 'shadowMask')
  return i468
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes"] = function (request, data, root) {
  var i470 = root || new UnityEngine.LightProbes()
  var i471 = data
  return i470
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material"] = function (request, data, root) {
  var i478 = root || new pc.UnityMaterial()
  var i479 = data
  i478.name = i479[0]
  request.r(i479[1], i479[2], 0, i478, 'shader')
  i478.renderQueue = i479[3]
  i478.enableInstancing = !!i479[4]
  var i481 = i479[5]
  var i480 = []
  for(var i = 0; i < i481.length; i += 1) {
    i480.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter', i481[i + 0]) );
  }
  i478.floatParameters = i480
  var i483 = i479[6]
  var i482 = []
  for(var i = 0; i < i483.length; i += 1) {
    i482.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter', i483[i + 0]) );
  }
  i478.colorParameters = i482
  var i485 = i479[7]
  var i484 = []
  for(var i = 0; i < i485.length; i += 1) {
    i484.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter', i485[i + 0]) );
  }
  i478.vectorParameters = i484
  var i487 = i479[8]
  var i486 = []
  for(var i = 0; i < i487.length; i += 1) {
    i486.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter', i487[i + 0]) );
  }
  i478.textureParameters = i486
  var i489 = i479[9]
  var i488 = []
  for(var i = 0; i < i489.length; i += 1) {
    i488.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag', i489[i + 0]) );
  }
  i478.materialFlags = i488
  return i478
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter"] = function (request, data, root) {
  var i492 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter' )
  var i493 = data
  i492.name = i493[0]
  i492.value = i493[1]
  return i492
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter"] = function (request, data, root) {
  var i496 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter' )
  var i497 = data
  i496.name = i497[0]
  i496.value = new pc.Color(i497[1], i497[2], i497[3], i497[4])
  return i496
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter"] = function (request, data, root) {
  var i500 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter' )
  var i501 = data
  i500.name = i501[0]
  i500.value = new pc.Vec4( i501[1], i501[2], i501[3], i501[4] )
  return i500
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter"] = function (request, data, root) {
  var i504 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter' )
  var i505 = data
  i504.name = i505[0]
  request.r(i505[1], i505[2], 0, i504, 'value')
  return i504
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag"] = function (request, data, root) {
  var i508 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag' )
  var i509 = data
  i508.name = i509[0]
  i508.enabled = !!i509[1]
  return i508
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader"] = function (request, data, root) {
  var i510 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader' )
  var i511 = data
  var i513 = i511[0]
  var i512 = new (System.Collections.Generic.List$1(Bridge.ns('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError')))
  for(var i = 0; i < i513.length; i += 1) {
    i512.add(request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError', i513[i + 0]));
  }
  i510.ShaderCompilationErrors = i512
  i510.name = i511[1]
  i510.guid = i511[2]
  var i515 = i511[3]
  var i514 = []
  for(var i = 0; i < i515.length; i += 1) {
    i514.push( i515[i + 0] );
  }
  i510.shaderDefinedKeywords = i514
  var i517 = i511[4]
  var i516 = []
  for(var i = 0; i < i517.length; i += 1) {
    i516.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass', i517[i + 0]) );
  }
  i510.passes = i516
  var i519 = i511[5]
  var i518 = []
  for(var i = 0; i < i519.length; i += 1) {
    i518.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass', i519[i + 0]) );
  }
  i510.usePasses = i518
  var i521 = i511[6]
  var i520 = []
  for(var i = 0; i < i521.length; i += 1) {
    i520.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue', i521[i + 0]) );
  }
  i510.defaultParameterValues = i520
  request.r(i511[7], i511[8], 0, i510, 'unityFallbackShader')
  i510.readDepth = !!i511[9]
  i510.hasDepthOnlyPass = !!i511[10]
  i510.isCreatedByShaderGraph = !!i511[11]
  i510.disableBatching = !!i511[12]
  i510.compiled = !!i511[13]
  return i510
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError"] = function (request, data, root) {
  var i524 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError' )
  var i525 = data
  i524.shaderName = i525[0]
  i524.errorMessage = i525[1]
  return i524
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass"] = function (request, data, root) {
  var i530 = root || new pc.UnityShaderPass()
  var i531 = data
  i530.id = i531[0]
  i530.subShaderIndex = i531[1]
  i530.name = i531[2]
  i530.passType = i531[3]
  i530.grabPassTextureName = i531[4]
  i530.usePass = !!i531[5]
  i530.zTest = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i531[6], i530.zTest)
  i530.zWrite = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i531[7], i530.zWrite)
  i530.culling = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i531[8], i530.culling)
  i530.blending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i531[9], i530.blending)
  i530.alphaBlending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i531[10], i530.alphaBlending)
  i530.colorWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i531[11], i530.colorWriteMask)
  i530.offsetUnits = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i531[12], i530.offsetUnits)
  i530.offsetFactor = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i531[13], i530.offsetFactor)
  i530.stencilRef = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i531[14], i530.stencilRef)
  i530.stencilReadMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i531[15], i530.stencilReadMask)
  i530.stencilWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i531[16], i530.stencilWriteMask)
  i530.stencilOp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i531[17], i530.stencilOp)
  i530.stencilOpFront = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i531[18], i530.stencilOpFront)
  i530.stencilOpBack = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i531[19], i530.stencilOpBack)
  var i533 = i531[20]
  var i532 = []
  for(var i = 0; i < i533.length; i += 1) {
    i532.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag', i533[i + 0]) );
  }
  i530.tags = i532
  var i535 = i531[21]
  var i534 = []
  for(var i = 0; i < i535.length; i += 1) {
    i534.push( i535[i + 0] );
  }
  i530.passDefinedKeywords = i534
  var i537 = i531[22]
  var i536 = []
  for(var i = 0; i < i537.length; i += 1) {
    i536.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup', i537[i + 0]) );
  }
  i530.passDefinedKeywordGroups = i536
  var i539 = i531[23]
  var i538 = []
  for(var i = 0; i < i539.length; i += 1) {
    i538.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i539[i + 0]) );
  }
  i530.variants = i538
  var i541 = i531[24]
  var i540 = []
  for(var i = 0; i < i541.length; i += 1) {
    i540.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i541[i + 0]) );
  }
  i530.excludedVariants = i540
  i530.hasDepthReader = !!i531[25]
  return i530
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value"] = function (request, data, root) {
  var i542 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value' )
  var i543 = data
  i542.val = i543[0]
  i542.name = i543[1]
  return i542
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending"] = function (request, data, root) {
  var i544 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending' )
  var i545 = data
  i544.src = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i545[0], i544.src)
  i544.dst = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i545[1], i544.dst)
  i544.op = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i545[2], i544.op)
  return i544
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp"] = function (request, data, root) {
  var i546 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp' )
  var i547 = data
  i546.pass = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i547[0], i546.pass)
  i546.fail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i547[1], i546.fail)
  i546.zFail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i547[2], i546.zFail)
  i546.comp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i547[3], i546.comp)
  return i546
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag"] = function (request, data, root) {
  var i550 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag' )
  var i551 = data
  i550.name = i551[0]
  i550.value = i551[1]
  return i550
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup"] = function (request, data, root) {
  var i554 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup' )
  var i555 = data
  var i557 = i555[0]
  var i556 = []
  for(var i = 0; i < i557.length; i += 1) {
    i556.push( i557[i + 0] );
  }
  i554.keywords = i556
  i554.hasDiscard = !!i555[1]
  return i554
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant"] = function (request, data, root) {
  var i560 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant' )
  var i561 = data
  i560.passId = i561[0]
  i560.subShaderIndex = i561[1]
  var i563 = i561[2]
  var i562 = []
  for(var i = 0; i < i563.length; i += 1) {
    i562.push( i563[i + 0] );
  }
  i560.keywords = i562
  i560.vertexProgram = i561[3]
  i560.fragmentProgram = i561[4]
  i560.exportedForWebGl2 = !!i561[5]
  i560.readDepth = !!i561[6]
  return i560
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass"] = function (request, data, root) {
  var i566 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass' )
  var i567 = data
  request.r(i567[0], i567[1], 0, i566, 'shader')
  i566.pass = i567[2]
  return i566
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue"] = function (request, data, root) {
  var i570 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue' )
  var i571 = data
  i570.name = i571[0]
  i570.type = i571[1]
  i570.value = new pc.Vec4( i571[2], i571[3], i571[4], i571[5] )
  i570.textureValue = i571[6]
  i570.shaderPropertyFlag = i571[7]
  return i570
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Sprite"] = function (request, data, root) {
  var i572 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Sprite' )
  var i573 = data
  i572.name = i573[0]
  request.r(i573[1], i573[2], 0, i572, 'texture')
  i572.aabb = i573[3]
  i572.vertices = i573[4]
  i572.triangles = i573[5]
  i572.textureRect = UnityEngine.Rect.MinMaxRect(i573[6], i573[7], i573[8], i573[9])
  i572.packedRect = UnityEngine.Rect.MinMaxRect(i573[10], i573[11], i573[12], i573[13])
  i572.border = new pc.Vec4( i573[14], i573[15], i573[16], i573[17] )
  i572.transparency = i573[18]
  i572.bounds = i573[19]
  i572.pixelsPerUnit = i573[20]
  i572.textureWidth = i573[21]
  i572.textureHeight = i573[22]
  i572.nativeSize = new pc.Vec2( i573[23], i573[24] )
  i572.pivot = new pc.Vec2( i573[25], i573[26] )
  i572.textureRectOffset = new pc.Vec2( i573[27], i573[28] )
  return i572
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.AudioClip"] = function (request, data, root) {
  var i574 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.AudioClip' )
  var i575 = data
  i574.name = i575[0]
  return i574
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font"] = function (request, data, root) {
  var i576 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font' )
  var i577 = data
  i576.name = i577[0]
  i576.ascent = i577[1]
  i576.originalLineHeight = i577[2]
  i576.fontSize = i577[3]
  var i579 = i577[4]
  var i578 = []
  for(var i = 0; i < i579.length; i += 1) {
    i578.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo', i579[i + 0]) );
  }
  i576.characterInfo = i578
  request.r(i577[5], i577[6], 0, i576, 'texture')
  i576.originalFontSize = i577[7]
  return i576
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo"] = function (request, data, root) {
  var i582 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo' )
  var i583 = data
  i582.index = i583[0]
  i582.advance = i583[1]
  i582.bearing = i583[2]
  i582.glyphWidth = i583[3]
  i582.glyphHeight = i583[4]
  i582.minX = i583[5]
  i582.maxX = i583[6]
  i582.minY = i583[7]
  i582.maxY = i583[8]
  i582.uvBottomLeftX = i583[9]
  i582.uvBottomLeftY = i583[10]
  i582.uvBottomRightX = i583[11]
  i582.uvBottomRightY = i583[12]
  i582.uvTopLeftX = i583[13]
  i582.uvTopLeftY = i583[14]
  i582.uvTopRightX = i583[15]
  i582.uvTopRightY = i583[16]
  return i582
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources"] = function (request, data, root) {
  var i584 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources' )
  var i585 = data
  var i587 = i585[0]
  var i586 = []
  for(var i = 0; i < i587.length; i += 1) {
    i586.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Resources+File', i587[i + 0]) );
  }
  i584.files = i586
  i584.componentToPrefabIds = i585[1]
  return i584
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources+File"] = function (request, data, root) {
  var i590 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources+File' )
  var i591 = data
  i590.path = i591[0]
  request.r(i591[1], i591[2], 0, i590, 'unityObject')
  return i590
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings"] = function (request, data, root) {
  var i592 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings' )
  var i593 = data
  var i595 = i593[0]
  var i594 = []
  for(var i = 0; i < i595.length; i += 1) {
    i594.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder', i595[i + 0]) );
  }
  i592.scriptsExecutionOrder = i594
  var i597 = i593[1]
  var i596 = []
  for(var i = 0; i < i597.length; i += 1) {
    i596.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer', i597[i + 0]) );
  }
  i592.sortingLayers = i596
  var i599 = i593[2]
  var i598 = []
  for(var i = 0; i < i599.length; i += 1) {
    i598.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer', i599[i + 0]) );
  }
  i592.cullingLayers = i598
  i592.timeSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings', i593[3], i592.timeSettings)
  i592.physicsSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings', i593[4], i592.physicsSettings)
  i592.physics2DSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings', i593[5], i592.physics2DSettings)
  i592.qualitySettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i593[6], i592.qualitySettings)
  i592.enableRealtimeShadows = !!i593[7]
  i592.enableAutoInstancing = !!i593[8]
  i592.enableStaticBatching = !!i593[9]
  i592.enableDynamicBatching = !!i593[10]
  i592.lightmapEncodingQuality = i593[11]
  i592.desiredColorSpace = i593[12]
  var i601 = i593[13]
  var i600 = []
  for(var i = 0; i < i601.length; i += 1) {
    i600.push( i601[i + 0] );
  }
  i592.allTags = i600
  return i592
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder"] = function (request, data, root) {
  var i604 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder' )
  var i605 = data
  i604.name = i605[0]
  i604.value = i605[1]
  return i604
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer"] = function (request, data, root) {
  var i608 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer' )
  var i609 = data
  i608.id = i609[0]
  i608.name = i609[1]
  i608.value = i609[2]
  return i608
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer"] = function (request, data, root) {
  var i612 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer' )
  var i613 = data
  i612.id = i613[0]
  i612.name = i613[1]
  return i612
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings"] = function (request, data, root) {
  var i614 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings' )
  var i615 = data
  i614.fixedDeltaTime = i615[0]
  i614.maximumDeltaTime = i615[1]
  i614.timeScale = i615[2]
  i614.maximumParticleTimestep = i615[3]
  return i614
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings"] = function (request, data, root) {
  var i616 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings' )
  var i617 = data
  i616.gravity = new pc.Vec3( i617[0], i617[1], i617[2] )
  i616.defaultSolverIterations = i617[3]
  i616.bounceThreshold = i617[4]
  i616.autoSyncTransforms = !!i617[5]
  i616.autoSimulation = !!i617[6]
  var i619 = i617[7]
  var i618 = []
  for(var i = 0; i < i619.length; i += 1) {
    i618.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask', i619[i + 0]) );
  }
  i616.collisionMatrix = i618
  return i616
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask"] = function (request, data, root) {
  var i622 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask' )
  var i623 = data
  i622.enabled = !!i623[0]
  i622.layerId = i623[1]
  i622.otherLayerId = i623[2]
  return i622
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings"] = function (request, data, root) {
  var i624 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings' )
  var i625 = data
  request.r(i625[0], i625[1], 0, i624, 'material')
  i624.gravity = new pc.Vec2( i625[2], i625[3] )
  i624.positionIterations = i625[4]
  i624.velocityIterations = i625[5]
  i624.velocityThreshold = i625[6]
  i624.maxLinearCorrection = i625[7]
  i624.maxAngularCorrection = i625[8]
  i624.maxTranslationSpeed = i625[9]
  i624.maxRotationSpeed = i625[10]
  i624.baumgarteScale = i625[11]
  i624.baumgarteTOIScale = i625[12]
  i624.timeToSleep = i625[13]
  i624.linearSleepTolerance = i625[14]
  i624.angularSleepTolerance = i625[15]
  i624.defaultContactOffset = i625[16]
  i624.autoSimulation = !!i625[17]
  i624.queriesHitTriggers = !!i625[18]
  i624.queriesStartInColliders = !!i625[19]
  i624.callbacksOnDisable = !!i625[20]
  i624.reuseCollisionCallbacks = !!i625[21]
  i624.autoSyncTransforms = !!i625[22]
  var i627 = i625[23]
  var i626 = []
  for(var i = 0; i < i627.length; i += 1) {
    i626.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask', i627[i + 0]) );
  }
  i624.collisionMatrix = i626
  return i624
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask"] = function (request, data, root) {
  var i630 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask' )
  var i631 = data
  i630.enabled = !!i631[0]
  i630.layerId = i631[1]
  i630.otherLayerId = i631[2]
  return i630
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.QualitySettings"] = function (request, data, root) {
  var i632 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.QualitySettings' )
  var i633 = data
  var i635 = i633[0]
  var i634 = []
  for(var i = 0; i < i635.length; i += 1) {
    i634.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i635[i + 0]) );
  }
  i632.qualityLevels = i634
  var i637 = i633[1]
  var i636 = []
  for(var i = 0; i < i637.length; i += 1) {
    i636.push( i637[i + 0] );
  }
  i632.names = i636
  i632.shadows = i633[2]
  i632.anisotropicFiltering = i633[3]
  i632.antiAliasing = i633[4]
  i632.lodBias = i633[5]
  i632.shadowCascades = i633[6]
  i632.shadowDistance = i633[7]
  i632.shadowmaskMode = i633[8]
  i632.shadowProjection = i633[9]
  i632.shadowResolution = i633[10]
  i632.softParticles = !!i633[11]
  i632.softVegetation = !!i633[12]
  i632.activeColorSpace = i633[13]
  i632.desiredColorSpace = i633[14]
  i632.masterTextureLimit = i633[15]
  i632.maxQueuedFrames = i633[16]
  i632.particleRaycastBudget = i633[17]
  i632.pixelLightCount = i633[18]
  i632.realtimeReflectionProbes = !!i633[19]
  i632.shadowCascade2Split = i633[20]
  i632.shadowCascade4Split = new pc.Vec3( i633[21], i633[22], i633[23] )
  i632.streamingMipmapsActive = !!i633[24]
  i632.vSyncCount = i633[25]
  i632.asyncUploadBufferSize = i633[26]
  i632.asyncUploadTimeSlice = i633[27]
  i632.billboardsFaceCameraPosition = !!i633[28]
  i632.shadowNearPlaneOffset = i633[29]
  i632.streamingMipmapsMemoryBudget = i633[30]
  i632.maximumLODLevel = i633[31]
  i632.streamingMipmapsAddAllCameras = !!i633[32]
  i632.streamingMipmapsMaxLevelReduction = i633[33]
  i632.streamingMipmapsRenderersPerFrame = i633[34]
  i632.resolutionScalingFixedDPIFactor = i633[35]
  i632.streamingMipmapsMaxFileIORequests = i633[36]
  i632.currentQualityLevel = i633[37]
  return i632
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

Deserializers.buildID = "9936b6ce-6768-4e7c-8e9f-1006ddc7a61c";

Deserializers.runtimeInitializeOnLoadInfos = [[["UnityEngine","Experimental","Rendering","ScriptableRuntimeReflectionSystemSettings","ScriptingDirtyReflectionSystemInstance"]],[],[],[],[]];

Deserializers.typeNameToIdMap = function(){ var i = 0; return Deserializers.types.reduce( function( res, item ) { res[ item ] = i++; return res; }, {} ) }()

