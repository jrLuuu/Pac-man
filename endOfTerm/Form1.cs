using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace endOfTerm
{
    public partial class Form1 : Form
    {
        WMPLib.WindowsMediaPlayer gameover = new WMPLib.WindowsMediaPlayer();

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            this.Focus();
            //restart.Focused = false;
            //end.Focused = false;
            context.form = this;
            velocity = new Vector2(1, 0);
            context.Initialize();
            task = Update(context);
            context.lifes.Add(pac1);
            context.baseLifes.Add(pac1);
            context.lifes.Add(pac2);
            context.baseLifes.Add(pac2);
            context.lifes.Add(pac3);
            context.baseLifes.Add(pac3);
            pacman.BringToFront();
            xoxo.Visible = false;

            foreach (var monster in context.monsters)
            {
                monster.BringToFront();
            }

            foreach (Control control in Controls)
            {
                if (control is PictureBox && (string)control.Tag == "water")
                {
                    context.watermelon.Add((PictureBox)control);
                    context.removedWatermelon.Add((PictureBox)control);
                }
            }
        }

        Context context = new Context();
        public Vector2 velocity;
        public Task task;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W && context.isContinue)
            {
                velocity = new Vector2(0, -1);
                pacman.Image = Properties.Resources._1111;
            }
            if (e.KeyCode == Keys.S && context.isContinue)
            {
                velocity = new Vector2(0, 1);
                pacman.Image = Properties.Resources._2222;
            }
            if (e.KeyCode == Keys.A && context.isContinue)
            {
                velocity = new Vector2(-1, 0);
                pacman.Image = Properties.Resources.Untitled_1;
            }
            if (e.KeyCode == Keys.D && context.isContinue)
            {
                velocity = new Vector2(1, 0);
                pacman.Image = Properties.Resources._3333;
            }
            if (e.KeyCode == Keys.R && !context.isContinue)
            {
                resetAction();
                task = Update(context);
                context.isContinue = true;
                context.form.restart.Visible = true;
            }
        }

        async static Task Update(Context context)
        {
            while (context.isContinue)
            {
                context.form.showScore.Text = context.score.ToString();
                context.Update();
                context.player.Update(context);
                GC.Collect();
                await Task.Delay(30);
            }
        }

        private void restart_Click(object sender, EventArgs e)
        {
            resetAction();
        }

        private void end_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void resetAction()
        {
            xoxo.Visible = false;
            memo.Text = "Press W A S D \n => move the pac-man";
            context.form.BackColor = Color.FromArgb(211, 211, 211);

            foreach (var life in context.lifes.ToList())
            {
                context.form.Controls.Remove(life);
            }
            foreach (var life in context.baseLifes.ToList())
            {
                context.form.Controls.Add(life);
                context.lifes.Add(life);
            }
            foreach (var water in context.watermelon.ToList())
            {
                context.form.Controls.Remove(water);
                context.watermelon.Remove(water);
            }
            foreach (var water in context.removedWatermelon)
            {
                context.form.Controls.Add(water);
                context.watermelon.Add(water);
            }

            context.ghost1.position = new Vector2(context.baseLeft.X, context.baseLeft.Y);
            context.ghost2.position = new Vector2(context.baseMiddle.X, context.baseMiddle.Y);
            context.ghost3.position = new Vector2(context.baseRight.X, context.baseRight.Y);
            context.ghost4.position = new Vector2(context.baseTop.X, context.baseTop.Y);
            context.player.position = context.basePosition;
            context.player.upperRight = new Rectangle(context.basePosition.x + context.form.pacman.Width - 1, context.basePosition.y + 1, 1, 1);
            context.player.bottomRight = new Rectangle(context.basePosition.x + context.form.pacman.Width - 1, context.basePosition.y + context.form.pacman.Height - 1, 1, 1);
            context.player.bottomLeft = new Rectangle(context.basePosition.x, context.basePosition.y + context.form.pacman.Height, 1, 1);

            context.score = 0;
            context.life = 3;
            context.isContinue = true;
            foreach (var water in context.removedWatermelon)
            {
                context.form.Controls.Add(water);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gameover.URL = "BabyRelax.mp3";
            gameover.controls.play();
            gameover.settings.volume = 88;
            gameover.settings.playCount = 100;
        }
    }
}
