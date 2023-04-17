using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyApp.Services.Factories.Interfaces;
using MyApp.WebMS.Controllers.Base;
using MyApp.WebMS.Models;

namespace MyApp.WebMS.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : BaseController
    {
        public UsersController(IServiceFactory serviceFactory) : base(serviceFactory) { }

        [Route("", Name = "UserList")]
        public ActionResult List()
        {
            var items = ServiceFactory.UserService.GetAll().Select(p => new UserListItemViewModel
            {
                Id = p.Id,
                Forename = p.Forename,
                Surname = p.Surname,
                Email = p.Email,
                IsActive = p.IsActive
            });

            var model = new UserListViewModel
            {
                Items = items.ToList()
            };

            return View("List", model);
        }

        [Route("IsActive/{status}", Name = "IsActive")]
        public ActionResult IsActive(bool status = true)
        {
            var items = ServiceFactory.UserService.FilterByActive(isActive:status).Select(p => new UserListItemViewModel
            {
                Id = p.Id,
                Forename = p.Forename,
                Surname = p.Surname,
                Email = p.Email,
                IsActive = p.IsActive
            });

            var model = new UserListViewModel
            {
                Items = items.ToList()
            };

            return View("List", model);
        }

        [Route("Edit/{userId}", Name = "Edit")]
        public ActionResult Edit(long userId)
        {
            var user = ServiceFactory.UserService.GetById(Convert.ToInt16(userId));
            if (user !=null)
            {
                var userDto = CustomAutoMapperEntityToDto(user).Items.FirstOrDefault();
                return View("Edit", userDto);
            }
            else
            {
                return View("Error");
            }

        }


        [Route("Details/{userId}", Name = "Details")]
        public ActionResult Details(long userId)
        {
            var user = ServiceFactory.UserService.GetById(Convert.ToInt16(userId));
            if (user != null)
            {
                var userDto = CustomAutoMapperEntityToDto(user).Items.FirstOrDefault();
                return View("Details", userDto);
            }
            else
            {
                return View("Error");
            }

        }



        [Route("Delete/{userId}", Name = "Delete")]
        public ActionResult Delete(long userId)
        {
            var user = ServiceFactory.UserService.GetById(Convert.ToInt16(userId));
            if (user != null)
            {
                var userDto = CustomAutoMapperEntityToDto(user).Items.FirstOrDefault();
                return View("Delete", userDto);
            }
            else
            {
                return View("Error");
            }

        }

        [Route("Create", Name = "Create")]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ActionName("SubmitForm")]

        public ActionResult SubmitForm(Models.UserListItemViewModel userDto)
        {
            if (ModelState.IsValid)
            {
                MyApp.Models.User user = CustomAutoMapperDtoToEntity(userDto);
                ServiceFactory.UserService.Create(user);
            }
          return RedirectToAction("List");
        }

        private MyApp.Models.User CustomAutoMapperDtoToEntity(Models.UserListItemViewModel user)
        {
            return new MyApp.Models.User()
            {
                DateOfBirth = user.DateOfBirth,
                IsActive = user.IsActive,
                Email = user.Email,
                Forename = user.Forename,
                Surname = user.Surname
            };
        }

        private UserListViewModel CustomAutoMapperEntityToDto(MyApp.Models.User user)
        {
            List<UserListItemViewModel> userItemList = new List<UserListItemViewModel>()
            {
                new UserListItemViewModel
                {
                    Id = user.Id,
                    Forename = user.Forename,
                    Surname = user.Surname,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    DateOfBirth = user.DateOfBirth
                }
            };

            var model = new UserListViewModel()
            {
                Items = userItemList.ToList()  
            };

            return model;
        }


    }
}