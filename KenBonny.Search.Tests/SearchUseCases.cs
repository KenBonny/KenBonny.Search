using System;
using System.Linq;
using FluentAssertions;
using KenBonny.Search.Core;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.DataAccess;
using KenBonny.Search.DefaultImplementation;
using KenBonny.Search.DefaultImplementation.Decorators;
using KenBonny.Search.DefaultImplementation.Filters;
using KenBonny.Search.DefaultImplementation.Mediator;
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
            ISearcher search = new ConfigurableSearcher(new SpecificRestaurantRepository(), new[] {new EmptySeatsFilter()},
                new[] {new SameFamilyScoreCalculator()}, new[] {new PreferTablesWithGuestsSorter()});
            var query = new UnreservedSeatInRestaurantQuery("De Peirdestal");

            var seats = search.FindSeats(query);

            seats.Should().HaveCount(38, "Should only be the empty seats from 'De Peirdestal'");
            seats.First().Table.Id.Should().Be(2, "Should be the table with people sitting at it.");
            seats.First().Table.Section.Servers.Should().Contain(s => s.Name.Equals("Joxer"));
        }

        [Fact]
        public void LookForSeatForDiner()
        {
            ISearcher search = new ConfigurableSearcher(new AllRestaurantRepository(), new[] {new EmptySeatsFilter()},
                new[] {new SameFamilyScoreCalculator()}, new[] {new PreferTablesWithGuestsSorter()});
            var query = new UnreservedSeatForDinerQuery("Dwayne", "Johnson");

            var seats = search.FindSeats(query);

            seats.Should().HaveCount(58, "Should only be the empty seats from 'De Peirdestal'");
            seats.First().Table.Id.Should().Be(2, "Should be the table with people sitting at it.");
            seats.First().Table.Section.Servers.Should().Contain(s => s.Name.Equals("Joxer"));
        }

        [Fact]
        public void CheckForReservationForDinerWithReservation()
        {
            ISearcher search = new ConfigurableSearcher(new AllRestaurantRepository(), new[] { new EmptySeatsFilter() },
                new[] { new SameFamilyScoreCalculator() }, new[] { new PreferTablesWithGuestsSorter() });
            ISearcher reservationChecker = new ReservationCheckerDecorator(search, new ReservationRepository());
            var firstName = "John";
            var lastName = "Boyega";
            var query = new UnreservedSeatForDinerQuery(firstName, lastName);

            var seats = reservationChecker.FindSeats(query);

            seats.Should().HaveCount(1);

            var diner = seats.First().Diner;
            diner.Should().NotBeNull();
            diner.FirstName.Should().Be(firstName);
            diner.LastName.Should().Be(lastName);
        }

        [Fact]
        public void CheckForReservationForDinerWithoutReservation()
        {
            ISearcher search = new ConfigurableSearcher(new AllRestaurantRepository(), new[] { new EmptySeatsFilter() },
                new[] { new SameFamilyScoreCalculator() }, new[] { new PreferTablesWithGuestsSorter() });
            var query = new UnreservedSeatForDinerQuery("Dwayne", "Johnson");
            ISearcher reservationChecker = new ReservationCheckerDecorator(search, new ReservationRepository());

            var seats = reservationChecker.FindSeats(query);

            seats.Should().HaveCount(58, "Should only be the empty seats from 'De Peirdestal'");
            seats.First().Table.Id.Should().Be(2, "Should be the table with people sitting at it.");
            seats.First().Table.Section.Servers.Should().Contain(s => s.Name.Equals("Joxer"));
        }

        [Fact]
        public void RouteQueryToCorrectSearcher()
        {
            ISearcher searchAcrossRestaurants = new ConfigurableSearcher(new SpecificRestaurantRepository(), new[] { new EmptySeatsFilter() },
                new[] { new SameFamilyScoreCalculator() }, new[] { new PreferTablesWithGuestsSorter() });
            ISearcher searchSpecificRestaurant = new ConfigurableSearcher(new AllRestaurantRepository(), new[] { new EmptySeatsFilter() },
                new[] { new SameFamilyScoreCalculator() }, new[] { new PreferTablesWithGuestsSorter() });
            var mediator = new SearchMediator();
            mediator.Register<UnreservedSeatInRestaurantQuery>(searchAcrossRestaurants);
            mediator.Register<UnreservedSeatForDinerQuery>(searchSpecificRestaurant);

            var searcher = (ISearcher) mediator;

            var unreservedSeatForDinerQuery = new UnreservedSeatForDinerQuery("Dwayne", "Johnson");
            var seats = searcher.FindSeats(unreservedSeatForDinerQuery);

            seats.Should().HaveCount(58, "Should only be the empty seats from 'De Peirdestal'");
            seats.First().Table.Id.Should().Be(2, "Should be the table with people sitting at it.");
            seats.First().Table.Section.Servers.Should().Contain(s => s.Name.Equals("Joxer"));

            var query = new UnreservedSeatInRestaurantQuery("De Peirdestal");
            seats = searcher.FindSeats(query);

            seats.Should().HaveCount(38, "Should only be the empty seats from 'De Peirdestal'");
            seats.First().Table.Id.Should().Be(2, "Should be the table with people sitting at it.");
            seats.First().Table.Section.Servers.Should().Contain(s => s.Name.Equals("Joxer"));
        }
    }
}