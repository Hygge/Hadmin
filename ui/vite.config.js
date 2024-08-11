import { defineConfig } from 'vite'
import { resolve } from 'path'
import vue from '@vitejs/plugin-vue'
import Components from 'unplugin-vue-components/vite';
import { AntDesignVueResolver } from 'unplugin-vue-components/resolvers';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue(),


    Components({
      resolvers: [
        AntDesignVueResolver({
          importStyle: false, // css in js
        }),
      ],
    }),
  ],

  resolve: {
    alias: {
      "@": resolve(__dirname, "src"),
    },
  },
  server: {
    host: '0.0.0.0',
    port: 3200,
    open: false,
    proxy: {
      '/api': {
        target: "https://localhost:7205",
        // https链接需要加上
        secure: false,
        changeOrigin: true,
        pathRewrite: {
          '^/api': '/api'
        },
        // rewrite: (path) => path.replace(new RegExp('^/api'), '/api'),
        configure: (proxy, options) => {
          // 配置此项可在响应头中看到请求的真实地址
          proxy.on('proxyRes', (proxyRes, req) => {
            proxyRes.headers['x-real-url'] = new URL(req.url || '', options.target)?.href || ''
          })
        },
      },
    },
  }

})
