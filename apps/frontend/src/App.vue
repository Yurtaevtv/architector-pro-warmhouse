<template>
  <div id="app">
    <nav class="navbar">
      <div class="nav-brand">
        <i class="pi pi-home"></i>
        <span>Smart Home</span>
      </div>
      <div class="nav-user" v-if="user">
        {{ user.name }}
        <Button icon="pi pi-power-off" class="p-button-text" @click="logout" />
      </div>
    </nav>

    <main class="main-content">
      <DevicesView v-if="currentView === 'devices'" />
      <div v-else class="welcome">
        <h1>Добро пожаловать в Smart Home</h1>
        <p>Выберите раздел для управления</p>
      </div>
    </main>

    <Toast />
  </div>
</template>

<script>
import { ref } from 'vue'
import Button from 'primevue/button'
import Toast from 'primevue/toast'
import DevicesView from './views/DevicesView.vue'


export default {
  name: 'App',
  components: {
    Button,
    Toast,
    DevicesView
  },
  setup() {
    const currentView = ref('devices')
    const user = ref({ name: 'Администратор' })

    const logout = () => {
      console.log('Logout')
    }

    return {
      currentView,
      user,
      logout
    }
  }
}
</script>

<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  font-family: -apple-system, BlinkMacSystemFont, sans-serif;
  background: #f5f5f5;
}

#app {
  min-height: 100vh;
}

.navbar {
  background: white;
  padding: 1rem 2rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.nav-brand {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  font-weight: bold;
  font-size: 1.2rem;
}

.main-content {
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
}

.welcome {
  text-align: center;
  padding: 4rem 2rem;
}
</style>