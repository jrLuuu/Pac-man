using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace endOfTerm
{
    class Ghost2 : Monster
    {
        public Ghost2()
        {
            ghost = new PictureBox();
        }

        public override void Initialize(Context context)
        {
            position = new Vector2(context.baseMiddle.X, context.baseMiddle.Y);
            velocity = new Vector2(0, 0);
            upperRight = new Rectangle(context.baseMiddle.X + 1 + context.ghost2.ghost.Width - 1, context.baseMiddle.Y + 1 + 1, 1, 1);
            bottomRight = new Rectangle(context.baseMiddle.X - 1 + context.ghost2.ghost.Width - 1 - 1, context.baseMiddle.Y + context.ghost2.ghost.Height - 1, 1, 1);
            bottomLeft = new Rectangle(context.baseMiddle.X, context.baseMiddle.Y + context.ghost2.ghost.Height, 1, 1);
            ghost.Size = new Size(25, 25);
            ghost.BackColor = Color.Transparent;
            ghost.Image = Properties.Resources.Untitled_1111f;
            ghost.Location = new Point(context.ghost2.position.x, context.ghost2.position.y);
            ghost.SizeMode = PictureBoxSizeMode.StretchImage;
            context.form.Controls.Add(ghost);
            context.monsters.Add(ghost);
            var task = Update(context);
        }

        async static Task Update(Context context)
        {
            while (true)
            {
                context.ghost2.velocity = context.player.position - context.ghost2.position;
                var x = context.ghost2.velocity.x == 0 ? 0 : context.ghost2.velocity.x / Math.Abs(context.ghost2.velocity.x);
                var y = context.ghost2.velocity.y == 0 ? 0 : context.ghost2.velocity.y / Math.Abs(context.ghost2.velocity.y);
                context.ghost2.velocity = Math.Abs(context.ghost2.velocity.x) > Math.Abs(context.ghost2.velocity.y) ? new Vector2(x, 0) : new Vector2(0, y);

                //bool upperLeftAble = Map.buffer[context.ghost2.position.y + context.ghost2.velocity.y, context.ghost2.position.x + context.ghost2.velocity.x] == 1 ?
                //    true : false;
                //bool upperRightAble = Map.buffer[context.ghost2.upperRight.Y + context.ghost2.velocity.y, context.ghost2.upperRight.X + context.ghost2.velocity.x] == 1 ?
                //    true : false;
                //bool bottomLeftAble = Map.buffer[context.ghost2.bottomLeft.Y + context.ghost2.velocity.y, context.ghost2.bottomLeft.X + context.ghost2.velocity.x] == 1 ?
                //    true : false;
                //bool bottomRightAble = Map.buffer[context.ghost2.bottomRight.Y + context.ghost2.velocity.y, context.ghost2.bottomRight.X + context.ghost2.velocity.x] == 1 ?
                //    true : false;

                foreach (var monster in context.monsters.ToList())
                {
                    if (monster != context.ghost2.ghost && monster.Bounds.IntersectsWith(context.ghost2.ghost.Bounds))
                    {
                        context.ghost2.position = new Vector2(context.baseMiddle.X, context.baseMiddle.Y);
                        //context.ghost2.velocity = new Vector2(0, 0);
                        context.ghost2.upperRight = new Rectangle(context.baseMiddle.X + 1 + context.ghost2.ghost.Width - 1, context.baseMiddle.Y + 1 + 1, 1, 1);
                        context.ghost2.bottomRight = new Rectangle(context.baseMiddle.X - 1 + context.ghost2.ghost.Width - 1 - 1, context.baseMiddle.Y + context.ghost2.ghost.Height - 1, 1, 1);
                        context.ghost2.bottomLeft = new Rectangle(context.baseMiddle.X, context.baseMiddle.Y + context.ghost2.ghost.Height, 1, 1);
                    }
                }

                context.ghost2.position += context.ghost2.velocity;
                context.ghost2.upperRight.X += context.ghost2.velocity.x;
                context.ghost2.upperRight.Y += context.ghost2.velocity.y;
                context.ghost2.bottomLeft.X += context.ghost2.velocity.x;
                context.ghost2.bottomLeft.Y += context.ghost2.velocity.y;
                context.ghost2.bottomRight.X += context.ghost2.velocity.x;
                context.ghost2.bottomRight.Y += context.ghost2.velocity.y;
                context.ghost2.ghost.Location = new Point(context.ghost2.position.x, context.ghost2.position.y);

                await Task.Delay(20);
            }
        }
    }
}
