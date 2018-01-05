using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Slide")]
    public class Slide: DomainEntity<int>, ISwitchable, ISortable
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public string GroupAlias { get; set; }

        public Status Status { get; set; }
        public int SortOrder { get; set; }
    }
}
