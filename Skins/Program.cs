using Skins.Menu;
using MenuApp = Skins.Menu.Menu;

namespace Skins
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var menu = new MenuApp();
                menu.DrawMenu();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"ERROR: {ex}");
            }
        }
    }
}
