<script setup>
import { MehOutlined,MailOutlined, AppstoreOutlined, SettingOutlined } from '@ant-design/icons-vue';
import { watch, ref, h, reactive, onUnmounted, onMounted } from 'vue';
import {usePermissionStore, useTabStore} from "@/store/index.js";
import {useRouter} from "vue-router";

onMounted(() =>{
  // 开启键盘监听事件
  // window.addEventListener('keydown', search, true)

  menues.value = permissionStore.menus

})

const value = ref('');
const menues = ref([])
const menues1 = ref([])

watch(value, () => {
  console.log(value.value)
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

const findMenuItem = (menuList, key) => {

  for (let i = 0; i < menuList.length; i++) {
    let b = menuList[i].label.includes(key)
    let b1 = findMenuItem2(menuList[i].children, key)
    if (!b && b1){
      menuList.splice(i, 1)
      i--
    }
  }

}

const findMenuItem2 = (menuList, key) => {
  if (menuList === undefined || menuList.length === 0)
    return true;
  for (let i = 0; i < menuList.length; i++) {
    let b = menuList[i].label.includes(key)
    let b1 = findMenuItem2(menuList[i].children, key)
    if (!b && b1){
      menuList.splice(i, 1);
      i--;
    }
  }
  return menuList.length > 0 ? false : true;
}
const permissionStore = usePermissionStore()

const search = () => {
  permissionStore.initPermissions()
  let data = permissionStore.menus
  if (value.value !== '' ) {
    findMenuItem(data, value.value)
    menues.value = data
  }
  menues.value = data
}

onUnmounted(() => {
  //销毁事件
  // window.removeEventListener('keydown', search, false)
});
</script>

<template>
  <div class="sider-menu" >
    <div style="padding: 10px; ">
      <a-input v-model:value="value" @keyup.enter="search" placeholder="请输入菜单名称" />
    </div>

    <a-menu
        theme="dark"
        v-model:selectedKeys="state.selectedKeys"
        mode="inline"
        :items="menues"
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