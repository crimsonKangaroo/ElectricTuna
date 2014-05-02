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
    class Character: MovableGamePiece
    {
        GamePadState gPadState;
        Rectangle place;

        // spritesheet-drawing variables
        float timer;
        float interval;
        int currentFrame;
        int spriteWidth;
        int spriteHeight;
        Rectangle sourceRect;

        KeyboardState kState;
        Keys[] backup;

        int direction;
        bool canMove = true;

        // attributes for jumping
        bool jumping = true;
        int startY; // holds current Y position
        int jumpSpeed; // will be used to act as 'gravity'

        // for collision
        // attributes to store Y cords in 
        int playerY1;
        int playerY2;

        public Character(Player pl)
        {
            player = pl;
            startY = place.Y;
            jumpSpeed = 0;
            direction = 0;

            // spritesheet-drawing information
            timer = 0f;
            interval = 100f;
            currentFrame = 0;
            spriteWidth = 45;
            spriteHeight = 61;
            sourceRect = new Rectangle(0, 0, 45, 61);//= new Rectangle((46 * currentFrame), 0, spriteWidth, spriteHeight);

            // Change the key controls based on the player (these are for testing purposes)
            switch (player.PlayerNum)
            {
                case 1:
                    backup = new Keys[] { Keys.W, Keys.A, Keys.S, Keys.D, Keys.F };
                    break;
                case 2:
                    backup = new Keys[] { Keys.I, Keys.J, Keys.K, Keys.L, Keys.G };
                    break;
                case 3:
                    backup = new Keys[] { Keys.Up, Keys.Left, Keys.Down, Keys.Right, Keys.NumPad0 };
                    break;
                case 4:
                    backup = new Keys[] { Keys.NumPad8, Keys.NumPad4, Keys.NumPad2, Keys.NumPad6, Keys.NumPad5 };
                    break;

            }
        }

        protected Player player;
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public Rectangle Place
        {
            get { return place; }
            set { place = value; }
        }

        public bool CanMove
        {
            get { return canMove; }
            set { canMove = value; }
        }
        

        public void Move(List<Rectangle> walls, GameTime gameTime)
        {
            gPadState = GamePad.GetState(player.Index);
            kState = Keyboard.GetState();
            timer += (float)gameTime.ElapsedGameTime.Milliseconds;

            if (canMove == true)
            {
                // moving left
                if (gPadState.ThumbSticks.Left.X <= -.9f || kState.IsKeyDown(backup[1]))
                {
                    place.X -= 6;
                    direction = 0;

                    // update the spritesheet
                    if (timer > interval)
                    {
                        currentFrame++; // show the next frame
                        if (currentFrame > 3) currentFrame = 0; // reset the frame if it goes to far
                        timer = 0f; // reset the timer
                    }
                }

                // moving right
                if (gPadState.ThumbSticks.Left.X >= .9f || kState.IsKeyDown(backup[3]))
                {
                    place.X += 6;
                    direction = 1;

                    // update the spritesheet
                    if (timer > interval)
                    {
                        currentFrame++;
                        if (currentFrame > 3) currentFrame = 0;
                        timer = 0f;
                    }
                }

                // jumping     
                if (jumping)
                {
                    place.Y += jumpSpeed;
                    jumpSpeed += 1;
                    playerY1 = jumpSpeed;

                    foreach (Rectangle rect in walls)
                    {
                        if (place.Intersects(rect))
                        {
                            jumpSpeed = 0;
                            jumping = false;

                            if (gPadState.IsButtonDown(Buttons.A) || kState.IsKeyDown(backup[0]))
                            {
                                jumping = true;
                                jumpSpeed = -20;
                            }

                            while (place.Intersects(rect))
                            {
                                place.Y--;
                            }
                        }
                        else
                        {
                            //jumpSpeed = playerY1;
                            jumping = true;
                        }
                    }
                }
                else if (gPadState.IsButtonDown(Buttons.A) || kState.IsKeyDown(backup[0]))
                {
                    jumping = true;
                    jumpSpeed = -20;
                }

                // standing still (for sprite-drawing purposes)
                else
                {
                    currentFrame = 0;
                }

                spriteWidth = 45;
                sourceRect.X = (47 * currentFrame);
                sourceRect.Width = spriteWidth;
            }         
        }


        public void TakeHit()
        {
            // will loose a macguffin when hit
        }

        /// <summary>
        /// Stun Baton methods
        /// </summary>
        // stun baton attributes


        Rectangle stunBox;
        Texture2D stunBaton;
        bool stunned = false;
        int stunCounter;
        int s = 0; 
        bool stunOn = false;

        bool dead = false;
        int respawnTimer;

        public Rectangle StunBox
        {
            get { return stunBox; }
        }

        public bool StunOn
        {
            get { return stunOn; }
            set { stunOn = value; }
        }

        public Boolean Dead
        {
            get { return dead; }
            set { dead = value; }
        }

        public void Respawn()
        {
            if (dead == true)
            {
                place.Y = -200;
                jumping = false;

                respawnTimer++;
            }

            if (respawnTimer > 100)
            {
                place.X = 300 + (player.PlayerNum * 50);
                place.Y = 300;
                jumping = true;

                dead = false;
            }
        }

        public void Attack()
        {
            kState = Keyboard.GetState();
            gPadState = GamePad.GetState(player.Index); // currently player one for testing purpose
            stunCounter = 0;

            if (gPadState.IsButtonDown(Buttons.B) || kState.IsKeyDown(backup[4]))
            {
                while (stunCounter < 100)
                {
                    switch (direction)
                    {
                        case 0:
                            stunBox = new Rectangle(place.X - 30, place.Y + 25, 50, 15);
                            stunOn = true;
                            break;
                        case 1:
                            stunBox = new Rectangle(place.X + 43, place.Y + 25, 50, 15);
                            stunOn = true;
                            break;
                    }
                    stunCounter++;
                }

                stunOn = false;
            }           
        }

        public void Stunned(GameTime gameTime)
        {
            Color oldColor = player.PlayerColor;
            timer += (float)gameTime.ElapsedGameTime.Milliseconds;

            player.PlayerColor = Color.Red;
            stunCounter++;
                if (timer == 500)
                {
                    player.PlayerColor = oldColor;
                    stunCounter = 0;
                    stunned = false;
                }
        }

        // Draw method (for sprites)
        public void Draw(SpriteBatch spriteBatch, Texture2D sprite)
        {
            switch (direction)
            {
                case 0:
                    spriteBatch.Draw(sprite, place, sourceRect, player.PlayerColor, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                    break;
                case 1:
                    spriteBatch.Draw(sprite, place, sourceRect, player.PlayerColor, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
                    break;
            }

        }
    }
}
