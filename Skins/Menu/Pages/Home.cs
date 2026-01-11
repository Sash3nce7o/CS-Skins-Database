using Skins.Menu.Constants;
using Skins.Menu.Interfaces;

namespace Skins.Menu.Pages
{
    public class HomePage : IPage
    {
        private Menu _menu;

        public HomePage(Menu menu)
        {
            _menu = menu;
        }

        public void Draw()
        {
            // If already logged in, redirect to LoggedInHome
            if (_menu.IsLoggedIn)
            {
                _menu.GoToLoggedInHome();
                return;
            }

            Console.WriteLine(MenuConstants.HEADER);
            Console.WriteLine($"{MenuConstants.SPACE}CS SKINS DATABASE");
            Console.WriteLine(MenuConstants.HEADER);

            Console.WriteLine($"\n{MenuConstants.BULLET}1. Login");
            Console.WriteLine($"{MenuConstants.BULLET}2. Register");
            Console.WriteLine($"{MenuConstants.BULLET}3. Exit");
            Console.Write($"\nChoose option{MenuConstants.COLON}");

            string? input = Console.ReadLine();
            if (!int.TryParse(input?.Trim(), out int choice))
            {
                Console.WriteLine($"{MenuConstants.ERROR} Invalid input");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            switch (choice)
            {
                case 1:
                    _menu.GoToLogin();
                    break;
                case 2:
                    _menu.GoToRegister();
                    break;
                case 3:
                    Console.WriteLine("👋 Goodbye!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine($"{MenuConstants.ERROR} Invalid option");
                    System.Threading.Thread.Sleep(1500);
                    break;
            }
        }

        public int Redirect(int input) => 0;
        public void ExecuteLogic(int option) { }
    }
}
