<template>
  <div class="devices-view">
    <div class="header">
      <h1>Мои устройства</h1>
      <Button label="Добавить" icon="pi pi-plus" @click="showAddDialog = true" />
    </div>

    <!-- Статистика -->
    <div class="stats">
      <div class="stat-card">
        <div class="stat-number">{{ devices.length }}</div>
        <div class="stat-label">Всего</div>
      </div>
      <div class="stat-card online">
        <div class="stat-number">{{ onlineCount }}</div>
        <div class="stat-label">Online</div>
      </div>
    </div>

    <!-- Список устройств -->
    <div class="devices-grid">
      <DeviceCard 
        v-for="device in devices" 
        :key="device.id"
        :device="device"
        @command="sendCommand"
      />
    </div>

    <!-- Диалог добавления -->
    <Dialog v-model:visible="showAddDialog" header="Добавить устройство">
      <div class="add-form">
        <InputText v-model="newDevice.name" placeholder="Название" class="full-width" />
        <Dropdown 
          v-model="newDevice.type" 
          :options="deviceTypes" 
          optionLabel="name" 
          placeholder="Тип устройства"
          class="full-width"
        />
        <InputText v-model="newDevice.location" placeholder="Местоположение" class="full-width" />
      </div>
      <template #footer>
        <Button label="Отмена" @click="showAddDialog = false" />
        <Button label="Добавить" @click="addDevice" />
      </template>
    </Dialog>

    <Toast />
  </div>
</template>

<script>
import { ref, computed, onMounted } from 'vue'
import { useToast } from 'primevue/usetoast'
import Button from 'primevue/button'
import Dialog from 'primevue/dialog'
import InputText from 'primevue/inputtext'
import Dropdown from 'primevue/dropdown'
import Toast from 'primevue/toast'
import DeviceCard from '@/components/DeviceCard.vue'

export default {
  components: {
    Button,
    Dialog,
    InputText,
    Dropdown,
    Toast,
    DeviceCard
  },
  setup() {
    const toast = useToast()
    const devices = ref([])
    const showAddDialog = ref(false)
    const newDevice = ref({
      name: '',
      type: null,
      location: ''
    })

    const deviceTypes = [
      { name: 'Лампа', value: 'light' },
      { name: 'Термостат', value: 'thermostat' },
      { name: 'Датчик', value: 'sensor' }
    ]

    const onlineCount = computed(() => 
      devices.value.filter(d => d.status === 'online').length
    )

    // Загрузка тестовых данных
    const loadDevices = () => {
      devices.value = [
        {
          id: 1,
          name: 'Кухонный свет',
          type: 'light',
          location: 'Кухня',
          status: 'online',
          state: 'on',
          brightness: 80
        },
        {
          id: 2,
          name: 'Термостат гостиная',
          type: 'thermostat',
          location: 'Гостиная',
          status: 'online',
          temperature: 22
        },
        {
          id: 3,
          name: 'Датчик движения',
          type: 'sensor',
          location: 'Прихожая',
          status: 'offline'
        }
      ]
    }

    const addDevice = () => {
      if (!newDevice.value.name) {
        toast.add({ severity: 'error', summary: 'Ошибка', detail: 'Введите название', life: 3000 })
        return
      }

      const device = {
        id: Date.now(),
        name: newDevice.value.name,
        type: newDevice.value.type?.value,
        location: newDevice.value.location,
        status: 'online',
        state: 'off'
      }

      devices.value.push(device)
      showAddDialog.value = false
      newDevice.value = { name: '', type: null, location: '' }
      
      toast.add({ severity: 'success', summary: 'Успех', detail: 'Устройство добавлено', life: 3000 })
    }

    const sendCommand = (command) => {
      console.log('Sending command:', command)
      toast.add({ 
        severity: 'success', 
        summary: 'Команда отправлена', 
        detail: `${command.deviceName}: ${command.action}`,
        life: 3000 
      })
    }

    onMounted(loadDevices)

    return {
      devices,
      showAddDialog,
      newDevice,
      deviceTypes,
      onlineCount,
      addDevice,
      sendCommand
    }
  }
}
</script>

<style scoped>
.devices-view {
  padding: 1rem 0;
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 2rem;
}

.stats {
  display: flex;
  gap: 1rem;
  margin-bottom: 2rem;
}

.stat-card {
  background: white;
  padding: 1.5rem;
  border-radius: 8px;
  text-align: center;
  min-width: 120px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.stat-card.online {
  border-left: 4px solid #10b981;
}

.stat-number {
  font-size: 2rem;
  font-weight: bold;
  color: #1f2937;
}

.stat-label {
  color: #6b7280;
  margin-top: 0.5rem;
}

.devices-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: 1.5rem;
}

.add-form {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.full-width {
  width: 100%;
}
</style>