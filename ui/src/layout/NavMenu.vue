<script setup>
import { MehOutlined,MailOutlined, AppstoreOutlined, SettingOutlined } from '@ant-design/icons-vue';
import { watch, ref, h, reactive  } from 'vue';
import {usePermissionStore, useTabStore} from "@/store/index.js";
import {useRouter} from "vue-router";

const permissionStore = usePermissionStore()
const value = ref('');

watch(value, () => {
  console.log(value.value);
});

const selectedKeys = ref(['1']);

function getItem(label, key, icon, children, type, disabled = false) {
  return {
    key,
    icon,
    children,
    label,
    type,
    disabled,
  };
}
const state = reactive({
  rootSubmenuKeys: ['sub1', 'sub2', 'sub4'],
  openKeys: ['sub1'],
  selectedKeys: [],
});
const onOpenChange = openKeys => {
  const latestOpenKey = openKeys.find(key => state.openKeys.indexOf(key) === -1);
  if (state.rootSubmenuKeys.indexOf(latestOpenKey) === -1) {
    state.openKeys = openKeys;
  } else {
    state.openKeys = latestOpenKey ? [latestOpenKey] : [];
  }
};
const router = useRouter()
const tabStore = useTabStore()
const onRouter = item =>{
  permissionStore.accessRoutess.forEach(o => {
    if (o.name === item.key){
      tabStore.setActiveTab(o.path)
      router.push(o.path)
    }

  })
//   没有的路由属于外链


}
</script>

<template>
  <div class="sider-menu" >
    <div style="padding: 10px; ">
      <a-input v-model:value="value" placeholder="请输入菜单名称" />
    </div>

    <a-menu
        theme="dark"
        v-model:selectedKeys="state.selectedKeys"
        mode="inline"

        :items="permissionStore.menus"
        @openChange="onOpenChange"
        @click="onRouter"
    ></a-menu>

  </div>
</template>

<style >
.sider-menu{
  height: 93.7vh;
  overflow: initial;
  background: #001529;
}
</style>