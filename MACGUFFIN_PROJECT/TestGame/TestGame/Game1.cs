#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Threading;
using System.IO;
#endregion

namespace TestGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Character chrctr = new Character();


        Random rgen = new Random();


        // will hold Texture2D sprite and position
        Texture2D ship;
        public Rectangle place;

        Texture2D ship2;
        public Rectangle place2;

        Texture2D ship3;
        Rectangle place3;

        Texture2D ship4;
        Rectangle place4;

        KeyboardState kState;

        Texture2D guffin;
        Rectangle guffinPlace;
        Rectangle guffinPlace2;

        // attributes for jumping
        bool jumping = false;
        // holds current Y position
        int startY; 
        // will be used to act as 'gravity'
        int jumpSpeed;

        // for collision
        // attributes to store Y cords in 
        int playerY1;
        int playerY2;

        // Player 1 
        UserInterface P1;
        
        // Textures, sprites, etc.
        Texture2D baton;
        Texture2D block;
        SpriteFont text;
        Rectangle getCrystal;

        // list of wall blocks
        List<Rectangle> walls = new List<Rectangle>();


        public bool guffinOn1 = false;
        bool guffinOn2 = false;

        /// <summary>
        /// Method to spawn new getCrystals. Uses a randomized switch case to put the crystal at one of the spawn points.
        /// Once the map editor is functional, the spawn points should be changeable
        /// </summary>
        public void SpawnCrystal()
        {
            Random rgen = new Random();
            switch (rgen.Next(5) + 1)
            {
                case 1:
                    getCrystal = new Rectangle(100, 50, 11, 18);
                    return;
                case 2:
                    getCrystal = new Rectangle(700, 50, 11, 18);
                    return;
                case 3:
                    getCrystal = new Rectangle(400, 250, 11, 18);
                    return;
                case 4:
                    getCrystal = new Rectangle(100, 400, 11, 18);
                    return;
                case 5:
                    getCrystal = new Rectangle(700, 400, 11, 18);
                    return;
            }
        }


        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /*
        //this method will be called in initialize, will take a map name as a parameter and load a list of coordinates into the Walls collection
        public void ReadMap(string mapFile)
        {
            StreamReader input = null;


            string mapText = "";


                input = new StreamReader(mapFile);


                int numLines = File.ReadAllLines(mapFile).Length;
                if (numLines >= 2)
                {
                    string line = input.ReadLine();
                    char first = line[0];

                    if (first == 'W')
                    {
                        while (first == 'W')
                        {
                            // populate the list of walls
                            //change for loop so that it will not continue for the rest of the doc
                            for (int i = 0; i < File.ReadAllLines(mapFile).Length; i++)
                            {
                                //split the read line by commas - should be 3 values - 1st is indicator, 2nd is X coordinate, 3rd is Y coordinate
                                string[] splitLine = line.Split(',');

                                //parse the coordinate values into ints
                                int wallXco;
                                int.TryParse(splitLine[1], out wallXco);
                                int wallYco;
                                int.TryParse(splitLine[2], out wallYco);

                                //make a new rectangle based on what was read and add it into the collection of walls
                                walls.Add(new Rectangle(wallXco, wallYco, 20, 20));
                            }//for
                        }//while
                    }//if

                    //to be worked on later - spawnpoints and traps will be added in the future
                        /*
                    else if (first == 'T')
                    {
                        while (first == 'T')
                        {
                            //create trap
                        }
                    }
                    else if (first == 'S')
                    {
                        while (first == 'S')
                        {
                            //create spawn point
                        }
                    }//if loops
                         */
                //}//if
        //}



        // to be called in Update method and contains movement & jumping
        public void Move()
        {
            


            kState = Keyboard.GetState();

            if (kState.IsKeyDown(Keys.A)) place.X -= 6;

            if (kState.IsKeyDown(Keys.D)) place.X += 6;



            // jumping 
            
            if (jumping)
            {
                place.Y += jumpSpeed;
                jumpSpeed += 1;


               /* if (place.Y >= startY)
                {
                    place.Y = startY;
                    jumping = false;
                }*/

                playerY1 = jumpSpeed;

                if (place.Intersects(guffinPlace2))
                {
                    jumpSpeed = 0;
                    jumping = false;
                }
                else
                {
                    jumpSpeed = playerY1;
                }
                

            }
            else
            {

                if (kState.IsKeyDown(Keys.Space))
                {
                    jumping = true;
                    jumpSpeed = -20;
                }
            }


            /*
            if (jumping)
            {
                place.Y += jumpSpeed;
                jumpSpeed += 1;
                if (place.Y >= startY)
                {
                    place.Y = startY;
                    jumping = false;
                }
            }
            else
            {

                if (kState.IsKeyDown(Keys.Space))
                {
                    jumping = true;
                    jumpSpeed = -14;
                }
            }

            */

        }





        // will be put in Update Method
        // Will add Macguffins to the board

        // THIS IS BY FAR NOT THE BEST WAY TO DO THIS
        // IT DOESNT EVEN CANGE THE PLACE
        public void AddGuffin()
        {

            if (guffinOn1 == false)
            {
                

                guffinPlace = new Rectangle(rgen.Next(1,700),rgen.Next(200,300),11,18);

                guffinOn1 = true;
            }


        }

        







        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            place = new Rectangle(400, 300, 28, 62);
            guffinPlace2 = new Rectangle(600, 300, 11, 18);

            // set guffin location equal to players y
            //guffinPlace = new Rectangle(450, 300, 50, 50);


            // initialize Jumping attributes
            startY = place.Y;
            jumpSpeed = 0;

            P1 = new UserInterface();
            SpawnCrystal();

            //ReadMap("map1.txt");
            /* should be unneccessary with the ReadMap Method
            // populate the list of walls (just an example floor for now)
            for (int i = 0; i < 20; i++)
            {
                walls.Add(new Rectangle(200 + i * 20, 375, 20, 20));
            }
            */
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            baton = Content.Load<Texture2D>("baton");
            block = Content.Load<Texture2D>("block1");
            P1.Character = Content.Load<Texture2D>("player1");
            text = Content.Load<SpriteFont>("Arial");   


            ship = Content.Load<Texture2D>("player1");

            guffin = Content.Load<Texture2D>("crystal");
            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            kState = Keyboard.GetState();
           
            // calls the move method that has left and right movement and jumping
            Move();


            

            AddGuffin();

            // detect whether the character has intersected a getCrystal
            Boolean crysCollision = getCrystal.Intersects(place);
            if (crysCollision == true)
            {
                SpawnCrystal();
                P1.CrystalCount++;
            }

            // update the baton timer, if necessary (doesn't do anything for now)
            if (P1.StunCharge < 100)
            {
                P1.ChargeStun();
            }


            // collision with jumping doesnt work
            if (place.Intersects(guffinPlace))
            {

                guffinPlace.Location.Equals(null);

               
                guffinOn1 = false;
            }




            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            spriteBatch.Draw(ship, place, Color.White);
            spriteBatch.Draw(guffin, guffinPlace, Color.White);
            spriteBatch.Draw(guffin, guffinPlace2, Color.White);
            
            // make a bunch of blocks
            foreach (Rectangle wall in walls)
            {
                spriteBatch.Draw(block, wall, Color.White);
            }

            /// <summary>
            /// Display for P1 - P4
            /// Right now, they all use P1's values for test purposes
            /// </summary>
            spriteBatch.Draw(guffin, new Vector2(250, 450), Color.Red);
            spriteBatch.DrawString(text, "" + P1.CrystalCount, new Vector2(270, 445), Color.Black);
            spriteBatch.Draw(baton, new Vector2(245, 398), Color.White);

            // P2
            spriteBatch.Draw(guffin, new Vector2(333, 450), Color.Blue);
            spriteBatch.DrawString(text, "" + P1.CrystalCount, new Vector2(353, 445), Color.Black);
            spriteBatch.Draw(baton, new Vector2(328, 398), Color.White);

            // P3
            spriteBatch.Draw(guffin, new Vector2(416, 450), Color.Green);
            spriteBatch.DrawString(text, "" + P1.CrystalCount, new Vector2(436, 445), Color.Black);
            spriteBatch.Draw(baton, new Vector2(411, 398), Color.White);

            // P4
            spriteBatch.Draw(guffin, new Vector2(500, 450), Color.Yellow);
            spriteBatch.DrawString(text, "" + P1.CrystalCount, new Vector2(520, 445), Color.Black);
            spriteBatch.Draw(baton, new Vector2(495, 398), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
