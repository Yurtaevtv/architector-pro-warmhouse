<template>
  <div class="device-card" :class="`status-${device.status}`">
    <div class="card-header">
      <div class="device-info">
        <h3>{{ device.name }}</h3>
        <span class="device-type">{{ typeLabel }}</span>
      </div>
      <div class="device-status">
        <span class="status-dot" :class="device.status"></span>
        {{ statusLabel }}
      </div>
    </div>

    <div class="card-content">
      <div class="device-location">
        <i class="pi pi-map-marker"></i>
        {{ device.location }}
      </div>

      <!-- Телеметрия -->
      <div v-if="device.state" class="telemetry">
        <div v-if="device.type === 'light'" class="telemetry-item">
          <span>Состояние:</span>
          <strong>{{ device.state === 'on' ? 'ВКЛ' : 'ВЫКЛ' }}</strong>
        </div>
        <div v-if="device.metrics.brightness" class="telemetry-item">
          <span>Яркость:</span>
          <strong>{{ device.metrics.brightness }}%</strong>
        </div>
        <div v-if="device.metrics.temperature" class="telemetry-item">
          <span>Температура:</span>
          <strong>{{ device.metrics.temperature }}°C</strong>
        </div>
      </div>
    </div>

    <!-- Действия -->
    <div class="card-actions">
      <Button 
        v-if="device.type === 'light'" 
        :label="device.state === 'on' ? 'Выключить' : 'Включить'"
        :class="device.state === 'on' ? 'p-button-danger' : 'p-button-success'"
        @click="toggleLight"
      />
      
      <Button 
        v-if="device.type === 'thermostat'" 
        icon="pi pi-plus" 
        class="p-button-text"
        @click="changeTemp(1)"
      />
      <Button 
        v-if="device.type === 'thermostat'" 
        icon="pi pi-minus" 
        class="p-button-text"
        @click="changeTemp(-1)"
      />
    </div>
  </div>
</template>

<script>
import Button from 'primevue/button'

export default {
  name: 'DeviceCard',
  props: {
    device: {
      type: Object,
      required: true
    }
  },
  emits: ['command'],
  components: {
    Button
  },
  setup(props, { emit }) {
    const typeLabels = {
      light: '💡 Лампа',
      thermostat: '🌡️ Термостат',
      sensor: '📡 Датчик'
    }

    const typeLabel = typeLabels[props.device.type] || props.device.type
    const statusLabel = props.device.status === 'online' ? 'Online' : 'Offline'

    const toggleLight = () => {
      const action = props.device.state === 'on' ? 'turn_off' : 'turn_on'
      emit('command', {
        deviceId: props.device.id,
        deviceName: props.device.name,
        action
      })
    }

    const changeTemp = (delta) => {
      emit('command', {
        deviceId: props.device.id,
        deviceName: props.device.name,
        action: 'set_temperature',
        value: (props.device.temperature || 20) + delta
      })
    }

    return {
      typeLabel,
      statusLabel,
      toggleLight,
      changeTemp
    }
  }
}
</script>

<style scoped>
.device-card {
  background: white;
  border-radius: 12px;
  padding: 1.5rem;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  border-left: 4px solid #e5e7eb;
}

.device-card.status-online {
  border-left-color: #10b981;
}

.device-card.status-offline {
  border-left-color: #6b7280;
  opacity: 0.7;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 1rem;
}

.device-info h3 {
  margin: 0 0 0.25rem 0;
  color: #1f2937;
}

.device-type {
  color: #6b7280;
  font-size: 0.9rem;
}

.device-status {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  font-size: 0.9rem;
  color: #6b7280;
}

.status-dot {
  width: 8px;
  height: 8px;
  border-radius: 50%;
}

.status-dot.online {
  background: #10b981;
}

.status-dot.offline {
  background: #6b7280;
}

.card-content {
  margin-bottom: 1.5rem;
}

.device-location {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  color: #6b7280;
  margin-bottom: 1rem;
}

.telemetry {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.telemetry-item {
  display: flex;
  justify-content: space-between;
  padding: 0.5rem;
  background: #f8f9fa;
  border-radius: 6px;
}

.card-actions {
  display: flex;
  gap: 0.5rem;
  flex-wrap: wrap;
}
</style>