using Skins.Menu.Constants;
using Skins.Menu.Interfaces;

namespace Skins.Menu.Pages
{
    public class LoginPage : IPage
    {
        private Menu _menu;

        public LoginPage(Menu menu)
        {
            _menu = menu;
        }

        public void Draw()
        {
            Console.WriteLine(MenuConstants.HEADER);
            Console.WriteLine($"{MenuConstants.SPACE}LOGIN");
            Console.WriteLine(MenuConstants.HEADER);

            Console.Write($"{MenuConstants.EMAIL} Email{MenuConstants.COLON}");
            string? email = Console.ReadLine();

            Console.Write($"{MenuConstants.LOCK} Password{MenuConstants.COLON}");
            string? password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine($"{MenuConstants.ERROR} Email and password required");
                System.Threading.Thread.Sleep(1500);
                _menu.GoToHome();
                return;
            }

            try
            {
                var user = _menu.UserService.GetByEmail(email);
                if (user != null && _menu.UserService.VerifyPassword(user.Username, password))
                {
                    _menu.SetLogin(user.Id);
                    Console.WriteLine($"{MenuConstants.SUCCESS} Welcome {user.Username}!");
                    System.Threading.Thread.Sleep(1500);
                    _menu.GoToLoggedInHome();
                }
                else
                {
                    Console.WriteLine($"{MenuConstants.ERROR} Invalid email or password");
                    System.Threading.Thread.Sleep(1500);
                    _menu.GoToHome();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MenuConstants.ERROR} Login failed: {ex.Message}");
                System.Threading.Thread.Sleep(1500);
                _menu.GoToHome();
            }
        }

        public int Redirect(int input) => 0;
        public void ExecuteLogic(int option) { }
    }
}
