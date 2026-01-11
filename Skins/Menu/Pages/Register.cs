using Skins.Core.Models.User;
using Skins.Menu.Constants;
using Skins.Menu.Interfaces;

namespace Skins.Menu.Pages
{
    public class RegisterPage : IPage
    {
        private Menu _menu;

        public RegisterPage(Menu menu)
        {
            _menu = menu;
        }

        public void Draw()
        {
            Console.WriteLine(MenuConstants.HEADER);
            Console.WriteLine($"{MenuConstants.SPACE}REGISTER");
            Console.WriteLine(MenuConstants.HEADER);

            Console.Write($"{MenuConstants.USER} Username{MenuConstants.COLON}");
            string? username = Console.ReadLine();

            Console.Write($"{MenuConstants.EMAIL} Email{MenuConstants.COLON}");
            string? email = Console.ReadLine();

            Console.Write($"{MenuConstants.LOCK} Password{MenuConstants.COLON}");
            string? password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine($"{MenuConstants.ERROR} All fields required");
                System.Threading.Thread.Sleep(1500);
                _menu.GoToHome();
                return;
            }

            try
            {
                var newUser = new UserRegisterViewModel
                {
                    Username = username,
                    Email = email,
                    Password = password
                };

                _menu.UserService.Add(newUser);
                Console.WriteLine($"{MenuConstants.SUCCESS} Registration successful! You can now login.");
                System.Threading.Thread.Sleep(1500);
                _menu.GoToHome();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MenuConstants.ERROR} Registration failed: {ex.Message}");
                System.Threading.Thread.Sleep(1500);
                _menu.GoToHome();
            }
        }

        public int Redirect(int input) => 0;
        public void ExecuteLogic(int option) { }
    }
}
