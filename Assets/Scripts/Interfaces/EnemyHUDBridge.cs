using UnityEngine;
using System;

public class EnemyHUDBridge : MonoBehaviour
{
    public int TotalScore => score;
    public static event Action<int, int, int> OnEnemyCountersChanged;
    public static event Action<int> OnScoreChanged;

    [Header("Opcional")]
    [Tooltip("Si tus enemigos no se instancian todos al inicio, ajusta este total manualmente o llama AddSpawn() desde tu spawner.")]
    public bool usarTotalesDeGameManager = true;

    int alive, defeated, total;
    int score;

    void Awake()
    {
        // Punto de partida: usa los totales del GameManager
        if (usarTotalesDeGameManager && GameManager.Instance != null)
        {
            total = GameManager.Instance.countEnemigoHadas
                  + GameManager.Instance.countEnemigoGrietas
                  + GameManager.Instance.countEnemigoEspíritus;
            alive = total; // asume que todos empezarán vivos
            PushCounters();
        }

        // Suscríbete a las “muertes” reportadas por la state machine
        BaseEnemyStateMachine<System.Enum>.OnEnemyDefeated += OnEnemyDefeatedHandler;
    }

    void OnDestroy()
    {
        BaseEnemyStateMachine<System.Enum>.OnEnemyDefeated -= OnEnemyDefeatedHandler;
    }

    void OnEnemyDefeatedHandler(int points)
    {
        // Un enemigo menos vivo, uno más derrotado, suma puntos
        if (alive > 0) alive--;
        defeated++;
        score += points;
        PushCounters();
        OnScoreChanged?.Invoke(score);
    }

    // Llama esto desde tu spawner si vas creando enemigos dinámicamente
    public void AddSpawn(int n = 1)
    {
        alive += n;
        total += n;
        PushCounters();
    }

    void PushCounters() => OnEnemyCountersChanged?.Invoke(alive, defeated, total);
}
