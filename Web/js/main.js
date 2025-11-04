// 1. Importar *todos* los componentes de nivel superior (pantallas)
import { Login } from './components/Login.js';
import { Dashboard } from './components/Dashboard.js';

// 2. Esperar a que la página esté lista
document.addEventListener('DOMContentLoaded', () => {

    // 3. Obtener el contenedor principal
    const appRoot = document.getElementById('app-root');

    // 4. --- Lógica de Ruteo Simple ---
    // Revisamos si tenemos datos de usuario guardados
    const user = localStorage.getItem('healthQuestUser');

    if (user) {
        // Si el usuario ya inició sesión, lo mandamos al Dashboard
        new Dashboard(appRoot);
    } else {
        // Si no, le mostramos el Login
        new Login(appRoot);
    }
});