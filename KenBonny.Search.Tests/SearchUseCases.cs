using System;
using System.Linq;
using FluentAssertions;
using KenBonny.Search.Core;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.DataAccess;
using KenBonny.Search.DefaultImplementation;
using KenBonny.Search.DefaultImplementation.Filters;
using KenBonny.Search.DefaultImplementation.ScoreCalculators;
using KenBonny.Search.DefaultImplementation.Sorters;
using Xunit;

namespace KenBonny.Search.Tests
{
    public class SearchUseCases
    {
        [Fact]
        public void LookForSeatInRestaurant()
        {
            ISearcher search = new ConfigurableSearch(new SpecificRestaurantRepository(), new[] {new EmptySeatsFilter()},
                new[] {new SameFamilyScoreCalculator()}, new[] {new PreferTablesWithGuestsSorter()});
            var query = new UnreservedSeatInRestaurantQuery("De Peirdestal");

            var seats = search.FindSeats(query);

            seats.Should().HaveCount(38, "Should only be the empty seats from 'De Peirdestal'");
            seats.First().Table.Id.Should().Be(2, "Should be the table with people sitting at it.");
            seats.First().Table.Section.Servers.Should().Contain(s => s.Name.Equals("Joxer"));
        }
    }
}