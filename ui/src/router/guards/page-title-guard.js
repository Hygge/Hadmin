const baseTitle = import.meta.env.VITE_TITLE
// 根据浏览器标签页标题

export function createPageTitleGuard(router) {
    router.afterEach((to) => {
        const pageTitle = to.meta?.title
        if (pageTitle) {
            // document.title = `${pageTitle} | ${baseTitle}`
            document.title = `${pageTitle} | HAdmin`
        } else {
            document.title = baseTitle
        }
    })
}
