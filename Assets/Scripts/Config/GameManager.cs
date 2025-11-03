using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // --- Patrón Singleton ---
    public static GameManager Instance;
    
    // Variables que persisten (No las cambiamos)
    [Header("Configuración de Partida (Persistente)")]
    public float tiempoPartidaSegundos = 300f; 
    public int countEnemigoHadas = 3;
    public int countEnemigoGrietas = 7;
    public int countEnemigoEspíritus = 5;

    // Referencias a UI (No las cambiamos)
    [Header("Referencias a UI (Solo Menú)")]
    public TMP_InputField timeInputField;
    public TMP_InputField inputEnemigoHadas;
    public TMP_InputField inputEnemigoGrietas;
    public TMP_InputField inputEnemigoEspíritus;

    // ... (El código de Awake(), Start(), InitializeEnemyCountInputs() se mantiene igual) ...

    // --- Métodos de Setup (omitiendo código repetido) ---
    private void Awake() { /* ... */ if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); } else { Destroy(gameObject); } }
    private void Start() { /* ... */ InitializeEnemyCountInputs(); }
    private void InitializeEnemyCountInputs() { /* ... */ }

    // --- 1. Método de Control de Tiempo (Solo String) ---
    public void SetTiempoPartidaFromInput(string input)
    {
        if (float.TryParse(input, out float minutes))
        {
            tiempoPartidaSegundos = Mathf.Max(10f, minutes * 60f); 
        }
        else
        {
            Debug.LogError("Entrada de tiempo inválida. Usando valor anterior.");
            if (timeInputField != null) timeInputField.text = (tiempoPartidaSegundos / 60f).ToString("F0");
        }
    }

    // --- 2. MÉTODO DE ENEMIGO SIMPLIFICADO (SOLO STRING) ---
    // ¡Usamos tres métodos diferentes para forzar la aparición en el Inspector!
    public void SetEnemigoHadasCount(string input)
    {
        if (int.TryParse(input, out int count))
        {
            countEnemigoHadas = Mathf.Max(0, count);
        }
        else
        {
            Debug.LogError("Entrada para Enemigo 1 inválida.");
        }
    }

    public void SetEnemigoGrietasCount(string input)
    {
        if (int.TryParse(input, out int count))
        {
            countEnemigoGrietas = Mathf.Max(0, count);
        }
        else
        {
            Debug.LogError("Entrada para Enemigo 2 inválida.");
        }
    }

    public void SetEnemigoEspíritusCount(string input)
    {
        if (int.TryParse(input, out int count))
        {
            countEnemigoEspíritus = Mathf.Max(0, count);
        }
        else
        {
            Debug.LogError("Entrada para Enemigo 3 inválida.");
        }
    }

    // --- Método de Inicio de Juego ---
    public void IniciarPartida(string nombreEscenaJuego)
    {
        SceneManager.LoadScene(nombreEscenaJuego);
    }
}