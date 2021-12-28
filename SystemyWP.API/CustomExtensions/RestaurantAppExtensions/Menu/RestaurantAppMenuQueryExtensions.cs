﻿using System.Linq;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Models.RestaurantAppModels.Menus;

namespace SystemyWP.API.CustomExtensions.RestaurantAppExtensions.Menu
{
    public static class RestaurantAppMenuQueryExtensions
    {
        public static IQueryable<RestaurantAppMenu> GetAllowedMenus(this IQueryable<RestaurantAppMenu> source,
            string userId, string role, AppDbContext context, bool active)
        {
            // var filteredSource = source
            //     .Where(menu =>
            //         context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).RestaurantAccessKey
            //             .ExpireDate >= DateTime.UtcNow &&
            //         menu.RestaurantAccessKeyId == context.Users
            //             .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).RestaurantAccessKey.Id);
            //
            // switch (role)
            // {
            //     case SystemyWpConstants.Roles.UserAdmin:
            //         source = filteredSource;
            //         break;
            //     case SystemyWpConstants.Roles.PortalAdmin:
            //         source = filteredSource;
            //         break;
            //     case SystemyWpConstants.Roles.User:
            //         source = filteredSource;
            //         break;
            // }

            return source;
        }

        public static IQueryable<RestaurantAppMenu> GetAllowedMenu(this IQueryable<RestaurantAppMenu> source,
            string userId, string role, long menuId, AppDbContext context, bool active)
        {
            // var filteredSource = source
            //     .Where(menu =>
            //         context.Users.FirstOrDefault(u => u.Id.Equals(userId)).RestaurantAccessKey.ExpireDate >= DateTime.UtcNow &&
            //         menu.Id == menuId &&
            //         menu.RestaurantAccessKeyId == context.Users
            //             .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).RestaurantAccessKey.Id);
            //
            // switch (role)
            // {
            //     case SystemyWpConstants.Roles.UserAdmin:
            //         source = filteredSource;
            //         break;
            //     case SystemyWpConstants.Roles.PortalAdmin:
            //         source = filteredSource;
            //         break;
            //     case SystemyWpConstants.Roles.User:
            //         source = filteredSource;
            //         break;
            // }

            return source;
        }
    }
}