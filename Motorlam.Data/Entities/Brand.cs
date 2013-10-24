
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("Brands")]
	public partial class Brand
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "BrandId", BaseTableName="Brands" )]
		public Int32 BrandId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "BrandName", BaseTableName="Brands" )]
		public String BrandName { get; set; }
		
	}
	
	public static partial class BrandFields
	{
		public const string BrandId = "BrandId";
		public const string BrandName = "BrandName";
	}
}
