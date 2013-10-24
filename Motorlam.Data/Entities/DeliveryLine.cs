
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("DeliveryLines")]
	public partial class DeliveryLine
	{
		[DataMember]
		[SqlField(DbType.Double, 8, 15, 255, AllowNull = true, BaseColumnName = "DeliveryLineQuantity", BaseTableName="DeliveryLines" )]
		public Double? DeliveryLineQuantity { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "CostUnit", BaseTableName="DeliveryLines" )]
		public Decimal? CostUnit { get; set; }
		
		[DataMember]
		[SqlField(DbType.Double, 8, 15, 255, AllowNull = true, BaseColumnName = "discount", BaseTableName="DeliveryLines" )]
		public Double? discount { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "LineCostTotal", BaseTableName="DeliveryLines" )]
		public Decimal? LineCostTotal { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "DeliveryId", BaseTableName="DeliveryLines" )]
		public Int32? DeliveryId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, BaseColumnName = "DeliveryLineId", BaseTableName="DeliveryLines" )]
		public Int32 DeliveryLineId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ProductId", BaseTableName="DeliveryLines" )]
		public Int32? ProductId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "ProductName", BaseTableName="Products" )]
		public String ProductName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "ProductReference", BaseTableName="Products" )]
		public String ProductReference { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "TypeProductId", BaseTableName="Products" )]
		public Int32? TypeProductId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "BrandProductId", BaseTableName="Products" )]
		public Int32? BrandProductId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "SupplierId", BaseTableName="Products" )]
		public Int32? SupplierId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "BrandProductName", BaseTableName="BrandsProduct" )]
		public String BrandProductName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "ProductCost", BaseTableName="Products" )]
		public Decimal? ProductCost { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "ProductSellingPrice", BaseTableName="Products" )]
		public Decimal? ProductSellingPrice { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 1000, 255, 255, BaseColumnName = "ProductDescription", BaseTableName="Products" )]
		public String ProductDescription { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "SupplierName", BaseTableName="Suppliers" )]
		public String SupplierName { get; set; }
		
	}
	
	public static partial class DeliveryLineFields
	{
		public const string DeliveryLineQuantity = "DeliveryLineQuantity";
		public const string CostUnit = "CostUnit";
		public const string discount = "discount";
		public const string LineCostTotal = "LineCostTotal";
		public const string DeliveryId = "DeliveryId";
		public const string DeliveryLineId = "DeliveryLineId";
		public const string ProductId = "ProductId";
		public const string ProductName = "ProductName";
		public const string ProductReference = "ProductReference";
		public const string TypeProductId = "TypeProductId";
		public const string BrandProductId = "BrandProductId";
		public const string SupplierId = "SupplierId";
		public const string BrandProductName = "BrandProductName";
		public const string ProductCost = "ProductCost";
		public const string ProductSellingPrice = "ProductSellingPrice";
		public const string ProductDescription = "ProductDescription";
		public const string SupplierName = "SupplierName";
	}
}
