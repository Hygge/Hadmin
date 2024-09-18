<script setup>
import {UserOutlined,LockOutlined } from '@ant-design/icons-vue'
import {useAuthStore, useTabStore, useUserStore} from "@/store/index.js";
import {useRoute, useRouter} from "vue-router";
import {initUserAndPermissions} from "@/router/index.js";
import {reactive, computed, toRaw, ref } from "vue";
import {Login} from "@/api/user.js";
import {message} from "ant-design-vue";

const userStore = useUserStore()
const authStore = useAuthStore()
const router = useRouter()
const route = useRoute()
const store = useTabStore()


const formState = reactive({
  userName: '',
  password: '',
  // remember: true,
});
const onFinish = values => {
  console.log('Success:', values);
  login()
};
const onFinishFailed = errorInfo => {
  console.log('Failed:', errorInfo);
};
const disabled = computed(() => {
  return !(formState.userName && formState.password);
});

const login = async () => {

    const data = await Login(toRaw(formState))

      message.success("登录成功！")
      authStore.setToken(data.data)
      await initUserAndPermissions()
         if (route.query.redeirect) {
      const path = route.query.redirect
      delete route.query.redirect
      router.push({ path, query: route.query })
      } else {
          router.push('/')
      }

}

const logo = ref('../../../public/MicrosoftStartLogo_light.svg')
const bgm = ref('../../assets/screenshot.png')
</script>

<template>

  <a-flex :vertical="true" justify="center" align="center" :gap="40" style="background-color: #e5f0f8;height: 100vh;">

      <a-form
          :model="formState"
          name="normal_login"
          class="login-form"
          @finish="onFinish"
          @finishFailed="onFinishFailed"
      >

        <a-flex :vertical="true" style="margin-bottom: 40px;" justify="center" align="center" >
          <a-image width="50%" :src="logo" :preview="false" mode="aspectFill" />
          <span style="font-weight: bold;font-size: 20px;margin: 20px;">Hadmin</span>
        </a-flex>
        <a-flex :vertical="true" gap="middle">
        <a-form-item
            name="userName"
            :rules="[{ required: true, message: 'Please input your userName!' }]"
        >
          <a-input v-model:value="formState.userName" size="large">
            <template #prefix>
              <UserOutlined class="site-form-item-icon" />
            </template>
          </a-input>
        </a-form-item>

        <a-form-item
            name="password"
            :rules="[{ required: true, message: 'Please input your password!' }]"
        >
          <a-input-password v-model:value="formState.password" size="large">
            <template #prefix>
              <LockOutlined class="site-form-item-icon" />
            </template>
          </a-input-password>
        </a-form-item>

        <a-form-item>
          <a-button :disabled="disabled" type="primary" size="large" html-type="submit" class="login-form-button">
            登录
          </a-button>
        </a-form-item>
          <span style="margin: 0 auto;" > © 2024 - Hygge  </span>
        </a-flex>

      </a-form>




  </a-flex>

<!--
  background-image: url("../../assets/screenshot.png");
  background-repeat: no-repeat;
  background-size: cover;
-->
<!--
  <a-flex gap="middle" horizontal class="login-page">

    <a-flex class="login-left">

      阿斯顿
    </a-flex>
    <a-flex class="login-right" justify="center" align="center" vertical>
      <a-image   width="80%" :src="bgm" :preview="false" ></a-image>
    </a-flex>

  </a-flex>
-->

</template>

<style scoped>
.login-page{
  width: 100%;
  height: 100vh;
  background-color: rgb(0, 0, 0.88);
  z-index: -9;
}

.login-left{
  width: 30%;
  height: 100vh;
  background-color: white;
  z-index: 9;
}
.login-right{
  min-width: 70%;
  height: 100vh;
/*  background-image: url("../../assets/screenshot.png");
  background-position: center center;
  background-attachment: fixed;
  background-repeat: no-repeat;
  background-size: cover;*/
  z-index: 999;
}


.login-form {
  width: 450px;
  background-color: white;
  padding: 60px;
  border-radius: 5px;
}
 .login-form-forgot {
  float: right;
}
 .login-form-button {
  width: 100%;
}
 .login-form-input{

 }

</style>