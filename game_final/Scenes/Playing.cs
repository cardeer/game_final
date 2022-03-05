using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_final.Scenes
{
    class Playing : Base.UIRenderer
    {
        private Shapes.Line _rightVerticalLine;

        public Playing(SpriteBatch spriteBatch, GraphicsDevice graphics) : base(spriteBatch)
        {
            _rightVerticalLine = new Shapes.Line(graphics, Constants.REFLECT_RIGHT + Settings.BALL_SIZE / 2, 0, Constants.REFLECT_RIGHT + Settings.BALL_SIZE / 2, Settings.WINDOW_HEIGHT, 5, Settings.WINDOW_HEIGHT);
            _rightVerticalLine.SetColor(Color.Black);
            _rightVerticalLine.SetOrigin(2, 0);
        }

        public void Draw()
        {
            DrawSprite(_rightVerticalLine);
        }
    }
}
