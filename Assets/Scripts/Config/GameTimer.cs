using UnityEngine;
using TMPro; 

public class GameTimer : MonoBehaviour
{
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
        

    }
}