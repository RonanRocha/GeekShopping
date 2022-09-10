﻿using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShopping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDataContext _ctx;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(RoleManager<IdentityRole> role,
            UserManager<ApplicationUser> user,
            AppDataContext ctx)
        {
            _role = role;
            _user = user;
            _ctx = ctx;
        }

        public void Initialize()
        {
            if (_role.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser
            {
                UserName = "ronan-admin",
                Email = "ronan-admin@erudio.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (11) 94513-2343",
                FirstName = "Ronan",
                LastName = "Admin",
            };

            _user.CreateAsync(admin, "Erudio123$").GetAwaiter().GetResult();
            _user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();

            var adminClaims = _user.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
            }).GetAwaiter().GetResult();


            ApplicationUser client = new ApplicationUser
            {
                UserName = "ronan-client",
                Email = "ronan-admin@erudio.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (11) 94513-2343",
                FirstName = "Ronan",
                LastName = "Client",
            };

            _user.CreateAsync(client, "Erudio123$").GetAwaiter().GetResult();
            _user.AddToRoleAsync(client, IdentityConfiguration.Admin).GetAwaiter().GetResult();

            var clientClaims = _user.AddClaimsAsync(client, new Claim[]
            {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client),
            }).GetAwaiter().GetResult();
        }
    }
}
