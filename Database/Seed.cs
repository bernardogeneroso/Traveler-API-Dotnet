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

        if (!context.Cities.Any() && !context.CategoriesCities.Any() && !context.CitiesPlaces.Any())
        {
            var cities = new List<City>
            {
                new City
                {
                    Name = "Lagoa",
                    Locations = 36,
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

            context.Cities.AddRange(cities);

            var categories = new List<CategoryCity>
            {
                new CategoryCity
                {
                    Name = "Comida e Bebida",
                },
                new CategoryCity
                {
                    Name = "Pontos Turísticos",
                },
                new CategoryCity
                {
                    Name = "Eventos Organizados",
                },
            };

            context.CategoriesCities.AddRange(categories);

            var cityPlaces = new List<CityPlace>
            {
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "Doce & Companhia",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "O Pequeno Café",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "O Pão de Queijo",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "Estrada Café",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "Café da Manhã",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "Bela Vida Café",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "Café a gosto",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "Café Creme",
                },

                new CityPlace
                {
                    City = cities[0],
                    Category = categories[1],
                    Name = "Torre de Londres",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[1],
                    Name = "Templo Wat Pho",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[1],
                    Name = "Burj Khalifa",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[1],
                    Name = "Pagode Shwedagon",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[1],
                    Name = "Sydney Opera House",
                },

                new CityPlace
                {
                    City = cities[0],
                    Category = categories[2],
                    Name = "Festival do amanhã",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[2],
                    Name = "The Point Arrow",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[2],
                    Name = "Festival dos Canários",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[2],
                    Name = "Festival de Lagoa",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[2],
                    Name = "Fuji Rock Festival",
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[2],
                    Name = "Expo Tattoo Floripa",
                },

                new CityPlace
                {
                    City = cities[1],
                    Category = categories[0],
                    Name = "Café da Madeira",
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[0],
                    Name = "Café Quentinho",
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[0],
                    Name = "Café de Londres",
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[0],
                    Name = "Café do Amanhã",
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[0],
                    Name = "Café da Noite",
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[0],
                    Name = "Doce & Café",
                },

                new CityPlace
                {
                    City = cities[1],
                    Category = categories[1],
                    Name = "Le Grande",
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[1],
                    Name = "La Baguette",
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[1],
                    Name = "Le Petit",
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[1],
                    Name = "The paradise",
                },

                new CityPlace
                {
                    City = cities[1],
                    Category = categories[2],
                    Name = "Festival do dia",
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[2],
                    Name = "Expo International Festival",
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[2],
                    Name = "Festival da Cidade",
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[2],
                    Name = "Festival da Amadora",
                },
            };

            categories.ForEach(x =>
            {
                cityPlaces.ForEach(y =>
                {
                    if (x.Name == y.Category.Name)
                    {
                        x.Places++;
                    }
                });
            });

            context.CitiesPlaces.AddRange(cityPlaces);

            await context.SaveChangesAsync();
        }
    }
}
