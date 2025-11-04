import { Dashboard } from './Dashboard.js';

export class AddPatient {
    constructor(container) {
        this.container = container;
        this.user = JSON.parse(localStorage.getItem('healthQuestUser'));

        if (!this.user) {
            alert("Sesión expirada. Inicia sesión de nuevo.");
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

    _loadCSS() {
        const styleId = 'add-patient-css';
        if (document.getElementById(styleId)) return;

        const link = document.createElement('link');
        link.id = styleId;
        link.rel = 'stylesheet';
        link.href = 'css/components/addpatient.css';
        document.head.appendChild(link);
    }

    _removeCSS() {
        const style = document.getElementById('add-patient-css');
        if (style) style.remove();
    }

    _addEvents() {
        // 🔙 Botón para volver al Dashboard
        this.container.querySelector('#back-dashboard').addEventListener('click', () => {
            this._removeCSS();
            new Dashboard(this.container);
        });

        // 💾 Botón para guardar paciente
        this.container.querySelector('#save-patient').addEventListener('click', async (e) => {
            e.preventDefault();

            const form = this.container.querySelector('#patient-form');
            const data = Object.fromEntries(new FormData(form).entries());
            data.fisioterapeuta_asignado_id = this.user.id_usuario; // Asociar al fisioterapeuta logueado

            if (!data.nombres || !data.apellidos || !data.email) {
                alert('Por favor, completa los campos obligatorios.');
                return;
            }

            try {
                const response = await fetch("http://localhost:3000/api/pacientes", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(data)
                });

                const result = await response.json();

                if (response.ok && result.success) {
                    alert(`✅ Paciente agregado (ID: ${result.paciente_id})`);
                    this._removeCSS();
                    new Dashboard(this.container);
                } else {
                    alert(`⚠️ Error: ${result.error || 'No se pudo guardar el paciente.'}`);
                }
            } catch (err) {
                console.error("🔥 Error de red al guardar:", err);
                alert("No se pudo conectar con el servidor.");
            }
        });
    }

    _getHTML() {
        return `
        <div class="add-patient-container">
            <h2>Agregar Nuevo Paciente</h2>
            <form id="patient-form">
                <div class="input-group">
                    <label for="nombres">Nombres</label>
                    <input type="text" id="nombres" name="nombres" required>
                </div>

                <div class="input-group">
                    <label for="apellidos">Apellidos</label>
                    <input type="text" id="apellidos" name="apellidos" required>
                </div>

                <div class="input-group">
                    <label for="email">Correo Electrónico</label>
                    <input type="email" id="email" name="email" required>
                </div>

                <div class="input-group">
                    <label for="fecha_nacimiento">Fecha de Nacimiento</label>
                    <input type="date" id="fecha_nacimiento" name="fecha_nacimiento" required>
                </div>

                <div class="input-group">
                    <label for="notas_expediente">Notas del Expediente</label>
                    <textarea id="notas_expediente" name="notas_expediente" rows="3"></textarea>
                </div>

                <div class="button-group">
                    <button type="button" id="save-patient" class="save-btn">Guardar Paciente</button>
                    <button type="button" id="back-dashboard" class="back-btn">Volver</button>
                </div>
            </form>
        </div>`;
    }
}
