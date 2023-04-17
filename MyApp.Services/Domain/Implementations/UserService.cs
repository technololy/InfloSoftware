using System;
using System.Collections.Generic;
using System.Linq;
using MyApp.Data;
using MyApp.Models;
using MyApp.Services.Domain.Implementations.Base;
using MyApp.Services.Domain.Interfaces;

namespace MyApp.Services.Domain.Implementations
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly IDataAccess _dataAccess;
        private readonly MyApp.Data.Data.MyAppWebMSDataAccess _myAppdataAccess;
        public UserService(IDataAccess dataAccess, MyApp.Data.Data.MyAppWebMSDataAccess myAppWebMSDataAccess) : base(myAppWebMSDataAccess) 
        {
            _dataAccess = dataAccess;
            _myAppdataAccess = myAppWebMSDataAccess;
        }

        /// <summary>
        /// Return users by active state
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<User> FilterByActive(bool isActive)
        {
          return _myAppdataAccess.GetAll<User>().Where(u => u.IsActive == isActive);
        }
    }
}
