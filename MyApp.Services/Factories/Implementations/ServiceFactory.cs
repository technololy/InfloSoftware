using MyApp.Data;
using MyApp.Data.Data;
using MyApp.Services.Domain.Implementations;
using MyApp.Services.Domain.Interfaces;
using MyApp.Services.Factories.Interfaces;

namespace MyApp.Services.Factories.Implementations
{
    public class ServiceFactory : IServiceFactory
    {
        private IDataAccess _dataAccess;
        private MyAppWebMSDataAccess _myAppWebMsDataAccess;
        protected IDataAccess DataAccess => _dataAccess ?? (_dataAccess = new DataAccess(new DataContext()));

        protected MyAppWebMSDataAccess MyAppWebMsDataAccess => _myAppWebMsDataAccess ?? (_myAppWebMsDataAccess = new MyAppWebMSDataAccess(new MyAppWebMSContext()));


        private IUserService _userService;
        public IUserService UserService => _userService ?? (_userService = new UserService(DataAccess,MyAppWebMsDataAccess));
    }
}
