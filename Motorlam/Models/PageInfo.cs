using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Motorlam.Entities;
using inercya.ORMLite;
using System.Web.Routing;
using Motorlam.Data;

namespace Motorlam.Data.Models
{
    public class PageInfo
    {
        public string Field { get; set; }
        public string Order { get; set; }
        public int? Page { get; set; }
        public int? Pages { get; set; }
        public int? Records { get; set; }
        public string Title { get; set; }
        public bool ShowRefreshButton { get; set; }
        public int FirstPage
        {
            get
            {
                if (Pages > 9)
                {
                    if (Pages - Page <= 4)
                    {
                        return Pages.Value - 8;
                    }
                    else
                    {
                        if (Page <= 4)
                        {
                            return 1;
                        }
                        else
                        {
                            return Page.Value - 4;
                        }
                    }
                }
                else
                {
                    return 1;
                }
            }
        }
        public int LastPage
        {
            get
            {
                if (Pages > 9)
                {
                    if (Pages - Page <= 4)
                    {
                        return Pages.Value;
                    }
                    else
                    {
                        if (Page <= 4)
                        {
                            return 9;
                        }
                        else
                        {
                            return Page.Value + 4;
                        }
                    }
                }
                else
                {
                    if (Pages == 0 || Pages == null) return 1;
                    return Pages.Value;
                }
            }
        }
        public string Action { get; set; }
        public int PageSize { get; set; }
        public string Parameters { get; set; }
        public object ActionParameters { get; set; }
        public string ContollerName { get; set; }

        public int PageFirstRecordIndex
        {
            get
            {
                if (Page == null) return 0;
                return (Page.Value - 1) * PageSize;
            }
        }

        public int PageLastRecordIndex
        {
            get
            {
                if (Page == null) return 0;
                return Page.Value * PageSize - 1;
            }
        }

        public void Initialize<TEntity>(Func<int> GetCount)
        {
            var user = (User)DataService.Current.UserRepository.CreateQuery(Proyection.Basic).Get(DataService.Current.CurrentUserId);

            int userPageSize = 50;

            if (user.PageSize > 0) userPageSize = (int)user.PageSize;

            if (PageSize == 0)
            {
                PageSize = userPageSize;
            }
            else if (PageSize != userPageSize)
            {
                UpdateUserPageSize(PageSize);

            }

            Records = GetCount();
            
            if (Page == null)
            {
                Page = 1;
            }

            Pages = Records.Value / PageSize + (Records.Value % PageSize > 0 ? 1 : 0);

            if (string.IsNullOrEmpty(Field))
            {
                var defaultSort = typeof(TEntity).GetDefaultSort();
                Field = defaultSort.FirstOrDefault().FieldName;
                Order = defaultSort.FirstOrDefault().SortOrder == System.Data.SqlClient.SortOrder.Ascending ? "Asc" : "Desc";
            }
            if (string.IsNullOrEmpty(Order))
            {
                Order = "Asc";
            }

        }

        public RouteValueDictionary GetRouteValues()
        {
            return GetRouteValues(string.Empty);
        }

        public RouteValueDictionary GetRouteValues(string prefix)
        {
            var routeValues = new RouteValueDictionary(){
                {"Field", this.Field },
                {"Order", this.Order},
                {"Page", this.Page},
                {"PageSize", this.PageSize },
                {"Records", this.Records },
                {"Title",this.Title }
         
            };

            if (this.ActionParameters != null)
            {
                var actionParametersType = this.ActionParameters.GetType();
                foreach (var pi in actionParametersType.GetProperties())
                {
                    routeValues.Add((!string.IsNullOrEmpty(prefix) ? prefix + "." : "") + pi.Name, pi.GetValue(this.ActionParameters, null));
                }
            }
            return routeValues;
        }

        public RouteValueDictionary GetRouteValues(int page)
        {
            return GetRouteValues(page, string.Empty);
        }

        public RouteValueDictionary GetRouteValues(int page, string prefix)
        {
            var routeValues = GetRouteValues(prefix);
            routeValues["Page"] = page;
            return routeValues;
        }

        public RouteValueDictionary GetRouteValues(string sortOrder, string fieldName, string title)
        {
            return GetRouteValues(sortOrder, fieldName, title, string.Empty);
        }

        public RouteValueDictionary GetRouteValues(string sortOrder, string fieldName, string title, string prefix)
        {
            var routeValues = GetRouteValues(prefix);
            routeValues["Order"] = sortOrder;
            routeValues["Field"] = fieldName;
            routeValues["Page"] = 1;
            routeValues["Title"] = title;
            return routeValues;
        }

        public RouteValueDictionary GetRouteValuesWithoutRecords()
        {
            var routeValues = GetRouteValues();
            routeValues.Remove("Records");
            return routeValues;
        }


        private void UpdateUserPageSize(int newPageSize)
        {
            var user = (User)DataService.Current.UserRepository.CreateQuery(Proyection.Basic).Get(DataService.Current.CurrentUserId);
            user.PageSize = newPageSize;
            DataService.Current.Update(user);
        }
    }
}