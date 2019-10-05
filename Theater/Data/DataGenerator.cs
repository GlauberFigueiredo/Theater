using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Theater.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PlayContext(serviceProvider.GetRequiredService<DbContextOptions<PlayContext>>()))
            {
                if (context.Plays.Any())
                    return; //Data seeded

                context.Genres.AddRange(
                new Genre { Id = 1, Description = "tragedy", Cache = 40000, MaximumAudience = 30, AudienceAdditionalValue = 1000 },
                new Genre { Id = 2, Description = "comedy", Cache = 30000, MaximumAudience = 20, AudienceAdditionalValue = 500 });

                context.Plays.AddRange(
                new Play { Id = 1, Description = "hamlet", GenreId = 1 },
                new Play { Id = 2, Description = "as-like", GenreId = 2 },
                new Play { Id = 3, Description = "othelo", GenreId = 1 });

                context.Customers.Add(
                new Customer { Id = 1, Name = "Raphael Camargo" });

                context.Performances.AddRange(
                new Performance { CustomerId = 1, PlayId = 1, Audience = 55 },
                new Performance { CustomerId = 1, PlayId = 2, Audience = 30 });


                context.SaveChanges();
            }
        }
    }
}
