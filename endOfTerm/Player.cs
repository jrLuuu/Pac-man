using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using WMPLib;

namespace endOfTerm
{
    class Player
    {
        WMPLib.WindowsMediaPlayer yum = new WMPLib.WindowsMediaPlayer();
        System.Media.SoundPlayer die = new System.Media.SoundPlayer();

        public Vector2 position;
        public Vector2 velocity;
        public Rectangle upperRight;
        public Rectangle bottomRight;
        public Rectangle bottomLeft;

        public Player()
        {
            position = new Vector2(0, 0);
            velocity = new Vector2(0, 0);
        }

        public void Initialize(Context context)
        {
            yum.URL = @"Yum.wav";
            die.SoundLocation = @"die.wav";
            position = context.basePosition;
            velocity = context.currentVelocity;
            upperRight = new Rectangle(context.basePosition.x + context.form.pacman.Width - 1, context.basePosition.y + 1, 1, 1);
            bottomRight = new Rectangle(context.basePosition.x + context.form.pacman.Width - 1, context.basePosition.y + context.form.pacman.Height - 1, 1, 1);
            bottomLeft = new Rectangle(context.basePosition.x, context.basePosition.y + context.form.pacman.Height, 1, 1);
        }

        public void Update(Context context)
        {
            foreach (var monster in context.monsters)
            {
                if (context.form.pacman.Bounds.IntersectsWith(monster.Bounds))
                {
                    context.life -= 1;

                    foreach (var life in context.lifes.ToList())
                    {
                        context.form.Controls.Remove(life);
                        context.lifes.Remove(life);
                        break;
                    }

                    context.player.position = context.basePosition;
                    context.player.velocity = context.currentVelocity;
                    context.player.upperRight = new Rectangle(context.basePosition.x + context.form.pacman.Width - 1, context.basePosition.y + 1, 1, 1);
                    context.player.bottomRight = new Rectangle(context.basePosition.x + context.form.pacman.Width - 1, context.basePosition.y + context.form.pacman.Height - 1, 1, 1);
                    context.player.bottomLeft = new Rectangle(context.basePosition.x, context.basePosition.y + context.form.pacman.Height, 1, 1);

                    context.ghost1.position = new Vector2(context.baseLeft.X, context.baseLeft.Y);
                    context.ghost1.velocity = new Vector2(0, 0);
                    context.ghost1.upperRight = new Rectangle(context.baseLeft.X + 1 + context.ghost1.ghost.Width - 1, context.baseLeft.Y + 1 + 1, 1, 1);
                    context.ghost1.bottomRight = new Rectangle(context.baseLeft.X - 1 + context.ghost1.ghost.Width - 1 - 1, context.baseLeft.Y + context.ghost1.ghost.Height - 1, 1, 1);
                    context.ghost1.bottomLeft = new Rectangle(context.baseLeft.X, context.baseLeft.Y + context.ghost1.ghost.Height, 1, 1);

                    context.ghost2.position = new Vector2(context.baseMiddle.X, context.baseMiddle.Y);
                    context.ghost2.velocity = new Vector2(0, 0);
                    context.ghost2.upperRight = new Rectangle(context.baseMiddle.X + 1 + context.ghost2.ghost.Width - 1, context.baseMiddle.Y + 1 + 1, 1, 1);
                    context.ghost2.bottomRight = new Rectangle(context.baseMiddle.X - 1 + context.ghost2.ghost.Width - 1 - 1, context.baseMiddle.Y + context.ghost2.ghost.Height - 1, 1, 1);
                    context.ghost2.bottomLeft = new Rectangle(context.baseMiddle.X, context.baseMiddle.Y + context.ghost2.ghost.Height, 1, 1);

                    context.ghost3.position = new Vector2(context.baseRight.X, context.baseRight.Y);
                    context.ghost3.velocity = new Vector2(0, 0);
                    context.ghost3.upperRight = new Rectangle(context.baseRight.X + 1 + context.ghost3.ghost.Width - 1, context.baseRight.Y + 1 + 1, 1, 1);
                    context.ghost3.bottomRight = new Rectangle(context.baseRight.X - 1 + context.ghost3.ghost.Width - 1 - 1, context.baseRight.Y + context.ghost3.ghost.Height - 1, 1, 1);
                    context.ghost3.bottomLeft = new Rectangle(context.baseRight.X, context.baseRight.Y + context.ghost3.ghost.Height, 1, 1);

                    context.ghost4.position = new Vector2(context.baseTop.X, context.baseTop.Y);
                    context.ghost4.velocity = new Vector2(0, 0);
                    context.ghost4.upperRight = new Rectangle(context.baseTop.X + 1 + context.ghost4.ghost.Width - 1, context.baseTop.Y + 1 + 1, 1, 1);
                    context.ghost4.bottomRight = new Rectangle(context.baseTop.X - 1 + context.ghost4.ghost.Width - 1 - 1, context.baseTop.Y + context.ghost4.ghost.Height - 1, 1, 1);
                    context.ghost4.bottomLeft = new Rectangle(context.baseTop.X, context.baseTop.Y + context.ghost4.ghost.Height, 1, 1);

                    if (context.life <= 0)
                    {
                        die.Play();
                        context.isContinue = false;
                        context.form.xoxo.Visible = true;
                        context.form.restart.Visible = false;
                        context.form.memo.Text = "Press R => restart";
                        context.form.BackColor = Color.FromArgb(255, 0, 0);
                    }
                }

                foreach (var watermelon in context.watermelon.ToList())
                {
                    if (context.form.pacman.Bounds.IntersectsWith(watermelon.Bounds))
                    {
                        context.score += 1;
                        yum.controls.play();
                        yum.settings.volume = 88;
                        context.watermelon.Remove(watermelon);
                        context.form.Controls.Remove(watermelon);
                    }
                }
            }

            velocity = new Vector2(context.currentVelocity.x * 3, context.currentVelocity.y * 3);
            bool upperLeftAble = Map.buffer[context.player.position.y + context.player.velocity.y, context.player.position.x + context.player.velocity.x] == 1 ? true : false;
            bool upperRightAble = Map.buffer[context.player.upperRight.Y + context.player.velocity.y, context.player.upperRight.X + context.player.velocity.x] == 1 ? true : false;
            bool bottomLeftAble = Map.buffer[context.player.bottomLeft.Y + context.player.velocity.y, context.player.bottomLeft.X + context.player.velocity.x] == 1 ? true : false;
            bool bottomRightAble = Map.buffer[context.player.bottomRight.Y + context.player.velocity.y, context.player.bottomRight.X + context.player.velocity.x] == 1 ? true : false;

            bool LeftTeleport = Map.buffer[context.player.position.y + context.player.velocity.y, context.player.position.x + context.player.velocity.x] == 2 ? true : false;
            bool RightTeleport = Map.buffer[context.player.upperRight.Y + context.player.velocity.y, context.player.upperRight.X + context.player.velocity.x] == 2 ? true : false;

            if (LeftTeleport)
            {
                context.player.position = new Vector2(context.form.BackgroundImage.Width - context.form.pacman.Width - 20, context.player.position.y);
                upperRight = new Rectangle(context.player.position.x + context.form.pacman.Width - 1, context.player.position.y + 1, 1, 1);
                bottomRight = new Rectangle(context.player.position.x + context.form.pacman.Width - 1, context.player.position.y + context.form.pacman.Height - 1, 1, 1);
                bottomLeft = new Rectangle(context.player.position.x, context.player.position.y + context.form.pacman.Height, 1, 1);
                upperLeftAble = true;
                upperRightAble = true;
                bottomLeftAble = true;
                bottomRightAble = true;
            }

            if (RightTeleport)
            {
                context.player.position = new Vector2(20, context.player.position.y);
                upperRight = new Rectangle(context.player.position.x + context.form.pacman.Width - 1, context.player.position.y + 1, 1, 1);
                bottomRight = new Rectangle(context.player.position.x + context.form.pacman.Width - 1, context.player.position.y + context.form.pacman.Height - 1, 1, 1);
                bottomLeft = new Rectangle(context.player.position.x, context.player.position.y + context.form.pacman.Height, 1, 1);
                upperLeftAble = true;
                upperRightAble = true;
                bottomLeftAble = true;
                bottomRightAble = true;
            }

            if (upperLeftAble && upperRightAble && bottomLeftAble && bottomRightAble)
            {
                context.player.position += context.player.velocity;
                context.player.upperRight.X += context.player.velocity.x;
                context.player.upperRight.Y += context.player.velocity.y;
                context.player.bottomLeft.X += context.player.velocity.x;
                context.player.bottomLeft.Y += context.player.velocity.y;
                context.player.bottomRight.X += context.player.velocity.x;
                context.player.bottomRight.Y += context.player.velocity.y;
            }

            context.form.pacman.Location = new Point(context.player.position.x, context.player.position.y);
            //Debug.WriteLine("aaaa");
        }
    }
}
