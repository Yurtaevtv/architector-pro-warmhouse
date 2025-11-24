// src/services/api.js
import axios from 'axios'

// Базовый URL API
const API_BASE_URL = import.meta.env.VITE_API_URL;


const api = axios.create({
  baseURL: API_BASE_URL,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json;charset=UTF-8',
  }
})

console.log(API_BASE_URL);

export const deviceApi = {
  getDevices: () => api.get('/devices')
  // getDevice: (id) => api.get(`/devices/${id}`)
}


export default api