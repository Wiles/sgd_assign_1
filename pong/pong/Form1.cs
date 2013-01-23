/*
 * PROGRAMMER : Samuel Lewis
 * PROJECT: PROJ3100 Assignment #1
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace pong
{
    /// <summary>
    /// The main form for running pong
    /// </summary>
    public partial class Pong : Form
    {
        /// <summary>
        /// The ball width
        /// </summary>
        private const int BallWidth = 10;
        /// <summary>
        /// The paddle width
        /// </summary>
        private const int PaddleWidth = 15;
        /// <summary>
        /// The paddle height
        /// </summary>
        private const int PaddleHeight = 200;

        /// <summary>
        /// The _first paddle
        /// </summary>
        private readonly Paddle _firstPaddle;
        /// <summary>
        /// The _second paddle
        /// </summary>
        private readonly Paddle _secondPaddle;

        /// <summary>
        /// The _first score
        /// </summary>
        private Score _firstScore;
        /// <summary>
        /// The _second score
        /// </summary>
        private Score _secondScore;

        /// <summary>
        /// The _ball
        /// </summary>
        private Ball _ball;

        /// <summary>
        /// The _game entities
        /// </summary>
        private readonly List<Entity> _gameEntities = new List<Entity>();

        /// <summary>
        /// The _width
        /// </summary>
        private readonly int _width;
        /// <summary>
        /// The _height
        /// </summary>
        private readonly int _height;

        /// <summary>
        /// The _paused
        /// </summary>
        private bool _paused;
        /// <summary>
        /// The _game over
        /// </summary>
        private bool _gameOver = true;

        /// <summary>
        /// The _game over message
        /// </summary>
        private String _gameOverMessage = "Start Game";

        /// <summary>
        /// The _rand
        /// </summary>
        private readonly Random _rand = new Random();

        /// <summary>
        /// The _speed
        /// </summary>
        private int _speed = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pong" /> class.
        /// </summary>
        public Pong()
        {
            InitializeComponent();
            _ball = new Ball(new SolidBrush(Color.White), BallWidth, centre(_width, BallWidth),
                             centre(_height, BallWidth), _speed);
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

        /// <summary>
        /// Handles the Tick event of the timer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {

            pictureBox1.Invalidate();

        }

        /// <summary>
        /// Handles the KeyDown event of the Pong control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the KeyUp event of the Pong control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Click event of the startToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _gameEntities.Remove(_ball);
            _gameEntities.Remove(_firstScore);
            _gameEntities.Remove(_secondScore);

            _ball = new Ball(new SolidBrush(Color.White), BallWidth, centre(_width, BallWidth), centre(_height, BallWidth), _speed)
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

        /// <summary>
        /// Handles the Click event of the pauseToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _paused = !_paused;
        }

        /// <summary>
        /// Handles the Paint event of the pictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs" /> instance containing the event data.</param>
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

        /// <summary>
        /// Centres the specified width.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        private int centre(int width, int length)
        {
            if (width > length)
            {
                return (int)(width/2.0 - length/2.0);
            }
            return (int)(length/2.0 - width/2.0);
        }

        /// <summary>
        /// Draws the text.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="text">The text.</param>
        private void DrawText(Graphics graphics, string text)
        {
            var font = new Font("courier", 20.0f);
            float textWidth = graphics.MeasureString(text, Font).Width;
            float textHeight = graphics.MeasureString(text, Font).Height;
            graphics.DrawString(text, font, new SolidBrush(Color.White), centre(_width, (int)textWidth), centre(_height, (int)textHeight));
        }

        /// <summary>
        /// Handles the Click event of the ts_random control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ts_random_Click(object sender, EventArgs e)
        {
            ts_slow.Checked = false;
            ts_medium.Checked = false;
            ts_fast.Checked = false;
        }

        /// <summary>
        /// Handles the Click event of the ts_slow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ts_slow_Click(object sender, EventArgs e)
        {
            _speed = 1;
            _ball.Speed = 1;
            ts_slow.Checked = false;
            ts_medium.Checked = false;
            ts_fast.Checked = false;
        }

        /// <summary>
        /// Handles the Click event of the ts_medium control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ts_medium_Click(object sender, EventArgs e)
        {
            _speed = 2;
            _ball.Speed = 2;
            ts_random.Checked = false;
            ts_slow.Checked = false;
            ts_fast.Checked = false;
        }

        /// <summary>
        /// Handles the Click event of the ts_fast control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ts_fast_Click(object sender, EventArgs e)
        {
            _speed = 3;
            _ball.Speed = 3;
            ts_random.Checked = false;
            ts_slow.Checked = false;
            ts_medium.Checked = false;

        }

        /// <summary>
        /// Handles the Tick event of the game_timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
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

                    if (_ball.Angle < 90 && _ball.Angle > 0)
                    {
                        _ball.Angle = 75;
                    }
                    else if (_ball.Angle < 0 && _ball.Angle > -90)
                    {
                        _ball.Angle = -75;
                    }

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

                    if (_ball.Angle > 90 && _ball.Angle < 180)
                    {
                        _ball.Angle = 75;
                    }
                    else if (_ball.Angle < -90 && _ball.Angle > -180)
                    {
                        _ball.Angle = -75;
                    }

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
                                     _height/2 - BallWidth/2, _speed) {Angle = _rand.Next(-90, 90) - 180};
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
                                    _height / 2 - BallWidth / 2, _speed) {Angle = _rand.Next(-90, 90)};
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

        /// <summary>
        /// Handles the Click event of the quitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the Click event of the aboutToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().Show();
        }

        /// <summary>
        /// Handles the Click event of the helpToolStripMenuItem1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Help().Show();
        }
    }
}
