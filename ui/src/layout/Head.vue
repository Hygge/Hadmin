<script setup>

import {FullscreenOutlined, UserOutlined} from "@ant-design/icons-vue";
import screenfull from "screenfull";
import {useAuthStore, useUserStore} from "@/store/index.js";
import {message} from "ant-design-vue";
import {onMounted, ref, onBeforeUnmount} from "vue";


const nowTime = ref('')
/**
 * 将小于10的数字前面补0
 * @function
 * @param {number} value
 * @returns {string} 返回补0后的字符串
 */
const complement = (value ) => {
  return value < 10 ? `0${value}` : value.toString()
}
/**
 * 格式化时间为XXXX年XX月XX日XX时XX分XX秒
 * @function
 * @returns {string} 返回格式化后的时间
 */
const formateDate = () => {
  const time = new Date()
  const year = time.getFullYear()
  const month = complement(time.getMonth() + 1)
  const day = complement(time.getDate())
  const hour = complement(time.getHours())
  const minute = complement(time.getMinutes())
  const second = complement(time.getSeconds())
  const week = '星期' + '日一二三四五六'.charAt(time.getDay());
  return `${year}年${month}月${day}日 ${hour}:${minute}:${second}`
}
let timer = 0
/**
 * 设置定时器
 */
onMounted(() => {
  timer = setInterval(() => {
    nowTime.value = formateDate()
  }, 1000)
})
/**
 * 取消定时器
 */
onBeforeUnmount(() => {
  clearInterval(timer) //清除定时器
  timer = 0
})

const userStore = useUserStore();
// 全屏和退出全屏
const cusFullScreen = () => {
  if (screenfull.isEnabled && !screenfull.isFullscreen) {
    screenfull.request();
    return;
  }
  if (screenfull.isEnabled && screenfull.isFullscreen) {
    screenfull.exit();

  }
};

const authStore = useAuthStore()
const logout = () =>{
  authStore.logout()
  message.success('注销成功')
}

const color = ref(getRandomColor());

function getRandomColor() {
  const letters = '0123456789ABCDEF';
  let color = '#';
  for (let i = 0; i < 6; i++) {
    color += letters[Math.floor(Math.random() * 16)];
  }
  return color;
}

function changeColor() {
  color.value = getRandomColor();
}
</script>

<template>
  <a-flex :vertical="false" style="width: 96%;">
    <div class="head-content"> </div>
    <a-flex justify="flex-end" :vertical="false" gap="large" style="width: 30%;height: 64px;">
      <span style="font-weight: bold;color: #1677ff;">{{nowTime}}</span>
      <FullscreenOutlined  style="font-size: 26px;" @click="cusFullScreen"/>
      <a-dropdown>
        <a class="ant-dropdown-link" @click.prevent>

          <a-avatar size="large" :style="{ backgroundColor: color, verticalAlign: 'middle' }" :gap="4">
            {{ userStore.nickName }}
          </a-avatar>
<!--         .split('')[0] <UserOutlined />-->
        </a>
        <template #overlay>
          <a-menu>
            <a-menu-item>
              <a href="javascript:;">个人资料</a>
            </a-menu-item>
            <a-menu-item>
              <a href="javascript:;">修改密码</a>
            </a-menu-item>
            <a-menu-item>
              <a @click="logout">退出登录</a>
            </a-menu-item>
          </a-menu>
        </template>
      </a-dropdown>

    </a-flex>
    <div style="width: 50px;">&nbsp;&nbsp;</div>
  </a-flex>
</template>

<style scoped>
.head-content{
  width: 70%;
  height: 64px;

}

</style>