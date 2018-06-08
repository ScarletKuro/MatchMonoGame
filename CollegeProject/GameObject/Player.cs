using System;

namespace CollegeProject.GameObject
{
    [Serializable]
    public class Player
    {
        public string Name { get; }

        public int Time { get; }

        public Player(string name, int time)
        {
            this.Name = name;
            this.Time = time;
        }
    }
}
