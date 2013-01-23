/*
 * PROGRAMMER : Samuel Lewis
 * PROJECT: PROJ3100 Assignment #1
 */
using System.Drawing;

namespace pong
{

    /// <summary>
    /// Base class for a game entity
    /// </summary>
    abstract class Entity
    {
        /// <summary>
        /// Gets the brush.
        /// </summary>
        /// <value>
        /// The brush.
        /// </value>
        public Brush Brush { get; private set; }
        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; private set; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; private set; }
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public int x { get; protected set; }
        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        /// <value>
        /// The Y.
        /// </value>
        public int Y { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity" /> class.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        protected Entity(Brush brush, int width, int height, int x, int y)
        {
            Brush = brush;
            Width = width;
            Height = height;
            this.x = x;
            Y = y;
        }

        /// <summary>
        /// Gets the bounds of the entity
        /// </summary>
        /// <returns></returns>
        public Rectangle GetBounds()
        {
            return new Rectangle(x, Y, Width, Height);
        }

        /// <summary>
        /// Draws the entity
        /// </summary>
        /// <param name="graphic">The graphic.</param>
        abstract public void Draw(Graphics graphic);
        /// <summary>
        /// Have the entity move itself
        /// </summary>
        public abstract void Move();
    }
}
