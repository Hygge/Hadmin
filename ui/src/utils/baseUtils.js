


// 遍历重置对象属性
export const resetObj = (obj) =>{
    Object.keys(obj).forEach( item =>{
        obj[item] = ''
    })
}

// 对象赋值  参数1原数据对象，参数2目标对象
export const copyObj = (sobj, tobj) =>{
    Object.keys(tobj).forEach( item =>{
        Object.keys(sobj).forEach( x => {
            if (item === x){
                tobj[item] = sobj[x];
            }
        })
    })
}

// 校验对象属性是否有空值
export function checkAllPropertiesEmpty(obj) {
    for (let key in obj) {
        if (obj.hasOwnProperty(key) && (obj[key] === null || obj[key] === undefined || obj[key] === '')) {
            return true; // 只要有一个属性为空就返回true
        }
    }
    return false; // 所有属性都不为空
}