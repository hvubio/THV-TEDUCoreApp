using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Blog")]
    public class Blog : DomainEntity<int>, IDateTracking, ISwitchable, IHasSeoMetaData, ISortable
    {
        public Blog(string name, string description, string content, string image, bool? homeFlg, bool? hotFlg, int viewCount, string tag,
            DateTime dateCreated, DateTime dateModified, string seoPageTitle, string seoAlias, string seoKeywords, string seoDescription,
            int sortOrder, Status status)
        {
            Name = name;
            Description = description;
            Content = content;
            Image = image;
            HomeFlg = homeFlg;
            HotFlg = hotFlg;
            ViewCount = viewCount;
            Tag = tag;
            DateCreated = dateCreated;
            DateModified = dateModified;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoKeywords;
            SeoDescription = seoDescription;
            SortOrder = sortOrder;
            Status = status;
        }

        public Blog()
        {
        }

        public Blog(int id, string name, string description, string content, string image, bool? homeFlg, bool? hotFlg, int viewCount, string tag,
            DateTime dateCreated, DateTime dateModified, string seoPageTitle, string seoAlias, string seoKeywords, string seoDescription,
            int sortOrder, Status status)
        {
            Id = id;
            Name = name;
            Description = description;
            Content = content;
            Image = image;
            HomeFlg = homeFlg;
            HotFlg = hotFlg;
            ViewCount = viewCount;
            Tag = tag;
            DateCreated = dateCreated;
            DateModified = dateModified;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoKeywords;
            SeoDescription = seoDescription;
            SortOrder = sortOrder;
            Status = status;
        }

        [Required] [StringLength(256)] public string Name { get; set; }
        [StringLength(500)] public string Description { get; set; }
        public string Content { get; set; }
        [StringLength(256)] public string Image { get; set; }
        public bool? HomeFlg { get; set; }
        public bool? HotFlg { get; set; }
        public int ViewCount { get; set; }
        public string Tag { get; set; }

        public ICollection<BlogTag> BlogTags { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [StringLength(256)] public string SeoPageTitle { get; set; }
        [StringLength(256)] public string SeoAlias { get; set; }
        [StringLength(256)] public string SeoKeywords { get; set; }
        [StringLength(256)] public string SeoDescription { get; set; }
        
        public int SortOrder { get; set; }
        public Status Status { get; set; }
    }
}