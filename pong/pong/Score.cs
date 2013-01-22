using System.Drawing;
using System.Globalization;

namespace pong
{
    class Score:Entity
    {
        public int Points { get; private set; }

        public Score(Brush brush, int width, int x, int y)
            : base(brush, width, width, x, y)
        {
        }

        public override void Draw(Graphics graphic)
        {
            if (graphic != null)
            {
                graphic.DrawString(Points.ToString(CultureInfo.InvariantCulture), new Font("courier", 40.0f), Brush, x, Y);
            }
        }

        public override void Move()
        {
        }

        public void AddPoint(int points)
        {
            Points += points;
        }
    }
}
