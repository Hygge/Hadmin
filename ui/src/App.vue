<script setup>
import dayjs from 'dayjs';
import 'dayjs/locale/zh-cn';
import locale from 'ant-design-vue/es/date-picker/locale/zh_CN';
dayjs.locale('zh-cn');

</script>

<template>
  <a-app>
  <a-config-provider
      :locale="locale"
      :theme="{
      token: {
        colorPrimary: '#1677ff',
      },
    }"
  >

    <router-view></router-view>
  </a-config-provider>
  </a-app>
</template>

<style >

body{
  margin: 0;
}
</style>
