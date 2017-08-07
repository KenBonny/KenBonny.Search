using System;
using System.Collections.Generic;
using System.Linq;
using KenBonny.Search.Core;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.Core.ReadModel;

namespace KenBonny.Search.DefaultImplementation
{
    public class ConfigurableSearch : ISearcher
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IEnumerable<IFilter> _filters;
        private readonly IEnumerable<IScoreCalculator> _scoreCalculators;
        private readonly IEnumerable<ISorter> _sorters;

        public ConfigurableSearch(IRestaurantRepository restaurantRepository, IEnumerable<IFilter> filters,
            IEnumerable<IScoreCalculator> scoreCalculators, IEnumerable<ISorter> sorters)
        {
            _restaurantRepository = restaurantRepository;
            _filters = filters;
            _scoreCalculators = scoreCalculators;
            _sorters = sorters;
        }
        
        public IReadOnlyCollection<Seat> FindSeats(SearchQuery query)
        {
            var allRestaurants = _restaurantRepository.FindRestaurants(query);
            IList<Seat> availableSeats = allRestaurants.SelectMany(restaurant => restaurant.Section)
                .SelectMany(section => section.Tables).SelectMany(table => table.Seats).ToList();
            foreach (var filter in _filters)
            {
                availableSeats = filter.RemoveUnwantedSeats(availableSeats);
            }

            var scoredSeats = new List<SeatWithScore>();
            foreach (var seat in availableSeats)
            {
                var seatWithScore = new SeatWithScore(seat);

                foreach (var calculator in _scoreCalculators)
                {
                    var score = calculator.CalculateScore(seat, query);
                    seatWithScore.AddScore(score, calculator);
                }
                
                scoredSeats.Add(seatWithScore);
            }
            
            // todo add sorting
            return availableSeats.ToList();
        }
    }

    internal class SeatWithScore
    {
        private Dictionary<Type, int> _scores;

        public SeatWithScore(Seat seat)
        {
            Seat = seat;
            _scores = new Dictionary<Type, int>();
        }

        public Seat Seat { get; private set; }

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