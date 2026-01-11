using Skins.Core.Models.Skin;
using Skins.Menu.Constants;
using Skins.Menu.Interfaces;

namespace Skins.Menu.Pages
{
    public class MySkinsPage : IPage
    {
        private Menu _menu;

        public MySkinsPage(Menu menu)
        {
            _menu = menu;
        }

        public void Draw()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(MenuConstants.HEADER);
                Console.WriteLine($"{MenuConstants.SPACE}MY SKINS");
                Console.WriteLine(MenuConstants.HEADER);

                Console.WriteLine($"{MenuConstants.BULLET}1. Create Skin");
                Console.WriteLine($"{MenuConstants.BULLET}2. Update Skin");
                Console.WriteLine($"{MenuConstants.BULLET}3. Delete Skin");
                Console.WriteLine($"{MenuConstants.BULLET}4. View Skins");
                Console.WriteLine($"{MenuConstants.BULLET}5. Back");
                Console.Write($"\nChoice{MenuConstants.COLON}");

                string? input = Console.ReadLine();
                if (!int.TryParse(input?.Trim(), out int choice))
                {
                    Console.WriteLine($"{MenuConstants.ERROR} Invalid input");
                    System.Threading.Thread.Sleep(1500);
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        CreateSkin();
                        break;
                    case 2:
                        UpdateSkin();
                        break;
                    case 3:
                        DeleteSkin();
                        break;
                    case 4:
                        ViewSkins();
                        break;
                    case 5:
                        _menu.GoToHome();
                        return;
                    default:
                        Console.WriteLine($"{MenuConstants.ERROR} Invalid option");
                        System.Threading.Thread.Sleep(1500);
                        break;
                }
            }
        }

        private void CreateSkin()
        {
            Console.Clear();
            Console.WriteLine($"{MenuConstants.WRITE} CREATE SKIN");
            Console.WriteLine(MenuConstants.SEPARATOR);

            Console.Write($"{MenuConstants.INFO} Skin name{MenuConstants.COLON}");
            string? name = Console.ReadLine();

            Console.Write($"Float {MenuConstants.OPTION}{MenuConstants.COLON}");
            if (!float.TryParse(Console.ReadLine(), out float floatValue))
            {
                Console.WriteLine($"{MenuConstants.ERROR} Invalid float");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            Console.Write($"{MenuConstants.INFO} Pattern{MenuConstants.COLON}");
            string? pattern = Console.ReadLine();

            Console.Write($"Max float {MenuConstants.OPTION}{MenuConstants.COLON}");
            if (!float.TryParse(Console.ReadLine(), out float maxFloat))
            {
                Console.WriteLine($"{MenuConstants.ERROR} Invalid max float");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(pattern))
            {
                Console.WriteLine($"{MenuConstants.ERROR} Name and pattern required");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            try
            {
                var skin = new SkinCreateViewModel
                {
                    Name = name,
                    Float = floatValue,
                    Pattern = pattern,
                    MaxFloat = maxFloat,
                    OwnerId = _menu.CurrentUserId!
                };

                _menu.SkinService.Add(skin);
                Console.WriteLine($"{MenuConstants.SUCCESS} Skin created!");
                System.Threading.Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MenuConstants.ERROR} {ex.Message}");
                System.Threading.Thread.Sleep(1500);
            }
        }

        private void ViewSkins()
        {
            Console.Clear();
            Console.WriteLine($"{MenuConstants.INFO} YOUR SKINS");
            Console.WriteLine(MenuConstants.SEPARATOR);

            try
            {
                var user = _menu.UserService.GetById(_menu.CurrentUserId!);
                if (user?.Skins != null && user.Skins.Count > 0)
                {
                    int count = 1;
                    foreach (var skin in user.Skins)
                    {
                        Console.WriteLine($"{count}. {skin.Name} - Float: {skin.Float} | Quality: {skin.Quality} | Pattern: {skin.Pattern}");
                        count++;
                    }
                }
                else
                {
                    Console.WriteLine("No skins found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MenuConstants.ERROR} {ex.Message}");
            }

            Console.Write("\nPress Enter to continue...");
            Console.ReadLine();
        }

        private void UpdateSkin()
        {
            Console.Clear();
            Console.WriteLine($"{MenuConstants.WRITE} UPDATE SKIN");
            Console.WriteLine(MenuConstants.SEPARATOR);

            ListSkinsForSelection();

            Console.Write($"{MenuConstants.INFO} Enter skin ID to update{MenuConstants.COLON}");
            string? skinId = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(skinId))
            {
                Console.WriteLine($"{MenuConstants.ERROR} ID required");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            Console.Write($"{MenuConstants.WRITE} New float (or press Enter to skip){MenuConstants.COLON}");
            string? floatInput = Console.ReadLine();

            Console.Write($"{MenuConstants.WRITE} New pattern (or press Enter to skip){MenuConstants.COLON}");
            string? pattern = Console.ReadLine();

            try
            {
                var model = new SkinUpdateViewModel
                {
                    Float = string.IsNullOrWhiteSpace(floatInput) ? null : float.Parse(floatInput),
                    Pattern = string.IsNullOrWhiteSpace(pattern) ? null : pattern
                };

                _menu.SkinService.Update(skinId, model);
                Console.WriteLine($"{MenuConstants.SUCCESS} Skin updated!");
                System.Threading.Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MenuConstants.ERROR} {ex.Message}");
                System.Threading.Thread.Sleep(1500);
            }
        }

        private void DeleteSkin()
        {
            Console.Clear();
            Console.WriteLine($"{MenuConstants.TRASH}{MenuConstants.SPACE} DELETE SKIN");
            Console.WriteLine(MenuConstants.SEPARATOR);

            ListSkinsForSelection();

            Console.Write($"{MenuConstants.TRASH} Enter skin ID to delete{MenuConstants.COLON}");
            string? skinId = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(skinId))
            {
                Console.WriteLine($"{MenuConstants.ERROR} ID required");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            Console.Write($"{MenuConstants.TRASH} Confirm delete? (y/n){MenuConstants.COLON}");
            if (Console.ReadLine()?.ToLower() != "y")
            {
                Console.WriteLine($"{MenuConstants.INFO} Cancelled");
                System.Threading.Thread.Sleep(1000);
                return;
            }

            try
            {
                _menu.SkinService.Remove(skinId);
                Console.WriteLine($"{MenuConstants.SUCCESS} Skin deleted!");
                System.Threading.Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MenuConstants.ERROR} {ex.Message}");
                System.Threading.Thread.Sleep(1500);
            }
        }

        private void ListSkinsForSelection()
        {
            try
            {
                var user = _menu.UserService.GetById(_menu.CurrentUserId!);
                if (user?.Skins != null && user.Skins.Count > 0)
                {
                    Console.WriteLine("Your skins:");
                    foreach (var skin in user.Skins)
                    {
                        Console.WriteLine($"  ID: {skin.Id} | {skin.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("No skins found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MenuConstants.ERROR} {ex.Message}");
            }
        }

        public int Redirect(int input) => 0;
        public void ExecuteLogic(int option) { }
    }
}
