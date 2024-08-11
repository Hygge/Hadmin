import { createApp } from 'vue'
import App from './App.vue'
import {setupRouter} from "@/router/index.js";
import {setupStore} from "@/store/index.js";
import * as antIcons from '@ant-design/icons-vue'

async function bootstrap() {
    const app = createApp(App)
    setupStore(app)
    await setupRouter(app)
    // 注册组件
    Object.keys(antIcons).forEach(key => {
        app.component(key, antIcons[key])
    })
    // 添加到全局
    app.config.globalProperties.$antIcons = antIcons

    app.mount('#app')
}

bootstrap()
