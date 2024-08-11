import { resolveResError } from './helpers'
import { useAuthStore } from '@/store'
import { message } from 'ant-design-vue'

export function setupInterceptors(axiosInstance) {
    function reqResolve(config) {
        // 处理不需要token的请求
        if (config.noNeedToken) {
            return config
        }

        const { accessToken } = useAuthStore()
        if (accessToken) {
            // token: Bearer + xxx
            config.headers.Authorization = 'Bearer ' + accessToken
        }

        return config
    }

    function reqReject(error) {
        return Promise.reject(error)
    }

    const SUCCESS_CODES = [0, 200]
    function resResolve(response) {
        const { data, status, config, statusText, headers } = response
        if (headers['content-type']?.includes('json')) {
            if (SUCCESS_CODES.includes(data?.code)) {
                return Promise.resolve(data)
            }

            const code = data?.code ?? status
            // 根据code处理对应的操作，并返回处理后的message
            const msg = resolveResError(code, data?.message ?? statusText)
            console.log(msg)
            //需要错误提醒
            // !config.noNeedTip && window.$message?.error(message)
            message.error(msg)
            return Promise.reject({ code, msg, error: data ?? response })
        }
        return Promise.resolve(data ?? response)
    }

    async function resReject(error) {
        if (!error || !error.response) {
            const code = error?.code
            /** 根据code处理对应的操作，并返回处理后的message */


            const msg = resolveResError(code, error.message)
            message.error(msg)
            return Promise.reject({ code, msg, error })
        }

        const { data, status, config } = error.response
        const code = data?.code ?? status

        const msg = resolveResError(code, data?.message ?? error.message)
        /** 需要错误提醒 */
        // !config?.noNeedTip && message && window.$message?.error(message)
        return Promise.reject({ code, msg, error: error.response?.data || error.response })
    }

    axiosInstance.interceptors.request.use(reqResolve, reqReject)
    axiosInstance.interceptors.response.use(resResolve, resReject)
}
