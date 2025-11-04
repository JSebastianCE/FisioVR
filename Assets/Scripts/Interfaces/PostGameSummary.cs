using UnityEngine;
using TMPro;

public class PostGameSummary : MonoBehaviour
{
    [SerializeField] GameObject panel;          // SummaryPanel
    [SerializeField] TMP_Text txtScore, txtTiempo;
    [SerializeField] GameObject hudRoot;        // Arrastra el GO raíz del HUD (opcional)

    public void Show(int score, float segundosTotales)
    {
        if (hudRoot) hudRoot.SetActive(false);  // oculta HUD
        if (panel) panel.SetActive(true);

        if (txtScore) txtScore.text = $"Score: {score}";
        if (txtTiempo) txtTiempo.text = $"Tiempo: {Formatear(segundosTotales)}";

        // Opcional: “pausa suave” del juego (si no afecta a tu UI)
        // Time.timeScale = 0f;
    }

    string Formatear(float s)
    {
        s = Mathf.Max(0f, s);
        int m = Mathf.FloorToInt(s / 60f);
        int ss = Mathf.FloorToInt(s % 60f);
        return $"{m:00}:{ss:00}";
    }
}
