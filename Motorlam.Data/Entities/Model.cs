
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("Models")]
	public partial class Model
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "ModelId", BaseTableName="Models" )]
		public Int32 ModelId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "ModelName", BaseTableName="Models" )]
		public String ModelName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "BrandId", BaseTableName="Models" )]
		public Int32? BrandId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CreatedBy", BaseTableName="Models" )]
		public Int32? CreatedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "CreatedDate", BaseTableName="Models" )]
		public DateTime? CreatedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ModifiedBy", BaseTableName="Models" )]
		public Int32? ModifiedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "ModifiedDate", BaseTableName="Models" )]
		public DateTime? ModifiedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "BrandName", BaseTableName="Brands" )]
		public String BrandName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "MotorName", BaseTableName="Motors" )]
		public String MotorName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "MotorType", BaseTableName="Motors" )]
		public String MotorType { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "MotorId", BaseTableName="ModelMotors" )]
		public Int32? MotorId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, BaseColumnName = "ModelMotorId", BaseTableName="ModelMotors" )]
		public Int32 ModelMotorId { get; set; }
		
	}
	
	public static partial class ModelFields
	{
		public const string ModelId = "ModelId";
		public const string ModelName = "ModelName";
		public const string BrandId = "BrandId";
		public const string CreatedBy = "CreatedBy";
		public const string CreatedDate = "CreatedDate";
		public const string ModifiedBy = "ModifiedBy";
		public const string ModifiedDate = "ModifiedDate";
		public const string BrandName = "BrandName";
		public const string MotorName = "MotorName";
		public const string MotorType = "MotorType";
		public const string MotorId = "MotorId";
		public const string ModelMotorId = "ModelMotorId";
	}
}
