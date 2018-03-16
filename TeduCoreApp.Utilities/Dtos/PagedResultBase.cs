using System;

namespace TeduCoreApp.Utilities.Dtos
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; } // trang hien tai
        public int  PageSize { get; set; } // so ban ghi trong 1 trang 10, 20, 30 or 50
        public int RowCount { get; set; } // so dong ban ghi hien co

        public int PageCount {
            get
            {
                var pageCount = (double)RowCount/PageSize;
                return (int)Math.Ceiling(pageCount);
            }
            set { PageCount = value; }
        } // tong so trang dua tren pagesize

        // tim dong dau tien trong trang

        public int FristRowOnPage
        {
            get { return (CurrentPage - 1) * PageSize + 1; } 
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }
}
