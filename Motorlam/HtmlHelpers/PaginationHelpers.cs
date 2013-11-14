using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using inercya.MillHill.Web.Models;
using Motorlam.Web.Models;



namespace inercya.MillHill.Web.HtmlHelpers
{
    public static class PaginationHelpers
    {
        public static void RenderPageSizesDropDown(this HtmlHelper helper, PageSizesViewModel model)
        {
            helper.RenderPartial("PageSizesDropDown", model);
        }

        public static void RenderPaginatedGridColumnHeader(this HtmlHelper helper, PaginatedGridColumnHeaderViewModel model)
        {
            helper.RenderPartial("PaginatedGridColumnHeader", model);
        }

        public static void RenderPaginatedGridColumnHeaders(this HtmlHelper helper, PaginatedGridColumnHeadersViewModel model)
        {
            foreach (var header in model.ColumnHeaders)
            {
                helper.RenderPaginatedGridColumnHeader(new PaginatedGridColumnHeaderViewModel
                {
                    PageInfo = model.PageInfo,
                    FieldName = header.FieldName,
                    FieldTitle = header.FieldTitle,
                    CssClass = header.CssClass,
                    Prefix = model.Prefix,
                    ColumnTitle = header.ColumnTitle
                });
            }
        }

        public static void RenderPageRecordsHeader(this HtmlHelper helper, PageRecordsViewModel model)
        {
            helper.RenderPartial("PageRecordsHeader", model);
        }
    }
}