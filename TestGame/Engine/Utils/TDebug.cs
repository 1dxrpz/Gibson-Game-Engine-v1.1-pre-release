﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using tainicom.Aether.Physics2D.Dynamics;

namespace GameEngineTK.Engine
{
	public static class TWorld
	{
		static public Vector2 ScreenPosition(Vector2 pos) => pos - Camera.Position;
		static public Vector2 WorldPosition(Vector2 pos) => pos + Camera.Position;
		static public World World;
	}
	public static class Time
	{
		public static float deltaTime;
	}
	public class ProjectSettings
	{
		public int MaxFPS = 900;
		public int WindowHeight = 1080;
		public int WindowWidth = 1920;
		public bool VSync = false;
		public bool FixedTS = true;
		public bool ShowColliders = true;
	}
	public class TDebug
	{
		public bool Enabled = ConfigReader.Parse("project").ContainsKey("EnableDebug") ? ConfigReader.Parse("project").GetBool("EnableDebug") : false;
		static public double FPS;
		private double frames = 0;
		private double updates = 0;
		private double elapsed = 0;
		private double last = 0;
		private double now = 0;
		public double msgFrequency = .2f;
		public string msg = "";
		public string text = "";
		public List<string> DebugLines = new List<string>();
		public void Update(GameTime gameTime)
		{
			now = gameTime.TotalGameTime.TotalSeconds;
			elapsed = (double)(now - last);
			FPS = Math.Round(frames / elapsed);
			if (elapsed > msgFrequency)
			{
				elapsed = 0;
				frames = 0;
				updates = 0;
				last = now;
			}
			updates++;
			frames++;

			text = "";
			foreach (string line in DebugLines)
			{
				text += "\n" + line;
			}

			DebugLines = new List<string>();
		}
	}
}