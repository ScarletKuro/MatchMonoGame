using System.Collections.Generic;
using CollegeProject.GameObject;
using CollegeProject.Screen;
using CollegeProject.Utility;
using HighScoreBuddy;
using MenuBuddy;

namespace CollegeProject
{
    public class Game2 : MouseGame
    {
        public Game2()
        {
            //FullScreen = true;

            //var debug = new DebugInputComponent(this, ResolutionBuddy.Resolution.TransformationMatrix);
            //debug.DrawOrder = 1;
        }
        public override IScreen[] GetMainMenuScreenStack()
        {
            return new IScreen[] { new BackgroundScreen(), new MainMenuScreen()};
        }

        protected override void Initialize()
        {
            base.Initialize();
            var table = new HighScoreTableComponent(this, new MemHighScoreTable());

            var players = LoadPlayers();
            foreach (var player in players)
            {
                table.AddHighScore("BestTime", (uint)player.Time, player.Name);
            }
            IsMouseVisible = true;
        }

        public List<Player> LoadPlayers()
        {
            const string filename = "score.json";
            var value = Materialize.DeserializeFile<List<Player>>(filename);
            return value ?? new List<Player>();
        }
    }
}
