using System;
using System.Collections.Generic;
using CollegeProject.GameObject;
using MenuBuddy;
using Microsoft.Xna.Framework.Graphics;

namespace CollegeProject
{
    public class Deck
    {
        private readonly List<Pair> _deck;
        private readonly List<Card> _cardsShuffled;

        public Deck(IScreen screen)
        {
            _deck = new List<Pair>();
            _cardsShuffled = new List<Card>();
            var cardback = screen.ScreenManager.Game.Content.Load<Texture2D>("mainBack");
            for (int i = 1; i <= 25; i++)
            {
                
                var texture = screen.ScreenManager.Game.Content.Load<Texture2D>(i.ToString());
                var cardone = new Card(texture, cardback, i);
                var cardtwo = new Card(texture, cardback, i);
                var pair = new Pair(cardone, cardtwo);
                _deck.Add(pair);
            }

            Shuffle(_deck);
            _cardsShuffled = GetCardsFromPairs();
            Shuffle(_cardsShuffled);
            
        }
        private void Shuffle<T>(List<T> inputList)
        {
            Random rng = new Random();
            int n = inputList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = inputList[k];
                inputList[k] = inputList[n];
                inputList[n] = value;
            }
        }
        private List<Card> GetCardsFromPairs()
        {
            List<Card> cards = new List<Card>();

            foreach (Pair p in _deck)
            {
                cards.Add(p.First);
                cards.Add(p.Second);
            }
            cards.RemoveRange(12, cards.Count - 12);
            
            return cards;
        }
        public List<Card> GetCardsShuffled()
        {
            return _cardsShuffled;
        }
        
    }
}
