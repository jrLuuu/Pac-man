using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace endOfTerm
{
    class Context
    {
        public Form1 form;
        public Player player = new Player();
        public List<PictureBox> monsters;
        public List<PictureBox> lifes;
        public List<PictureBox> baseLifes;
        public List<PictureBox> watermelon;
        public List<PictureBox> removedWatermelon;
        public bool isContinue = true;
        public int score = 0;
        public int life = 3;

        public Rectangle baseLeft;
        public Rectangle baseMiddle;
        public Rectangle baseRight;
        public Rectangle baseTop;
        public Ghost1 ghost1;
        public Ghost2 ghost2;
        public Ghost3 ghost3;
        public Ghost4 ghost4;
        public Vector2 basePosition;
        public Vector2 currentVelocity;

        public void Update()
        {
            currentVelocity = form.velocity;
        }

        public void Initialize()
        {
            monsters = new List<PictureBox>();
            lifes = new List<PictureBox>();
            baseLifes = new List<PictureBox>();
            watermelon = new List<PictureBox>();
            removedWatermelon = new List<PictureBox>();
            baseLeft = new Rectangle(347, 368, 1, 1);
            baseMiddle = new Rectangle(412, 368, 1, 1);
            baseRight = new Rectangle(472, 368, 1, 1);
            baseTop = new Rectangle(410, 293, 1, 1);
            ghost1 = new Ghost1();
            ghost1.Initialize(this);
            ghost2 = new Ghost2();
            ghost2.Initialize(this);
            ghost3 = new Ghost3();
            ghost3.Initialize(this);
            ghost4 = new Ghost4();
            ghost4.Initialize(this);
            basePosition = new Vector2(16, 16);
            currentVelocity = form.velocity;
            player.Initialize(this);
        }
    }
}
