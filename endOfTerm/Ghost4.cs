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
    class Ghost4 : Monster
    {
        public Ghost4()
        {
            ghost = new PictureBox();
        }

        public override void Initialize(Context context)
        {
            position = new Vector2(context.baseTop.X, context.baseTop.Y);
            velocity = new Vector2(0, 0);
            upperRight = new Rectangle(context.baseTop.X + 1 + context.ghost4.ghost.Width - 1, context.baseTop.Y + 1 + 1, 1, 1);
            bottomRight = new Rectangle(context.baseTop.X - 1 + context.ghost4.ghost.Width - 1 - 1, context.baseTop.Y + context.ghost4.ghost.Height - 1, 1, 1);
            bottomLeft = new Rectangle(context.baseTop.X, context.baseTop.Y + context.ghost4.ghost.Height, 1, 1);
            ghost.Size = new Size(25, 25);
            ghost.BackColor = Color.Transparent;
            ghost.Image = Properties.Resources.Untitled_11f;
            ghost.Location = new Point(context.ghost4.position.x, context.ghost4.position.y);
            ghost.SizeMode = PictureBoxSizeMode.StretchImage;
            context.form.Controls.Add(ghost);
            context.monsters.Add(ghost);
            var task = Update(context);
        }

        async static Task Update(Context context)
        {
            while (true)
            {
                context.ghost4.velocity = context.player.position - context.ghost4.position;
                var x = context.ghost4.velocity.x == 0 ? 0 : context.ghost4.velocity.x / Math.Abs(context.ghost4.velocity.x);
                var y = context.ghost4.velocity.y == 0 ? 0 : context.ghost4.velocity.y / Math.Abs(context.ghost4.velocity.y);
                context.ghost4.velocity = Math.Abs(context.ghost4.velocity.x) > Math.Abs(context.ghost4.velocity.y) ? new Vector2(x, 0) : new Vector2(0, y);

                //bool upperLeftAble = Map.buffer[context.ghost4.position.y + context.ghost4.velocity.y, context.ghost4.position.x + context.ghost4.velocity.x] == 1 ?
                //true : false;
                //bool upperRightAble = Map.buffer[context.ghost4.upperRight.Y + context.ghost4.velocity.y, context.ghost4.upperRight.X + context.ghost4.velocity.x] == 1 ?
                //true : false;
                // bool bottomLeftAble = Map.buffer[context.ghost4.bottomLeft.Y + context.ghost4.velocity.y, context.ghost4.bottomLeft.X + context.ghost4.velocity.x] == 1 ?
                //true : false;
                //bool bottomRightAble = Map.buffer[context.ghost4.bottomRight.Y + context.ghost4.velocity.y, context.ghost4.bottomRight.X + context.ghost4.velocity.x] == 1 ?
                //true : false;

                foreach (var monster in context.monsters.ToList())
                {
                    if (monster != context.ghost4.ghost && monster.Bounds.IntersectsWith(context.ghost4.ghost.Bounds))
                    {
                        context.ghost4.position = new Vector2(context.baseTop.X, context.baseTop.Y);
                        //context.ghost4.velocity = new Vector2(0, 0);
                        context.ghost4.upperRight = new Rectangle(context.baseTop.X + 1 + context.ghost4.ghost.Width - 1, context.baseTop.Y + 1 + 1, 1, 1);
                        context.ghost4.bottomRight = new Rectangle(context.baseTop.X - 1 + context.ghost4.ghost.Width - 1 - 1, context.baseTop.Y + context.ghost4.ghost.Height - 1, 1, 1);
                        context.ghost4.bottomLeft = new Rectangle(context.baseTop.X, context.baseTop.Y + context.ghost4.ghost.Height, 1, 1);
                    }
                }

                context.ghost4.position += context.ghost4.velocity;
                context.ghost4.upperRight.X += context.ghost4.velocity.x;
                context.ghost4.upperRight.Y += context.ghost4.velocity.y;
                context.ghost4.bottomLeft.X += context.ghost4.velocity.x;
                context.ghost4.bottomLeft.Y += context.ghost4.velocity.y;
                context.ghost4.bottomRight.X += context.ghost4.velocity.x;
                context.ghost4.bottomRight.Y += context.ghost4.velocity.y;
                context.ghost4.ghost.Location = new Point(context.ghost4.position.x, context.ghost4.position.y);

                await Task.Delay(20);
            }
        }
    }
}
