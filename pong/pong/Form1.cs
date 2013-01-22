using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace pong
{
    public partial class Pong : Form
    {
        private const int BallWidth = 10;
        private const int PaddleWidth = 15;
        private const int PaddleHeight = 200;

        private readonly Paddle _firstPaddle;
        private readonly Paddle _secondPaddle;

        private Score _firstScore;
        private Score _secondScore;

        private Ball _ball;

        private readonly List<Entity> _gameEntities = new List<Entity>();

        private readonly int _width;
        private readonly int _height;

        private bool _paused;
        private bool _gameOver = true;

        private String _gameOverMessage = "Start Game";

        private readonly Random _rand = new Random();

        private int speed = 1;

        public Pong()
        {
            InitializeComponent();
            _ball = new Ball(new SolidBrush(Color.White), BallWidth, centre(_width, BallWidth),
                             centre(_height, BallWidth), speed);
            _width = pictureBox1.ClientRectangle.Width;
            _height = pictureBox1.ClientRectangle.Height;

            draw_timer.Interval = 1;
            draw_timer.Start();
            game_timer.Interval = 10;
            game_timer.Start();
            _firstPaddle = new Paddle(new SolidBrush(Color.White), PaddleWidth, PaddleHeight, PaddleWidth, _height / 2 - PaddleHeight / 2, _height - PaddleHeight);
            _gameEntities.Add(_firstPaddle);

            _secondPaddle = new Paddle(new SolidBrush(Color.White), PaddleWidth, PaddleHeight, _width - PaddleWidth * 2, _height / 2 - PaddleHeight / 2, _height - PaddleHeight);
            _gameEntities.Add(_secondPaddle);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            pictureBox1.Invalidate();

        }

        private void Pong_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.Q):
                    _firstPaddle.Up(true);
                    break;
                case (Keys.W):
                    _firstPaddle.Down(true);
                    break;
                case (Keys.O):
                    _secondPaddle.Up(true);
                    break;
                case (Keys.P):
                    _secondPaddle.Down(true);
                    break;
                case (Keys.Tab):
                    _paused = !_paused;
                    break;
            }
        }

        private void Pong_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.Q):
                    _firstPaddle.Up(false);
                    break;
                case (Keys.W):
                    _firstPaddle.Down(false);
                    break;
                case (Keys.O):
                    _secondPaddle.Up(false);
                    break;
                case (Keys.P):
                    _secondPaddle.Down(false);
                    break;
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _gameEntities.Remove(_ball);
            _gameEntities.Remove(_firstScore);
            _gameEntities.Remove(_secondScore);

            _ball = new Ball(new SolidBrush(Color.White), BallWidth, centre(_width, BallWidth), centre(_height, BallWidth), speed)
                {
                    Angle = _rand.Next(-180, 180)
                };
            _gameEntities.Add(_ball);

            _firstScore = new Score(new SolidBrush(Color.White), 10, 20, _height - 65);
            _gameEntities.Add(_firstScore);

            _secondScore = new Score(new SolidBrush(Color.White), 10, _width - 70, _height - 65);
            _gameEntities.Add(_secondScore);

            _gameOver = false;
            _paused = false;
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _paused = !_paused;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var g in _gameEntities)
            {
                g.Draw(e.Graphics);
            }

            if (_gameOver)
            {
                DrawText(e.Graphics, _gameOverMessage);
            }
            else if (_paused)
            {
                DrawText(e.Graphics, "Paused");

            }
        }

        private int centre(int width, int length)
        {
            if (width > length)
            {
                return (int)(width/2.0 - length/2.0);
            }
            return (int)(length/2.0 - width/2.0);
        }

        private void DrawText(Graphics graphics, string text)
        {
            var font = new Font("courier", 20.0f);
            float textWidth = graphics.MeasureString(text, Font).Width;
            float textHeight = graphics.MeasureString(text, Font).Height;
            graphics.DrawString(text, font, new SolidBrush(Color.White), centre(_width, (int)textWidth), centre(_height, (int)textHeight));
        }

        private void ts_random_Click(object sender, EventArgs e)
        {
            ts_slow.Checked = false;
            ts_medium.Checked = false;
            ts_fast.Checked = false;
        }

        private void ts_slow_Click(object sender, EventArgs e)
        {
            speed = 1;
            _ball.Speed = 1;
            ts_slow.Checked = false;
            ts_medium.Checked = false;
            ts_fast.Checked = false;
        }

        private void ts_medium_Click(object sender, EventArgs e)
        {
            speed = 2;
            _ball.Speed = 2;
            ts_random.Checked = false;
            ts_slow.Checked = false;
            ts_fast.Checked = false;
        }

        private void ts_fast_Click(object sender, EventArgs e)
        {
            speed = 3;
            _ball.Speed = 3;
            ts_random.Checked = false;
            ts_slow.Checked = false;
            ts_medium.Checked = false;

        }

        private void game_timer_Tick(object sender, EventArgs e)
        {
            if (!_paused && !_gameOver)
            {
                foreach (var g in _gameEntities)
                {
                    g.Move();
                }

                if (_secondPaddle.GetBounds().IntersectsWith(_ball.GetBounds()))
                {
                    var diff = ((_secondPaddle.Y + PaddleHeight / 2.0) - (_ball.Y + BallWidth / 2.0)) / (PaddleHeight / 2.0);

                    _ball.Angle = 2 * 90 - _ball.Angle + (45 * diff);
                    if (ts_random.Checked)
                    {
                        _ball.Speed = _rand.Next(1, 4);
                    }
                    _ball.X = _secondPaddle.x - BallWidth - 1;
                }
                if (_firstPaddle.GetBounds().IntersectsWith(_ball.GetBounds()))
                {
                    var diff = ((_firstPaddle.Y + PaddleHeight / 2.0) - (_ball.Y + BallWidth / 2.0)) / (PaddleHeight / 2.0);
                    _ball.Angle = 2 * 90 - _ball.Angle + (45 * -diff);
                    if (ts_random.Checked)
                    {
                        _ball.Speed = _rand.Next(1, 4);
                    }
                    _ball.X = _firstPaddle.x + PaddleWidth + 1;
                }


                if (_ball.x < 0 - BallWidth)
                {
                    _gameEntities.Remove(_ball);
                    _ball = new Ball(new SolidBrush(Color.White), BallWidth, _width/2 - BallWidth/2,
                                     _height/2 - BallWidth/2, speed) {Angle = _rand.Next(-90, 90) - 180};
                    _gameEntities.Add(_ball);
                    _secondScore.AddPoint(1);

                    if (_secondScore.Points > 9)
                    {
                        _gameOver = true;
                        _gameOverMessage = "Player 2 wins";
                        _gameEntities.Remove(_ball);
                    }
                }

                if (_ball.x > _width)
                {
                    _gameEntities.Remove(_ball);
                    
                    _ball = new Ball(new SolidBrush(Color.White), BallWidth, _width / 2 - BallWidth / 2,
                                    _height / 2 - BallWidth / 2, speed) {Angle = _rand.Next(-90, 90)};
                    _gameEntities.Add(_ball);
                    _firstScore.AddPoint(1);
                    if (_firstScore.Points > 9)
                    {
                        _gameOver = true;
                        _gameOverMessage = "Player 1 wins";
                        _gameEntities.Remove(_ball);
                    }
                }

                if (_ball.Y <= 0 || _ball.Y + BallWidth >= _height)
                {
                    _ball.Angle = -_ball.Angle;
                }
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().Show();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Help().Show();
        }
    }
}
