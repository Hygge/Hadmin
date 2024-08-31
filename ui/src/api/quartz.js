import {request} from "@/utils/http/index.js";



export function FindQuartz(data){
    return request({
        url: '/Quartz/list',
        method: 'post',
        params: data
    })
}

