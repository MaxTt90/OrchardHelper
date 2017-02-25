using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AOWEN.Model;

namespace AOWEN.DAL
{
    public class CustomDbContext : DbContext
    {
        #region 初始化DbContext

        public static CustomDbContext Instance
        {

            get
            {
                CustomDbContext DbContextHelper = null;
                if (System.Runtime.Remoting.Messaging.CallContext.GetData("EFOBJ") == null)
                {
                    DbContextHelper = new CustomDbContext();
                    System.Runtime.Remoting.Messaging.CallContext.SetData("EFOBJ", DbContextHelper);
                }
                else
                {
                    return (CustomDbContext) System.Runtime.Remoting.Messaging.CallContext.GetData("EFOBJ");
                }
                return DbContextHelper;
            }
        }

        public CustomDbContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions
                .Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();

        }

        #endregion

        #region log

        public DbSet<LogTrack> LogTracks { set; get; }

        #endregion

        #region 后台系统

        public DbSet<SysUserInfo> SysUserInfos { get; set; }
        public DbSet<SysRightInfo> SysRightInfos { get; set; }
        public DbSet<SysRoleInfo> SysRoleInfos { get; set; }
        public DbSet<SysRoleRight> SysRoleRights { get; set; }
        public DbSet<SysUserRole> SysUserRoles { get; set; }
        public DbSet<SysCity> SysCitys { get; set; }

        #endregion

    }
}
