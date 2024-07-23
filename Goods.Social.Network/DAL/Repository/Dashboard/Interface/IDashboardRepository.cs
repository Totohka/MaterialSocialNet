using Goods.System.Social.Network.DomainModel.Entities.Dashboard;
namespace Goods.System.Social.Network.DAL.Repository.Interface
{
    public interface IDashboardRepository
    {
        Task<List<Visit>> GetAllAsync();
        void Create(Visit item);
    }
}
