using InputHelper;
using MenuBuddy;
using Microsoft.Xna.Framework;

namespace CollegeProject.Screen
{
    public class MainMenuScreen  : MenuScreen, IMainMenu
    {
        public MainMenuScreen()
            : base("Main Menu")
        {
            base.Highlight = true;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            var startMenuEntry = new MenuEntry("Start");
            startMenuEntry.OnClick += (sender, e) =>
            {
                LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
            };
            AddMenuEntry(startMenuEntry);

            var highScoreEntry = new MenuEntry("High Score");
            highScoreEntry.OnClick += (sender, e) =>
            {
                ScreenManager.AddScreen(
                    new IScreen[] {new HighScoreScreen("HighScore", "BestTime", 10)},
                    PlayerIndex.One);
            };
            AddMenuEntry(highScoreEntry);

            var exitMenuEntry = new MenuEntry("Exit");
            exitMenuEntry.OnClick += OnExit;
            AddMenuEntry(exitMenuEntry);
        }

        protected void OnExit(object sender, ClickEventArgs e)
        {
            const string message = "Are you sure you want to exit?";
            var confirmExitMessageBox = new MessageBoxScreen(message);
            confirmExitMessageBox.OnSelect += ConfirmExitMessageBoxAccepted;
            ScreenManager.AddScreen(confirmExitMessageBox, e.PlayerIndex);
        }

        private void ConfirmExitMessageBoxAccepted(object sender, ClickEventArgs e)
        {
            ScreenManager.Game.Exit();
        }
    }
}
