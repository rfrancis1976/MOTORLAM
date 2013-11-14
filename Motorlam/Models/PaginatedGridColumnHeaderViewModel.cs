using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Motorlam.Web.Models;
using Motorlam.Data.Models;

namespace Motorlam.Web.Models
{
    public class PaginatedGridColumnHeaderViewModel
    {
        public PageInfo PageInfo { get; set; }
        public string FieldName { get; set; }
        public string FieldTitle { get; set; }
        public string CssClass { get; set; }
        public string FieldType { get; set; }
        public string FieldAlign { get; set; }
        public string Prefix { get; set; }
        public string ColumnTitle { get; set; }
        
    }

    public class PaginatedGridColumnHeadersViewModel
    {
        public PageInfo PageInfo { get; set; }
        public IEnumerable<ColumnHeader> ColumnHeaders { get; set; }
        public string Prefix { get; set; }
    }

    public class ColumnHeader
    {
        public string FieldName { get; set; }
        public string FieldTitle { get; set; }
        public string CssClass { get; set; }
        public string FieldType { get; set; }
        public string FieldAlign { get; set; }
        public string ColumnTitle { get; set; }
    }
}