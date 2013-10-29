using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using inercya.ORMLite;

namespace Motorlam.Data
{
    public class BaseTableQueryLite<TEntity> : QueryLite<TEntity> where TEntity : class, new()
    {

        public BaseTableQueryLite(DataAccess dataAccess)
            : base("BaseTable", dataAccess)
        {
        }

        public BaseTableQueryLite()
            : this(null)
        {
        }

        protected override string GetFullViewName(System.Data.Common.DbCommand selectCommand, ref int paramIndex)
        {
            var metadata = inercya.ORMLite.DataAccess.GetEntityMetadata(typeof(TEntity));
            if (string.IsNullOrEmpty(metadata.BaseTableName))
            {
                throw new InvalidOperationException(string.Format("Cannot execute BaseTableQueryLite because entity {0} does not hava a base table", typeof(TEntity).Name));

            }
            return metadata.BaseTableName;
        }
    }

    public static class BaseTableQueryLite
    {

        private static Type genericBaseTableQueryLiteType = typeof(BaseTableQueryLite<>);

        public static IQueryLite Create(Type entityType)
        {
            Type baseTableQueryLiteType = genericBaseTableQueryLiteType.MakeGenericType(entityType);
            return (IQueryLite)DynamicActivatorFactory.GetDynamicActivator(baseTableQueryLiteType)();
        }

        //public static IQueryLite Create(Type entityType, DataAccess datAccess)
        //{
        //    Type baseTableQueryLiteType = genericBaseTableQueryLiteType.MakeGenericType(entityType);
        //    return (IQueryLite)Activator.CreateInstance(baseTableQueryLiteType, new object[] { datAccess });
        //}
    }
}
