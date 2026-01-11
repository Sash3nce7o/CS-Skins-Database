using Skins.Menu.Constants;
using Skins.Menu.Interfaces;

namespace Skins.Menu.Pages
{
    public class LoggedInHomePage : IPage
    {
        private Menu _menu;

        public LoggedInHomePage(Menu menu)
        {
            _menu = menu;
        }

        public void Draw()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(MenuConstants.HEADER);
                Console.WriteLine($"{MenuConstants.SPACE}CS SKINS DATABASE");
                Console.WriteLine(MenuConstants.HEADER);

                try
                {
                    var user = _menu.UserService.GetById(_menu.CurrentUserId!);
                    Console.WriteLine($"{MenuConstants.SUCCESS} Logged in as: {user.Username}");
                    Console.WriteLine($"{MenuConstants.INFO} ID: {user.Id}");
                }
                catch
                {
                    Console.WriteLine($"{MenuConstants.ERROR} Error loading user info");
                }

                Console.WriteLine(MenuConstants.SEPARATOR);
                Console.WriteLine($"{MenuConstants.BULLET}1. My Skins");
                Console.WriteLine($"{MenuConstants.BULLET}2. Logout");
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
                        _menu.GoToMySkins();
                        return;
                    case 2:
                        _menu.Logout();
                        _menu.GoToHome();
                        return;
                    default:
                        Console.WriteLine($"{MenuConstants.ERROR} Invalid option");
                        System.Threading.Thread.Sleep(1500);
                        break;
                }
            }
        }

        public int Redirect(int input) => 0;
        public void ExecuteLogic(int option) { }
    }
}
