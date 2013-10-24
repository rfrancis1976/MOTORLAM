using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Mvc;

namespace inercya.Gataca.Web.Models
{
    public class ChildItem
    {
        /// <summary>
        /// Represents option value in DropDownList
        /// </summary>
        public int value { get; set; }
        /// <summary>
        /// Represents option text in DropDownList
        /// </summary>
        public string text { get; set; }

        public static IList<ChildItem> GetChildItems(IEnumerable<SelectListItem> childItems)
        {
            var ChildItems = (from childItem in childItems
                              select new ChildItem()
                              {
                                  value = int.Parse(childItem.Value),
                                  text = childItem.Text
                              }).ToList();

            return ChildItems;
        }
    }
}