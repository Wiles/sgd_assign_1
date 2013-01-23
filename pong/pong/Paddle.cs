/*
 * PROGRAMMER : Samuel Lewis
 * PROJECT: PROJ3100 Assignment #1
 */
using System.Drawing;

namespace pong
{
    /// <summary>
    /// Represents a paddle entity
    /// </summary>
    class Paddle : Entity
    {
        /// <summary>
        /// The _move up
        /// </summary>
        private bool _moveUp;
        /// <summary>
        /// The _move down
        /// </summary>
        private bool _moveDown;
        /// <summary>
        /// The _max Y
        /// </summary>
        private readonly int _maxY;

        /// <summary>
        /// Initializes a new instance of the <see cref="Paddle" /> class.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="maxY">The max Y.</param>
        public Paddle(Brush brush, int width, int height, int x, int y, int maxY):base(brush, width, height, x, y)
        {
            _maxY = maxY;

        }

        /// <summary>
        /// Draws the entity
        /// </summary>
        /// <param name="graphic">The graphic.</param>
        public override void Draw(Graphics graphic)
        {
            graphic.FillRectangle(Brush, x, Y, Width, Height);
        }

        /// <summary>
        /// Ups the specified b.
        /// </summary>
        /// <param name="b">if set to <c>true</c> [b].</param>
        public void Up(bool b)
        {
            _moveUp = b;
        }

        /// <summary>
        /// Downs the specified b.
        /// </summary>
        /// <param name="b">if set to <c>true</c> [b].</param>
        public void Down(bool b)
        {
            _moveDown = b;
        }

        /// <summary>
        /// Have the entity move itself
        /// </summary>
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
