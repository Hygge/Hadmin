<script setup>
import {useTabStore} from "@/store/index.js";
import {computed, ref} from 'vue';
import {useRouter} from 'vue-router';
import {CloseCircleOutlined, SmallDashOutlined }  from '@ant-design/icons-vue';

const tabsStore = useTabStore();

const onEdit = targetKey => {
  tabsStore.removeTab(targetKey)
};

const router = useRouter();
// 计算属性，用于监听菜单的变化
const tabsList = ref(tabsStore.tabs);

const list = computed({
  get: () =>  tabsStore.tabs,
  set: (value) => {
    tabsList.value = value;
  },
});



// 导航到指定路由
const navigateTo = (key) => {
  router.push(key);
};

const handleTabClick = (opt) => {
  navigateTo(opt)
};
const removeAll = () => {
  tabsStore.removeOther()
}


// 清除
const handleTabClean = () => {
  tabsStore.removeTab()
}
const handleTabCleanDirection = (str) => {
  console.log(str)
  switch (str){
    case 'left':
      tabsStore.removeLeft(tabsStore.activeTab)
      break;
    case 'right':
      tabsStore.removeRight(tabsStore.activeTab)
      break;
    case 'all':
      tabsStore.resetTabs()
      navigateTo('/console')
      break;
    case 'other':
      tabsStore.removeOther(tabsStore.activeTab)
      break;
    default:
      tabsStore.resetTabs();
  }
}


</script>

<template>
  <section class="zy-tabs" id="zy-tab">
    <a-tabs size="small" hide-add v-model:activeKey="tabsStore.activeTab" @tabClick="handleTabClick" @edit="onEdit"
            type="editable-card">
      <a-tab-pane  v-for="pane in list" :key="pane.path" :tab="pane.title" :closable="pane.path!=='/console'"/>
    </a-tabs>
    <a-tooltip placement="topLeft">
      <template #title>
        <span>关闭全部</span>
      </template>
      <CloseCircleOutlined class="close-tab close-tab-all"   @click="removeAll" />
    </a-tooltip>
    <a-dropdown class="close-tab" placement="bottom">
      <SmallDashOutlined class="close-tab-other-icon" />
      <template #overlay>
        <a-menu style="width: 90px;">
          <a-menu-item @click="handleTabCleanDirection('left')">
            关闭左侧
          </a-menu-item>
          <a-menu-item @click="handleTabCleanDirection('right')">
            关闭右侧
          </a-menu-item>
          <a-menu-item @click="handleTabCleanDirection('other')">
            关闭其他
          </a-menu-item>
          <a-menu-item @click="handleTabCleanDirection('all')">
            关闭全部
          </a-menu-item>
        </a-menu>
      </template>
    </a-dropdown>
  </section>
</template>

<style lang="scss">
.zy-tabs {
  width: 100%;
  display: flex;


  .ant-tabs {
    width: 100%;
    padding-right: 15px;
    border: none;
    .ant-tabs-tab {

      font-size: .9rem;
      font-family: "Helvetica Neue", Helvetica, "PingFang SC", "Hiragino Sans GB", "Microsoft YaHei", 微软雅黑, Arial, sans-serif;

      &:first-child {
        //border-left: none;
        border-top-left-radius: 5px;
        overflow: hidden;
      }
    }

  }

  .ant-tabs > .ant-tabs-nav {
    margin-bottom: 0;

  }

  .close-tab {
    background: #ffffff;
    border: 1px solid #f0f0f0;
    padding: 8px 16px;
    margin-right: 15px;

  }

  .close-tab-other-icon svg {
    transform: rotate(-90deg);
  }

  .close-tab-all {
    margin-right: 0;
    padding: 8px 16px;
    background: #fafafa;
    border: 1px solid #f0f0f0;
  }
}

</style>