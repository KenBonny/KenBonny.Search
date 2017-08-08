using System;
using System.Collections.Generic;
using KenBonny.Search.Core;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.Core.ReadModel;

namespace KenBonny.Search.DefaultImplementation.Mediator
{
    public class SearchMediator : ISearcher
    {
        private readonly Dictionary<Type, ISearcher> _searchers = new Dictionary<Type, ISearcher>();

        public void Register<T>(ISearcher searcher) where T : SearchQuery
        {
            var queryType = typeof(T);
            if (IsKnownType(queryType))
            {
                _searchers[queryType] = searcher;
            }
            else
            {
                _searchers.Add(queryType, searcher);
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