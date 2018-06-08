using System;
using System.Collections.Generic;
using System.IO;
using CollegeProject.GameObject;
using CollegeProject.Utility;
using HadoukInput;
using HighScoreBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MouseBuddy;
using Newtonsoft.Json;

namespace CollegeProject.Screen
{
    public class GameplayScreen : WidgetScreen//MenuBuddy.Screen, IGameScreen
    {
        private Texture2D backgroundTexture;
        //private Texture2D cardBack;
        int screenWidth;
        int screenHeight;
        private MouseState mouseState;
        private Deck deck;
        private List<Card> cards;
        ButtonState prevState = ButtonState.Released;
        private Rectangle[] rectangles;
        private IMouseManager mouseManager;
        private SpriteFont font;
        float timer = 0;
        Boolean runTimer;
        private InputState m_Input = new InputState();
        private int cardsFlipped;
        private int card1;
        private int card2;

        float elapsedTime = 0;

        public GameplayScreen() : base("GamePlay")
        {
            CoveredByOtherScreens = false;
            
        }
        public override void LoadContent()
        {
            base.LoadContent();
            AddCancelButton();
            runTimer = true;
            cardsFlipped = 0;
            font = ScreenManager.Game.Content.Load<SpriteFont>("myFont");
            
            screenWidth = ScreenManager.Game.GraphicsDevice.Viewport.Width;
            screenHeight = ScreenManager.Game.GraphicsDevice.Viewport.Height;
            backgroundTexture = ScreenManager.Game.Content.Load<Texture2D>("background");
            
            

            var mouse = (MouseInputHelper)ScreenManager.Game.Services.GetService(typeof(IInputHandler));
            mouseManager = (IMouseManager)mouse.InputHelper;
            deck = new Deck(this);

            cards = deck.GetCardsShuffled();

            rectangles = new Rectangle[12] {
                new Rectangle(250, 100, 100, 100),
                new Rectangle(250, 225, 100, 100),
                new Rectangle(250, 350, 100, 100),
                new Rectangle(450, 100, 100, 100),
                new Rectangle(450, 225, 100, 100),
                new Rectangle(450, 350, 100, 100),
                new Rectangle(650, 100, 100, 100),
                new Rectangle(650, 225, 100, 100),
                new Rectangle(650, 350, 100, 100),
                new Rectangle(850, 100, 100, 100),
                new Rectangle(850, 225, 100, 100),
                new Rectangle(850, 350, 100, 100),
            };

            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].Rect = rectangles[i];
            }
            
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            m_Input.Update();
            HandleInput(m_Input);

            if (runTimer)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            mouseState = Mouse.GetState();

            ButtonState currState = mouseState.LeftButton;
            if (currState == ButtonState.Pressed && currState != prevState)
            {
                var mousePosition = new Point((int) mouseManager.MousePos.X, (int) mouseManager.MousePos.Y);

                foreach (Rectangle r in rectangles)
                {
                    if (r.Contains(mousePosition))
                    {
                        foreach (Card card in cards)
                        {
                            if (card.Rect.Intersects(r) && !card.Flipped)
                            {
                                if (cardsFlipped == 0)
                                {
                                    card.Flip();
                                    cardsFlipped++;
                                    card1 = cards.IndexOf(card);
                                }
                                else if (cardsFlipped == 1 && cards.IndexOf(card) != card1)
                                {
                                    card.Flip();
                                    cardsFlipped++;
                                    card2 = cards.IndexOf(card);
                                }
                            }
                        }
                    }
                }

                prevState = currState;
            }

            if (currState == ButtonState.Released)
            {
                prevState = currState;
            }

            if (cardsFlipped == 2)
            {
                elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (elapsedTime >= 1000)
                {

                    if (cards[card1].PairId == cards[card2].PairId)
                    {
                        cards[card1].Visible = false;
                        cards[card2].Visible = false;

                    }
                    else if (cards[card1].PairId != cards[card2].PairId)
                    {
                        cards[card1].Flip();
                        cards[card2].Flip();
                    }

                    int count = 0;

                    foreach (Card c in cards)
                    {
                        if (c.Flipped)
                        {
                            count++;
                        }
                    }
                    if (count == 12)// user wins
                    {
                        runTimer = false;
                        string username = Environment.UserName;
                        float time = timer;
                        Player p = new Player(username, (int)time);
                        SavePlayer(p);

                    }
                    cardsFlipped = 0;
                    elapsedTime = 0;

                }
            }
        }

        public void SavePlayer(Player p)
        {
            const string filename = "score.json";
            var score = ScreenManager.Game.Services.GetService<IHighScoreTable>();
            List<Player> players = new List<Player>();
            if (File.Exists(filename))
            {
                players = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(filename));
            }
            players.Add(p);
            Materialize.SerializeFile(filename, players);
            score.AddHighScore("BestTime", (uint) p.Time, p.Name);
            ExitScreen();
            ScreenManager.AddScreen(new HighScoreScreen("HighScore", "BestTime", 10), PlayerIndex.One);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);

            foreach (Card card in cards)
            {
                card.Draw(spriteBatch);
            }

            spriteBatch.DrawString(font, timer.ToString("0.00"), new Vector2(10, 10), Color.PeachPuff);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void ExitScreen()
        {
            base.ExitScreen();
            ScreenManager.AddScreen(new IScreen[]{ new BackgroundScreen(), new MainMenuScreen() });
        }

        public void HandleInput(InputState input)
        {
            if (CheckKeyDown(input, Keys.Escape))
            {
                ExitScreen();
            }
        }
        private bool CheckKeyDown(InputState rInputState, Keys myKey)
        {
            return (rInputState.CurrentKeyboardState.IsKeyDown(myKey) && rInputState.LastKeyboardState.IsKeyUp(myKey));
        }
    }
}
