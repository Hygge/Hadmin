<script setup>
import { ref, watch } from 'vue';
import {newmenus} from "@/router/menu.js";

const treeData = ref([
  {
    title: '主类目',
    id: 0,
    key: '0',
    children: newmenus,
  },
]);
const selectedKeys = ref([]);
const checkedKeys = ref([]);
watch(selectedKeys, () => {
  console.log('selectedKeys', selectedKeys);
});
watch(checkedKeys, () => {
  console.log('checkedKeys', checkedKeys);
});

</script>

<template>

  <div >
    <a-tree
        v-model:selectedKeys="selectedKeys"
        v-model:checkedKeys="checkedKeys"
        default-expand-all
        checkable
        :height="233"
        :tree-data="treeData"
    >
      <template #title="{ title, key }">
        <span v-if="key === '0-0-1-0'" style="color: #1890ff">{{ title }}</span>
        <template v-else>{{ title }}</template>
      </template>
    </a-tree>
  </div>

</template>

<style scoped>

</style>