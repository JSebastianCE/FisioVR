using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] GameTimer timer;     // Referencia al timer para dibujar mm:ss
    [SerializeField] TMP_Text txtTimer;   // Texto del reloj
    [SerializeField] TMP_Text txtEnemies; // Texto "vivos / derrotados / total"
    [SerializeField] TMP_Text txtScore;   // Texto de puntos totales
    [SerializeField] TMP_Text txtHadas, txtGrietas; // (si usas contadores de ítems)

    // --- Estado que pintamos en pantalla (lo alimenta el bridge) ---
    int enemiesAlive, enemiesDefeated, enemiesTotal;
    int score;

    // Contadores opcionales (si más adelante los usas)
    int hadasRest, hadasTotal, grietasRest, grietasTotal;

    void OnEnable()
    {
        // Nos suscribimos a los eventos del bridge para recibir datos actualizados
        EnemyHUDBridge.OnEnemyCountersChanged += ApplyEnemyCounters;
        EnemyHUDBridge.OnScoreChanged += ApplyScore;
    }

    void OnDisable()
    {
        // Siempre desuscribir para evitar referencias colgantes y duplicados
        EnemyHUDBridge.OnEnemyCountersChanged -= ApplyEnemyCounters;
        EnemyHUDBridge.OnScoreChanged -= ApplyScore;
    }

    void Update()
    {
        // ---- TIMER (mm:ss) ----
        if (timer && txtTimer)
        {
            float sec = Mathf.Max(0f, timer.TiempoRestante);
            int m = Mathf.FloorToInt(sec / 60f);
            int s = Mathf.FloorToInt(sec % 60f);
            txtTimer.text = $"{m:00}:{s:00}";
        }

        // ---- ENEMIGOS ----
        if (txtEnemies)
            txtEnemies.text = $"Enemigos: {enemiesAlive} vivos / {enemiesDefeated} derrotados / {enemiesTotal} total";

        // ---- SCORE ----
        if (txtScore)
            txtScore.text = $"Score: {score}";

        // ---- HADAS / GRIETAS (opcional) ----
        if (txtHadas) txtHadas.text = $"Hadas: {hadasRest}/{hadasTotal}";
        if (txtGrietas) txtGrietas.text = $"Grietas: {grietasRest}/{grietasTotal}";
    }

    // Recibe los contadores del bridge y los guarda para pintarlos en Update()
    void ApplyEnemyCounters(int alive, int defeated, int total)
    {
        enemiesAlive = alive;
        enemiesDefeated = defeated;
        enemiesTotal = total;
    }

    // Recibe el score acumulado del bridge
    void ApplyScore(int totalScore) => score = totalScore;
}
