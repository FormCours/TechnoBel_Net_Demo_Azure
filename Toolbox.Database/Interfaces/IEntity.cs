namespace Toolbox.Database.Interfaces
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}
