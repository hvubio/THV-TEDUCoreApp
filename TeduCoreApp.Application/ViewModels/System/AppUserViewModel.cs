using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Enums;

namespace TeduCoreApp.Application.ViewModels.System
{
    public class AppUserViewModel
    {
        public AppUserViewModel()
        {
            Roles = new List<string>();
        }

        public AppUserViewModel(Guid id, string fullName, DateTime birthDay, string email, string password, string userName, string address, string avatar,string phoneNumber, Status status, string gender, DateTime dateCreated, List<string> roles)
        {
            Id = id;
            FullName = fullName;
            BirthDay = birthDay;
            Email = email;
            Password = password;
            UserName = userName;
            Address = address;
            Avatar = avatar;
            PhoneNumber = phoneNumber;
            Status = status;
            Gender = gender;
            DateCreated = dateCreated;
            Roles = roles;
        }


        public Guid? Id { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public Status Status { get; set; }
        public string Gender { get; set; }

        public DateTime DateCreated { get; set; }

        public List<string> Roles { get; set; }
    }
}
