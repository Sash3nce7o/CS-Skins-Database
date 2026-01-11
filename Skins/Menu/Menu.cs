using Skins.Infrastructure.Data;
using Skins.Infrastructure.Common;
using Skins.Core.Services;
using Skins.Menu.Interfaces;
using Skins.Menu.Pages;
using Microsoft.EntityFrameworkCore;
using Skins.Menu.Constants;
using System.Reflection.Metadata;

namespace Skins.Menu
{
    public class Menu
    {
        public int CurrentMenuIndex = 0;
        public string? CurrentUserId = null;
        public bool IsLoggedIn = false;

        public SkinsDbContext Context { get; private set; }
        public IRepository Repository { get; private set; }
        public UserService UserService { get; private set; }
        public SkinService SkinService { get; private set; }

        private List<IPage> Pages = new();

        public Menu()
        {
            Context = new SkinsDbContext();
            Context.Database.Migrate();
            Repository = new Repository(Context);
            UserService = new UserService(Repository);
            SkinService = new SkinService(Repository);

            Pages = new()
            {
                new HomePage(this),           // Index 0
                new LoginPage(this),          // Index 1
                new RegisterPage(this),       // Index 2
                new LoggedInHomePage(this),   // Index 3
                new MySkinsPage(this)         // Index 4
            };
        }

        public void DrawMenu()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Pages[CurrentMenuIndex].Draw();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{MenuConstants.ERROR} Error: {ex.Message}");
                    System.Threading.Thread.Sleep(1500);
                }
            }
        }

        public void GoToHome() => CurrentMenuIndex = 0;
        public void GoToLogin() => CurrentMenuIndex = 1;
        public void GoToRegister() => CurrentMenuIndex = 2;
        public void GoToLoggedInHome() => CurrentMenuIndex = 3;
        public void GoToMySkins() => CurrentMenuIndex = 4;

        public void SetLogin(string userId)
        {
            IsLoggedIn = true;
            CurrentUserId = userId;
        }

        public void Logout()
        {
            IsLoggedIn = false;
            CurrentUserId = null;
        }
    }
}
