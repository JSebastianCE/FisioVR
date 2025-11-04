using UnityEngine;
using TMPro;

public class PostGameSummary : MonoBehaviour
{
    // Panel de resumen que se muestra al finalizar la partida (debe estar inactivo al inicio)
    [SerializeField] GameObject panel;

    // Textos donde pintamos los datos finales (score y tiempo total)
    [SerializeField] TMP_Text txtScore, txtTiempo;

    // (Opcional) Raíz del HUD en juego; si se asigna, lo ocultamos al mostrar el resumen
    [SerializeField] GameObject hudRoot;

    /// <summary>
    /// Muestra el panel de resumen con los datos finales.
    /// Llamar cuando el timer llega a 0.
    /// </summary>
    /// <param name="score">Puntuación acumulada al terminar.</param>
    /// <param name="segundosTotales">Duración total configurada para la sesión (en segundos).</param>
    public void Show(int score, float segundosTotales)
    {
        // Oculta el HUD de juego (si se asignó) para evitar superposición visual
        if (hudRoot) hudRoot.SetActive(false);

        // Activa el panel de resumen en pantalla
        if (panel) panel.SetActive(true);

        // Pinta los valores finales en los textos (verifica que existan)
        if (txtScore) txtScore.text = $"Score: {score}";
        if (txtTiempo) txtTiempo.text = $"Tiempo: {Formatear(segundosTotales)}";

        // Si quisieras pausar el juego al mostrar el resumen, descomenta:
        // Time.timeScale = 0f;
    }

    /// <summary>
    /// Convierte segundos a formato mm:ss para mostrar en UI.
    /// </summary>
    string Formatear(float s)
    {
        s = Mathf.Max(0f, s); // evita negativos por redondeos
        int m = Mathf.FloorToInt(s / 60f);
        int ss = Mathf.FloorToInt(s % 60f);
        return $"{m:00}:{ss:00}";
    }
}
