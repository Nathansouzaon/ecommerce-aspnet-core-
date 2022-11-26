namespace App.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
