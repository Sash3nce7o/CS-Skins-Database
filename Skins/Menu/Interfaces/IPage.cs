namespace Skins.Menu.Interfaces
{
    public interface IPage
    {
        void Draw();
        int Redirect(int input);
        void ExecuteLogic(int option);
    }
}
