using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace TestGame
{
    class UserInterface
    {
        /// <summary>
        /// (For the purpose of this summary, I'm declaring the macguffins to be 'crystals')
        /// The user interface consists of two parts: the Counter and the Stun Baton Meter
        /// 
        /// The Counter needs to increment and decrement as a player collects crystals or loses them from hits.
        /// 
        /// The Stun Baton Meter needs to keep track of how much the Baton has charged, as well as whether or
        /// not it can be used at the moment.
        /// </summary>
        
        // attributes
        Player player;
        private int stunCharge;
        int stunCountdown;
        bool stunIsActive;

        // constructor (each player will need their own Counter and Meter objects)
        public UserInterface(Player pl)
        {
            player = pl;
            player.MacGuffinCount = 0;
            stunCharge = 0;
            stunCountdown = 100;
            stunIsActive = false;
        }
        
        public int StunCharge
        {
            get { return stunCharge; }
            set { stunCharge = value; }
        }

        public int StunCountdown
        {
            get { return stunCountdown; }
            set { stunCountdown = value; }
        }

        public bool StunIsActive
        {
            get { return stunIsActive; }
            set { stunIsActive = value; }
        }

        // charge the stun - this doesn't quite work yet, as I have yet
        // to get the timer to work, but it should probably end up looking
        // like something along these lines:
        public void ChargeStun()
        {
            stunCharge++;
            stunCountdown--;

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D guffin, Texture2D baton, SpriteFont text)
        {
            int shift = player.PlayerNum * 83;

            spriteBatch.Draw(guffin, new Vector2(167 + shift, 450), player.PlayerColor);
            spriteBatch.DrawString(text, "" + player.MacGuffinCount, new Vector2(187 + shift, 445), Color.Black);
            spriteBatch.Draw(baton, new Vector2(162 + shift, 398), Color.White);
        }
    }
}
