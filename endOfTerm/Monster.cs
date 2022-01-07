using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace endOfTerm
{
    class Monster
    {
        public PictureBox ghost;
        public Vector2 position;
        public Vector2 velocity;
        public Rectangle upperRight;
        public Rectangle bottomRight;
        public Rectangle bottomLeft;

        //public bool upperLeftAble = true;
        //public bool upperRightAble = true;
        //public bool  bottomRightAble = true;
        //public bool bottomLeftAble = true;

        public virtual void Initialize(Context context)
        {

        }
    }
}
