using Humanizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Volunteering.Data.Models;

namespace Volunteering.Helpers
{
    public class DbInitializer
    {
        public static void Initialize(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                var configuration = serviceScope.ServiceProvider.GetService<IConfiguration>();
                context.Database.EnsureCreated();

                if (!context.UserRoles.Any())
                {
                    var list = new UserRole[]
                    {
                        new UserRole { UserRoleName = "Admin" },
                        new UserRole { UserRoleName = "Registered"}
                    };

                    foreach (var r in list)
                    {
                        context.UserRoles.Add(r);
                    }
                    context.SaveChanges();
                }

                if(!context.Users.Any(u => u.UserRole.UserRoleName == "Admin"))
                {
                    //var adminPassword = configuration.GetValue<string>("Admin:Password");
                    var adminPassword = configuration.GetSection("Admin:Password").Value;

                    var adminUser = new User
                    {
                        UserName = "admin",
                        UserSurname = "admin",
                        Email = "oleksandr.o.shpak@gmail.com",
                        Password = HashProcessor.HashPassword(adminPassword), // Hashing the password securely
                        //Password = HashProcessor.HashPassword("password"), // Hashing the password securely
                        UserRoleId = context.UserRoles.Single(r => r.UserRoleName == "Admin").UserRoleId,
                        Rating = 100,
                        PhoneNumber = "380980000000",
                        City = "Львів",
                        Organisation = "ЄДопомога",
                        CardNumber = "1111222233334444",
                        Speciality = "Адміністратор"
                    };

                    context.Users.Add(adminUser);
                    context.SaveChanges();
                }


                if (!context.CampaignStatuses.Any())
                {
                    var list = new CampaignStatus[]
                    {
                        new CampaignStatus { StatusName = "Новий" },
                        new CampaignStatus { StatusName = "Перевірений"},
                        new CampaignStatus { StatusName = "Триває" },
                        new CampaignStatus { StatusName = "Очікується звіт" },
                        new CampaignStatus { StatusName = "Завершений"}
                    };

                    foreach (var r in list)
                    {
                        context.CampaignStatuses.Add(r);
                    }
                    context.SaveChanges();
                }

                if (!context.CampaignPriorities.Any())
                {
                    var list = new CampaignPriority[]
                    {
                        new CampaignPriority { PriorityValue = 1, PriorityDescription = "Критично важливий" },
                        new CampaignPriority { PriorityValue = 2, PriorityDescription = "Терміновий" },
                        new CampaignPriority { PriorityValue = 3, PriorityDescription = "Регулярний" },
                        new CampaignPriority { PriorityValue = 4, PriorityDescription = "На перспективу" }
                    };

                    foreach (var r in list)
                    {
                        context.CampaignPriorities.Add(r);
                    }
                    context.SaveChanges();
                }

                if (!context.Categories.Any())
                {
                    var list = new Category[]
                    {
                        new Category 
                        {
                            CategoryName = "Медицина",
                            CategorySubcategories = new List<CategorySubcategory>
                            {
                                new CategorySubcategory
                                {
                                    Subcategory = new Subcategory { SubcategoryName = "Реабілітація" }
                                },
                                new CategorySubcategory
                                {
                                    Subcategory = new Subcategory { SubcategoryName = "Медичні установи" }
                                },
                                new CategorySubcategory
                                {
                                    Subcategory = new Subcategory { SubcategoryName = "Адресна допомога" }
                                }
                            }
                        },
                        new Category
                        {
                            CategoryName = "Військо",
                            CategorySubcategories = new List<CategorySubcategory>
                            {
                                new CategorySubcategory
                                {
                                    Subcategory = new Subcategory { SubcategoryName = "Дрони" }
                                },
                                new CategorySubcategory
                                {
                                    Subcategory = new Subcategory { SubcategoryName = "Авто" }
                                },
                                new CategorySubcategory
                                {
                                    Subcategory = new Subcategory { SubcategoryName = "Амуніція" }
                                },
                                new CategorySubcategory
                                {
                                    Subcategory = new Subcategory { SubcategoryName = "Засоби зв'язку, електроніка" }
                                },
                                new CategorySubcategory
                                {
                                    Subcategory = new Subcategory { SubcategoryName = "Медицина на полі бою" }
                                }
                            }
                        },
                        new Category
                        {
                            CategoryName = "Гуманітарна",
                            CategorySubcategories = new List<CategorySubcategory>
                            {
                                new CategorySubcategory
                                {
                                    Subcategory = new Subcategory { SubcategoryName = "Соціальні проекти" }
                                },
                                new CategorySubcategory
                                {
                                    Subcategory = new Subcategory { SubcategoryName = "ВПО" }
                                },
                                new CategorySubcategory
                                {
                                    Subcategory = new Subcategory { SubcategoryName = "Притулки тварин" }
                                },
                                new CategorySubcategory
                                {
                                    Subcategory = new Subcategory { SubcategoryName = "Діти" }
                                }
                            }
                        }
                    };

                    foreach (var r in list)
                    {
                        context.Categories.Add(r);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
