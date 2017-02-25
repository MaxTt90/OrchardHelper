using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AOWEN.DAL;
using AOWEN.Model;

namespace AOWEN.BLL
{       
	 	//SysUserRole
	public class SysUserRoleMan
	{
	    private CustomDbContext db = CustomDbContext.Instance;

        public void Add(SysUserRole entity)
        {
            db.SysUserRoles.Add(entity);
            db.SaveChanges();
        }
        public void Update(SysUserRole entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(SysUserRole entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            SysUserRole entity = this.GetEntity(id);
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public SysUserRole GetEntity(int id)
        {
            return db.SysUserRoles.Find(id);
        }

        /// <summary>
        /// 通过userid获取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// hyp2015-11-16
        public SysUserRole GetModelByUserId(int userId)
        {
            SysUserRole sysUserRole;
            sysUserRole = db.SysUserRoles.Where(m => m.UserId == userId).First();
            return sysUserRole;
        }


	}
}