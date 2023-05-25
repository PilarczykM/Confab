﻿using Confab.Modules.Users.Core.DAL;
using Confab.Modules.Users.Core.DAL.Repositories;
using Confab.Modules.Users.Core.Entity;
using Confab.Modules.Users.Core.Repositories;
using Confab.Modules.Users.Core.Services;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Users.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddPostgres<UsersDbContext>();

            return services;
        }
    }
}

