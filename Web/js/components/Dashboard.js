// Importamos Login para poder "regresar" a él al cerrar sesión
import { Login } from './Login.js';
import { AddPatient } from './AddPatient.js';

export class Dashboard {

    constructor(container) {
        this.container = container;
        this.user = JSON.parse(localStorage.getItem('healthQuestUser'));

        // Si no hay usuario en localStorage, algo salió mal. Volvemos al Login.
        if (!this.user) {
            new Login(this.container);
            return;
        }

        this.render();
        this._addEvents();
    }

    render() {
        this._loadCSS();
        this.container.innerHTML = this._getHTML();
    }

    // Carga la hoja de estilos del dashboard
    _loadCSS() {
        const styleId = 'dashboard-component-css';
        if (document.getElementById(styleId)) return; // No duplicar

        const link = document.createElement('link');
        link.id = styleId;
        link.rel = 'stylesheet';
        link.href = 'css/components/dashboard.css';
        document.head.appendChild(link);
    }

    // Remueve la hoja de estilos del dashboard
    _removeCSS() {
        const style = document.getElementById('dashboard-component-css');
        if (style) {
            style.remove();
        }
    }

    // Añade los eventos de clic a los botones
    _addEvents() {
        // --- Evento de Salir ---
        const logoutButton = this.container.querySelector('#logout-button');
        logoutButton.addEventListener('click', () => {
            // 1. Limpiar sesión
            localStorage.removeItem('healthQuestUser');

            // 2. Quitar estilos del dashboard
            this._removeCSS();

            // 3. Cargar el componente de Login
            new Login(this.container);
        });

        // --- Evento Agregar Paciente (placeholder) ---
        const addButton = this.container.querySelector('#add-patient-button');
        addButton.addEventListener('click', () => {
            this._removeCSS();
            new AddPatient(this.container);

        });

        // --- Evento Mis Pacientes (placeholder) ---
        const listButton = this.container.querySelector('#list-patients-button');
        listButton.addEventListener('click', () => {
            alert('Cargando pantalla de "Mis Pacientes"...');
            // Aquí cargarías el componente PatientList
            // new PatientList(this.container);
        });
    }

    // Genera el HTML del componente
    _getHTML() {
        // Usamos el nombre del usuario guardado en localStorage
        const userName = this.user.Nombre || 'Fisioterapeuta';

        return `
            <div class="dashboard-container">
                <header class="dashboard-header">
                    <h3>Bienvenido, ${userName}</h3>
                    <h1>HealthQuest</h1>
                </header>
                
                <nav class="dashboard-nav">
                    <button id="add-patient-button" class="nav-button">
                        <span>&#43;</span>
                        Agregar Paciente
                    </button>
                    <button id="list-patients-button" class="nav-button">
                        <span>&#128101;</span>
                        Mis Pacientes
                    </button>
                    <button id="logout-button" class="nav-button logout">
                        <span>&#10162;</span>
                        Salir
                    </button>
                </nav>
            </div>
        `;
    }
}