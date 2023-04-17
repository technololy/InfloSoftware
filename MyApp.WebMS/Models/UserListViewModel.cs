using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyApp.WebMS.Models
{
    public class UserListViewModel
    {
        public IList<UserListItemViewModel> Items { get; set; } 
    }

    public class UserListItemViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Forename is required")]

        public string Forename { get; set; }
        [Required(ErrorMessage = "Surname is required")]

        public string Surname { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name ="Active?")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        [Display(Name ="Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
    }
}