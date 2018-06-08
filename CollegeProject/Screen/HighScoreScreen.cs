using HighScoreBuddy;

namespace CollegeProject.Screen
{
    public class HighScoreScreen : HighScoreTableScreen
    {
        public HighScoreScreen(string screenTitle, string highScoreList, int num) : base(screenTitle, highScoreList, num)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            this.AddCancelButton();
        }
    }
}
