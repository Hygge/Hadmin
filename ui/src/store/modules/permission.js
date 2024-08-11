import {h, } from 'vue'
import * as antIcons from '@ant-design/icons-vue'
import { defineStore } from 'pinia'
import { newmenus} from "@/router/menu.js";
import {TreeMenus} from "@/api/menu.js";


//  vite 使用import.meta.glob动态添加vue页面
const routeComponents = import.meta.glob('@/views/**/*.vue')

export const usePermissionStore = defineStore('permission', {
    state: () => ({
        // 菜单
        menus: [],
        // 路由
        accessRoutes: [],
        // 后端返回菜单数据
        asyncPermissions: [],
    }),
    getters: {
        permissions() {
            return basePermissions.concat(this.asyncPermissions)
        },
        accessRoutess(){
            return this.accessRoutes;
        }
    },
    actions: {
        async initPermissions() {
             const { data } = (await TreeMenus()) || []

            this.asyncPermissions = data
            this.menus = this.asyncPermissions
                // 找到菜单类型
                .filter((item) => item.type === 1 || item.type === 2)
                .map((item) => this.getMenuItem(item))
                .filter((item) => !!item)
                .sort((a, b) => a.order - b.order)
        },
        // 递归
        getMenuItem(item, parent) {
            // 生成路由
            const route = this.generateRoute(item, item.show ? null : parent?.key)
            // 判定菜单已启用 并且 路径存在 并且 不是外跳转   添加到路由里
            if (item.enable && route.path && item.target === 0 ) this.accessRoutes.push(route)
            if (!item.show) return null
            // 生成菜单
            const menuItem = {
                label: item.title,
                key: item.key,
                disabled: !item.enable,
                icon: () => h(antIcons[item.icon]),
                order: item.order ?? 0,
            }
            // 过滤出子菜单和目录
            const children = item.children?.filter((item) =>  item.type === 1 || item.type === 2) || []
            // 子菜单存在，生成ui子菜单
            if (children.length) {
                menuItem.children = children
                    .map((child) => this.getMenuItem(child, menuItem))
                    .filter((item) => !!item)
                    .sort((a, b) => a.order - b.order)
                // 子菜生成不存在，则删除子菜单
                if (!menuItem.children.length) delete menuItem.children
            }
            return menuItem
        },

        // 生成路由
        generateRoute(item, parentKey) {

            return {
                name: item.key,
                path: item.route,
                // redirect: item.redirect,
                component: routeComponents[item.path] || undefined,
                meta: {
                    icon: item.icon,
                    title: item.title,
                    layout: item.layout,
                    keepAlive: item.keepAlive,
                    parentKey,
                    btns: item.children
                        ?.filter((item) => item.type === 3)
                        .map((item) => ({ key: item.key, name: item.title })),
                },
            }
        },
        resetPermission() {
            this.$reset()
        },
    },
})
