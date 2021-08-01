﻿using System;
using System.Collections.Generic;
using System.Text;
using GameEngineTK.Engine;
using GameEngineTK.Engine.Prototypes.Interfaces;
using GameEngineTK.Engine.Rendering;
using Microsoft.Xna.Framework;

namespace GameEngineTK.Scripts
{
	
	public class CameraScript : DxScript
	{
		
		Debug debug;
		public override void Start()
		{
			
		}

		public override void Update()
		{
			//ScriptManager.Services.GetService<ProjectSettings>()
			debug = ScriptManager.Services.GetService<Debug>();
			debug.AddDebugLine($"FPS: {debug.FPS}");
			Vector2 pos = PlayerScript.Player.GetComponent<Transform>().Position;
			Camera.Position = Vector2.Lerp(Camera.Position,
				pos - (new Vector2(1920 / 2 - PlayerScript.Player.GetComponent<Transform>().Width / 4, 1080 / 2 - PlayerScript.Player.GetComponent<Transform>().Height / 4)),
				.005f * Time.deltaTime);
		}
	}
}
