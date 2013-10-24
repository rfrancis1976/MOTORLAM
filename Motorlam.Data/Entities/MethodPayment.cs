
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("MethodsPayment	")]
	public partial class MethodPayment
	{
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "MethodPaymentName", BaseTableName="MethodsPayment" )]
		public String MethodPaymentName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, BaseColumnName = "MethodPaymentId", BaseTableName="MethodsPayment" )]
		public Int32 MethodPaymentId { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "CreatedDate", BaseTableName="MethodsPayment" )]
		public DateTime? CreatedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "CreatedBy", BaseTableName="MethodsPayment" )]
		public Int32 CreatedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "ModifiedDate", BaseTableName="MethodsPayment" )]
		public DateTime? ModifiedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ModifiedBy", BaseTableName="MethodsPayment" )]
		public Int32? ModifiedBy { get; set; }
		
	}
	
	public static partial class MethodPaymentFields
	{
		public const string MethodPaymentName = "MethodPaymentName";
		public const string MethodPaymentId = "MethodPaymentId";
		public const string CreatedDate = "CreatedDate";
		public const string CreatedBy = "CreatedBy";
		public const string ModifiedDate = "ModifiedDate";
		public const string ModifiedBy = "ModifiedBy";
	}
}
