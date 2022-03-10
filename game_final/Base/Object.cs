using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game_final.Base
{
    class Object
    {
        public Vector2 Position = new Vector2(0, 0);
        public Vector2 Origin = new Vector2(0, 0);
        public Texture2D Instance;
        public float Scale = 1f;
        public float LayerDepth = 0f;
        public float Opacity = 1f;
        public bool CanClick = true;

        public float Rotation = 0f;

        protected int _width, _height;

        public event EventHandler Click;

        public bool isDestroyed = false;

        public float X
        {
            get { return Position.X; }
            set { Position.X = value; }
        }

        public float Y
        {
            get { return Position.Y; }
            set { Position.Y = value; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public Rectangle HitBox
        {
            get { return new Rectangle((int)(X - Origin.X * Scale), (int)(Y - Origin.Y * Scale), Width, Height); }
        }

        public bool IsHovering
        {
            get {
                Rectangle mouseBox = new Rectangle(Environments.Global.CurrentMouseState.X, Environments.Global.CurrentMouseState.Y, 1, 1);
                return CanClick && (!Environments.Global.WindowActive ? false : HitBox.Intersects(mouseBox));
            }
        }

        public bool IsClicked
        {
            get
            {
                return IsHovering && 
                    Environments.Global.CurrentMouseState.LeftButton == ButtonState.Released && 
                    Environments.Global.CurrentMouseState.LeftButton != Environments.Global.PreviousMouseState.LeftButton;
            }
        }

        public void SetPosition(float x, float y)
        {
            Position.X = x;
            Position.Y = y;
        }

        public void SetOrigin(float x, float y)
        {
            Origin.X = x;
            Origin.Y = y;
        }

        public void Destroy()
        {
            isDestroyed = true;
        }

        public void InvokeClick()
        {
            Click?.Invoke(this, new EventArgs());
        }

        public void InvokeClick(object obj) {
            Click?.Invoke(obj, new EventArgs());
        }
    }
}
