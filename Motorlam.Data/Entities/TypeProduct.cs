
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("TypesProduct")]
	public partial class TypeProduct
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "TypeProductId", BaseTableName="TypesProduct" )]
		public Int32 TypeProductId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "TypeProductName", BaseTableName="TypesProduct" )]
		public String TypeProductName { get; set; }
		
	}
	
	public partial class TypeProductRepository : Repository<TypeProduct> 
	{
		public TypeProductRepository(DataAccess dataAccess) : base(dataAccess)
		{
		}
	}
	
	public static partial class TypeProductFields
	{
		public const string TypeProductId = "TypeProductId";
		public const string TypeProductName = "TypeProductName";
	}
}
