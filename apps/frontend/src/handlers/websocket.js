
class WebSocketService {

  constructor() {
    this.socket = null;
    this.isConnected = false;
  }

  connect() {    
    
    this.socket = new WebSocket(import.meta.env.VITE_WS_URL);
    
    this.socket.onopen = () => {
      console.log('✅ WebSocket connected')
      this.isConnected = true
    }

    this.socket.onmessage = (event) => {
      try {
        const message = JSON.parse(event.data)
        // Просто логируем сообщения
        console.log('📨', message)
      } catch (error) {
        console.error('Error parsing message:', error)
      }
    }

    this.socket.onclose = () => {
      console.log('🔌 WebSocket disconnected')
      this.isConnected = false
    }

    this.socket.onerror = (error) => {
      console.error('❌ WebSocket error:', error)
    }
  }

  send(message) {
    if (this.socket && this.isConnected) {
      this.socket.send(JSON.stringify(message))
    }
  }
}

export default new WebSocketService()