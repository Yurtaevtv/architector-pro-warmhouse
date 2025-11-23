// src/services/api.js
import axios from 'axios'

// Базовый URL API
const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://device-service:5000/api'


const api = axios.create({
  baseURL: API_BASE_URL,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json'
  }
})

console.log(API_BASE_URL);

// Интерцептор для добавления токена
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('auth_token')
    if (token) {
      config.headers.Authorization = `Bearer ${token}`
    }
    return config
  },
  (error) => {
    return Promise.reject(error)
  }
)

// Интерцептор для обработки ошибок
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      // Перенаправление на страницу логина
      localStorage.removeItem('auth_token')
      window.location.href = '/login'
    }
    return Promise.reject(error)
  }
)

export const deviceApi = {
  // Получить все устройства
  getDevices: () => api.get('/devices'),
  
  // Получить устройство по ID
  getDevice: (id) => api.get(`/devices/${id}`)

}


export default api