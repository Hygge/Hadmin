import {request} from "@/utils/http/index.js";


export function getRolePermissions(data){
    return request({

    })
}

export function TreeMenus(){
    return request({
        url: '/SysMenu/tree',
        method: 'get'
    })
}

export function ListMenus(data){
    return request({
        url: '/SysMenu/list',
        method: 'get',
        params: data
    })
}

export function AddMenus(data){
    return request({
        url: '/SysMenu/addMenu',
        method: 'post',
        data: data
    })
}
export function ModifyMenus(data){
    return request({
        url: '/SysMenu/modify',
        method: 'put',
        data: data
    })
}
export function RemoveMenus(data){
    return request({
        url: '/SysMenu/delMenu/' + data,
        method: 'delete',
    })
}