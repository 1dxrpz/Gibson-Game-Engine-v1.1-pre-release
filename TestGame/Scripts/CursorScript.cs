﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using GameEngineTK.Engine;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using GameEngineTK.Engine.Prototypes.Interfaces;
using GameEngineTK.Engine.Components;
using GameEngineTK.Engine.Utils;
using GameEngineTK.Engine.Rendering;
using GameEngineTK.Engine.Prototypes.Enums;

namespace GameEngineTK.Scripts
{
	class CursorScript : DxScript
	{
		TextureHandler cursorTexture = new TextureHandler(@"\TestGame\Content\CursorSpriteSheet.png");
		public static GameObject Cursor;
		
		public override void Start()
		{
			ScriptManager.Services.GetService<ProjectSettings>().ShowColliders = false;
			Cursor = new GameObject(cursorTexture);
			Cursor.AddComponent(new Animation());
			Cursor.GetComponent<Animation>().SpriteSheet = cursorTexture.ToTexture2D();
			Cursor.GetComponent<Transform>().Parallax = new Vector2(0, 0);
			Cursor.GetComponent<Transform>().Width = 32;
			Cursor.GetComponent<Transform>().Height = 42;

		}

		public override void Update()
		{
			
			Cursor.GetComponent<Animation>().FrameCount = 2;
			Cursor.GetComponent<Animation>().FrameSize = new Point(16, 21);
			Cursor.GetComponent<Animation>().AnimationSpeed = 0;
			Cursor.GetComponent<Animation>().CurrentFrame = Mouse.GetState().LeftButton == ButtonState.Pressed ? 1 : 0;

			Cursor.GetComponent<Transform>().Position = Mouse.GetState().Position.ToVector2();
			Cursor.GetComponent<Transform>().Parallax = new Vector2(0, 0);
		}
		
	}
}
