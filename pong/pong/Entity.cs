using System.Drawing;

namespace pong
{
    abstract class Entity
    {
        public Brush Brush { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int x { get; protected set; }
        public int Y { get; protected set; }
        
        protected Entity(Brush brush, int width, int height, int x, int y)
        {
            Brush = brush;
            Width = width;
            Height = height;
            this.x = x;
            Y = y;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(x, Y, Width, Height);
        }

        abstract public void Draw(Graphics graphic);
        public abstract void Move();
    }
}
