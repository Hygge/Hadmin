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

export function StatusJob(data){
    return request({
        url: '/Quartz/statusJob',
        method: 'post',
        params: data
    })
}

