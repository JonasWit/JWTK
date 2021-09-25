﻿using System.Collections.Generic;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.RestaurantAppModels.Access.DataAccessModifiers;
using SystemyWP.Data.Models.RestaurantAppModels.Menus;

namespace SystemyWP.Data.Models.RestaurantAppModels.Access
{
    public class RestaurantAccessKey : AccessKeyBase<int>
    {
        public List<RestaurantAppMenu> RestaurantAppMenus { get; set; } = new();
        public List<RestaurantAppDataAccess> RestaurantAppDataAccesses { get; set; } = new();
    }
}