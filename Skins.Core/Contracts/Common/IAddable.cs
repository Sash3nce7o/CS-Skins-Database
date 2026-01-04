namespace   Skins.Core.Contracts.Common
{
    public interface IAddable<T> where T : class
    {
        void Add(T entity);
    }
}