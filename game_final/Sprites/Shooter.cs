using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using game_final.Utils;

namespace game_final.Sprites
{
    class Shooter : Base.SpriteGroup
    {
        public int GuideLength = 1;
        public Vector2 ReflectPoint;

        private GraphicsDevice _graphics;

        private Shapes.Rectangle _body;
        private Shapes.Line _guide;
        private Shapes.Line _reflectGuide;

        public Shooter(SpriteBatch spriteBatch, GraphicsDevice graphics) : base(spriteBatch, 50, 50)
        {
            ReflectPoint = new Vector2(0, 0);

            SetPosition(Settings.WINDOW_WIDTH / 2 - _width / 2, Settings.WINDOW_HEIGHT - _height);

            _graphics = graphics;

            _body = new Shapes.Rectangle(graphics, _width, _height);
            _body.SetColor(Color.Red);
            _body.SetOrigin(_width / 2, _height / 2);
            _body.SetPosition(_width / 2, _height / 2);
        }

        public void Update()
        {
            if (GuideLength > Settings.MAX_GUIDE_LENGTH || GuideLength <= 0) GuideLength = Settings.MAX_GUIDE_LENGTH;

            _guide = new Shapes.Line(_graphics, _width / 2, _height / 2, _width / 2, 0, 5, GuideLength);
            _guide.SetColor(Color.Black);
            _guide.SetOrigin(5 / 2, _guide.Height);
            _guide.SetPosition(_width / 2, _height / 2);

            int pointX = 400;
            int pointY = -(int)(400 * Math.Tan(Rotation));
            _reflectGuide = new Shapes.Line(_graphics, 0, 0, pointX, pointY, 5, 200);
            _reflectGuide.SetColor(Color.Black);
            _reflectGuide.SetOrigin(2, 0);
            _reflectGuide.SetPosition(ReflectPoint.X, ReflectPoint.Y);
            _reflectGuide.Rotation += Converter.DegressToRadians(ReflectPoint.X < 400 ? -90 : 90);
        }

        public void Draw()
        {
            DrawSprite(_body);
            DrawSprite(_guide);

            _spriteBatch.Draw(_reflectGuide.Instance, _reflectGuide.Position, null, _reflectGuide.Color, _reflectGuide.Rotation, _reflectGuide.Origin, 1f, SpriteEffects.None, 0f);
        }

        public void SetReflectPoint(int x, int y)
        {
            ReflectPoint.X = x;
            ReflectPoint.Y = y;
        }
    }
}
