using System.Drawing;

namespace pong
{
    class Paddle : Entity
    {
        private bool _moveUp;
        private bool _moveDown;
        private readonly int _maxY;

        public Paddle(Brush brush, int width, int height, int x, int y, int maxY):base(brush, width, height, x, y)
        {
            _maxY = maxY;

        }

        public override void Draw(Graphics graphic)
        {
            graphic.FillRectangle(Brush, x, Y, Width, Height);
        }

        public void Up(bool b)
        {
            _moveUp = b;
        }

        public void Down(bool b)
        {
            _moveDown = b;
        }

        public override void Move()
        {
            if (_moveUp)
            {
                Y -= 4;
                if (Y < 0)
                {
                    Y = 0;
                }
            }
            if (_moveDown)
            {
                Y += 4;
                if (Y > _maxY)
                {
                    Y = _maxY;
                }
            }

        }
    }
}
