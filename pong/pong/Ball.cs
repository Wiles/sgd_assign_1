/*
 * PROGRAMMER : Samuel Lewis
 * PROJECT: PROJ3100 Assignment #1
 */

using System;
using System.Drawing;

namespace pong
{
    /// <summary>
    /// Represents a ball
    /// </summary>
    class Ball:Entity
    {
        /// <summary>
        /// The direction of travel of the ball
        /// </summary>
        private double _radians;

        /// <summary>
        /// Gets or sets the angle of travel for the ball
        /// </summary>
        /// <value>
        /// The angle.
        /// </value>
        public double Angle {
            get { return _radians * 180.0 / Math.PI; }
            set
            {
                double degrees = value;
                while (degrees < -180)
                {
                    degrees += 360;
                }
                while (degrees > 180)
                {
                    degrees -= 360;
                }
                if (degrees < 105 && degrees > 75 )
                {
                    if (degrees < 90)
                    {
                        degrees = 75;
                    }
                    else
                    {
                        {
                            degrees = 105;
                        }
                    }
                }
                else if (degrees > -105 && degrees < -75)
                {
                    if (degrees > -90)
                    {
                        degrees = -75;
                    }
                    else
                    {
                        {
                            degrees = -105;
                        }
                    }
                }

                _radians = degrees * (Math.PI / 180.0);
            }
        }
        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public double Speed { get; set; }
        /// <summary>
        /// The current x location of the ball
        /// </summary>
        private double _dX;
        /// <summary>
        /// The current y directino of the ball
        /// </summary>
        private double _dY;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ball" /> class.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="width">The width.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="speed">The speed.</param>
        public Ball(Brush brush, int width, int x, int y, int speed)
            : base(brush, width, width, x, y)
        {
            _dX = x;
            _dY = y;
            Speed = speed;
        }

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        /// <value>
        /// The X.
        /// </value>
        public int X
        {
            get { return (int) _dX;}
            set { 
                _dX = value;
                x = value;
            }
        }

        /// <summary>
        /// Draws the specified graphic.
        /// </summary>
        /// <param name="graphic">The graphic.</param>
        public override void Draw(Graphics graphic)
        {
            graphic.FillEllipse(Brush, x, Y, Width, Width);
        }

        /// <summary>
        /// Moves this instance.
        /// </summary>
        public override void Move()
        {
            _dX += Speed * 2.0 * Math.Cos(_radians);
            _dY += Speed * 2.0 * Math.Sin(_radians);

            x = (int) _dX;
            Y = (int) _dY;
        }

    }
}
