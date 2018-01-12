using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TeduCoreApp.Data.Entities
{
    [Table("AppRole")]
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole() : base()
        {

        }
        public AppRole(string name, string description): base()
        {
            this.Description = description;
        }

        public string Description { get; set; }

    }
}
