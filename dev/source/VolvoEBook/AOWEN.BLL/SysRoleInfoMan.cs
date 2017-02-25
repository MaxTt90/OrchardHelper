using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using AOWEN.DAL;
using AOWEN.Model;

namespace AOWEN.BLL
{       
	 	//SysRoleInfo
	public class SysRoleInfoMan
	{
	    private CustomDbContext db = CustomDbContext.Instance;
        #region 基本方法
        public void Add(SysRoleInfo entity)
        {
            db.SysRoleInfos.Add(entity);
            db.SaveChanges();
        }
        public void Update(SysRoleInfo entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(SysRoleInfo entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            SysRoleInfo entity = this.GetEntity(id);
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public SysRoleInfo GetEntity(int id)
        {
            return db.SysRoleInfos.Find(id);
        }
        #endregion

        /// <summary>
        /// 获得所有角色信息
        /// </summary>
        /// <returns></returns>         
        public IQueryable<SysRoleInfo> GetAllRolesInfo()
        {
            return db.SysRoleInfos.Where(c => c.DataState !=DataStateEnum.删除);
        }

        /// <summary>
        /// 通过用户名获得model
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        public SysRoleInfo GetModelByUsername(string name)
        {
            return db.SysRoleInfos.FirstOrDefault(c => c.DataState != DataStateEnum.删除 && c.Name == name);
        }
        /// <summary>
        /// 通过角色代码获得model
        /// </summary>
        /// <param name="code">角色代码</param>
        /// <returns></returns>
        /// author:duanxianghai 2015-12-2 13:18:07
	    public SysRoleInfo GetModelByCode(string code)
	    {
	        return db.SysRoleInfos.FirstOrDefault(c => c.DataState != DataStateEnum.删除 && c.Code == code);
	    }

	    /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="sum">总页数</param>
        /// <param name="pageSize">页容量</param>
        /// <returns></returns>
        /// author：duanxinaghai 2015-12-1 20:54:33
        public List<SysRoleInfo> GetModelByPage(int pageIndex, out int sum, out int totalCount, int pageSize = 10)
        {
            List<SysRoleInfo> modelList = null;

            totalCount = db.SysRoleInfos.Count(c => c.DataState != DataStateEnum.删除);
            if (totalCount != 0)
            {
                modelList = db.SysRoleInfos.Where(c => c.DataState != DataStateEnum.删除).OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            if (totalCount % pageSize == 0)
            {
                sum = totalCount / pageSize;
            }
            else
            {
                sum = totalCount / pageSize + 1;
            }
            return modelList;
        }

       
	}
}