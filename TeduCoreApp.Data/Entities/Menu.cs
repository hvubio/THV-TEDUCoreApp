using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    public class Menu : DomainEntity<int>, ISortable, ISwitchable
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Css { get; set; }
        public int ParentId { get; set; }

        public int SortOrder { get; set; }
        public Status Status { get; set; }
    }
}