import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { fileURLToPath, URL } from 'node:url'

export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  https: false,
  server: {
    port: 3000
  },
  devServer: {
      proxy: {
        '/app': {
          target: 'http://api.gateway.local:81',
          changeOrigin: false,
          secure: false,
      }     
    }
  }
})