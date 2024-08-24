import { request } from '@/utils/http/index.js'


export const ListLog = (data) =>{
    return request({
        url: '/SysLog/list',
        method: 'POST',
        params: data
    })
}

export const DelLog = (data) =>{
    return request({
        url: '/SysLog/del',
        method: 'delete',
        data: data
    })
}