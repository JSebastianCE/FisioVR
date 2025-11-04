// routes/auth.js
// Exporta una función que recibe el pool y retorna el router
const express = require('express');

module.exports = function (pool) {
    const router = express.Router();

    // POST /login
    router.post('/', async (req, res) => {
        const { email, password } = req.body;

        if (!email || !password) {
            return res.status(400).json({
                success: false,
                message: 'El correo y la contraseña son obligatorios.'
            });
        }

        try {
            // Primero buscamos por email
            const [rows] = await pool.query('SELECT * FROM Usuarios WHERE email = ?', [email]);

            if (rows.length === 0) {
                return res.status(401).json({
                    success: false,
                    message: 'Correo o contraseña incorrectos.'
                });
            }

            const usuario = rows[0];

            // Manejo flexible de columna de contraseña:
            // puede llamarse password_hash, password, contraseña, etc.
            const dbPassword =
                usuario.password_hash ??
                usuario.password ??
                usuario.contraseña ??
                usuario.contrasena ??
                usuario.pass ??
                null;

            // Comparación básica (si tus contraseñas ya están hasheadas, reemplaza por bcrypt.compare)
            if (dbPassword === null || dbPassword !== password) {
                return res.status(401).json({
                    success: false,
                    message: 'Correo o contraseña incorrectos.'
                });
            }

            // No enviar campos de contraseña al frontend
            delete usuario.password_hash;
            delete usuario.password;
            delete usuario.contraseña;
            delete usuario.contrasena;
            delete usuario.pass;

            // Respuesta compatible con tu Login.js (espera `fisio`)
            return res.json({
                success: true,
                message: 'Inicio de sesión exitoso.',
                fisio: usuario
            });

        } catch (err) {
            console.error('Error en /login:', err);
            return res.status(500).json({
                success: false,
                message: 'Error interno del servidor.'
            });
        }
    });

    return router;
};
