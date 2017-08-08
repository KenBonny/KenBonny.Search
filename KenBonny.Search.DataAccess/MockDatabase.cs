﻿using System.Collections.Generic;
using KenBonny.Search.Core.ReadModel;

namespace KenBonny.Search.DataAccess
{
    public class MockDatabase
    {
        public static IList<Restaurant> Restaurants()
        {
            return RestaurantsBuilder.Init()
                .AddRestaurant("Come Chez Moi")
                .AddSection("Ken")
                .AddTable(2)
                .AddTable(4)
                .AddTable(4)
                .AddSection("Kelly")
                .AddTable(4)
                .AddTable(8)
                .AddRestaurant("De Peirdestal")
                .AddSection("Joxer")
                .AddTable(2)
                .AddTable(2)
                .AddTable(4)
                .AddTable(4)
                .AddSection("Danny")
                .AddTable(6)
                .AddTable(6)
                .AddSection("Stijn")
                .AddTable(4)
                .AddTable(6)
                .AddTable(6)
                .BuildRestaurants();
        }
    }
}