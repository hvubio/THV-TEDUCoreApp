using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Data.Interfaces
{
    public interface IMultilanguage<T>
    {
        T LanguageId { get; set; }
    }
}
