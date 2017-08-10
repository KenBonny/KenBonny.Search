using System.Collections.Generic;
using System.Linq;
using KenBonny.Search.Core;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DefaultImplementation
{
    public class ConfigurableSeatSearcher : ISeatSearcher
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IEnumerable<IFilter> _filters;
        private readonly IEnumerable<IScoreCalculator> _scoreCalculators;
        private readonly IEnumerable<ISorter> _sorters;

        public ConfigurableSeatSearcher(IRestaurantRepository restaurantRepository, IEnumerable<IFilter> filters,
            IEnumerable<IScoreCalculator> scoreCalculators, IEnumerable<ISorter> sorters)
        {
            _restaurantRepository = restaurantRepository;
            _filters = filters;
            _scoreCalculators = scoreCalculators;
            _sorters = sorters;
        }

        public IReadOnlyCollection<Core.ReturnModel.Seat> FindSeats(SearchQuery query)
        {
            var availableSeats = GetAvailableSeats(query);
            var scoredSeats = ScoreSeats(query, availableSeats);
            var orderedSeats = OrderSeats(query, scoredSeats);

            return MapResult(orderedSeats);
        }

        private static List<Core.ReturnModel.Seat> MapResult(IEnumerable<ISeat> orderedSeats)
        {
            return orderedSeats.Select(seat =>
                    new Core.ReturnModel.Seat(seat.Table.Section.Restaurant.Name, seat.Table.Section.Id, seat.Table.Id))
                .ToList();
        }

        private IEnumerable<ISeat> OrderSeats(SearchQuery query, List<SeatWithScore> scoredSeats)
        {
            var orderedSeats = query.SortOrder == SortOrder.BestFirst
                ? OrderForBestResult(scoredSeats)
                : OrderForWorstResult(scoredSeats);
            return orderedSeats.Select(s => s.Seat);
        }

        private IOrderedEnumerable<SeatWithScore> OrderForWorstResult(List<SeatWithScore> scoredSeats)
        {
            IOrderedEnumerable<SeatWithScore> orderedSeats;
            orderedSeats = scoredSeats.OrderByDescending(s => s.TotalScore);

            foreach (var sorter in _sorters)
            {
                orderedSeats = orderedSeats.ThenByDescending(s => s.Seat, sorter);
            }
            return orderedSeats;
        }

        private IOrderedEnumerable<SeatWithScore> OrderForBestResult(List<SeatWithScore> scoredSeats)
        {
            IOrderedEnumerable<SeatWithScore> orderedSeats;
            orderedSeats = scoredSeats.OrderBy(s => s.TotalScore);

            foreach (var sorter in _sorters)
            {
                orderedSeats = orderedSeats.ThenBy(s => s.Seat, sorter);
            }
            return orderedSeats;
        }

        private List<SeatWithScore> ScoreSeats(SearchQuery query, IEnumerable<ISeat> availableSeats)
        {
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
            return scoredSeats;
        }

        private IEnumerable<ISeat> GetAvailableSeats(SearchQuery query)
        {
            var allRestaurants = _restaurantRepository.FindRestaurants(query);
            var availableSeats = allRestaurants.SelectMany(restaurant => restaurant.Sections)
                .SelectMany(section => section.Tables).SelectMany(table => table.Seats);

            foreach (var filter in _filters)
            {
                availableSeats = filter.RemoveUnwantedSeats(availableSeats);
            }

            return availableSeats;
        }
    }
}