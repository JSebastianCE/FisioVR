using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    // Exponemos el tiempo restante para que otros (HUD) lo lean sin modificarlo.
    public float TiempoRestante => tiempoRestante;

    [Header("Referencias")]
    public TextMeshProUGUI timerText; // Texto donde se muestra mm:ss en pantalla

    // --- Estado interno del temporizador ---
    private float tiempoRestante;  // Segundos que faltan para terminar la sesión
    private bool partidaActiva = false; // Si el timer está corriendo o no

    void Start()
    {
        // Inicializa el temporizador usando el valor del GameManager si existe.
        if (GameManager.Instance != null)
        {
            tiempoRestante = GameManager.Instance.tiempoPartidaSegundos; // viene en segundos
            partidaActiva = true;
            Debug.Log($"Temporizador iniciado con {tiempoRestante} segundos.");
        }
        else
        {
            // Fallback por si no hay GameManager (evitamos romper el juego)
            tiempoRestante = 300f; // 5 minutos por defecto
            partidaActiva = true;
            Debug.LogError("GameManager no encontrado. Usando 5 minutos por defecto.");
        }

        // Refresca el mm:ss inicial en la UI
        UpdateTimerDisplay();
    }

    void Update()
    {
        // Solo cuenta si la partida está activa
        if (partidaActiva)
        {
            if (tiempoRestante > 0)
            {
                // Restamos tiempo real por frame
                tiempoRestante -= Time.deltaTime;
                // Actualizamos el texto del reloj
                UpdateTimerDisplay();
            }
            else
            {
                // Clamp y cierre de la partida
                tiempoRestante = 0;
                partidaActiva = false;
                FinalizarPartida();
            }
        }
    }

    // Pinta el tiempo en formato mm:ss
    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }

    // Se llama automáticamente cuando el tiempo llega a 0
    void FinalizarPartida()
    {
        Debug.Log("--- ¡TIEMPO AGOTADO! Partida Finalizada. ---");

        // Recogemos el score desde el bridge (si no existe, queda en 0)
        int score = 0;
        var bridge = FindObjectOfType<EnemyHUDBridge>();
        if (bridge != null) score = bridge.TotalScore;

        // Tiempo total configurado al inicio (para mostrarlo)
        float tiempoTotal = GameManager.Instance ? GameManager.Instance.tiempoPartidaSegundos : 0f;

        // Buscamos y mostramos el panel de resumen
        var summary = FindObjectOfType<PostGameSummary>(true);
        if (summary != null) summary.Show(score, tiempoTotal);

        // No reiniciamos escena ni mostramos botones aquí (requisito del diseño).
    }

}
