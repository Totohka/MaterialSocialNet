using DomainModel.Entities.Dashboard;
using DomainModel.Entities.ViewModels;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities.Dashboard;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.Extensions.Logging;
using NLog;
namespace Goods.System.Social.Network.DomainServices.Realization
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repositoryDashboard;
        private readonly IRepository<CountUsers> _repositoryCountUsers;
        private readonly ILogger<UserService> _logger;
        public DashboardService(IUserRepository repository,
                           ILogger<UserService> logger,
                           IDashboardRepository repositoryDashboard,
                           IRepository<CountUsers> repositoryCountUsers)
        {
            _logger = logger;
            _repositoryDashboard = repositoryDashboard;
            _repositoryCountUsers = repositoryCountUsers;
        }

        public void CreateNewVisit(Visit visit)
        {
            _logger.LogTrace($"Вызван метод CreateNewVisit с параметрами: visit: {visit}");

            _repositoryDashboard.Create(visit);
        }

        public async Task<DashboardData[]> GetCountUsersAsync()
        {
            _logger.LogTrace($"Вызван метод GetCountUsersAsync");

            var users = await _repositoryCountUsers.GetAllAsync();
            DashboardData[] arrToCountUsers = new DashboardData[users.Count];
            for (var i = 0; i < arrToCountUsers.Length; i++)
            {
                arrToCountUsers[i] = new DashboardData() { name = $"{i}", value = users[i].Count };
            }
            return arrToCountUsers;
        }
        public async Task<DashboardData[]> GetCountVisitingToDayAsync()
        {
            _logger.LogTrace($"Вызван метод GetCountVisitingToDayAsync");

            var visits = await _repositoryDashboard.GetAllAsync();
            var visitsToDay = visits.Where(v => v.Date.Year == DateTime.Now.Year &&
                                           v.Date.Month == DateTime.Now.Month && 
                                           v.Date.Day == DateTime.Now.Day).ToList();
            DashboardData[] arrToVisitstoHours = new DashboardData[24];
            for (var i = 0; i < arrToVisitstoHours.Length; i++)
            {
                arrToVisitstoHours[i] = new DashboardData() { name = $"{i}" };
            }
            foreach (var v in visitsToDay)
            {
                arrToVisitstoHours[v.Date.Hour].value += 1;
            }
            return arrToVisitstoHours;
        }
        public async Task<DashboardData[]> GetCountVisitingToMounthAsync()
        {
            _logger.LogTrace($"Вызван метод GetCountVisitingToMounthAsync");

            var visits = await _repositoryDashboard.GetAllAsync();
            var visitsToMonth = visits.Where(v => v.Date.Year == DateTime.Now.Year && 
                                                v.Date.Month == DateTime.Now.Month).ToList();
            DashboardData[] arrToVisitstoDay = new DashboardData[31];
            for (var i = 0; i < arrToVisitstoDay.Length; i++)
            {
                arrToVisitstoDay[i] = new DashboardData() { name = $"{i + 1}"};
            }
            foreach (var v in visitsToMonth)
            {
                arrToVisitstoDay[v.Date.Day - 1].value += 1;
            }
            return arrToVisitstoDay;
        }
        public async Task<DashboardData[]> GetCountVisitingToYearAsync()
        {
            _logger.LogTrace($"Вызван метод GetCountVisitingToYearAsync");

            var visits = await _repositoryDashboard.GetAllAsync();
            var visitsToYear = visits.Where(v => v.Date.Year == DateTime.Now.Year).ToList();
            DashboardData[] arrToVisitstoMouth = new DashboardData[12];
            for (var i = 0; i < arrToVisitstoMouth.Length; i++)
            {
                arrToVisitstoMouth[i] = new DashboardData() { name = $"{i + 1}" };
            }
            foreach (var v in visitsToYear)
            {
                arrToVisitstoMouth[v.Date.Month - 1].value += 1;
            }
            return arrToVisitstoMouth;
        }

    }
}