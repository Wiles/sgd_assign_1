using System;
using System.Drawing;

namespace pong
{
    class Ball:Entity
    {
        private double _radians;
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
                if (degrees < 95 && degrees > 85 )
                {
                    if (degrees < 90)
                    {
                        degrees = 85;
                    }
                    else
                    {
                        {
                            degrees = 95;
                        }
                    }
                }
                else if (degrees > -95 && degrees < -85)
                {
                    if (degrees > -90)
                    {
                        degrees = -85;
                    }
                    else
                    {
                        {
                            degrees = -95;
                        }
                    }
                }

                _radians = degrees * (Math.PI / 180.0);
            }
        }
        public double Speed { get; set; }
        private double _dX;
        private double _dY;

        public Ball(Brush brush, int width, int x, int y, int speed)
            : base(brush, width, width, x, y)
        {
            _dX = x;
            _dY = y;
            Speed = speed;
        }

        public int X
        {
            get { return (int) _dX;}
            set { 
                _dX = value;
                x = value;
            }
        }

        public override void Draw(Graphics graphic)
        {
            graphic.FillEllipse(Brush, x, Y, Width, Width);
        }

        public override void Move()
        {
            _dX += Speed * 2.0 * Math.Cos(_radians);
            _dY += Speed * 2.0 * Math.Sin(_radians);

            x = (int) _dX;
            Y = (int) _dY;
        }

    }
}
