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
    class Ghost3 : Monster
    {
        public Ghost3()
        {
            ghost = new PictureBox();
        }

        public override void Initialize(Context context)
        {
            position = new Vector2(context.baseRight.X, context.baseRight.Y);
            velocity = new Vector2(0, 0);
            upperRight = new Rectangle(context.baseRight.X + 1 + context.ghost3.ghost.Width - 1, context.baseRight.Y + 1 + 1, 1, 1);
            bottomRight = new Rectangle(context.baseRight.X - 1 + context.ghost3.ghost.Width - 1 - 1, context.baseRight.Y + context.ghost3.ghost.Height - 1, 1, 1);
            bottomLeft = new Rectangle(context.baseRight.X, context.baseRight.Y + context.ghost3.ghost.Height, 1, 1);
            ghost.Size = new Size(25, 25);
            ghost.BackColor = Color.Transparent;
            ghost.Image = Properties.Resources.Untitled_111f;
            ghost.Location = new Point(context.ghost3.position.x, context.ghost3.position.y);
            ghost.SizeMode = PictureBoxSizeMode.StretchImage;
            context.form.Controls.Add(ghost);
            context.monsters.Add(ghost);
            var task = Update(context);
        }

        async static Task Update(Context context)
        {
            while (true)
            {
                context.ghost3.velocity = context.player.position - context.ghost3.position;
                var x = context.ghost3.velocity.x == 0 ? 0 : context.ghost3.velocity.x / Math.Abs(context.ghost3.velocity.x);
                var y = context.ghost3.velocity.y == 0 ? 0 : context.ghost3.velocity.y / Math.Abs(context.ghost3.velocity.y);
                context.ghost3.velocity = Math.Abs(context.ghost3.velocity.x) > Math.Abs(context.ghost3.velocity.y) ? new Vector2(x, 0) : new Vector2(0, y);

                //bool upperLeftAble = Map.buffer[context.ghost3.position.y + context.ghost3.velocity.y, context.ghost3.position.x + context.ghost3.velocity.x] == 1 ?
                //    true : false;
                //bool upperRightAble = Map.buffer[context.ghost3.upperRight.Y + context.ghost3.velocity.y, context.ghost3.upperRight.X + context.ghost3.velocity.x] == 1 ?
                //    true : false;
                //bool bottomLeftAble = Map.buffer[context.ghost3.bottomLeft.Y + context.ghost3.velocity.y, context.ghost3.bottomLeft.X + context.ghost3.velocity.x] == 1 ?
                //    true : false;
                //bool bottomRightAble = Map.buffer[context.ghost3.bottomRight.Y + context.ghost3.velocity.y, context.ghost3.bottomRight.X + context.ghost3.velocity.x] == 1 ?
                //    true : false;

                foreach (var monster in context.monsters.ToList())
                {
                    if (monster != context.ghost3.ghost && monster.Bounds.IntersectsWith(context.ghost3.ghost.Bounds))
                    {
                        context.ghost3.position = new Vector2(context.baseRight.X, context.baseRight.Y);
                        // context.ghost3.velocity = new Vector2(0, 0);
                        context.ghost3.upperRight = new Rectangle(context.baseRight.X + 1 + context.ghost3.ghost.Width - 1, context.baseRight.Y + 1 + 1, 1, 1);
                        context.ghost3.bottomRight = new Rectangle(context.baseRight.X - 1 + context.ghost3.ghost.Width - 1 - 1, context.baseRight.Y + context.ghost3.ghost.Height - 1, 1, 1);
                        context.ghost3.bottomLeft = new Rectangle(context.baseRight.X, context.baseRight.Y + context.ghost3.ghost.Height, 1, 1);
                    }
                }

                context.ghost3.position += context.ghost3.velocity;
                context.ghost3.upperRight.X += context.ghost3.velocity.x;
                context.ghost3.upperRight.Y += context.ghost3.velocity.y;
                context.ghost3.bottomLeft.X += context.ghost3.velocity.x;
                context.ghost3.bottomLeft.Y += context.ghost3.velocity.y;
                context.ghost3.bottomRight.X += context.ghost3.velocity.x;
                context.ghost3.bottomRight.Y += context.ghost3.velocity.y;
                context.ghost3.ghost.Location = new Point(context.ghost3.position.x, context.ghost3.position.y);

                await Task.Delay(20);
            }
        }
    }
}
