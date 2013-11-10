
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("Deliverys")]
	public partial class Delivery
	{
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "RefDelivery", BaseTableName="Deliverys" )]
		public String RefDelivery { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "DeliveryDate", BaseTableName="Deliverys" )]
		public DateTime? DeliveryDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "TotalDeliverySinIVA", BaseTableName="Deliverys" )]
		public Decimal? TotalDeliverySinIVA { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "TotalDeliveryConIVA", BaseTableName="Deliverys" )]
		public Decimal? TotalDeliveryConIVA { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "SupplierId", BaseTableName="Deliverys" )]
		public Int32? SupplierId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "DeliveryId", BaseTableName="Deliverys" )]
		public Int32 DeliveryId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "DeliveryTypePaid", BaseTableName="Deliverys" )]
		public String DeliveryTypePaid { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 1000, 255, 255, BaseColumnName = "DeliveryComments", BaseTableName="Deliverys" )]
		public String DeliveryComments { get; set; }
		
		[DataMember]
		[SqlField(DbType.Boolean, 1, 255, 255, AllowNull = true, BaseColumnName = "DeliveryIsPaid", BaseTableName="Deliverys" )]
		public Boolean? DeliveryIsPaid { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "DeliveryPaidDate", BaseTableName="Deliverys" )]
		public DateTime? DeliveryPaidDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Currency, 8, 19, 255, AllowNull = true, BaseColumnName = "DeliveryNetTotal", BaseTableName="Deliverys" )]
		public Decimal? DeliveryNetTotal { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "SupplierName", BaseTableName="Suppliers" )]
		public String SupplierName { get; set; }
		
	}
	
	public partial class DeliveryRepository : Repository<Delivery> 
	{
		public DeliveryRepository(DataAccess dataAccess) : base(dataAccess)
		{
		}
	}
	
	public static partial class DeliveryFields
	{
		public const string RefDelivery = "RefDelivery";
		public const string DeliveryDate = "DeliveryDate";
		public const string TotalDeliverySinIVA = "TotalDeliverySinIVA";
		public const string TotalDeliveryConIVA = "TotalDeliveryConIVA";
		public const string SupplierId = "SupplierId";
		public const string DeliveryId = "DeliveryId";
		public const string DeliveryTypePaid = "DeliveryTypePaid";
		public const string DeliveryComments = "DeliveryComments";
		public const string DeliveryIsPaid = "DeliveryIsPaid";
		public const string DeliveryPaidDate = "DeliveryPaidDate";
		public const string DeliveryNetTotal = "DeliveryNetTotal";
		public const string SupplierName = "SupplierName";
	}
}
