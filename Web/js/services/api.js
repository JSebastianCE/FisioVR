const express = require('express');
const cors = require('cors');
const mysql = require('mysql2/promise'); // Usamos la versión con promesas

const app = express();
const port = 3000;

// --- Middlewares ---
// 1. Habilita CORS para permitir peticiones desde tu frontend
app.use(cors());
// 2. Permite a Express entender JSON
app.use(express.json());

// --- Configuración de la Base de Datos ---
// (Asegúrate de que estos datos sean correctos para tu MySQL)
const dbConfig = {
    host: 'localhost',
    user: 'root',
    password: '', // <--- PON TU CONTRASEÑA DE MYSQL AQUÍ
    database: 'HealthQuest'
};

// Creamos un "pool" de conexiones, que es más eficiente
const pool = mysql.createPool(dbConfig);

// --- Endpoint de Prueba ---
app.get('/', (req, res) => {
    res.json({ message: '¡API de HealthQuest funcionando!' });
});

// -------------------------------------------------
// --- ENDPOINT DE LOGIN (¡NUEVO!) ---
// -------------------------------------------------
app.post('/login', async (req, res) => {
    const { email, password } = req.body;

    // Validación básica
    if (!email || !password) {
        return res.status(400).json({
            success: false,
            message: 'El correo y la contraseña son obligatorios.'
        });
    }

    try {
        // -----------------------------------------------------------------
        // ADVERTENCIA DE SEGURIDAD: ¡Esto es inseguro!
        // Estás guardando contraseñas en texto plano.
        // Esto es solo para que funcione temporalmente.
        // Lo correcto es usar 'bcrypt' para comparar contraseñas.
        // -----------------------------------------------------------------
        const query = 'SELECT * FROM Fisios WHERE Correo = ? AND Contraseña = ?';

        const [rows] = await pool.query(query, [email, password]);

        if (rows.length > 0) {
            // ¡Usuario encontrado!
            const fisio = rows[0];

            // ¡NUNCA devuelvas la contraseña al frontend!
            delete fisio.Contraseña;

            res.json({
                success: true,
                message: 'Inicio de sesión exitoso.',
                fisio: fisio // Enviamos los datos del fisio
            });
        } else {
            // Usuario no encontrado o contraseña incorrecta
            res.status(401).json({
                success: false,
                message: 'Correo o contraseña incorrectos.'
            });
        }

    } catch (error) {
        console.error('Error en la consulta de login:', error);
        res.status(500).json({
            success: false,
            message: 'Error interno del servidor.'
        });
    }
});


// --- Iniciar el Servidor ---
app.listen(port, () => {
    console.log(`Servidor corriendo en http://localhost:${port}`);
    // Verificamos la conexión a la BD al iniciar
    pool.getConnection()
        .then(connection => {
            console.log('Conectado exitosamente a la base de datos MySQL.');
            connection.release();
        })
        .catch(err => {
            console.error('Error al conectar con la base de datos:', err.message);
        });
});