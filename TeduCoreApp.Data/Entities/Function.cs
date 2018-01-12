using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Function")]
    public class Function : DomainEntity<string>, ISwitchable, ISortable
    {
        public Function()
        {
        }

        public Function(string name, string url, string iconCss, Status status, int sortOrder)
        {
            Name = name;
            Url = url;
            IconCss = iconCss;
            Status = status;
            SortOrder = sortOrder;
        }


        [Required] [StringLength(128)] public string Name { get; set; }

        [StringLength(256)] public string Url { get; set; }

        public string IconCss { get; set; }
        public int SortOrder { get; set; }

        public Status Status { get; set; }
    }
}