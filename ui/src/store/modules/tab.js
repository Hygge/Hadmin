
import { defineStore } from 'pinia'
import { router } from '@/router'

export const useTabStore = defineStore('tab', {
    state: () => ({
        tabs: [],
        activeTab: '',
        reloading: false,
    }),
    getters: {
        activeIndex() {
            return this.tabs.findIndex((item) => item.path === this.activeTab)
        },

    },
    actions: {
        async setActiveTab(path) {

            this.activeTab = path
        },
        setTabs(tabs) {
            this.tabs = tabs
        },
        addTab(tab = {}) {

            const findIndex = this.tabs.findIndex((item) => item.path === tab.path)
            if (findIndex !== -1) {
                this.tabs.splice(findIndex, 1, tab)
            } else {
                this.setTabs([...this.tabs, tab])
            }
            this.setActiveTab(tab.path)
        },

        async removeTab(path) {
            this.setTabs(this.tabs.filter((tab) => tab.path !== path))
            if (this.tabs.length === 0){
                router.push('/console')
                return
            }
            if (path === this.activeTab) {
                router.push(this.tabs[this.tabs.length - 1].path)
            }
        },
        removeOther(curPath = this.activeTab) {
            this.setTabs(this.tabs.filter((tab) => tab.path === curPath))
            if (curPath !== this.activeTab) {
                router.push(this.tabs[this.tabs.length - 1].path)
            }
        },
        removeLeft(curPath) {
            const curIndex = this.tabs.findIndex((item) => item.path === curPath)
            const filterTabs = this.tabs.filter((item, index) => index >= curIndex)
            this.setTabs(filterTabs)
            if (!filterTabs.find((item) => item.path === this.activeTab)) {
                router.push(filterTabs[filterTabs.length - 1].path)
            }
        },
        removeRight(curPath) {
            const curIndex = this.tabs.findIndex((item) => item.path === curPath)
            const filterTabs = this.tabs.filter((item, index) => index <= curIndex)
            this.setTabs(filterTabs)
            if (!filterTabs.find((item) => item.path === this.activeTab.value)) {
                router.push(filterTabs[filterTabs.length - 1].path)
            }
        },
        resetTabs() {
            this.$reset()
        },
    },
    persist: {
        paths: ['tabs'],
        storage: sessionStorage,
    },
})
