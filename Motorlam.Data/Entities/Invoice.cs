
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("Invoices")]
	public partial class Invoice
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "InvoiceId", BaseTableName="Invoices" )]
		public Int32 InvoiceId { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "InvoiceDate", BaseTableName="Invoices" )]
		public DateTime? InvoiceDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.String, 20, 255, 255, IsReadOnly = true, BaseColumnName = "InvoiceDate2", BaseTableName="" )]
		public String InvoiceDate2 { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "InvoiceNetTotal", BaseTableName="Invoices" )]
		public Decimal? InvoiceNetTotal { get; set; }
		
		[DataMember]
		[SqlField(DbType.Double, 8, 15, 255, AllowNull = true, BaseColumnName = "InvoiceDiscount", BaseTableName="Invoices" )]
		public Double? InvoiceDiscount { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "InvoiceTotal", BaseTableName="Invoices" )]
		public Decimal? InvoiceTotal { get; set; }
		
		[DataMember]
		[SqlField(DbType.Boolean, 1, 255, 255, AllowNull = true, BaseColumnName = "IsInvoicePaid", BaseTableName="Invoices" )]
		public Boolean? IsInvoicePaid { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CustomerId", BaseTableName="Invoices" )]
		public Int32? CustomerId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 1000, 255, 255, BaseColumnName = "InvoiceComments", BaseTableName="Invoices" )]
		public String InvoiceComments { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "CreatedDate", BaseTableName="Invoices" )]
		public DateTime? CreatedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CreatedBy", BaseTableName="Invoices" )]
		public Int32? CreatedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "ModifiedDate", BaseTableName="Invoices" )]
		public DateTime? ModifiedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ModifiedBy", BaseTableName="Invoices" )]
		public Int32? ModifiedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 200, 255, 255, BaseColumnName = "InvoiceCustomerName", BaseTableName="Invoices" )]
		public String InvoiceCustomerName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 200, 255, 255, BaseColumnName = "InvoiceCustomerAddress", BaseTableName="Invoices" )]
		public String InvoiceCustomerAddress { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 12, 255, 255, BaseColumnName = "InvoiceCustomerNIF", BaseTableName="Invoices" )]
		public String InvoiceCustomerNIF { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 5, 255, 255, BaseColumnName = "InvoiceCustomerCodPostal", BaseTableName="Invoices" )]
		public String InvoiceCustomerCodPostal { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CityId", BaseTableName="Invoices" )]
		public Int32? CityId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 10, 255, 255, BaseColumnName = "InvoiceCustomerPhone", BaseTableName="Invoices" )]
		public String InvoiceCustomerPhone { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "InvoiceIVA", BaseTableName="Invoices" )]
		public Int32? InvoiceIVA { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "InvoiceBy", BaseTableName="Invoices" )]
		public Int32? InvoiceBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "InvoicePaidDate", BaseTableName="Invoices" )]
		public DateTime? InvoicePaidDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "InvoiceTypePaid", BaseTableName="Invoices" )]
		public String InvoiceTypePaid { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "InvoiceTotalDiscount", BaseTableName="Invoices" )]
		public Decimal? InvoiceTotalDiscount { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CarId", BaseTableName="Invoices" )]
		public Int32? CarId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 10, 255, 255, BaseColumnName = "InvoiceKilometres", BaseTableName="Invoices" )]
		public String InvoiceKilometres { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 30, 255, 255, BaseColumnName = "InvoiceNumber", BaseTableName="Invoices" )]
		public String InvoiceNumber { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "InvoiceIVAId", BaseTableName="Invoices" )]
		public Int32? InvoiceIVAId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "Expense", BaseTableName="Invoices" )]
		public Decimal? Expense { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "CustomerName", BaseTableName="Customers" )]
		public String CustomerName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "CustomerSurName", BaseTableName="Customers" )]
		public String CustomerSurName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "ProvinceName", BaseTableName="Provinces" )]
		public String ProvinceName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ProvinceId", BaseTableName="Cities" )]
		public Int32? ProvinceId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "CityName", BaseTableName="Cities" )]
		public String CityName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "IvaValor", BaseTableName="Ivas" )]
		public Int32? IvaValor { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, IsReadOnly = true, BaseColumnName = "TotalInvoice", BaseTableName="" )]
		public Decimal? TotalInvoice { get; set; }
		
	}
	
	public partial class InvoiceRepository : Repository<Invoice> 
	{
		public InvoiceRepository(DataAccess dataAccess) : base(dataAccess)
		{
		}
	}
	
	public static partial class InvoiceFields
	{
		public const string InvoiceId = "InvoiceId";
		public const string InvoiceDate = "InvoiceDate";
		public const string InvoiceDate2 = "InvoiceDate2";
		public const string InvoiceNetTotal = "InvoiceNetTotal";
		public const string InvoiceDiscount = "InvoiceDiscount";
		public const string InvoiceTotal = "InvoiceTotal";
		public const string IsInvoicePaid = "IsInvoicePaid";
		public const string CustomerId = "CustomerId";
		public const string InvoiceComments = "InvoiceComments";
		public const string CreatedDate = "CreatedDate";
		public const string CreatedBy = "CreatedBy";
		public const string ModifiedDate = "ModifiedDate";
		public const string ModifiedBy = "ModifiedBy";
		public const string InvoiceCustomerName = "InvoiceCustomerName";
		public const string InvoiceCustomerAddress = "InvoiceCustomerAddress";
		public const string InvoiceCustomerNIF = "InvoiceCustomerNIF";
		public const string InvoiceCustomerCodPostal = "InvoiceCustomerCodPostal";
		public const string CityId = "CityId";
		public const string InvoiceCustomerPhone = "InvoiceCustomerPhone";
		public const string InvoiceIVA = "InvoiceIVA";
		public const string InvoiceBy = "InvoiceBy";
		public const string InvoicePaidDate = "InvoicePaidDate";
		public const string InvoiceTypePaid = "InvoiceTypePaid";
		public const string InvoiceTotalDiscount = "InvoiceTotalDiscount";
		public const string CarId = "CarId";
		public const string InvoiceKilometres = "InvoiceKilometres";
		public const string InvoiceNumber = "InvoiceNumber";
		public const string InvoiceIVAId = "InvoiceIVAId";
		public const string Expense = "Expense";
		public const string CustomerName = "CustomerName";
		public const string CustomerSurName = "CustomerSurName";
		public const string ProvinceName = "ProvinceName";
		public const string ProvinceId = "ProvinceId";
		public const string CityName = "CityName";
		public const string IvaValor = "IvaValor";
		public const string TotalInvoice = "TotalInvoice";
	}
}
