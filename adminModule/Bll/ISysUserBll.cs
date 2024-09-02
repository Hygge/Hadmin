using api.Common.DTO;
using domain.Records;
using domain.Vo;
using infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adminModule.Bll
{
    public interface ISysUserBll
    {
        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public long Login(string userName, string password);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="createdBy"></param>
        public void Add(SysUserDto userDto, string createdBy);

        /// <summary>
        /// 分页获取用户
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public Pager<SysUserVo> GetList(Dictionary<string, object> map);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        public void Delete(long id);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="sysUserDto"></param>
        public void Update(SysUserDto sysUserDto);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public void UpdatePassword(long userId, string? password, string newPassword);

        public UserInfo GetInfo(long id);
    }
}
