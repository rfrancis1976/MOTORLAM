
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("Suppliers")]
	public partial class Supplier
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "SupplierId", BaseTableName="Suppliers" )]
		public Int32 SupplierId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "SupplierName", BaseTableName="Suppliers" )]
		public String SupplierName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 200, 255, 255, BaseColumnName = "SupplierAddress", BaseTableName="Suppliers" )]
		public String SupplierAddress { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 5, 255, 255, BaseColumnName = "SupplierCodPostal", BaseTableName="Suppliers" )]
		public String SupplierCodPostal { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 12, 255, 255, BaseColumnName = "SupplierPhone1", BaseTableName="Suppliers" )]
		public String SupplierPhone1 { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 12, 255, 255, BaseColumnName = "SupplierPhone2", BaseTableName="Suppliers" )]
		public String SupplierPhone2 { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 12, 255, 255, BaseColumnName = "SupplierFax1", BaseTableName="Suppliers" )]
		public String SupplierFax1 { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 12, 255, 255, BaseColumnName = "SupplierFax2", BaseTableName="Suppliers" )]
		public String SupplierFax2 { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CityId", BaseTableName="Suppliers" )]
		public Int32? CityId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 10, 255, 255, BaseColumnName = "SupplierNIF", BaseTableName="Suppliers" )]
		public String SupplierNIF { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "SupplierMail", BaseTableName="Suppliers" )]
		public String SupplierMail { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "SupplierWeb", BaseTableName="Suppliers" )]
		public String SupplierWeb { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CreatedBy", BaseTableName="Suppliers" )]
		public Int32? CreatedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "CreatedDate", BaseTableName="Suppliers" )]
		public DateTime? CreatedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ModifiedBy", BaseTableName="Suppliers" )]
		public Int32? ModifiedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "ModifiedDate", BaseTableName="Suppliers" )]
		public DateTime? ModifiedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 150, 255, 255, BaseColumnName = "SupplierContactName", BaseTableName="Suppliers" )]
		public String SupplierContactName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "CityName", BaseTableName="Cities" )]
		public String CityName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ProvinceId", BaseTableName="Cities" )]
		public Int32? ProvinceId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "ProvinceName", BaseTableName="Provinces" )]
		public String ProvinceName { get; set; }
		
	}
	
	public static partial class SupplierFields
	{
		public const string SupplierId = "SupplierId";
		public const string SupplierName = "SupplierName";
		public const string SupplierAddress = "SupplierAddress";
		public const string SupplierCodPostal = "SupplierCodPostal";
		public const string SupplierPhone1 = "SupplierPhone1";
		public const string SupplierPhone2 = "SupplierPhone2";
		public const string SupplierFax1 = "SupplierFax1";
		public const string SupplierFax2 = "SupplierFax2";
		public const string CityId = "CityId";
		public const string SupplierNIF = "SupplierNIF";
		public const string SupplierMail = "SupplierMail";
		public const string SupplierWeb = "SupplierWeb";
		public const string CreatedBy = "CreatedBy";
		public const string CreatedDate = "CreatedDate";
		public const string ModifiedBy = "ModifiedBy";
		public const string ModifiedDate = "ModifiedDate";
		public const string SupplierContactName = "SupplierContactName";
		public const string CityName = "CityName";
		public const string ProvinceId = "ProvinceId";
		public const string ProvinceName = "ProvinceName";
	}
}
