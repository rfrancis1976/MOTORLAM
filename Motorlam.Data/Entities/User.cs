
using System;
using System.Data;
using System.Runtime.Serialization;
using inercya.ORMLite;

namespace Motorlam.Entities
{
	
	[Serializable]
	[DataContract]
	[SqlMetadata("Users")]
	public partial class User
	{
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, IsKey=true, IsAutoincrement=true, IsReadOnly = true, BaseColumnName = "UserId", BaseTableName="Users" )]
		public Int32 UserId { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 100, 255, 255, BaseColumnName = "UserName", BaseTableName="Users" )]
		public String UserName { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 15, 255, 255, BaseColumnName = "UserPasswordHash", BaseTableName="Users" )]
		public String UserPasswordHash { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "CreatedDate", BaseTableName="Users" )]
		public DateTime? CreatedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "CreatedBy", BaseTableName="Users" )]
		public Int32? CreatedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.DateTime, 8, 23, 3, AllowNull = true, BaseColumnName = "ModifiedDate", BaseTableName="Users" )]
		public DateTime? ModifiedDate { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "ModifiedBy", BaseTableName="Users" )]
		public Int32? ModifiedBy { get; set; }
		
		[DataMember]
		[SqlField(DbType.AnsiString, 50, 255, 255, BaseColumnName = "UserLoginName", BaseTableName="Users" )]
		public String UserLoginName { get; set; }
		
		[DataMember]
		[SqlField(DbType.Int32, 4, 10, 255, AllowNull = true, BaseColumnName = "PageSize", BaseTableName="Users" )]
		public Int32? PageSize { get; set; }
		
	}
	
	public partial class UserRepository : Repository<User> 
	{
		public UserRepository(DataAccess dataAccess) : base(dataAccess)
		{
		}
	}
	
	public static partial class UserFields
	{
		public const string UserId = "UserId";
		public const string UserName = "UserName";
		public const string UserPasswordHash = "UserPasswordHash";
		public const string CreatedDate = "CreatedDate";
		public const string CreatedBy = "CreatedBy";
		public const string ModifiedDate = "ModifiedDate";
		public const string ModifiedBy = "ModifiedBy";
		public const string UserLoginName = "UserLoginName";
		public const string PageSize = "PageSize";
	}
}
