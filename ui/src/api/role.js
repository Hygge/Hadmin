import {request} from "@/utils/http/index.js";


export function AddRole(data) {
    // return request.post('/Account/login', data)
    return request({
        url: '/SysRole/add',
        method: 'post',
        params: data
    })
}
export function PutRole(data) {
    // return request.post('/Account/login', data)
    return request({
        url: '/SysRole/put',
        method: 'put',
        data: data
    })
}

export function ListRole(data){
    return request({
        url: '/SysRole/list',
        method: 'get',
        params: data
    })
}
export function DelRole(data){
    return request({
        url: '/SysRole/del/' + data,
        method: 'delete'
    })
}
export function GetPermissonsMenuIdsByRole(data){
    return request({
        url: '/SysRole/getPermissonMenuIds/' + data,
        method: 'get'
    })
}
export function AddPermissons(data){
    return request({
        url: '/SysRole/addPermissons/' ,
        method: 'post',
        data: data
    })
}
export function AadBindUserRole(data){
    return request({
        url: '/SysRole/addBindUserRole/' ,
        method: 'post',
        data: data
    })
}
export function getRoleBindUser(data){
    return request({
        url: '/SysRole/getRoleBindUser/' + data,
        method: 'get'
    })
}

