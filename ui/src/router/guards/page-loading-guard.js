// 路由跳转加载进度条

export function createPageLoadingGuard(router) {
    router.beforeEach(() => {
       //$loadingBar.start()
    })

    router.afterEach(() => {
        setTimeout(() => {
         //   $loadingBar.finish()
        }, 200)
    })

    router.onError(() => {
      //  $loadingBar.error()
    })
}
