using UnityEngine;
using System;

public class EnemyHUDBridge : MonoBehaviour
{
    // Exponemos el total para que otros (GameTimer/Resumen) lo lean
    public int TotalScore => score;

    // Eventos para notificar cambios al HUD (enemies y score)
    public static event Action<int, int, int> OnEnemyCountersChanged; // alive, defeated, total
    public static event Action<int> OnScoreChanged;                   // score total

    [Header("Opcional")]
    [Tooltip("Si tus enemigos no se instancian todos al inicio, ajusta este total manualmente o llama AddSpawn() desde tu spawner.")]
    public bool usarTotalesDeGameManager = true;

    // --- Estado que calcula este bridge ---
    int alive;    // enemigos vivos
    int defeated; // enemigos derrotados
    int total;    // enemigos totales (vivos + derrotados)
    int score;    // puntuación acumulada

    void Awake()
    {
        // Punto de partida: toma el total desde GameManager si quieres autocalcular
        if (usarTotalesDeGameManager && GameManager.Instance != null)
        {
            total = GameManager.Instance.countEnemigoHadas
                  + GameManager.Instance.countEnemigoGrietas
                  + GameManager.Instance.countEnemigoEspíritus;
            alive = total; // asumimos que al inicio todos están “por aparecer/vivos”
            PushCounters();
        }

        // Escuchamos cuando un enemigo “reporta su muerte” (sumamos puntos/derrotados)
        BaseEnemyStateMachine<System.Enum>.OnEnemyDefeated += OnEnemyDefeatedHandler;
    }

    void OnDestroy()
    {
        // Siempre desuscribir para no dejar eventos enganchados
        BaseEnemyStateMachine<System.Enum>.OnEnemyDefeated -= OnEnemyDefeatedHandler;
    }

    // Se llama cuando un enemigo muere y reporta sus puntos
    void OnEnemyDefeatedHandler(int points)
    {
        if (alive > 0) alive--; // baja vivos
        defeated++;              // sube derrotados
        score += points;         // acumula score
        PushCounters();          // notifica al HUD los nuevos números
        OnScoreChanged?.Invoke(score);
    }

    // Útil si los spawners van creando enemigos dinámicamente
    public void AddSpawn(int n = 1)
    {
        alive += n;
        total += n;
        PushCounters();
    }

    // Notifica a todos los HUD suscritos los contadores actuales
    void PushCounters() => OnEnemyCountersChanged?.Invoke(alive, defeated, total);
}
