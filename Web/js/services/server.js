// server.js — API funcional conectada a Railway
import express from "express";
import cors from "cors";
import mysql from "mysql2/promise";

const app = express();
const PORT = process.env.PORT || 3000;

// === MIDDLEWARE ===
app.use(cors({
    origin: "*", // permite cualquier origen (local o Railway)
    methods: ["GET", "POST"],
    allowedHeaders: ["Content-Type"],
}));
app.use(express.json());

// === CONEXIÓN BD RAILWAY ===
const pool = mysql.createPool({
    host: "interchange.proxy.rlwy.net",
    user: "root",
    password: "kROiQgdsGFKGYfkSnaBsuEQbOxDScqnE",
    database: "HealthQuest", // ✅ nombre correcto de tu BD
    port: 20977,
});

// === PRUEBA DE CONEXIÓN AL INICIAR ===
(async () => {
    try {
        const conn = await pool.getConnection();
        console.log("🟢 Conexión establecida con la base de datos HealthQuest.");
        const [rows] = await conn.query("SHOW TABLES;");
        console.log("📋 Tablas disponibles:", rows);
        conn.release();
    } catch (err) {
        console.error("🔴 Error al conectar con la base de datos:", err);
    }
})();

// === RUTA DE LOGIN (POST) ===
app.post("/login", async (req, res) => {
    const { email, password } = req.body;
    console.log("🟡 Petición de login recibida:", email);

    try {
        const [rows] = await pool.query(
            "SELECT * FROM Usuarios WHERE email = ? AND password_hash = ?",
            [email, password]
        );

        if (rows.length > 0) {
            console.log("✅ Usuario autenticado:", rows[0].email);
            res.json({ success: true, fisio: rows[0] });
        } else {
            console.log("❌ Credenciales incorrectas para:", email);
            res.status(401).json({ success: false, message: "Credenciales incorrectas." });
        }
    } catch (error) {
        console.error("🔥 Error en /login:", error);
        res.status(500).json({ success: false, message: "Error interno del servidor." });
    }
});

// === RUTA GET para probar conexión desde navegador ===
app.get("/", async (req, res) => {
    try {
        const [users] = await pool.query("SELECT email FROM Usuarios;");
        res.send(`<h3>✅ API HealthQuest funcionando</h3><pre>${JSON.stringify(users, null, 2)}</pre>`);
    } catch (err) {
        res.status(500).send("❌ Error consultando la BD.");
    }
});

// === AGREGAR PACIENTE ===
// ⚠️ Se quitó "verificarFisio" porque no lo definiste aún
app.post("/api/pacientes", async (req, res) => {
    const { nombres, apellidos, email, fecha_nacimiento, notas_expediente, fisioterapeuta_asignado_id } = req.body;

    try {
        const [result] = await pool.query(
            `INSERT INTO Pacientes 
            (nombres, apellidos, email, fecha_nacimiento, notas_expediente, fisioterapeuta_asignado_id) 
            VALUES (?, ?, ?, ?, ?, ?)`,
            [nombres, apellidos, email, fecha_nacimiento, notas_expediente, fisioterapeuta_asignado_id]
        );

        console.log("✅ Paciente insertado con ID:", result.insertId);

        res.json({
            success: true,
            mensaje: "Paciente agregado exitosamente",
            paciente_id: result.insertId
        });
    } catch (error) {
        console.error("❌ Error al registrar paciente:", error);
        res.status(500).json({ success: false, error: "Error al registrar paciente" });
    }
});

// === INICIO DEL SERVIDOR ===
app.listen(PORT, () => {
    console.log(`🚀 Servidor corriendo en http://localhost:${PORT}`);
});
