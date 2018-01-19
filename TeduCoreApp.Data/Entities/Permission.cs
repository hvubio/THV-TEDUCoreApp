using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    public class Permission : DomainEntity<int>
    {
        public Permission()
        {
        }

        public Permission(Guid roleId, string functionId, bool canRead, bool canCreate, bool canUpdate, bool canDelete)
        {
            RoleId = roleId;
            FunctionId = functionId;
            CanRead = canRead;
            CanCreate = canCreate;
            CanUpdate = canUpdate;
            CanDelete = canDelete;
        }

        [Required] public Guid RoleId { get; set; }

        [Required] [StringLength(128)] public string FunctionId { get; set; }

        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }

        [ForeignKey("RoleId")] public virtual AppRole AppRole { get; set; }

        [ForeignKey("FunctionId")] public virtual Function Function { get; set; }
    }
}