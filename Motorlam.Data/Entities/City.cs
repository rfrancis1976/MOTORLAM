
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("Cities")]
	public partial class City
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, BaseColumnName = "CityId", BaseTableName="Cities" )]
		public Int32 CityId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "CityName", BaseTableName="Cities" )]
		public String CityName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ProvinceId", BaseTableName="Cities" )]
		public Int32? ProvinceId { get; set; }
		
	}
	
	public static partial class CityFields
	{
		public const string CityId = "CityId";
		public const string CityName = "CityName";
		public const string ProvinceId = "ProvinceId";
	}
}
