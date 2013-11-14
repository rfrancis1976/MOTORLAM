
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("PageSizes")]
	public partial class PageSize
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "Id", BaseTableName="PageSizes" )]
		public Int32 Id { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "Size", BaseTableName="PageSizes" )]
		public String Size { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "SizeValue", BaseTableName="PageSizes" )]
		public Int32? SizeValue { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CreatedBy", BaseTableName="PageSizes" )]
		public Int32? CreatedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "CreateDate", BaseTableName="PageSizes" )]
		public DateTime? CreateDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ModifiedBy", BaseTableName="PageSizes" )]
		public Int32? ModifiedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "ModifiedDate", BaseTableName="PageSizes" )]
		public DateTime? ModifiedDate { get; set; }
		
	}
	
	public partial class PageSizeRepository : Repository<PageSize> 
	{
		public PageSizeRepository(DataAccess dataAccess) : base(dataAccess)
		{
		}
	}
	
	public static partial class PageSizeFields
	{
		public const string Id = "Id";
		public const string Size = "Size";
		public const string SizeValue = "SizeValue";
		public const string CreatedBy = "CreatedBy";
		public const string CreateDate = "CreateDate";
		public const string ModifiedBy = "ModifiedBy";
		public const string ModifiedDate = "ModifiedDate";
	}
}
