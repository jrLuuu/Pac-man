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
    class Ghost1 : Monster
    {
        public Ghost1()
        {
            ghost = new PictureBox();
        }

        public override void Initialize(Context context)
        {
            position = new Vector2(context.baseLeft.X, context.baseLeft.Y);
            velocity = new Vector2(0, 0);
            upperRight = new Rectangle(context.baseLeft.X + 1 + context.ghost1.ghost.Width - 1, context.baseLeft.Y + 1 + 1, 1, 1);
            bottomRight = new Rectangle(context.baseLeft.X - 1 + context.ghost1.ghost.Width - 1 - 1, context.baseLeft.Y + context.ghost1.ghost.Height - 1, 1, 1);
            bottomLeft = new Rectangle(context.baseLeft.X, context.baseLeft.Y + context.ghost1.ghost.Height, 1, 1);
            ghost.Size = new Size(25, 25);
            ghost.BackColor = Color.Transparent;
            ghost.Image = Properties.Resources.Untitled_1f;
            ghost.Location = new Point(context.ghost1.position.x, context.ghost1.position.y);
            ghost.SizeMode = PictureBoxSizeMode.StretchImage;
            context.form.Controls.Add(ghost);
            context.monsters.Add(ghost);
            var task = Update(context);
        }

        async static Task Update(Context context)
        {
            while (true)
            {
                context.ghost1.velocity = context.player.position - context.ghost1.position;
                var x = context.ghost1.velocity.x == 0 ? 0 : context.ghost1.velocity.x / Math.Abs(context.ghost1.velocity.x);
                var y = context.ghost1.velocity.y == 0 ? 0 : context.ghost1.velocity.y / Math.Abs(context.ghost1.velocity.y);
                context.ghost1.velocity = Math.Abs(context.ghost1.velocity.x) > Math.Abs(context.ghost1.velocity.y) ? new Vector2(x, 0) : new Vector2(0, y);

                //bool upperLeftAble = Map.buffer[context.ghost1.position.y + context.ghost1.velocity.y, context.ghost1.position.x + context.ghost1.velocity.x] == 1 ?
                //    true : false;
                //bool upperRightAble = Map.buffer[context.ghost1.upperRight.Y + context.ghost1.velocity.y, context.ghost1.upperRight.X + context.ghost1.velocity.x] == 1 ?
                //    true : false;
                //bool bottomLeftAble = Map.buffer[context.ghost1.bottomLeft.Y + context.ghost1.velocity.y, context.ghost1.bottomLeft.X + context.ghost1.velocity.x] == 1 ?
                //    true : false;
                //bool bottomRightAble = Map.buffer[context.ghost1.bottomRight.Y + context.ghost1.velocity.y, context.ghost1.bottomRight.X + context.ghost1.velocity.x] == 1 ?
                //    true : false;

                foreach (var monster in context.monsters.ToList())
                {
                    if (monster != context.ghost1.ghost && monster.Bounds.IntersectsWith(context.ghost1.ghost.Bounds))
                    {
                        context.ghost1.position = new Vector2(context.baseLeft.X, context.baseLeft.Y);
                        //context.ghost1.velocity = new Vector2(0, 0);
                        context.ghost1.upperRight = new Rectangle(context.baseLeft.X + 1 + context.ghost1.ghost.Width - 1, context.baseLeft.Y + 1 + 1, 1, 1);
                        context.ghost1.bottomRight = new Rectangle(context.baseLeft.X - 1 + context.ghost1.ghost.Width - 1 - 1, context.baseLeft.Y + context.ghost1.ghost.Height - 1, 1, 1);
                        context.ghost1.bottomLeft = new Rectangle(context.baseLeft.X, context.baseLeft.Y + context.ghost1.ghost.Height, 1, 1);
                    }
                }

                context.ghost1.position += context.ghost1.velocity;
                context.ghost1.upperRight.X += context.ghost1.velocity.x;
                context.ghost1.upperRight.Y += context.ghost1.velocity.y;
                context.ghost1.bottomLeft.X += context.ghost1.velocity.x;
                context.ghost1.bottomLeft.Y += context.ghost1.velocity.y;
                context.ghost1.bottomRight.X += context.ghost1.velocity.x;
                context.ghost1.bottomRight.Y += context.ghost1.velocity.y;
                context.ghost1.ghost.Location = new Point(context.ghost1.position.x, context.ghost1.position.y);

                await Task.Delay(20);
            }
        }
    }
}
