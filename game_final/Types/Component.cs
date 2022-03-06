using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace game_final.Types
{
	public abstract class Component
	{
		public abstract void Draw();

		public abstract void Update();
	}
}
