/*
 * PROGRAMMER : Samuel Lewis
 * PROJECT: PROJ3100 Assignment #1
 */
using System.Drawing;
using System.Globalization;

namespace pong
{
    /// <summary>
    /// REpresents the score
    /// </summary>
    class Score:Entity
    {
        /// <summary>
        /// Gets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public int Points { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Score" /> class.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="width">The width.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Score(Brush brush, int width, int x, int y)
            : base(brush, width, width, x, y)
        {
        }

        /// <summary>
        /// Draws the entity
        /// </summary>
        /// <param name="graphic">The graphic.</param>
        public override void Draw(Graphics graphic)
        {
            if (graphic != null)
            {
                graphic.DrawString(Points.ToString(CultureInfo.InvariantCulture), new Font("courier", 40.0f), Brush, x, Y);
            }
        }

        /// <summary>
        /// Have the entity move itself
        /// </summary>
        public override void Move()
        {
        }

        /// <summary>
        /// Adds the point.
        /// </summary>
        /// <param name="points">The points.</param>
        public void AddPoint(int points)
        {
            Points += points;
        }
    }
}
