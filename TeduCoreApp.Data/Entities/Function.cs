using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Function")]
    public class Function: DomainEntity<int>, ISwitchable,ISortable
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string IconCss { get; set; }

        public Status Status { get; set; }
        public int SortOrder { get; set; }
    }
}
