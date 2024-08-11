﻿using api.Common.DTO;
using domain.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adminModule.Bll
{
    public interface ISysMenuBll
    {

        public List<SysMenuVo> GetList(int? status, string? title);

        public List<SysMenuVo> GetTree(long id);

        public void AddMenu(SysMenuDto sysMenuDto);

        public void UpdateMenu(SysMenuDto sysMenuDto);

        public void Remove(long id);




    }
}