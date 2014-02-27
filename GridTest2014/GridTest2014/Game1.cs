using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GridTest2014
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random r = new Random();

        public Texture2D WhitePixel;

        public SpriteFont font1;

        public Cell[,] cellArray;

        public int cellWidth = 25;
        public int gridWidth = 25;
        public int gridHeight = 25;

        public int count;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

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

            WhitePixel = Content.Load<Texture2D>("WhitePixel");
            font1 = Content.Load<SpriteFont>("SpriteFont1");

           // CreateRandomGrid();
            //CreateBasicGrid();
            FillGridRecursive();
      
        }


        //empty except 4 connected squares
        private void CreateBasicGrid()
        {
            cellArray = new Cell[gridWidth, gridHeight];
            Random r = new Random();

            for (int i = 0; i < cellArray.GetLength(0); i++)
            {
                for (int j = 0; j < cellArray.GetLength(1); j++)
                {
                    cellArray[i, j] = new Cell(0, 0, false, false, false, false);
                    
                }
            }

            cellArray[10, 10] = new Cell(0, 1, true, true, true, true); //center
            cellArray[11, 10] = new Cell(1, 1, false, false, false, true); //east
            cellArray[10, 11] = new Cell(2, 1, true, false, false, false); //south
            cellArray[9, 10] = new Cell(3, 1, false, false, true, false); //west
            cellArray[10, 9] = new Cell(4, 1, false, true, false, false); //north

        }


        private void CreateRandomGrid()
        {
            cellArray = new Cell[gridWidth, gridHeight];
            Random r = new Random();
            
            for(int i=0;i<cellArray.GetLength(0);i++)
            {
                for(int j=0;j<cellArray.GetLength(1);j++)
                {
                    cellArray[i, j] = new Cell(count, r.Next(5), Bit(), Bit(), Bit(), Bit());
                    count++;
                }
            }
        }

        private bool Bit()
        {
            if (r.Next(2) == 0)
                return true;
            else
                return false;
        }

        private void FillGridRecursive()
        { 
            //init the grid
            cellArray = new Cell[gridWidth, gridHeight];
            Random r = new Random();
            count = 0;

            for (int i = 0; i < cellArray.GetLength(0); i++)
            {
                for (int j = 0; j < cellArray.GetLength(1); j++)
                {
                    cellArray[i, j] = new Cell(0,0, false, false, false,false);
                }
            }

            //call FillCell on a random cell
            FillCell(gridHeight / 2, gridWidth / 2);
          

            //make sure we make a decent sized map
            if (count < 20 )
            {
                FillGridRecursive();
            }

        }

        private void FillCell(int x, int y)
        {
            //fill this cell
                //check if this is off the grid, or already filled.  If so, stop
            if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight)
            {
                int i = 0;
                
            }
            else if (cellArray[x, y].type ==0)
            {
                cellArray[x, y].count = count;
                count++;
                cellArray[x, y].type += 1;
                for (int i = 0; i <= r.Next(4); i++)
                {
                    switch (r.Next(4))
                    {
                        case 0: //north
                            FillNorth(x, y);
                            break;
                        case 1: //south
                            FillSouth(x, y);
                            break; 
                        case 2: //east
                             FillEast(x, y);
                            break; 
                        case 3: //west
                            FillWest(x, y);
                            break;
                        default:
                            break;
                    }
                }
                //call FillCell on those.
            }
            
           
        }

        private void FillNorth(int x, int y)
        {
            int newx = x;
            int newy = y - 1;

            //check if we can go north
            if (newx < 0 || newx >= gridWidth || newy < 0 || newy >= gridHeight)
            {
               
            }
             else if (cellArray[newx, newy].type == 0)
             {
                 cellArray[x, y].north = true;
                 cellArray[newx, newy].south = true;
                 FillCell(newx, newy);
             }
        }

        private void FillSouth(int x, int y)
        {
            int newx = x;
            int newy = y + 1;

            //check if we can go north
            if (newx < 0 || newx >= gridWidth || newy < 0 || newy >= gridHeight)
            {

            }
            else if (cellArray[newx, newy].type == 0)
            {
                cellArray[x, y].south = true;
                cellArray[newx, newy].north = true;
                FillCell(newx, newy);
            }
        }

        private void FillEast(int x, int y)
        {
            int newx = x+1;
            int newy = y ;

            //check if we can go north
            if (newx < 0 || newx >= gridWidth || newy < 0 || newy >= gridHeight)
            {

            }
            else if (cellArray[newx, newy].type == 0)
            {
                cellArray[x, y].east = true;
                cellArray[newx, newy].west = true;
                FillCell(newx, newy);
            }
        }

        private void FillWest(int x, int y)
        {
            int newx = x-1;
            int newy = y ;

            //check if we can go north
            if (newx < 0 || newx >= gridWidth || newy < 0 || newy >= gridHeight)
            {

            }
            else if (cellArray[newx, newy].type == 0)
            {
                cellArray[x, y].west = true;
                cellArray[newx, newy].east = true;
                FillCell(newx, newy);
            }
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter) == true)
            {
                FillGridRecursive();
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
            spriteBatch.Begin();

            Rectangle rec = new Rectangle(10,10,100,100);
            //DrawPrimitives.DrawRectangle(rec, WhitePixel, Color.Red, spriteBatch, true, 1);
            DrawGrid(gameTime, spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawGrid(GameTime gameTime, SpriteBatch spriteBatch)
        {
          
            Vector2 pos = new Vector2(0, 0);
            Rectangle rec;

            
                for (int j = 0; j < cellArray.GetLength(1); j++)
                {
                    for (int i = 0; i < cellArray.GetLength(0); i++)
                    {
                        pos.X += cellWidth;
                        if (cellArray[i, j].type != 0)
                        {
                            
                            rec = new Rectangle((int)pos.X, (int)pos.Y, cellWidth, cellWidth);
                            DrawPrimitives.DrawRectangle(rec, WhitePixel, getColor(cellArray[i, j].type), spriteBatch, true, 1);
                            DrawPrimitives.DrawRectangle(rec, WhitePixel, Color.Black, spriteBatch, false, 1);
                            spriteBatch.DrawString(font1, cellArray[i, j].count.ToString(), new Vector2(pos.X + 5, pos.Y + 5), Color.Red);

                            DrawConnector(pos, spriteBatch, 0, cellArray[i, j].north);
                            DrawConnector(pos, spriteBatch, 1, cellArray[i, j].south);
                            DrawConnector(pos, spriteBatch, 2, cellArray[i, j].east);
                            DrawConnector(pos, spriteBatch, 3, cellArray[i, j].west);
                        }
                }
                pos.X = 0;
                pos.Y += cellWidth;
            }
        }

        //draw the connectors of 10 x 10 cells
        private void DrawConnector(Vector2 pos, SpriteBatch spriteBatch, int dir, bool isConn)
        {
            Color c = Color.Black;
            if (isConn)
                c = Color.White;

            Rectangle rec;
            switch (dir)
            {
                case 0: //north
                    rec = new Rectangle((int)pos.X + (cellWidth/2), (int)pos.Y, 2, 4);
                    break;
                case 1: //south
                    rec = new Rectangle((int)pos.X + (cellWidth/2), (int)pos.Y+(cellWidth-4),2, 4);
                    break;
                case 2: //east
                    rec = new Rectangle((int)pos.X + (cellWidth-4), (int)pos.Y+(cellWidth/2), 4, 2);
                    break;
                case 3: //west
                    rec = new Rectangle((int)pos.X, (int)pos.Y+(cellWidth/2), 4, 2);
                    break;
                default:
                    rec = new Rectangle((int)pos.X, (int)pos.Y, 0, 0);
                    break;
            }

            DrawPrimitives.DrawRectangle(rec, WhitePixel, c, spriteBatch, true, 1);
        }

        private Color getColor(int num)
        {
            switch (num)
            {
                case 0:
                    return Color.LightGray;
                case 1:
                    return Color.Blue;
                case 2:
                    return Color.Green;
                case 3:
                    return Color.Yellow;
                case 4:
                    return Color.Red;
                default:
                    return Color.Orange;
            }
        }


    }
}
