﻿using GibsonCore.Abstract;
using GibsonCore.Components;
using GibsonCore.Core;
using GibsonCore.Enums;
using GibsonCore.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using tainicom.Aether.Physics2D.Dynamics;

namespace GameEngineTK.Scripts
{
	public class PlayerScript : DxScript
	{
		public static GameObject Player;
		public static GameObject Ground;
		Texture2D _texture;
		Texture2D _groundTexture;

		Transform pt;
		public override void Start()
		{
			ScriptManager.Services.GetService<Lighting>().Enabled = true;
			ScriptManager.Services.GetService<Lighting>().AmbientColor = Color.Black;
			ScriptManager.Services.GetService<Lighting>().ApplyLighting();

			_texture = GameEntry.contentManager.Load<Texture2D>("Knight");
			_groundTexture = GameEntry.contentManager.Load<Texture2D>("frame");

			Ground = new GameObject();
			Player = new GameObject();

			Player.AddComponent(new Animation());
			
			pt = Player.GetComponent<Transform>();
			pt.Width = 64 * 2;
			pt.Height = 64 * 2;
			Ground.AddComponent(new Sprite());
			Ground.GetComponent<Transform>().Width = 500;
			Ground.GetComponent<Transform>().Height = 100;
			Ground.GetComponent<Sprite>().Texture = _groundTexture;
			Player.GetComponent<Transform>().Position = new Vector2(0, -200);

			Player.GetComponent<Animation>().SpriteSheet = _texture;

			Player.GetComponent<Animation>().FrameCount = 8;
			Player.GetComponent<Animation>().FrameSize = new Point(32, 32);
			Player.GetComponent<Animation>().AnimationSpeed = 1;
			//Player.GetComponent<Animation>().OriginPosition = new Vector2(32, 32);

			Ground.AddComponent(new Physics());
			Ground.GetComponent<Physics>().BodyType = BodyType.Static;
			Player.AddComponent(new Physics());
			Player.GetComponent<Physics>().BodyType = BodyType.Dynamic;
			
			Player.GetComponent<Animation>().OriginPosition = new Vector2(64, 64);
			Player.GetComponent<Physics>().OnCollision += collide;

			Ground.AddComponent(new ShadowCaster());

			light = new PointLight()
			{
				Scale = new Vector2(500, 500),
				ShadowType = ShadowType.Occluded,
				Radius = 100f
			};
			ScriptManager.Services.GetService<Lighting>().AddLightSource(light);
			Scene s1 = new Scene();
			ScriptManager.Services.GetService<SceneManager>().Add(s1);

			Scene s2 = new Scene();
			ScriptManager.Services.GetService<SceneManager>().Add(s2);

			Player.Scene = GameEntry.scene;
			Ground.Scene = GameEntry.scene;

		}
		Light light;

		private void collide(Body obj)
		{
			//Player.GetComponent<Physics>().ApplyForce(new Vector2(0, -1000f));
		}

		public override void Update()
		{
			light.Position = Mouse.GetState().Position.ToVector2();
			if (Keyboard.GetState().IsKeyDown(Keys.V))
			{
				Player.isVisible = VisibleState.Invisible;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.D))
			{
				Player.GetComponent<Physics>().ApplyForce(new Vector2(10f, 0));
			}

			if (Keyboard.GetState().IsKeyDown(Keys.B))
			{
				ScriptManager.Services.GetService<SceneManager>().LoadScene(1);
				ScriptManager.Services.GetService<Lighting>().Enabled = false;
				ScriptManager.Services.GetService<Lighting>().ApplyLighting();
			}
			if (Keyboard.GetState().IsKeyDown(Keys.N))
			{
				ScriptManager.Services.GetService<SceneManager>().LoadScene(0);
				ScriptManager.Services.GetService<Lighting>().Enabled = true;
				ScriptManager.Services.GetService<Lighting>().ApplyLighting();
			}

		}
	}
}