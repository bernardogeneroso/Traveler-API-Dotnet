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

        if (!context.City.Any() && !context.CategoryCity.Any() && !context.CityPlace.Any())
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

            context.City.AddRange(cities);

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

            context.CategoryCity.AddRange(categories);

            var cityPlaces = new List<CityPlace>
            {
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "Doce & Companhia",
                    PhoneNumber = "(+351) 912 345 678",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "O Pequeno Café",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "O Pão de Queijo",
                    PhoneNumber = "(+351) 912 345 678",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "Estrada Café",
                    PhoneNumber = "(+351) 912 345 678",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "Café da Manhã",
                    PhoneNumber = "(+351) 912 345 678",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "Bela Vida Café",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "Café a gosto",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[0],
                    Name = "Café Creme",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },

                new CityPlace
                {
                    City = cities[0],
                    Category = categories[1],
                    Name = "Torre de Londres",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[1],
                    Name = "Templo Wat Pho",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[1],
                    Name = "Burj Khalifa",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[1],
                    Name = "Pagode Shwedagon",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[1],
                    Name = "Sydney Opera House",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },

                new CityPlace
                {
                    City = cities[0],
                    Category = categories[2],
                    Name = "Festival do amanhã",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[2],
                    Name = "The Point Arrow",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[2],
                    Name = "Festival dos Canários",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[2],
                    Name = "Festival de Lagoa",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[2],
                    Name = "Fuji Rock Festival",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[0],
                    Category = categories[2],
                    Name = "Expo Tattoo Floripa",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },

                new CityPlace
                {
                    City = cities[1],
                    Category = categories[0],
                    Name = "Café da Madeira",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[0],
                    Name = "Café Quentinho",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[0],
                    Name = "Café de Londres",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[0],
                    Name = "Café do Amanhã",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[0],
                    Name = "Café da Noite",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[0],
                    Name = "Doce & Café",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },

                new CityPlace
                {
                    City = cities[1],
                    Category = categories[1],
                    Name = "Le Grande",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[1],
                    Name = "La Baguette",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[1],
                    Name = "Le Petit",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[1],
                    Name = "The paradise",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },

                new CityPlace
                {
                    City = cities[1],
                    Category = categories[2],
                    Name = "Festival do dia",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[2],
                    Name = "Expo International Festival",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[2],
                    Name = "Festival da Cidade",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
                },
                new CityPlace
                {
                    City = cities[1],
                    Category = categories[2],
                    Name = "Festival da Amadora",
                    Description = "Aenean sed luctus sem, nec tempor erat. Donec aliquam erat urna. Nulla non mauris justo."
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

            context.CityPlace.AddRange(cityPlaces);

            await context.SaveChangesAsync();
        }
    }
}
