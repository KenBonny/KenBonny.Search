using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KenBonny.Search.Core;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.Core.ReturnModel;
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
            ISeatSearcher search = new ConfigurableSeatSearcher(new SpecificRestaurantRepository(), new[] {new EmptySeatsFilter()},
                new[] {new SameFamilyScoreCalculator()}, new[] {new PreferTablesWithGuestsSorter()});
            var query = new UnreservedSeatInRestaurantQuery("De Peirdestal");

            var seats = search.FindSeats(query);

            ValidatePeirdestal(seats);
        }

        [Fact]
        public void LookForSeatForDiner()
        {
            ISeatSearcher search = new ConfigurableSeatSearcher(new AllRestaurantRepository(), new[] {new EmptySeatsFilter()},
                new[] {new SameFamilyScoreCalculator()}, new[] {new PreferTablesWithGuestsSorter()});
            var query = new UnreservedSeatForDinerQuery("Dwayne", "Johnson");

            var seats = search.FindSeats(query);

            ValidateMultipleRestaurants(seats);
        }

        [Fact]
        public void CheckForReservationForDinerWithReservation()
        {
            ISeatSearcher search = new ConfigurableSeatSearcher(new AllRestaurantRepository(), new[] { new EmptySeatsFilter() },
                new[] { new SameFamilyScoreCalculator() }, new[] { new PreferTablesWithGuestsSorter() });
            ISeatSearcher reservationChecker = new ReservationCheckerDecorator(search, new ReservationRepository());
            var firstName = "John";
            var lastName = "Boyega";
            var query = new UnreservedSeatForDinerQuery(firstName, lastName);

            var seats = reservationChecker.FindSeats(query);

            seats.Should().HaveCount(1);
            var seat = seats.First();
            seat.Restaurant.Should().Be("De Peirdestal");
            seat.SectionId.Should().Be(0);
            seat.TableId.Should().Be(2);
        }

        [Fact]
        public void CheckForReservationForDinerWithoutReservation()
        {
            ISeatSearcher search = new ConfigurableSeatSearcher(new AllRestaurantRepository(), new[] { new EmptySeatsFilter() },
                new[] { new SameFamilyScoreCalculator() }, new[] { new PreferTablesWithGuestsSorter() });
            var query = new UnreservedSeatForDinerQuery("Dwayne", "Johnson");
            ISeatSearcher reservationChecker = new ReservationCheckerDecorator(search, new ReservationRepository());

            var seats = reservationChecker.FindSeats(query);

            ValidateMultipleRestaurants(seats);
        }

        [Fact]
        public void RouteQueryToCorrectSearcher()
        {
            ISeatSearcher searchAcrossRestaurants = new ConfigurableSeatSearcher(new SpecificRestaurantRepository(), new[] { new EmptySeatsFilter() },
                new[] { new SameFamilyScoreCalculator() }, new[] { new PreferTablesWithGuestsSorter() });
            ISeatSearcher searchSpecificRestaurant = new ConfigurableSeatSearcher(new AllRestaurantRepository(), new[] { new EmptySeatsFilter() },
                new[] { new SameFamilyScoreCalculator() }, new[] { new PreferTablesWithGuestsSorter() });
            var mediator = new SearchMediator();
            mediator.Register<UnreservedSeatInRestaurantQuery>(searchAcrossRestaurants);
            mediator.Register<UnreservedSeatForDinerQuery>(searchSpecificRestaurant);

            var searcher = (ISeatSearcher) mediator;

            var unreservedSeatForDinerQuery = new UnreservedSeatForDinerQuery("Dwayne", "Johnson");
            var seats = searcher.FindSeats(unreservedSeatForDinerQuery);

            ValidateMultipleRestaurants(seats);

            var query = new UnreservedSeatInRestaurantQuery("De Peirdestal");
            seats = searcher.FindSeats(query);

            ValidatePeirdestal(seats);
        }

        private void ValidatePeirdestal(IReadOnlyCollection<Seat> foundSeats)
        {
            foundSeats.Should().HaveCount(38, "Should only be the empty seats from 'De Peirdestal'");
            var seat = foundSeats.First();
            seat.Should().NotBeNull();
            seat.Restaurant.Should().Be("De Peirdestal");
            seat.SectionId.Should().Be(0, "Should be Joxers section");
            seat.TableId.Should().Be(2, "Should be table with people");
        }
        
        private void ValidateMultipleRestaurants(IReadOnlyCollection<Seat> foundSeats)
        {
            foundSeats.Should().HaveCount(58, "Should be all empty seats in both restaurants");
            var seat = foundSeats.First();
            seat.Restaurant.Should().Be("Come Chez Moi");
            seat.SectionId.Should().Be(0, "Should be Ken's section");
            seat.TableId.Should().Be(1, "Should be the table with no people sitting at it.");
        }
    }
}