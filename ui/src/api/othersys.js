import {request} from "@/utils/http/index.js";

export function GetList(data){
    return request({
        url: '/OtherSysInfo/getList',
        method: 'get',
        params: data
    })
}

export function AddOtherSys(data){
    return request({
        url: '/OtherSysInfo/add',
        method: 'post',
        data: data
    })
}
export function UpdateOtherSys(data){
    return request({
        url: '/OtherSysInfo/update',
        method: 'put',
        data: data
    })
}

export function DelOtherSys(data){
    return request({
        url: '/OtherSysInfo/del',
        method: 'delete',
        data: data
    })
}
export function OtherSysBuildAppkey(data){
    return request({
        url: '/OtherSysInfo/buildAppkey',
        method: 'post',
        params: data
    })
}

export function DelOtherSysLog(data){
    return request({
        url: '/OtherSysLog/del',
        method: 'delete',
        data: data
    })
}
export function GetListLog(data){
    return request({
        url: '/OtherSysLog/getList',
        method: 'get',
        params: data
    })
}