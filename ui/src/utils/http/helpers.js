import { useAuthStore } from '@/store'
import {message} from "ant-design-vue";
import {useRouter} from "vue-router";

let isConfirming = false
export function resolveResError(code, msg) {
  const router = useRouter()
  const  authStore = useAuthStore()
  switch (code) {
    case 401:
      authStore.logout()
        // message.error(msg)

      /*
      if (isConfirming) return
     isConfirming = true
    $dialog.confirm({
       title: '提示',
       type: 'info',
       content: '登录已过期，是否重新登录？',
       confirm() {
         useAuthStore().logout()
         window.$message?.success('已退出登录')
         isConfirming = false
       },
       cancel() {
         isConfirming = false
       },
     })*/
      return false
    case 424:
      return msg;
    case 404:
      message.error(msg)
      router.push("/404")
      break;
    case 11008:
      if (isConfirming) return
      isConfirming = true
      $dialog.confirm({
        title: '提示',
        type: 'info',
        content: `${msg}，是否重新登录？`,
        confirm() {
          useAuthStore().logout()
          window.$message?.success('已退出登录')
          isConfirming = false
        },
        cancel() {
          isConfirming = false
        },
      })
      return false
    case 403:
      msg = '请求被拒绝'
      break
    case 404:
      msg = '请求资源或接口不存在'
      break
    case 500:
      msg = '服务器发生异常'
      break
    default:
      msg = msg ?? `【${code}】: 未知异常!`
      break
  }
  return msg
}
