// js/services/api.js
// ¡ESTE ARCHIVO ES PARA EL NAVEGADOR!
// NO LLEVA 'import express', 'mysql2', etc.

// 1. DEFINE LA URL DE TU API (LA QUE CREASTE CON SERVER.JS)
const API_BASE_URL = 'http://localhost:3000';

// 2. CREA EL OBJETO 'api'
const api = {

    // Función helper para hacer llamadas
    async request(endpoint, options = {}) {
        const url = `${API_BASE_URL}${endpoint}`;

        const config = {
            method: 'GET', // Por defecto es GET
            headers: {
                'Content-Type': 'application/json',
            },
            ...options, // Sobrescribe si se envía un método, body, etc.
        };

        try {
            const response = await fetch(url, config);

            // Intenta leer la respuesta como JSON
            const data = await response.json();

            if (!response.ok) {
                // Si el servidor mandó un error (401, 500, etc.)
                // usa el mensaje de error del JSON (ej. "Email o contraseña incorrectos")
                throw new Error(data.message || 'Error en la petición');
            }

            return data; // Devuelve el JSON (ej. { success: true, ... })

        } catch (error) {
            // Si fetch truena (ej. no se puede conectar a localhost:3000)
            // o si la respuesta no es un JSON válido
            console.error('Error en api.js -> request():', error.message);
            throw error; // Propaga el error para que Login.js lo atrape
        }
    },

    // 3. FUNCIÓN DE LOGIN
    login(email, password) {
        // Llama a: POST http://localhost:3000/login
        return this.request('/login', {
            method: 'POST',
            body: JSON.stringify({ email, password })
        });
        // Espera una respuesta como: { success: true, fisioId: 1 }
    },

    // 4. OTRAS FUNCIONES (DEBEN COINCIDIR CON EL SERVER.JS)
    getPacientes(fisioId) {
        // Llama a: GET http://localhost:3000/patients?fisio=1
        return this.request(`/patients?fisio=${fisioId}`); // 'GET' es el método por defecto
    },

    getPacienteById(id) {
        // Llama a: GET http://localhost:3000/patients/5
        return this.request(`/patients/${id}`);
    }


};