
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("Cars")]
	public partial class Car
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "CarId", BaseTableName="Cars" )]
		public Int32 CarId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ModelId", BaseTableName="Cars" )]
		public Int32? ModelId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CustomerId", BaseTableName="Cars" )]
		public Int32? CustomerId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "CarRegistration", BaseTableName="Cars" )]
		public String CarRegistration { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "CarType", BaseTableName="Cars" )]
		public String CarType { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "CarRack", BaseTableName="Cars" )]
		public String CarRack { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "CarColor", BaseTableName="Cars" )]
		public String CarColor { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CreatedBy", BaseTableName="Cars" )]
		public Int32? CreatedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "CreatedDate", BaseTableName="Cars" )]
		public DateTime? CreatedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ModifiedBy", BaseTableName="Cars" )]
		public Int32? ModifiedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "ModifiedDate", BaseTableName="Cars" )]
		public DateTime? ModifiedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 10, 255, 255, BaseColumnName = "CarKilometres", BaseTableName="Cars" )]
		public String CarKilometres { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ModelMotorId", BaseTableName="Cars" )]
		public Int32? ModelMotorId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "MotorId", BaseTableName="Cars" )]
		public Int32? MotorId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CarITVId", BaseTableName="Cars" )]
		public Int32? CarITVId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 250, 255, 255, BaseColumnName = "CarITVName", BaseTableName="Cars" )]
		public String CarITVName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "BrandName", BaseTableName="Brands" )]
		public String BrandName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "ModelName", BaseTableName="Models" )]
		public String ModelName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "BrandId", BaseTableName="Models" )]
		public Int32? BrandId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "CustomerName", BaseTableName="Customers" )]
		public String CustomerName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "CustomerSurName", BaseTableName="Customers" )]
		public String CustomerSurName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "MotorName", BaseTableName="Motors" )]
		public String MotorName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "MotorType", BaseTableName="Motors" )]
		public String MotorType { get; set; }
		
	}
	
	public static partial class CarFields
	{
		public const string CarId = "CarId";
		public const string ModelId = "ModelId";
		public const string CustomerId = "CustomerId";
		public const string CarRegistration = "CarRegistration";
		public const string CarType = "CarType";
		public const string CarRack = "CarRack";
		public const string CarColor = "CarColor";
		public const string CreatedBy = "CreatedBy";
		public const string CreatedDate = "CreatedDate";
		public const string ModifiedBy = "ModifiedBy";
		public const string ModifiedDate = "ModifiedDate";
		public const string CarKilometres = "CarKilometres";
		public const string ModelMotorId = "ModelMotorId";
		public const string MotorId = "MotorId";
		public const string CarITVId = "CarITVId";
		public const string CarITVName = "CarITVName";
		public const string BrandName = "BrandName";
		public const string ModelName = "ModelName";
		public const string BrandId = "BrandId";
		public const string CustomerName = "CustomerName";
		public const string CustomerSurName = "CustomerSurName";
		public const string MotorName = "MotorName";
		public const string MotorType = "MotorType";
	}
}
