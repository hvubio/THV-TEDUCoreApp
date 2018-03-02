using System.ComponentModel.DataAnnotations;
using TeduCoreApp.Data.Enums;

namespace TeduCoreApp.Application.ViewModels.System
{
    public class FunctionViewModel
    {
        public string Id { get; set; }
        [Required] [StringLength(128)] public string Name { get; set; }

        [StringLength(256)] public string Url { get; set; }

        [StringLength(128)] public string ParentId { get; set; }

        public string IconCss { get; set; }
        public int SortOrder { get; set; }

        public Status Status { get; set; }
    }
}