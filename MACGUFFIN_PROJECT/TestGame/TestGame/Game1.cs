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

        ScreenManager screenManager; // manages non-gameplay screens
        GameScreen[] otherScreens; // matrix of non-gameplay screens (Even though there's only the one for now)


        Random rgen = new Random();


        // will hold sprite position (The texture is now held by the Player class instead)
        public Rectangle place;
        public Rectangle place2;
        public Rectangle place3;
        public Rectangle place4;

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

        // Players & respective interfaces
        Player player1;
        UserInterface user1;
        Player player2;
        UserInterface user2;
        Player player3;
        UserInterface user3;
        Player player4;
        UserInterface user4;
        
        // Textures, sprites, etc.
        Texture2D baton;
        Texture2D block;
        SpriteFont text;
        Rectangle getCrystal;

        // list of wall blocks
        List<Rectangle> walls = new List<Rectangle>();


        public bool guffinOn1 = false;


        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            screenManager = new ScreenManager(this);
            Random rgen = new Random();
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

            screenManager.AddScreen(new MainMenuScreen("Menu"), PlayerIndex.One);
            screenManager.Initialize();
            otherScreens = screenManager.GetScreens();


            // initialize Jumping attributes
            startY = place.Y;
            jumpSpeed = 0;
            
            // player loading - for this logic, they just sort of move together in a conga line
            // until the seperate controls are implemented
            player1 = new Player(1, Color.Red);
            user1 = new UserInterface(player1);
            place = new Rectangle(400, 300, 28, 62);

            player2 = new Player(2, Color.Blue);
            user2 = new UserInterface(player2);
            place2 = new Rectangle(475, 300, 28, 62);

            player3 = new Player(3, Color.Green);
            user3 = new UserInterface(player3);
            place3 = new Rectangle(550, 300, 28, 62);

            player4 = new Player(4, Color.Yellow);
            user4 = new UserInterface(player4);
            place4 = new Rectangle(625, 300, 28, 62);

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
            text = Content.Load<SpriteFont>("Arial");   

            player1.Sprite = Content.Load<Texture2D>("player1");
            player2.Sprite = Content.Load<Texture2D>("player1");
            player3.Sprite = Content.Load<Texture2D>("player1");
            player4.Sprite = Content.Load<Texture2D>("player1");

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
                
                screenManager.Update(gameTime);
            if (otherScreens[0].IsActive == true)
            {
                goto UpdateEnd;
            }
            kState = Keyboard.GetState();
           
            // calls the move method that has left and right movement and jumping
            Move();
            AddGuffin();

            // detect whether the character has intersected a getCrystal
            if (place.Intersects(guffinPlace))
            {
                player1.MacGuffinCount++;
                guffinPlace.Location.Equals(null); 
                guffinOn1 = false;
            }
            if (place2.Intersects(guffinPlace))
            {
                player2.MacGuffinCount++;
                guffinPlace.Location.Equals(null);
                guffinOn1 = false;
            }
            if (place3.Intersects(guffinPlace))
            {
                player3.MacGuffinCount++;
                guffinPlace.Location.Equals(null);
                guffinOn1 = false;
            }
            if (place4.Intersects(guffinPlace))
            {
                player4.MacGuffinCount++;
                guffinPlace.Location.Equals(null);
                guffinOn1 = false;
            }
            
            // TEMPORARY TESTING MEASURES - players 2-4 move with player 1 
            // (change this once separate controls)
            place2.X = place.X + 30;
            place2.Y = place.Y;

            place3.X = place.X + 60;
            place3.Y = place.Y;

            place4.X = place.X + 90;
            place4.Y = place.Y;

            // TODO: Add update logic here
            // marker for the goto, to allow the program to skip unnecessary updates while the 
            // Main Menu is active.
            UpdateEnd: 
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
            if (otherScreens[0].IsActive == true)
            {
                otherScreens[0].Draw(gameTime);
                goto DrawEnd;
            }
            
            // make a bunch of blocks
            foreach (Rectangle wall in walls)
            {
                spriteBatch.Draw(block, wall, Color.White);
            }

            // Draw the players
            spriteBatch.Draw(player1.Sprite, place, Color.White);
            spriteBatch.Draw(player2.Sprite, place2, player2.PlayerColor);
            spriteBatch.Draw(player3.Sprite, place3, player3.PlayerColor);
            spriteBatch.Draw(player4.Sprite, place4, player4.PlayerColor);
            
            /// <summary>
            /// Display for P1 - P4
            /// </summary>
            user1.Draw(spriteBatch, guffin, baton, text);
            user2.Draw(spriteBatch, guffin, baton, text);
            user3.Draw(spriteBatch, guffin, baton, text);
            user4.Draw(spriteBatch, guffin, baton, text);
            
            // goto marker to skip Drawing main game stuff while the Main Menu is running
            DrawEnd: 
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
