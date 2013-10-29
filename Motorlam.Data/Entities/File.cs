
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("Files")]
	public partial class File
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "FileId", BaseTableName="Files" )]
		public Int32 FileId { get; set; }
		
		[DataMember]
		[SqlField(DbType.Binary, 2147483647, 255, 255, BaseColumnName = "FileContent", BaseTableName="Files" )]
		public Byte[] FileContent { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, BaseColumnName = "EntityId", BaseTableName="Files" )]
		public Int32 EntityId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 250, 255, 255, BaseColumnName = "FileName", BaseTableName="Files" )]
		public String FileName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 1000, 255, 255, BaseColumnName = "FileDescription", BaseTableName="Files" )]
		public String FileDescription { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, BaseColumnName = "CreatedBy", BaseTableName="Files" )]
		public Int32 CreatedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, BaseColumnName = "CreatedDate", BaseTableName="Files" )]
		public DateTime CreatedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ModifiedBy", BaseTableName="Files" )]
		public Int32? ModifiedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "ModifiedDate", BaseTableName="Files" )]
		public DateTime? ModifiedDate { get; set; }
		
	}
	
	public partial class FileRepository : Repository<File> 
	{
		public FileRepository(DataAccess dataAccess) : base(dataAccess)
		{
		}
	}
	
	public static partial class FileFields
	{
		public const string FileId = "FileId";
		public const string FileContent = "FileContent";
		public const string EntityId = "EntityId";
		public const string FileName = "FileName";
		public const string FileDescription = "FileDescription";
		public const string CreatedBy = "CreatedBy";
		public const string CreatedDate = "CreatedDate";
		public const string ModifiedBy = "ModifiedBy";
		public const string ModifiedDate = "ModifiedDate";
	}
}
