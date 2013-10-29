
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("Products")]
	public partial class Product
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "ProductId", BaseTableName="Products" )]
		public Int32 ProductId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "ProductName", BaseTableName="Products" )]
		public String ProductName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "TypeProductId", BaseTableName="Products" )]
		public Int32? TypeProductId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "SupplierId", BaseTableName="Products" )]
		public Int32? SupplierId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 1000, 255, 255, BaseColumnName = "ProductDescription", BaseTableName="Products" )]
		public String ProductDescription { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "ProductCost", BaseTableName="Products" )]
		public Decimal? ProductCost { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CreatedBy", BaseTableName="Products" )]
		public Int32? CreatedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "CreatedDate", BaseTableName="Products" )]
		public DateTime? CreatedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ModifiedBy", BaseTableName="Products" )]
		public Int32? ModifiedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "ModifiedDate", BaseTableName="Products" )]
		public DateTime? ModifiedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "BrandProductId", BaseTableName="Products" )]
		public Int32? BrandProductId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "ProductSellingPrice", BaseTableName="Products" )]
		public Decimal? ProductSellingPrice { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "ProductReference", BaseTableName="Products" )]
		public String ProductReference { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "TypeProductName", BaseTableName="TypesProduct" )]
		public String TypeProductName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "SupplierName", BaseTableName="Suppliers" )]
		public String SupplierName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 150, 255, 255, BaseColumnName = "SupplierContactName", BaseTableName="Suppliers" )]
		public String SupplierContactName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "BrandProductName", BaseTableName="BrandsProduct" )]
		public String BrandProductName { get; set; }
		
	}
	
	public partial class ProductRepository : Repository<Product> 
	{
		public ProductRepository(DataAccess dataAccess) : base(dataAccess)
		{
		}
	}
	
	public static partial class ProductFields
	{
		public const string ProductId = "ProductId";
		public const string ProductName = "ProductName";
		public const string TypeProductId = "TypeProductId";
		public const string SupplierId = "SupplierId";
		public const string ProductDescription = "ProductDescription";
		public const string ProductCost = "ProductCost";
		public const string CreatedBy = "CreatedBy";
		public const string CreatedDate = "CreatedDate";
		public const string ModifiedBy = "ModifiedBy";
		public const string ModifiedDate = "ModifiedDate";
		public const string BrandProductId = "BrandProductId";
		public const string ProductSellingPrice = "ProductSellingPrice";
		public const string ProductReference = "ProductReference";
		public const string TypeProductName = "TypeProductName";
		public const string SupplierName = "SupplierName";
		public const string SupplierContactName = "SupplierContactName";
		public const string BrandProductName = "BrandProductName";
	}
}
