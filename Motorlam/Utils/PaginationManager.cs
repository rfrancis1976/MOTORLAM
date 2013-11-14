using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using inercya.ORMLite;
using Motorlam.Web.Models;
using Motorlam.Data;
using Motorlam.Data.Models;

namespace Motorlam.Web
{

    public class PaginationManager<TEntity> where TEntity: class, new()
    {

        private PageInfo pageInfo;
        private Func<IQueryLite<TEntity>> queryCreationFunction;


        public PaginationManager(PageInfo pageInfo, Func<IQueryLite<TEntity>> queryCreationFunction)
        {
            if (pageInfo == null) throw new ArgumentNullException("pageInfo");
            if (queryCreationFunction == null) throw new ArgumentNullException("queryCreationFunction");
            this.pageInfo = pageInfo;
            this.queryCreationFunction = queryCreationFunction;
            pageInfo.Initialize<TEntity>(GetCount);
        }

        private int GetCount()
        {
            var query = queryCreationFunction(); 
            return query.GetCount();
        }


        public IList<TEntity> GetPage()
        {
            return CreateQueryWithOrder().ToList(this.pageInfo.PageFirstRecordIndex, this.pageInfo.PageLastRecordIndex);
        }

        private IQueryLite<TEntity> CreateQueryWithOrder()
        {
            var query = queryCreationFunction(); 
                     
            if (this.pageInfo.Order == "Desc")
            {
                query.OrderByDesc(this.pageInfo.Field);
            }
            else
            {
                query.OrderBy(this.pageInfo.Field);
            }
            
            return query;
        }

        public IList<TEntity> ToList()
        {
            var query = CreateQueryWithOrder();
            return query.ToList();
        }
    }
}