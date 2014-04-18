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
        private int crystalCount;
        private int stunCharge;
        int stunCountdown;
        bool stunIsActive;
        Texture2D testCharacter;

        // constructor (each player will need their own Counter and Meter objects)
        public UserInterface()
        {
            crystalCount = 0;
            stunCharge = 0;
            stunCountdown = 100;
            stunIsActive = false;
        }
        
        // attributes as properties
        public Texture2D Character
        {
            get { return testCharacter; }
            set { testCharacter = value; }
        }

        public int CrystalCount
        {
            get { return crystalCount; }
            set 
            { 
                if (value < 0) crystalCount = 0;
                else crystalCount = value;
            }
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
    }
}
