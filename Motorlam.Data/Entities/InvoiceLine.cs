
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("InvoiceLines")]
	public partial class InvoiceLine
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, BaseColumnName = "InvoiceLineId", BaseTableName="InvoiceLines" )]
		public Int32 InvoiceLineId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ProductId", BaseTableName="InvoiceLines" )]
		public Int32? ProductId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "InvoiceId", BaseTableName="InvoiceLines" )]
		public Int32? InvoiceId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Double, 8, 15, 255, AllowNull = true, BaseColumnName = "InvoiceLineQuantity", BaseTableName="InvoiceLines" )]
		public Double? InvoiceLineQuantity { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "InvoiceLineTotal", BaseTableName="InvoiceLines" )]
		public Decimal? InvoiceLineTotal { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "ProductName", BaseTableName="Products" )]
		public String ProductName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "ProductSellingPrice", BaseTableName="Products" )]
		public Decimal? ProductSellingPrice { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "ProductReference", BaseTableName="Products" )]
		public String ProductReference { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "ProductCost", BaseTableName="Products" )]
		public Decimal? ProductCost { get; set; }
		
		[DataMember]
		[SqlField(DbType.Double, 8, 15, 255, AllowNull = true, BaseColumnName = "InvoiceLineDiscount", BaseTableName="InvoiceLines" )]
		public Double? InvoiceLineDiscount { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "InvoicePrice", BaseTableName="InvoiceLines" )]
		public Decimal? InvoicePrice { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "InvoideLineTotalDiscount", BaseTableName="InvoiceLines" )]
		public Decimal? InvoideLineTotalDiscount { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "BrandProductName", BaseTableName="BrandsProduct" )]
		public String BrandProductName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "SupplierName", BaseTableName="Suppliers" )]
		public String SupplierName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, AllowNull = true, BaseColumnName = "SupplierId", BaseTableName="Suppliers" )]
		public Int32? SupplierId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "BrandProductId", BaseTableName="Products" )]
		public Int32? BrandProductId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "SupplierId", BaseTableName="Products" )]
		public Int32? Expr1 { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "TypeProductName", BaseTableName="TypesProduct" )]
		public String TypeProductName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "TypeProductId", BaseTableName="Products" )]
		public Int32? TypeProductId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "InvoiceProductValue", BaseTableName="InvoiceLines" )]
		public Decimal? InvoiceProductValue { get; set; }
		
	}
	
	public partial class InvoiceLineRepository : Repository<InvoiceLine> 
	{
		public InvoiceLineRepository(DataAccess dataAccess) : base(dataAccess)
		{
		}
	}
	
	public static partial class InvoiceLineFields
	{
		public const string InvoiceLineId = "InvoiceLineId";
		public const string ProductId = "ProductId";
		public const string InvoiceId = "InvoiceId";
		public const string InvoiceLineQuantity = "InvoiceLineQuantity";
		public const string InvoiceLineTotal = "InvoiceLineTotal";
		public const string ProductName = "ProductName";
		public const string ProductSellingPrice = "ProductSellingPrice";
		public const string ProductReference = "ProductReference";
		public const string ProductCost = "ProductCost";
		public const string InvoiceLineDiscount = "InvoiceLineDiscount";
		public const string InvoicePrice = "InvoicePrice";
		public const string InvoideLineTotalDiscount = "InvoideLineTotalDiscount";
		public const string BrandProductName = "BrandProductName";
		public const string SupplierName = "SupplierName";
		public const string SupplierId = "SupplierId";
		public const string BrandProductId = "BrandProductId";
		public const string Expr1 = "Expr1";
		public const string TypeProductName = "TypeProductName";
		public const string TypeProductId = "TypeProductId";
		public const string InvoiceProductValue = "InvoiceProductValue";
	}
}
