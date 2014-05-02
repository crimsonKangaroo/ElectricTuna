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
    // MileStone 2

    // Electric Tuna
    // Justin John
    // Monica Ambrose
    // Zachary Wilken


    // Currently Movement is controlled by A(Left) and D(Right) and Jump(Space)
    // Will change movement to be controlled by a gamepad in next milestone



    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;      
        Random rgen = new Random();

        ScreenManager screenManager;
        GameScreen[] otherScreens;

        // will hold Texture2D sprite and position
        List<Character> characters = new List<Character>();
        Character char1;
        Character char2;
        Character char3;
        Character char4;

        Rectangle startPlace;
        
        KeyboardState kState;

        Texture2D guffin;
        Rectangle guffinPlace;

        // Players & respective interfaces
        List<Player> players = new List<Player>();
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
        bool guffinOn2 = false;

        Boolean gameOver;
        
        public Game1()
            : base()
        {
            screenManager = new ScreenManager(this);
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Random rgen = new Random();

        
        }

        public void ReadMap(string map)
        {
            StreamReader input = null;
            String line = "";

            input = new StreamReader(map);

            // read the rest of the information as walls
            while ((line = input.ReadLine()) != null)
            {
                CreateWall(line);
            }
        }

        // place the walls from a map file
        public void CreateWall(string str)
        {
            // create an array of the sunstrings
            String[] wallInfo = str.Split(',');
           // check if the line has the required number of properties

            try
            {
                int wallX = int.Parse(wallInfo[1]);
                int wallY = int.Parse(wallInfo[2]);

                // create a new wall based on those ints, and add it to the list
                walls.Add(new Rectangle(wallX,wallY,20,20));
            }
            catch (FormatException fe)
            {
                // if the wall information is incorrect... do nothing.
            }
        }

        // THIS IS BY FAR NOT THE BEST WAY TO DO THIS
        // IT DOESNT EVEN CHANGE THE PLACE
        // will be put in Update Method
        // Will add Macguffins to the board
        // THIS IS BY FAR NOT THE BEST WAY TO DO THIS
        // IT DOESNT EVEN CANGE THE PLACE

        public void AddGuffin()
        {
	        if (guffinOn1 == false)
 	        {
		        foreach (Rectangle rect in walls)
 		        {
                    guffinPlace = new Rectangle(rgen.Next(1, 700), rgen.Next(200, 300), 11, 18);
			        while (rect.Intersects(guffinPlace))
			        {
 				        guffinPlace.Y = rgen.Next(200, 350);
 				        guffinPlace.X = rgen.Next(1, 700);
 			        }
 			        guffinOn1 = true;
		        }
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
            screenManager.AddScreen(new MainMenuScreen("Menu"), PlayerIndex.One);
            screenManager.Initialize();
            otherScreens = screenManager.GetScreens();
            gameOver = false;

            startPlace = new Rectangle(400, 300, 45, 62);

            player1 = new Player(1, Color.Red);
            user1 = new UserInterface(player1);
            char1 = new Character(player1);
            char1.Place = new Rectangle(300, 300, 45, 61);

            player2 = new Player(2, Color.Blue);
            user2 = new UserInterface(player2);
            char2 = new Character(player2);
            char2.Place = new Rectangle(375, 300, 45, 61);

            player3 = new Player(3, Color.Green);
            user3 = new UserInterface(player3);
            char3 = new Character(player3);
            char3.Place = new Rectangle(450, 300, 45, 61);

            player4 = new Player(4, Color.Yellow);
            user4 = new UserInterface(player4);
            char4 = new Character(player4);
            char4.Place = new Rectangle(525, 300, 45, 61);

            characters.Add(char1);
            characters.Add(char2);
            characters.Add(char3);
            characters.Add(char4);

            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
            players.Add(player4);

            // read the map
            ReadMap("map1.txt");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            spriteBatch = new SpriteBatch(GraphicsDevice);

            baton = Content.Load<Texture2D>("baton");
            block = Content.Load<Texture2D>("block1");
            text = Content.Load<SpriteFont>("Arial");

            player1.Sprite = Content.Load<Texture2D>("BobbyBoxshoes");
            player2.Sprite = Content.Load<Texture2D>("BobbyBoxshoes");
            player3.Sprite = Content.Load<Texture2D>("BobbyBoxshoes");
            player4.Sprite = Content.Load<Texture2D>("BobbyBoxshoes");

            guffin = Content.Load<Texture2D>("crystal");
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
            screenManager.Update(gameTime);
            if (otherScreens[0].IsActive == true)
            {
                goto UpdateEnd;
            }
            // TODO: Add your update logic here
            kState = Keyboard.GetState();

            // calls the move method that has left and right movement and jumping
            char1.Move(walls, gameTime);
            char2.Move(walls, gameTime);
            char3.Move(walls, gameTime);
            char4.Move(walls, gameTime);
            AddGuffin();

            // attacking logic

            foreach (Character attacker in characters)
            {
                attacker.Attack();

                foreach (Character victim in characters)
                {
                    if (attacker.StunBox.Intersects(victim.Place))
                    {
                        victim.Stunned(gameTime);
                    }
                }
            }
           

            // update the baton timer, if necessary (doesn't do anything for now)
            if (user1.StunCharge < 100)
            {
                user1.ChargeStun();
            }


            // collision with jumping doesnt work
            foreach (Character chr in characters)
            {
                if (chr.Place.Intersects(guffinPlace))
                {
                    chr.Player.MacGuffinCount++;
                    guffinPlace.Location.Equals(null);
                    guffinOn1 = false;
                }
            }

            foreach (Character chr in characters)
            {
                if (chr.Place.Y > GraphicsDevice.Viewport.Height + 500)
                {
                    chr.Dead = true;
                }

                if (chr.Dead == true) chr.Respawn();
            }

            foreach (Player pl in players)
            {
                if (pl.MacGuffinCount >= 10) gameOver = true;
            }
            UpdateEnd:
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            
            spriteBatch.Begin();

            if (otherScreens[0].IsActive == true)
            {
                otherScreens[0].Draw(gameTime);
                goto DrawEnd;
            }

            GraphicsDevice.Clear(Color.LightSteelBlue);
            spriteBatch.Draw(guffin, guffinPlace, Color.White);

            // make a bunch of blocks
            foreach (Rectangle wall in walls)
            {
                spriteBatch.Draw(block, wall, Color.White);
            }

            /// <summary>
            /// Draw the players
            /// Right now, they all use player1's place (just for test purpose)
            /// </summary>
            char1.Draw(spriteBatch, player1.Sprite);
            char2.Draw(spriteBatch, player1.Sprite);
            char3.Draw(spriteBatch, player1.Sprite);
            char4.Draw(spriteBatch, player1.Sprite);

            /// <summary>
            /// Display for P1 - P4
            /// Right now, they all use P1's values for test purposes
            /// </summary>
            user1.Draw(spriteBatch, guffin, baton, text);
            user2.Draw(spriteBatch, guffin, baton, text);
            user3.Draw(spriteBatch, guffin, baton, text);
            user4.Draw(spriteBatch, guffin, baton, text);

            foreach (Character victim in characters)
            {
                    spriteBatch.Draw(guffin, victim.StunBox, Color.White);
            }

            if (char1.StunOn == true) spriteBatch.Draw(guffin, char1.StunBox, Color.Silver);

            if (gameOver == true)
            {
                GraphicsDevice.Clear(Color.SkyBlue);
                spriteBatch.DrawString(text, "GAME OVER", new Vector2(200,200), Color.SlateGray);
                char1.CanMove = false;
                char2.CanMove = false;
                char3.CanMove = false;
                char4.CanMove = false;
                goto DrawEnd;
            }

            DrawEnd:
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
