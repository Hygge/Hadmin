import { request } from '@/utils/http/index.js'

// /Account/login
export function Login(data){
    // return request.post('/Account/login', data)
    return request({
        url: '/Account/login',
        method: 'post',
        params: data
    })
}
export function Info(){
    return request({
        url: '/Account/info',
        method: 'get'
    })
}

export function AddUser(data){
    return request({
        url: '/SysUser',
        method: 'post',
        data: data
    })
}
export function FindUser(data){
    return request({
        url: '/SysUser',
        method: 'get',
        params: data
    })
}
export function RemoveUser(id){
    return request({
        url: '/SysUser/' + id,
        method: 'delete',
    })
}
export function UpdateUser(id,data){
    return request({
        url: '/SysUser/' + id,
        method: 'put',
        data: data
    })
}
export function ResetPassword(id, data){
    console.log(data)
    return request({
        url: '/SysUser/' + id,
        method: 'patch',
        params: data
    })
}
