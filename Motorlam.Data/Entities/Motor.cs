
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("Motors")]
	public partial class Motor
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "MotorId", BaseTableName="Motors" )]
		public Int32 MotorId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "MotorType", BaseTableName="Motors" )]
		public String MotorType { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "MotorName", BaseTableName="Motors" )]
		public String MotorName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ModelId", BaseTableName="ModelMotors" )]
		public Int32? ModelId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, AllowNull = true, BaseColumnName = "ModelMotorId", BaseTableName="ModelMotors" )]
		public Int32? ModelMotorId { get; set; }
		
	}
	
	public static partial class MotorFields
	{
		public const string MotorId = "MotorId";
		public const string MotorType = "MotorType";
		public const string MotorName = "MotorName";
		public const string ModelId = "ModelId";
		public const string ModelMotorId = "ModelMotorId";
	}
}
