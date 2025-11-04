// 1. Importar el (los) componente(s) que necesitamos
import { Login } from './components/Login.js';

// 2. Esperar a que la página esté lista
document.addEventListener('DOMContentLoaded', () => {

    // 3. Obtener el contenedor principal
    const appRoot = document.getElementById('app-root');

    // 4. "Montar" el componente de Login en el contenedor
    // (En el futuro, aquí tendrías un "router" para decidir
    // si cargar Login, Dashboard, etc.)
    new Login(appRoot);
});