using Microsoft.AspNetCore.Identity;
using Models;

namespace Database;

public class Seed
{
    public static async Task SeedData(UserManager<AppUser> userManager, DataContext context)
    {
        if (!userManager.Users.Any())
        {
            var users = new List<AppUser>
            {
                new AppUser
                {
                    DisplayName = "Bob Beta",
                    UserName = "bobeta",
                    Email = "bob@test.com",
                },
                new AppUser
                {
                    DisplayName = "Jimy Tester",
                    UserName = "jimytester",
                    Email = "jimy@test.com",
                },
                new AppUser
                {
                    DisplayName = "Admin Master",
                    UserName = "adminmaster",
                    Email = "admin@test.com",
                }
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }

        if (!context.Cities.Any())
        {
            var cities = new List<City>
            {
                new City
                {
                    Name = "Alcácer do Sal",
                    Locations = 16,
                    Detail = new CityDetail
                    {
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean dictum, ex sed consequat feugiat, nibh ante egestas mi, ut egestas tortor turpis id lectus.",
                        SubDescription = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                    }
                },
                new City
                {
                    Name = "Coimbra",
                    Locations = 54,
                    Detail = new CityDetail
                    {
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean dictum, ex sed consequat feugiat, nibh ante egestas mi, ut egestas tortor turpis id lectus.",
                        SubDescription = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                    }
                },
                new City
                {
                    Name = "Amadora",
                    Locations = 24,
                    Detail = new CityDetail
                    {
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean dictum, ex sed consequat feugiat, nibh ante egestas mi, ut egestas tortor turpis id lectus.",
                        SubDescription = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                    }
                },
                new City
                {
                    Name = "Portimão",
                    Locations = 38,
                    Detail = new CityDetail
                    {
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean dictum, ex sed consequat feugiat, nibh ante egestas mi, ut egestas tortor turpis id lectus.",
                        SubDescription = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                    }
                },
                new City
                {
                    Name = "Faro",
                    Locations = 26,
                    Detail = new CityDetail
                    {
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean dictum, ex sed consequat feugiat, nibh ante egestas mi, ut egestas tortor turpis id lectus.",
                        SubDescription = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                    }
                },
                new City
                {
                    Name = "Loulé",
                    Locations = 17,
                    Detail = new CityDetail
                    {
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean dictum, ex sed consequat feugiat, nibh ante egestas mi, ut egestas tortor turpis id lectus.",
                        SubDescription = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                    }
                },
                new City
                {
                    Name = "Porto",
                    Locations = 88,
                    Detail = new CityDetail
                    {
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean dictum, ex sed consequat feugiat, nibh ante egestas mi, ut egestas tortor turpis id lectus.",
                        SubDescription = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                    }
                },
                new City
                {
                    Name = "Viseu",
                    Locations = 65,
                    Detail = new CityDetail
                    {
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean dictum, ex sed consequat feugiat, nibh ante egestas mi, ut egestas tortor turpis id lectus.",
                        SubDescription = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                    }
                },
                new City
                {
                    Name = "Lagos",
                    Locations = 49,
                    Detail = new CityDetail
                    {
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean dictum, ex sed consequat feugiat, nibh ante egestas mi, ut egestas tortor turpis id lectus.",
                        SubDescription = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                    }
                },
                new City
                {
                    Name = "Castelo Branco",
                    Locations = 44,
                    Detail = new CityDetail
                    {
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean dictum, ex sed consequat feugiat, nibh ante egestas mi, ut egestas tortor turpis id lectus.",
                        SubDescription = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                    }
                },
                new City
                {
                    Name = "Évora",
                    Locations = 32,
                    Detail = new CityDetail
                    {
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean dictum, ex sed consequat feugiat, nibh ante egestas mi, ut egestas tortor turpis id lectus.",
                        SubDescription = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                    }
                },
                new City
                {
                    Name = "Serpa",
                    Locations = 28,
                    Detail = new CityDetail
                    {
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean dictum, ex sed consequat feugiat, nibh ante egestas mi, ut egestas tortor turpis id lectus.",
                        SubDescription = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                    }
                },
            };

            await context.Cities.AddRangeAsync(cities);
            await context.SaveChangesAsync();
        }
    }
}
