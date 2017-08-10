using System;
using System.Collections.Generic;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.Core.ReturnModel;

namespace KenBonny.Search.Core.Mediator
{
    public class SearchMediator : ISeatSearcher
    {
        private readonly Dictionary<Type, ISeatSearcher> _searchers = new Dictionary<Type, ISeatSearcher>();

        public void Register<T>(ISeatSearcher seatSearcher) where T : SearchQuery
        {
            var queryType = typeof(T);
            if (IsKnownType(queryType))
            {
                _searchers[queryType] = seatSearcher;
            }
            else
            {
                _searchers.Add(queryType, seatSearcher);
            }
        }

        public IReadOnlyCollection<Seat> FindSeats(SearchQuery query)
        {
            var queryType = query.GetType();

            if (IsUnknownType(queryType))
            {
                throw new ArgumentOutOfRangeException("Unknown query type: " + queryType);
            }

            return _searchers[queryType].FindSeats(query);
        }

        private bool IsKnownType(Type queryType)
        {
            return _searchers.ContainsKey(queryType);
        }

        private bool IsUnknownType(Type queryType)
        {
            return !_searchers.ContainsKey(queryType);
        }
    }
}