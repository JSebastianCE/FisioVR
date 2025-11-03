using UnityEngine;
using System.Collections.Generic;

public class EnemyCountReceiver : MonoBehaviour
{

    [Header("Referencias de Prefabs")]
    public GameObject prefabEnemigoHadas;
    public GameObject prefabEnemigoGrietas;
    public GameObject prefabEnemigoEspíritus;


    [Header("Puntos de Aparición (Para futura implementación)")]
    public List<Transform> spawnPoints; 

    void Start()
    {

        if (GameManager.Instance != null)
        {
            int count1 = GameManager.Instance.countEnemigoHadas;
            int count2 = GameManager.Instance.countEnemigoGrietas;
            int count3 = GameManager.Instance.countEnemigoEspíritus;
            

            Debug.Log("--- CONFIGURACIÓN DE ENEMIGOS RECIBIDA ---");
            Debug.Log($"Enemigo Hadas: {count1} unidades.");
            Debug.Log($"Enemigo Grietas: {count2} unidades.");
            Debug.Log($"Enemigo Espíritu: {count3} unidades.");
            Debug.Log("------------------------------------------");

        }
        else
        {
            Debug.LogError("GameManager no cargado. No se pudieron obtener las cantidades de enemigos.");
        }
    }
}