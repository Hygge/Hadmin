import {request} from "@/utils/http/index.js";



export function GetList(data){
    return request({
        url: '/Quartz/list',
        method: 'post',
        params: data
    })
}

export function DelJob(data){
    return request({
        url: '/Quartz/delJob',
        method: 'post',
        data: data
    })
}


export function AddJob(data){
    return request({
        url: '/Quartz/addJob',
        method: 'post',
        data: data
    })
}
export function UpdateJob(data){
    return request({
        url: '/Quartz/update',
        method: 'put',
        data: data
    })
}
export function StatusJob(data){
    return request({
        url: '/Quartz/statusJob',
        method: 'post',
        params: data
    })
}

export function GetListLog(data){
    return request({
        url: '/JobLog/list',
        method: 'post',
        params: data
    })
}
export function DelLog(data){
    return request({
        url: '/JobLog/delLog',
        method: 'post',
        data: data
    })
}
export function ClearLog(){
    return request({
        url: '/JobLog/delAll',
        method: 'delete',
    })
}
