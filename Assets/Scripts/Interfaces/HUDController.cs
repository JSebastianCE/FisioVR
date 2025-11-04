using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GameTimer timer;          // arrastra tu GameTimer de la escena
    [SerializeField] TMP_Text txtTimer;
    [SerializeField] TMP_Text txtEnemies;      // "vivos / derrotados / total"
    [SerializeField] TMP_Text txtScore;        // puntos totales
    [SerializeField] TMP_Text txtHadas, txtGrietas;
    int hadasRest, hadasTotal, grietasRest, grietasTotal;

    int enemiesAlive, enemiesDefeated, enemiesTotal;
    int score;

    void OnEnable()
    {
        EnemyHUDBridge.OnEnemyCountersChanged += ApplyEnemyCounters;
        EnemyHUDBridge.OnScoreChanged += ApplyScore;
    }

    void OnDisable()
    {
        EnemyHUDBridge.OnEnemyCountersChanged -= ApplyEnemyCounters;
        EnemyHUDBridge.OnScoreChanged -= ApplyScore;
    }

    void Update()
    {
        // TIMER
        if (timer && txtTimer)
        {
            float sec = Mathf.Max(0f, timer.TiempoRestante);
            int m = Mathf.FloorToInt(sec / 60f);
            int s = Mathf.FloorToInt(sec % 60f);
            txtTimer.text = $"{m:00}:{s:00}";
        }

        // ENEMIGOS
        if (txtEnemies)
            txtEnemies.text = $"Enemigos: {enemiesAlive} vivos / {enemiesDefeated} derrotados / {enemiesTotal} total";

        // SCORE
        if (txtScore)
            txtScore.text = $"Score: {score}";

        if (txtHadas) txtHadas.text = $"Hadas: {hadasRest}/{hadasTotal}";
        if (txtGrietas) txtGrietas.text = $"Grietas: {grietasRest}/{grietasTotal}";

    }

    void ApplyEnemyCounters(int alive, int defeated, int total)
    {
        enemiesAlive = alive;
        enemiesDefeated = defeated;
        enemiesTotal = total;
    }

    void ApplyScore(int totalScore) => score = totalScore;
}
