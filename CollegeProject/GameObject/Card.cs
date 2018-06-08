using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CollegeProject.GameObject
{
    public class Card
    {
        public Texture2D FrontImage { get; }

        public Texture2D BackImage { get; }

        public int PairId { get; }

        public bool Flipped { get; set; }

        public Rectangle Rect { get; set; }

        public Boolean Visible { get; set; }

        public Card(Texture2D frontimage, Texture2D backimage, int pairId)
        {
            this.FrontImage = frontimage;
            this.BackImage = backimage;
            this.PairId = pairId;
            this.Visible = true;
            this.Flipped = false;
        }

        public void Flip()
        {
            Flipped = !Flipped;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.Visible)
            {
                spriteBatch.Draw(this.Flipped ? this.FrontImage : this.BackImage, this.Rect, Color.White);
            }
        }
    }
}
