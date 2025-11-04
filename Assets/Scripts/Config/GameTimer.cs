using UnityEngine;
using TMPro; 


public class GameTimer : MonoBehaviour
{
    public float TiempoRestante => tiempoRestante;

    [Header("Referencias")]

    public TextMeshProUGUI timerText; 

    private float tiempoRestante;
    private bool partidaActiva = false;

    void Start()
    {

        if (GameManager.Instance != null)
        {
            tiempoRestante = GameManager.Instance.tiempoPartidaSegundos;
            partidaActiva = true;
            Debug.Log($"Temporizador iniciado con {tiempoRestante} segundos.");
        }
        else
        {

            tiempoRestante = 300f; // 5 minutos por defecto
            partidaActiva = true;
            Debug.LogError("GameManager no encontrado. Usando 5 minutos por defecto.");
        }

      
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (partidaActiva)
        {
            if (tiempoRestante > 0)
            {
          
                tiempoRestante -= Time.deltaTime;
                
        
                UpdateTimerDisplay();
            }
            else
            {
           
                tiempoRestante = 0;
                partidaActiva = false;
                FinalizarPartida();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
       
            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);
            

            timerText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }

    void FinalizarPartida()
    {
        Debug.Log("--- ¡TIEMPO AGOTADO! Partida Finalizada. ---");

        // Score actual (si no hay bridge, queda 0)
        int score = 0;
        var bridge = FindObjectOfType<EnemyHUDBridge>();
        if (bridge != null) score = bridge.TotalScore;

        // Tiempo total de la sesión (lo que configuraste al inicio)
        float tiempoTotal = GameManager.Instance ? GameManager.Instance.tiempoPartidaSegundos : 0f;

        // Mostrar resumen
        var summary = FindObjectOfType<PostGameSummary>(true);
        if (summary != null) summary.Show(score, tiempoTotal);

        // Aquí no reiniciamos escena, ni mostramos botones.
    }


}