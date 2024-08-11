
import { defineStore } from 'pinia'
import { useUserStore, usePermissionStore, useTabStore } from '@/store'
import { resetRouter, router } from '@/router'
import {unref, } from 'vue'

export const useAuthStore = defineStore('auth', {
    state: () => ({
        accessToken: undefined,
    }),
    actions: {
        setToken({ accessToken }) {
            this.accessToken = accessToken
        },
        resetToken() {
            this.$reset()
        },
        toLogin() {
            const currentRoute = unref(router.currentRoute)
            router.replace({
                path: '/login',
                query: currentRoute.query,
            })
        },
        resetLoginState() {
            const { resetUser } = useUserStore()
            const { resetPermission } = usePermissionStore()
            const { resetTabs } = useTabStore()
            // 重置用户
            resetUser()
            // 重置权限
            resetPermission()
            // 重置Tabs
            resetTabs()
            // 重置路由
             resetRouter()
            // 重置token
            this.resetToken()
        },
        async logout() {
            this.resetLoginState()
            this.toLogin()
        },
    },
    persist: {
        key: 'vue-antdesign-admin_auth',
    },
})
