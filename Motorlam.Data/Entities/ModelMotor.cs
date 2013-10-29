
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("ModelMotors")]
	public partial class ModelMotor
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, BaseColumnName = "ModelMotorId", BaseTableName="ModelMotors" )]
		public Int32 ModelMotorId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ModelId", BaseTableName="ModelMotors" )]
		public Int32? ModelId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "MotorId", BaseTableName="ModelMotors" )]
		public Int32? MotorId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "MotorName", BaseTableName="Motors" )]
		public String MotorName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "MotorType", BaseTableName="Motors" )]
		public String MotorType { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "ModelName", BaseTableName="Models" )]
		public String ModelName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, AllowNull = true, BaseColumnName = "BrandId", BaseTableName="Brands" )]
		public Int32? BrandId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "BrandName", BaseTableName="Brands" )]
		public String BrandName { get; set; }
		
	}
	
	public partial class ModelMotorRepository : Repository<ModelMotor> 
	{
		public ModelMotorRepository(DataAccess dataAccess) : base(dataAccess)
		{
		}
	}
	
	public static partial class ModelMotorFields
	{
		public const string ModelMotorId = "ModelMotorId";
		public const string ModelId = "ModelId";
		public const string MotorId = "MotorId";
		public const string MotorName = "MotorName";
		public const string MotorType = "MotorType";
		public const string ModelName = "ModelName";
		public const string BrandId = "BrandId";
		public const string BrandName = "BrandName";
	}
}
