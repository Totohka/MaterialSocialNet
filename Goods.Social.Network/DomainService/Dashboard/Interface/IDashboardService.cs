using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DomainModel.Entities.Dashboard;

namespace Goods.System.Social.Network.DomainServices.Interface
{
    public interface IDashboardService
    {
        void CreateNewVisit(Visit visit);
        Task<DashboardData[]> GetCountUsersAsync();
        Task<DashboardData[]> GetCountVisitingToDayAsync();
        Task<DashboardData[]> GetCountVisitingToMounthAsync();
        Task<DashboardData[]> GetCountVisitingToYearAsync();
    }
}