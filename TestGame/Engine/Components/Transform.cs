﻿using GameEngineTK.Engine.Components;
using GameEngineTK.Engine.Prototypes.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngineTK.Engine
{
	public class Transform : ComponentInstance
	{
		private int width = 32, height = 32;
		public Vector2 Velocity;
		public float Rotation;
		public Vector2 Parallax = new Vector2(1, 1);
		public Vector2 Position { get; set; }
		public int Width
		{
			get
			{
				return width;
			}
			set
			{
				width = value;
			}
		}
		public int Height
		{
			get
			{
				return height;
			}
			set
			{
				height = value;
			}
		}

		public Vector2 Forward
		{
			get
			{
				return new Vector2(
					(float)(1 * Math.Cos(Rotation)),
					(float)(1 * Math.Sin(Rotation))
				);
			}
			private set { }
		}
		public Vector2 Backward { get { return -Forward; } }
		public Vector2 Top
		{
			get
			{ return new Vector2(
					(float)(1 * Math.Sin(Rotation)),
					(float)(-1 * Math.Cos(Rotation))
				);
			}
		}
		public Vector2 Bottom
		{
			get { return -Top; }
		}

		public void Translate(Vector2 pos)
		{
			Position += pos;
		}
		public void Translate(float _x, float _y)
		{
			Position += new Vector2(_x, _y);
		}
		public void TranslateX(float _x)
		{
			Position += new Vector2(_x, 0);
		}
		public void TranslateY(float _y)
		{
			Position += new Vector2(0, _y);
		}
		public Vector2 ScreenPosition()
		{
			return ((Position) - Camera.Position * Parallax);
		}

		public void RotateTowardPosition(Vector2 pos)
		{
			ParentObject.GetComponent<Transform>().Rotation = (float)Math.Atan2(
				pos.Y - ParentObject.GetComponent<Transform>().ScreenPosition().Y,
				pos.X - ParentObject.GetComponent<Transform>().ScreenPosition().X);
		}
		public void RotateTowardObject(GameObject obj)
		{
			RotateTowardPosition(obj.GetComponent<Transform>().Position);
		}
		public void RotateClockwise(float angle)
		{
			ParentObject.GetComponent<Transform>().Rotation += angle;
		}
		public void RotateCounterClockwise(float angle)
		{
			ParentObject.GetComponent<Transform>().Rotation -= angle;
		}

		public override void Update()
		{
			Position += Velocity;
		}
	}
}
