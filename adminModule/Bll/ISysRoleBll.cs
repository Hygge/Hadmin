using domain.Pojo.sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adminModule.Bll
{
    public interface ISysRoleBll
    {

        public List<SysRole> List(string? roleName, string? key, int? status, DateTime? start, DateTime? end);

        public void Add(string name, string key, string remark, int? status, string createBy);

        public void Delete(long id);

        public void Update(SysRole role);

        public void AddUserAndRole(List<long> roleIds, long userId);

        public void AddPermissons(long roleId, List<long> menuIds);

        public void BindUserRole(long id, List<long> roleIds);
        
        public void RoleBindUser(long roleId, List<long> userIds);

        public List<string> GetPermissons(long userId);
        
        public List<long> GetPermissonsMenuIdsByRole(long roleId);

        public List<long> GetRoleBindUserIds(long id);

    }

}
