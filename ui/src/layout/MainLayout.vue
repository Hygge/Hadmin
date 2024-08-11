<template>

  <a-layout>
    <a-layout-sider

        v-model:collapsed="collapsed" :trigger="null" collapsible>
      <div class="logo">
<!--         :style="{backgroundImage: 'url(' + logo + ')' }" -->
        <a-image :src="logo" :preview="false" />
      </div>
      <nav-menu />

    </a-layout-sider>
    <a-layout>
      <a-layout-header style="background: #fff; padding: 0;color: black;display: flex;">
        <MenuUnfoldOutlined
            v-if="collapsed"
            class="trigger"
            @click="() => (collapsed = !collapsed)"
        />
        <MenuFoldOutlined  v-else class="trigger" @click="() => (collapsed = !collapsed)" />
        <Head />
      </a-layout-header>
      <Tabs />
      <a-layout-content
          :style="{  padding: '15px', background: '#f5f5f5', minHeight: '280px', overflow: 'initial' }"
      >
        <router-view v-slot="{ Component, route }">

            <keep-alive :include="getIncludedComponents()"
                       >
              <component :is="Component" :key="route.fullPath"/>
            </keep-alive>

        </router-view>

      </a-layout-content>
      <a-layout-footer :style="{ textAlign: 'center' }">
        Ant Design ©2024 Created by Hygge
      </a-layout-footer>
    </a-layout>
  </a-layout>

</template>
<script setup>
import { MenuUnfoldOutlined, MenuFoldOutlined, UserOutlined, FullscreenOutlined} from '@ant-design/icons-vue';
import { ref, onMounted } from 'vue';
import NavMenu from "@/layout/NavMenu.vue";
import {message} from "ant-design-vue";
import Tabs from "@/layout/Tabs.vue";
import {useTabStore} from "@/store/index.js";
import Head from "@/layout/Head.vue";


const logo = ref('')

const tabsStore = useTabStore();
onMounted(() =>{

  // 刷新浏览器保留刚打开页面
  tabsStore.removeOther(tabsStore.activeTab)

  // 可动态配置后端存放
  logo.value =  '../../public/MicrosoftStartLogo_light.svg';

})

const collapsed = ref(false);
const fullscreen = ref(false);



const getIncludedComponents = () =>{
  let kp = []
   tabsStore.tabs.forEach(item => {
     kp.push(item.keepAlive)
   })
  return kp
}

</script>
<style >
.trigger {
  font-size: 18px;
  line-height: 64px;
  padding: 0 24px;
  cursor: pointer;
  transition: color 0.3s;
}

#components-layout-demo-custom-trigger .trigger:hover {
  color: #1890ff;
}

.logo {
  height: 40px;
  background-repeat: no-repeat;
  background-size: cover;
  margin: 14px;
  padding: 10px
}

.site-layout .site-layout-background {
  background: #fff;
}
:where(.css-dev-only-do-not-override-1pp8hoo).ant-layout .ant-layout-footer {
  padding: 10px;
}

</style>