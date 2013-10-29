
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("Ivas")]
	public partial class Iva
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "Id", BaseTableName="Ivas" )]
		public Int32 Id { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "IvaValor", BaseTableName="Ivas" )]
		public Int32? IvaValor { get; set; }
		
	}
	
	public partial class IvaRepository : Repository<Iva> 
	{
		public IvaRepository(DataAccess dataAccess) : base(dataAccess)
		{
		}
	}
	
	public static partial class IvaFields
	{
		public const string Id = "Id";
		public const string IvaValor = "IvaValor";
	}
}
