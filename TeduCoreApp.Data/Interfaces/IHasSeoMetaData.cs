using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeduCoreApp.Data.Interfaces
{
    public interface IHasSeoMetaData
    {
        string SeoPageTitle { get; set; }

        [Column(TypeName = "vachar")]
        [StringLength(255)]
        string SeoAlias { get; set; }

        string SeoKeywords { get; set; }
        string SeoDescription { get; set; }

    }
}
