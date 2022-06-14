using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brick_Breaker
{
    public partial class Form1 : Form
    {
        Rectangle hero = new Rectangle(280, 450, 80, 8);
        Rectangle ball = new Rectangle(280, 300, 10, 10);
        Rectangle movingBrick = new Rectangle(0, 350, 30, 10);

        int herospeed = 15;
        int ballXspeed = 0;
        int ballYspeed = 8;
        int brickXspeed = 5;

        List<Rectangle> brickRow1 = new List<Rectangle>();
        List<Rectangle> brickRow2 = new List<Rectangle>();
        List<Rectangle> brickRow3 = new List<Rectangle>();
        List<Rectangle> brickRow4 = new List<Rectangle>();
        List<Rectangle> brickRow5 = new List<Rectangle>();
        List<Rectangle> brickRow6 = new List<Rectangle>();

        int lives = 3;
        int score = 0;
        int time = 0;

        bool leftKey;
        bool rightKey;

        string gameState = "waiting";

        SolidBrush whiteBrush = new SolidBrush(Color.Honeydew);
        SolidBrush redBrush = new SolidBrush(Color.LightCoral);
        SolidBrush orangeBrush = new SolidBrush(Color.Coral);
        SolidBrush yellowBrush = new SolidBrush(Color.Khaki);
        SolidBrush greenBrush = new SolidBrush(Color.DarkSeaGreen);
        SolidBrush blueBrush = new SolidBrush(Color.MediumTurquoise);
        SolidBrush purpleBrush = new SolidBrush(Color.Thistle);
        SolidBrush pinkBrush = new SolidBrush(Color.PaleVioletRed);
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftKey = true;
                    break;
                case Keys.Right:
                    rightKey = true;
                    break;
                case Keys.Space:

                    if (gameState == "waiting" || gameState == "over")

                    {
                        GameInitialize();
                    }
                    break;
                case Keys.Escape:

                    if (gameState == "waiting" || gameState == "over")

                    {
                        Application.Exit();
                    }
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftKey = false;
                    break;
                case Keys.Right:
                    rightKey = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball 
            ball.X += ballXspeed;
            ball.Y += ballYspeed;
            
            //move hero
            if (leftKey == true && hero.X > 0)
            {
                hero.X -= herospeed;
            }

            if (rightKey == true && hero.X < this.Width - hero.Width)
            {
                hero.X += herospeed;
            }

            // move  brick across
            movingBrick.X += brickXspeed;

            //brick intersection with side walls
            if (movingBrick.X > this.Width || movingBrick.X < 0)
            {
                brickXspeed *= -1;
            }

            //ball intersection with side walls 
            if (ball.X > this.Width || ball.X < 0)
            {
                ballXspeed *= -1;
            }

            //ball intersection with top wall
            if (ball.Y < 0)
            {
                ballYspeed = -ballYspeed;
            }

            //ball intersection with player
            if (hero.IntersectsWith(ball))
            {
                if (ballXspeed == 0)
                {
                    ballXspeed = 2;
                    // give it an x speed  
                }
                ballYspeed *= -1;
                ball.Y = hero.Y - ball.Height;
            }

            //check if player missed ball  
            if (ball.Y > this.Height)
            {
                ball.X = 280;
                ball.Y = 300;

                hero.X = 280;
                hero.Y = 450;

                lives -= 1;
                livesLabel.Text = $"x{lives}";
            }

            //check for point scored
            if (ball.IntersectsWith(movingBrick))
            {
                score += 2;
                scoreLabel.Text = $"{score}";

                movingBrick.X = -50;
                movingBrick.Y = -50;    
                ballXspeed = -1;
            }
            for (int i = 0; i < brickRow1.Count(); i++)
            {
                if (ball.IntersectsWith(brickRow1[i]))
                {
                    score++;
                    scoreLabel.Text = $"{score}";

                    brickRow1.RemoveAt(i);
                    ballXspeed *= -1;

                    //hero.Y = 450;  
                }
            }
            for (int i = 0; i < brickRow2.Count(); i++)
            {
                if (ball.IntersectsWith(brickRow2[i]))
                {
                    score++;
                    scoreLabel.Text = $"{score}";

                    brickRow2.RemoveAt(i);
                    ballXspeed *= -1;

                    //hero.Y = 450;
                }
            }
            for (int i = 0; i < brickRow3.Count(); i++)
            {
                if (ball.IntersectsWith(brickRow3[i]))
                {
                    score++;
                    scoreLabel.Text = $"{score}";

                    brickRow3.RemoveAt(i);
                    ballXspeed *= -1;

                    //hero.Y = 450;
                }
            }
            for (int i = 0; i < brickRow4.Count(); i++)
            {
                if (ball.IntersectsWith(brickRow4[i]))
                {
                    score++;
                    scoreLabel.Text = $"{score}";

                    brickRow4.RemoveAt(i);
                    ballXspeed *= -1;

                    //hero.Y = 450;
                }
            }
            for (int i = 0; i < brickRow5.Count(); i++)
            {
                if (ball.IntersectsWith(brickRow5[i]))
                {
                    score++;
                    scoreLabel.Text = $"{score}";

                    brickRow5.RemoveAt(i);
                    ballXspeed *= -1;

                    //hero.Y = 450;
                }
            }
            for (int i = 0; i < brickRow6.Count(); i++)
            {
                if (ball.IntersectsWith(brickRow6[i]))
                {
                    score++;
                    scoreLabel.Text = $"{score}";

                    brickRow6.RemoveAt(i);
                    ballXspeed *= -1;

                    //hero.Y = 450;
                }
            }

            //Determine when the game is over
            if (score == 23 || lives == 0)
            {
                gameTimer.Enabled = false;
                pictureBox1.Visible = false;
                gameState = "over";
            }
            Refresh();
        }

        public void GameInitialize()
        {
            titleLabel.Text = "";
            subtitleLabel.Text = "";

            score = 0;
            scoreLabel.Text = "0";

            gameTimer.Enabled = true;
            gameState = "running";
            
            pictureBox1.Visible = true;

            lives = 3;
            livesLabel.Text = $"x3";

            brickRow1.Clear();
            brickRow1.Add(new Rectangle(270, 270, 30, 10));

            brickRow2.Clear();
            brickRow2.Add(new Rectangle(250, 250, 30, 10));
            brickRow2.Add(new Rectangle(290, 250, 30, 10));

            brickRow3.Clear();
            brickRow3.Add(new Rectangle(230, 230, 30, 10));
            brickRow3.Add(new Rectangle(270, 230, 30, 10));
            brickRow3.Add(new Rectangle(310, 230, 30, 10));

            brickRow4.Clear();
            brickRow4.Add(new Rectangle(210, 210, 30, 10));
            brickRow4.Add(new Rectangle(250, 210, 30, 10));
            brickRow4.Add(new Rectangle(290, 210, 30, 10));
            brickRow4.Add(new Rectangle(330, 210, 30, 10));

            brickRow5.Clear();
            brickRow5.Add(new Rectangle(190, 190, 30, 10));
            brickRow5.Add(new Rectangle(230, 190, 30, 10));
            brickRow5.Add(new Rectangle(270, 190, 30, 10));
            brickRow5.Add(new Rectangle(310, 190, 30, 10));
            brickRow5.Add(new Rectangle(350, 190, 30, 10));

            brickRow6.Clear();
            brickRow6.Add(new Rectangle(170, 170, 30, 10));
            brickRow6.Add(new Rectangle(210, 170, 30, 10));
            brickRow6.Add(new Rectangle(250, 170, 30, 10));
            brickRow6.Add(new Rectangle(290, 170, 30, 10));
            brickRow6.Add(new Rectangle(330, 170, 30, 10));
            brickRow6.Add(new Rectangle(370, 170, 30, 10));

            movingBrick.X = 0;
            movingBrick.Y = 350;

            ball.X = 280;
            ball.Y = 300;

            hero.Y = this.Height - hero.Height - 150;
            hero.X = this.Width - hero.Width - 280;

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (gameState == "waiting")
            {
                titleLabel.Text = "BRICK BREAKER";
                subtitleLabel.Text = "PRESS SPACE BAR TO START OR ESC TO EXIT";
                pictureBox1.Visible = false;
                livesLabel.Text = $"";
            }
            else if (gameState == "running")
            {
                //draw hero 
                e.Graphics.FillRectangle(whiteBrush, hero);

                //draw ball
                e.Graphics.FillRectangle(whiteBrush, ball);

                //draw moving brick
                e.Graphics.FillRectangle(pinkBrush, movingBrick);

                //paint bricks
                for (int i = 0; i < brickRow1.Count(); i++)
                {
                    e.Graphics.FillRectangle(redBrush, brickRow1[i]);
                }
                for (int i = 0; i < brickRow2.Count(); i++)
                {
                    e.Graphics.FillRectangle(orangeBrush, brickRow2[i]);
                }
                for (int i = 0; i < brickRow3.Count(); i++)
                {
                    e.Graphics.FillRectangle(yellowBrush, brickRow3[i]);
                }
                for (int i = 0; i < brickRow4.Count(); i++)
                {
                    e.Graphics.FillRectangle(greenBrush, brickRow4[i]);
                }
                for (int i = 0; i < brickRow5.Count(); i++)
                {
                    e.Graphics.FillRectangle(blueBrush, brickRow5[i]);
                }
                for (int i = 0; i < brickRow6.Count(); i++)
                {
                    e.Graphics.FillRectangle(purpleBrush, brickRow6[i]);
                }
                
            }

            else if (gameState == "over")
            {
                scoreLabel.Text = "";

                titleLabel.Text = "GAME OVER";
                subtitleLabel.Text = $"\nYOUR FINAL SCORE WAS {score}";
                subtitleLabel.Text += "\nPRESS SPACE BAR TO START OR ESC TO EXIT";
                livesLabel.Text = "";
            }

        }
    }
}
