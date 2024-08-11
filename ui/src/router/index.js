//路由文件
import {createRouter, createWebHashHistory, createWebHistory} from 'vue-router'
import {useAuthStore, usePermissionStore, useUserStore} from "@/store/index.js";
import {setupRouterGuards} from "@/router/guards/index.js";


const basicRoutes = [
    {
        path: '/',
        name: 'index', //唯一
        component: () => import('@/layout/MainLayout.vue'),
        children : [
            // {
            //     path: '/index',
            //     name: 'home',
            //     meta: {
            //         title: '控制台',
            //         keepAlive: "home",
            //     },
            //     component: () => import('@/views/home/index.vue'),
            //     //独享守卫
            //     beforeEnter(to,from,next){
            //         next()
            //     }
            // },
            {
                path: '/404',
                name: '404',
                component: () =>  import('@/views/404/index.vue'),
            }

        ]

    },

    {
        path: '/login',
        name: 'login',
        component: () => import('@/views/login/login.vue'),
        meta: {
            title: '登录页',
            layout: 'empty',
        },
    },


]
export const router = createRouter({
    history:
        import.meta.env.VITE_USE_HASH === 'true' ? createWebHashHistory('/') : createWebHistory('/'),
    routes: basicRoutes,
    scrollBehavior: () => ({ left: 0, top: 0 }),
})

//全局守卫
router.beforeEach((to,from,next)=>{

    if (to.path === '/')
        router.replace('/console')
    else {
        next()
    }
    // next()
})

router.beforeResolve((to,from,next)=>{
    next()
})

router.afterEach((to,from)=>{

})

export async function setupRouter(app){
    try {
       await initUserAndPermissions()
    } catch (error) {
        console.error('🚀 初始化失败', error)
    }
     setupRouterGuards(router)
    app.use(router)
}


//  初始化用户和权限
export async function initUserAndPermissions() {
    const permissionStore = usePermissionStore()
    const userStore = useUserStore()
    const authStore = useAuthStore()

    if (!authStore.accessToken) {
        authStore.toLogin()
        return
    }
    // [userStore.getUserInfo(),
    await Promise.all([userStore.getUserInfo(), permissionStore.initPermissions()])
    permissionStore.accessRoutess.forEach((route) => {
        if (router.hasRoute(route.name)){
            router.removeRoute(route.name)
        }
        !router.hasRoute(route.name) && router.addRoute("index",route)
    })
}
// 重置路由 一般是退出登录的时候调用
export async function resetRouter() {
    const basicRouteNames = getRouteNames(basicRoutes)
    router.getRoutes().forEach((route) => {
        const name = route.name
        // 基础路由删除
        if (!basicRouteNames.includes(name)) {
            router.removeRoute(name)
        }
    })
}

export function getRouteNames(routes) {
    const names = []
    for (const route of routes) {
        names.push(route.name)
        if (route.children?.length) {
            names.push(...getRouteNames(route.children))
        }
    }
    return names
}
