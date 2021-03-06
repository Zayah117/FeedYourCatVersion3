﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Feed_your_cat_
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        Stopwatch stopWatch = new Stopwatch();
        Stopwatch stopWatch2 = new Stopwatch();
        Cat[] CatArray = new Cat[4];
        MyMath myMath = new MyMath();
        //public string newCatName;
        int food;
        decimal score;
        decimal highscore;
        int time;
        
        public Form1()
        {
            InitializeComponent();
            CatArray[0] = new Cat() { MyLabel = hungerLabel1, MyPictureBox = pictureBox1, Normal = global::Feed_your_cat_.Properties.Resources.cat, Starved = global::Feed_your_cat_.Properties.Resources.starved, Hungry = global::Feed_your_cat_.Properties.Resources.hungry, Full = global::Feed_your_cat_.Properties.Resources.full, Boom = global::Feed_your_cat_.Properties.Resources.boom };
            CatArray[1] = new Cat() { MyLabel = hungerLabel2, MyPictureBox = pictureBox2, Normal = global::Feed_your_cat_.Properties.Resources.cat_blue, Starved = global::Feed_your_cat_.Properties.Resources.starved_blue, Hungry = global::Feed_your_cat_.Properties.Resources.hungry_blue, Full = global::Feed_your_cat_.Properties.Resources.full_blue, Boom = global::Feed_your_cat_.Properties.Resources.boom_blue };
            CatArray[2] = new Cat() { MyLabel = hungerLabel3, MyPictureBox = pictureBox3, Normal = global::Feed_your_cat_.Properties.Resources.cat_grey, Starved = global::Feed_your_cat_.Properties.Resources.starved_grey, Hungry = global::Feed_your_cat_.Properties.Resources.hungry_grey, Full = global::Feed_your_cat_.Properties.Resources.full_grey, Boom = global::Feed_your_cat_.Properties.Resources.boom_grey };
            CatArray[3] = new Cat() { MyLabel = hungerLabel4, MyPictureBox = pictureBox4, Normal = global::Feed_your_cat_.Properties.Resources.cat_brown, Starved = global::Feed_your_cat_.Properties.Resources.starved_brown, Hungry = global::Feed_your_cat_.Properties.Resources.hungry_brown, Full = global::Feed_your_cat_.Properties.Resources.full_brown, Boom = global::Feed_your_cat_.Properties.Resources.boom_brown };
            food = 25;
            score = 0M;
            highscore = 0M;
            time = 0;
            myMath.generateProblem(mathQuestionLabel);
            updateLabels();

            //naming cats
            Form2 nameForm = new Form2();
            nameForm.ShowDialog();
            nameLabel1.Text = nameForm.catName1;
            nameLabel2.Text = nameForm.catName2;
            nameLabel3.Text = nameForm.catName3;
            nameLabel4.Text = nameForm.catName4;
            mainTimer.Start();
            gameTimer.Start();
        }
        private void updateLabels()
        {
            for (int i = 0; i < CatArray.Length; i++)
            {
                CatArray[i].UpdateLabels();
            }
            foodLabel.Text = "Food\n" + food;
            scoreLabel.Text = "Score: " + score;
            //this is for the warning label
            if (stopWatch.ElapsedMilliseconds > 500)
            {
                warningLabel.Visible = false;
                stopWatch.Stop();
                stopWatch.Reset();
            }
            //this is for the math information label
            if (stopWatch2.ElapsedMilliseconds > 500)
            {
                warningLabel.Visible = false;
                stopWatch2.Stop();
                stopWatch2.Reset();
            }
            
        }
        private decimal updateScore()
        {
            decimal newScore = 0M;
            int catsAlive = 0;
            for (int i = 0; i < CatArray.Length; i++)
            {
                if (CatArray[i].Status == "hungry")
                {
                    newScore += 1M;
                    catsAlive += 1;
                }
                if (CatArray[i].Status == "normal")
                {
                    newScore += 2M;
                    catsAlive += 1;
                }
                if (CatArray[i].Status == "full")
                {
                    newScore += 4M;
                    catsAlive += 1;
                }
            }
            if (catsAlive == 0)
            {
                scorePerTickLabel.Text = "Score per tick: " + 0;
                return 0M;
            }
            if (catsAlive == 4)
            {
                scorePerTickLabel.Text = "Score per tick: " + newScore + " (x1.50)";
                return newScore * 1.5M;
            }
            if (catsAlive == 3)
            {
                scorePerTickLabel.Text = "Score per tick: " + newScore + " (x1.25)";
                return newScore * 1.25M;
            }
            if (catsAlive == 2)
            {
                scorePerTickLabel.Text = "Score per tick: " + newScore + " (x1.12)";
                return newScore * 1.12M;
            }
            else
            {
                scorePerTickLabel.Text = "Score per tick: " + newScore + " (x1.00)";
                return newScore;
            }
        }
        private bool gameOver()
        {
            if (CatArray[0].IsAlive() || CatArray[1].IsAlive() || CatArray[2].IsAlive() || CatArray[3].IsAlive())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void ShowLabel(Label label, string text, Color color)
        {
            label.Visible = true;
            label.ForeColor = color;
            label.Text = text;
        }
        private void feedCat(int i)
        {
            if (food > 0)
            {
                if (CatArray[i].IsAlive())
                {
                    CatArray[i].FeedCat(10);
                    CatArray[i].UpdateStatus();
                    food -= 1;
                    updateLabels();
                }
                else
                {
                    ShowLabel(warningLabel, "You can't feed a dead cat", Color.Red);
                    stopWatch.Start();
                }
            }
            else
            {
                ShowLabel(warningLabel, "You don't have enough food", Color.Red);
                stopWatch.Start();
            }
        }

        //this happens when you submit your math problem
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int answer = Convert.ToInt32(textBox1.Text);
                    if (myMath.checkAnswer(answer))
                    {
                        ShowLabel(mathInformationLabel, "Correct!", Color.Green);
                        stopWatch2.Start();
                        myMath.generateProblem(mathQuestionLabel);
                        textBox1.Text = "";
                        food += 3;
                    }
                    else
                    {
                        ShowLabel(mathInformationLabel, "Incorrect!", Color.Red);
                        stopWatch2.Start();
                        textBox1.Text = "";
                    }
                }
                catch (FormatException)
                {
                    ShowLabel(mathInformationLabel, "Not a number!", Color.Red);
                    stopWatch2.Start();
                    textBox1.Text = "";
                }
            }
            updateLabels();
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < CatArray.Length; i++)
            {
                if (CatArray[i].IsAlive() == true)
                {
                    CatArray[i].GetHungry(random.Next(3));
                    CatArray[i].UpdateStatus();
                }
                else
                {
                    if (CatArray[i].DeclaredDead == false)
                    {
                        if (CatArray[i].Status == "starved")
                        {
                            ShowLabel(warningLabel, "Your cat has starved to death!", Color.Red);
                            stopWatch.Start();
                            CatArray[i].DeclaredDead = true;
                        }
                        if (CatArray[i].Status == "boom")
                        {
                            ShowLabel(warningLabel, "Your cat has exploded!", Color.Red);
                            stopWatch.Start();
                            CatArray[i].DeclaredDead = true;
                        }
                    }
                }
            }
            score += updateScore();
            updateLabels();
            if (gameOver())
            {
                mainTimer.Stop();
                gameTimer.Stop();
                if (score > highscore)
                {
                    highscore = score;
                    highscoreLabel.Text = "Highscore: " + score;
                }
                MessageBox.Show("Score: " + score, "Game Over");
            }
        }

        private void feedButton1_Click(object sender, EventArgs e)
        {
            feedCat(0);
        }

        private void feedButton2_Click(object sender, EventArgs e)
        {
            feedCat(1);
        }

        private void feedButton3_Click(object sender, EventArgs e)
        {
            feedCat(2);
        }

        private void feedButton4_Click(object sender, EventArgs e)
        {
            feedCat(3);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (mainTimer.Interval > 50)
            {
                mainTimer.Interval -= 1;
                progressBar1.Value = 1050 - mainTimer.Interval;
            }
            time += 1;
            timeLabel.Text = "Time: " + time;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            CatArray[0] = new Cat() { MyLabel = hungerLabel1, MyPictureBox = pictureBox1, Normal = global::Feed_your_cat_.Properties.Resources.cat, Starved = global::Feed_your_cat_.Properties.Resources.starved, Hungry = global::Feed_your_cat_.Properties.Resources.hungry, Full = global::Feed_your_cat_.Properties.Resources.full, Boom = global::Feed_your_cat_.Properties.Resources.boom };
            CatArray[1] = new Cat() { MyLabel = hungerLabel2, MyPictureBox = pictureBox2, Normal = global::Feed_your_cat_.Properties.Resources.cat_blue, Starved = global::Feed_your_cat_.Properties.Resources.starved_blue, Hungry = global::Feed_your_cat_.Properties.Resources.hungry_blue, Full = global::Feed_your_cat_.Properties.Resources.full_blue, Boom = global::Feed_your_cat_.Properties.Resources.boom_blue };
            CatArray[2] = new Cat() { MyLabel = hungerLabel3, MyPictureBox = pictureBox3, Normal = global::Feed_your_cat_.Properties.Resources.cat_grey, Starved = global::Feed_your_cat_.Properties.Resources.starved_grey, Hungry = global::Feed_your_cat_.Properties.Resources.hungry_grey, Full = global::Feed_your_cat_.Properties.Resources.full_grey, Boom = global::Feed_your_cat_.Properties.Resources.boom_grey };
            CatArray[3] = new Cat() { MyLabel = hungerLabel4, MyPictureBox = pictureBox4, Normal = global::Feed_your_cat_.Properties.Resources.cat_brown, Starved = global::Feed_your_cat_.Properties.Resources.starved_brown, Hungry = global::Feed_your_cat_.Properties.Resources.hungry_brown, Full = global::Feed_your_cat_.Properties.Resources.full_brown, Boom = global::Feed_your_cat_.Properties.Resources.boom_brown };
            food = 25;
            score = 0M;
            time = 0;
            mainTimer.Interval = 1000;
            myMath.generateProblem(mathQuestionLabel);
            updateLabels();
            mainTimer.Start();
            gameTimer.Start();
        }
    }
}
