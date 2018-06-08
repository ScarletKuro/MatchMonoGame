using FontBuddyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;

namespace CollegeProject.Screen
{
    public class BackgroundScreen : MenuBuddy.Screen
    {
        #region Member Variables

        private readonly RainbowTextBuddy _titleText;

        #endregion //Member Variables

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public BackgroundScreen()
        {
            Transition.OnTime = 0.5f;
            Transition.OffTime = 0.5f;

            _titleText = new RainbowTextBuddy();
        }

        /// <summary>
        /// Loads graphics content for this screen. The background texture is quite
        /// big, so we use our own local ContentManager to load it. This allows us
        /// to unload before going from the menus into the game itself, wheras if we
        /// used the shared ContentManager provided by the Game class, the content
        /// would remain loaded forever.
        /// </summary>
        public override void LoadContent()
        {
            _titleText.Font = ScreenManager.Game.Content.Load<SpriteFont>(@"Fonts\ArialBlack48");
            _titleText.ShadowOffset = new Vector2(-5.0f, 3.0f);
            _titleText.ShadowSize = 1.025f;
            _titleText.RainbowSpeed = 4.0f;

            base.LoadContent();
        }

        /// <summary>
        /// Unloads graphics content for this screen.
        /// </summary>
        public override void UnloadContent()
        {
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Draws the background screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            ScreenManager.SpriteBatchBegin();

            //Draw the game title!
            _titleText.ShadowColor = new Color(0.15f, 0.15f, 0.15f, Transition.Alpha);
            _titleText.Write("Mono Game Project",
                new Vector2(Resolution.TitleSafeArea.Center.X, Resolution.TitleSafeArea.Center.Y * 0.05f),
                Justify.Center,
                1.5f,
                new Color(0.85f, 0.85f, 0.85f, Transition.Alpha),
                spriteBatch,
                Time);


            ScreenManager.SpriteBatchEnd();
        }

        #endregion
    }
}
