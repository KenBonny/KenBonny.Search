using System;
using System.Collections.Generic;
using System.Linq;
using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DefaultImplementation
{
    internal class SeatWithScore
    {
        private Dictionary<Type, int> _scores;

        public SeatWithScore(ISeat seat)
        {
            Seat = seat;
            _scores = new Dictionary<Type, int>();
        }

        public ISeat Seat { get; private set; }

        public int TotalScore => _scores.Sum(pair => pair.Value);

        public IReadOnlyDictionary<Type, int> Scores => _scores;
        
        public void AddScore(int score, IScoreCalculator calculator)
        {
            var type = calculator.GetType();
            if (_scores.ContainsKey(type))
            {
                _scores[type] = score;
            }
            else
            {
                _scores.Add(type, score);                
            }
        }
    }
}