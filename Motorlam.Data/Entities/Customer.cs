
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("Customers")]
	public partial class Customer
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "CustomerId", BaseTableName="Customers" )]
		public Int32 CustomerId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "CustomerName", BaseTableName="Customers" )]
		public String CustomerName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "CustomerSurName", BaseTableName="Customers" )]
		public String CustomerSurName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 12, 255, 255, BaseColumnName = "CustomerPhone1", BaseTableName="Customers" )]
		public String CustomerPhone1 { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 12, 255, 255, BaseColumnName = "CustomerPhone2", BaseTableName="Customers" )]
		public String CustomerPhone2 { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 200, 255, 255, BaseColumnName = "CustomerAddress", BaseTableName="Customers" )]
		public String CustomerAddress { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CityId", BaseTableName="Customers" )]
		public Int32? CityId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "CustomerEmail", BaseTableName="Customers" )]
		public String CustomerEmail { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 12, 255, 255, BaseColumnName = "CustomerFax", BaseTableName="Customers" )]
		public String CustomerFax { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CreatedBy", BaseTableName="Customers" )]
		public Int32? CreatedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "CreatedDate", BaseTableName="Customers" )]
		public DateTime? CreatedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ModifiedBy", BaseTableName="Customers" )]
		public Int32? ModifiedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "ModifiedDate", BaseTableName="Customers" )]
		public DateTime? ModifiedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 5, 255, 255, BaseColumnName = "CustomerCodPostal", BaseTableName="Customers" )]
		public String CustomerCodPostal { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 12, 255, 255, BaseColumnName = "CustomerNIF", BaseTableName="Customers" )]
		public String CustomerNIF { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "CityName", BaseTableName="Cities" )]
		public String CityName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "ProvinceName", BaseTableName="Provinces" )]
		public String ProvinceName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ProvinceId", BaseTableName="Cities" )]
		public Int32? ProvinceId { get; set; }
		
	}
	
	public partial class CustomerRepository : Repository<Customer> 
	{
		public CustomerRepository(DataAccess dataAccess) : base(dataAccess)
		{
		}
	}
	
	public static partial class CustomerFields
	{
		public const string CustomerId = "CustomerId";
		public const string CustomerName = "CustomerName";
		public const string CustomerSurName = "CustomerSurName";
		public const string CustomerPhone1 = "CustomerPhone1";
		public const string CustomerPhone2 = "CustomerPhone2";
		public const string CustomerAddress = "CustomerAddress";
		public const string CityId = "CityId";
		public const string CustomerEmail = "CustomerEmail";
		public const string CustomerFax = "CustomerFax";
		public const string CreatedBy = "CreatedBy";
		public const string CreatedDate = "CreatedDate";
		public const string ModifiedBy = "ModifiedBy";
		public const string ModifiedDate = "ModifiedDate";
		public const string CustomerCodPostal = "CustomerCodPostal";
		public const string CustomerNIF = "CustomerNIF";
		public const string CityName = "CityName";
		public const string ProvinceName = "ProvinceName";
		public const string ProvinceId = "ProvinceId";
	}
}
