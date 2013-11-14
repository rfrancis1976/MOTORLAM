using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Motorlam.Entities;
using Motorlam.Data.Models;

namespace inercya.MillHill.Web.Models
{
    public class PageSizesViewModel
    {
        public PageInfo PageInfo { get; set; }
        public IList<PageSize> PageSizes { get; set; }
        public string GridContainerId { get; set; }
        public string Prefix { get; set; }
    }

    public class PageRecordsViewModel
    {
        public PageInfo PageInfo { get; set; }
        public string PluralNameEntityResource { get; set; }
    }
}