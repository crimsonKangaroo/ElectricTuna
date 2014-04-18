#region Using Statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

#endregion

namespace MonoGameWindowsFormTest
{
    public partial class Form1 : Form
    {

        Game1 gm1 = new Game1();

        public KeyboardState kState;

        GamePadState gState;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            kState = Keyboard.GetState();

            

            //if (kState.IsKeyDown(Keys.A)) place.X -= 2;
           // if (kState.IsKeyDown(Keys)) pictureBox1.Left += 5;

           

            //gm1.ProcessInput();


            //if (kState.IsKeyDown(Keys.D)) place.X += 2;


        }
    }
}
