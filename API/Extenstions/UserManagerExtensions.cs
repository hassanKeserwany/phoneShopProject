﻿using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace API.Extenstions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByUserByClaimsPrincipleWithAddressAsync(this UserManager<AppUser> input ,ClaimsPrincipal user)
        {
            var email =user?.Claims?.FirstOrDefault(x=>x.Type==ClaimTypes.Email)?.Value;

            return await input.Users.Include(x => x.address).SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<AppUser> FindByEmailFromClaimPrinciple(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);


        }
    }
}
