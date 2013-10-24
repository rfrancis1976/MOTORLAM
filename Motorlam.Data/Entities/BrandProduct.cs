
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("BrandsProduct")]
	public partial class BrandProduct
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "BrandProductId", BaseTableName="BrandsProduct" )]
		public Int32 BrandProductId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "BrandProductName", BaseTableName="BrandsProduct" )]
		public String BrandProductName { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "CreatedDate", BaseTableName="BrandsProduct" )]
		public DateTime? CreatedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CreatedBy", BaseTableName="BrandsProduct" )]
		public Int32? CreatedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "ModifiedDate", BaseTableName="BrandsProduct" )]
		public DateTime? ModifiedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ModifiedBy", BaseTableName="BrandsProduct" )]
		public Int32? ModifiedBy { get; set; }
		
	}
	
	public static partial class BrandProductFields
	{
		public const string BrandProductId = "BrandProductId";
		public const string BrandProductName = "BrandProductName";
		public const string CreatedDate = "CreatedDate";
		public const string CreatedBy = "CreatedBy";
		public const string ModifiedDate = "ModifiedDate";
		public const string ModifiedBy = "ModifiedBy";
	}
}
