
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("Provinces")]
	public partial class Province
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, BaseColumnName = "ProvinceId", BaseTableName="Provinces" )]
		public Int32 ProvinceId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "ProvinceName", BaseTableName="Provinces" )]
		public String ProvinceName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, BaseColumnName = "ProvinceId", BaseTableName="Provinces" )]
		public Int32 Expr1 { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "ProvinceName", BaseTableName="Provinces" )]
		public String Expr2 { get; set; }
		
	}
	
	public static partial class ProvinceFields
	{
		public const string ProvinceId = "ProvinceId";
		public const string ProvinceName = "ProvinceName";
		public const string Expr1 = "Expr1";
		public const string Expr2 = "Expr2";
	}
}
