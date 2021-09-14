using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScreenplayApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScreenplayApp.Infrastructure.Data.Seed
{
    public class Seed
    {
        public static async Task SeedScreenplays(DataContext context)
        {
            if (await context.Screenplays.AnyAsync()) return;

            var screenplayData = await System.IO.File.ReadAllTextAsync("../ScreenplayApp.Infrastructure/Data/Seed/ScreenplaySeedData.json");
            var screenplays = JsonSerializer.Deserialize<List<Screenplay>>(screenplayData);

            foreach (var screenplay in screenplays)
            {
                context.Screenplays.Add(screenplay);
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedActors(DataContext context)
        {
            if (await context.Actors.AnyAsync()) return;

            var actorData = await System.IO.File.ReadAllTextAsync("../ScreenplayApp.Infrastructure/Data/Seed/ActorSeedData.json");
            var actors = JsonSerializer.Deserialize<List<Actor>>(actorData);

            foreach (var actor in actors)
            {
                context.Actors.Add(actor);
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedRatings(DataContext context)
        {
            if (await context.Ratings.AnyAsync()) return;

            var ratingData = await System.IO.File.ReadAllTextAsync("../ScreenplayApp.Infrastructure/Data/Seed/RatingSeedData.json");
            var ratings = JsonSerializer.Deserialize<List<Rating>>(ratingData);

            foreach (var rating in ratings)
            {
                context.Ratings.Add(new Rating { 
                    Screenplay = rating.Screenplay,
                    Rate = rating.Rate
                });
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var roles = new List<AppRole>
            {
                new AppRole { Name = "Consumer"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            var consumer = new AppUser
            {
                UserName = "user"
            };

            await userManager.CreateAsync(consumer, "Test123!");
            await userManager.AddToRoleAsync(consumer, "Consumer");
        }
    }
}
