// js/components/Login.js
import { Dashboard } from './Dashboard.js';

export class Login {
    constructor(container) {
        this.container = container;
        // Pon aquí la URL pública de tu Railway (asegúrate de que coincida)
        this.API_URL = 'http://localhost:3000/login';

        this.render();
        this._addEvents();
    }

    render() {
        this._loadCSS();
        this.container.innerHTML = this._getHTML();
    }

    _loadCSS() {
        const styleId = 'login-component-css';
        if (document.getElementById(styleId)) return;

        const link = document.createElement('link');
        link.id = styleId;
        link.rel = 'stylesheet';
        link.href = 'css/components/login.css';
        document.head.appendChild(link);
    }

    _removeCSS() {
        const style = document.getElementById('login-component-css');
        if (style) style.remove();
    }

    _addEvents() {
        const loginForm = this.container.querySelector('#loginForm');
        if (loginForm) loginForm.addEventListener('submit', this._handleSubmit.bind(this));
    }

    async _handleSubmit(e) {
        e.preventDefault();

        const email = this.container.querySelector('#email').value.trim();
        const password = this.container.querySelector('#password').value;
        const errorMessage = this.container.querySelector('#errorMessage');

        errorMessage.textContent = '';

        try {
            const response = await fetch(this.API_URL, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ email, password })
            });

            const data = await response.json();

            if (response.ok && data.success) {
                // Guardamos los datos que el backend devuelve en la propiedad `fisio`
                localStorage.setItem('healthQuestUser', JSON.stringify(data.fisio));

                // Quitamos estilos y cargamos dashboard
                this._removeCSS();
                new Dashboard(this.container);
            } else {
                // Mensaje de error del backend
                errorMessage.textContent = data.message || 'Error al iniciar sesión.';
            }
        } catch (err) {
            console.error('Error de conexión:', err);
            errorMessage.textContent = 'No se pudo conectar al servidor.';
        }
    }

    _getHTML() {
        return `
      <div class="login-container">
        <h2>Iniciar Sesión</h2>
        <form id="loginForm">
          <div class="input-group">
            <label for="email">Correo Electrónico</label>
            <input type="email" id="email" name="email" required>
          </div>
          <div class="input-group">
            <label for="password">Contraseña</label>
            <input type="password" id="password" name="password" required>
          </div>
          <button type="submit" class="login-button">Entrar</button>
          <p id="errorMessage" style="color:crimson;margin-top:8px;"></p>
        </form>
      </div>
    `;
    }
}
