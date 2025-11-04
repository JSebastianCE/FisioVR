using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform playerTransform;
    
    [Header("Configuracion del Spam")]
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin; // Rango mínimo de altura
    [SerializeField] private float yMax; // Rango máximo de altura
    //----------------------------
    [SerializeField] private float zMin; // Rango mínimo de profundidad
    [SerializeField] private float zMax; // Rango máximo de profundidad
    //----------------------------

    [SerializeField] private float yOffset = 0.0f; // Desplazamiento de altura
    [SerializeField] private float zSpawnLevel = 0.0f; // Desplazamiento de profunidad
    
    [Header("Control de Tiempo")]
    [SerializeField] private float spawnRateTime = 1.5f;  
    [SerializeField] private float initialDelay = 2.0f;
    
    void Start()
    {
        //Busca al jugador por el Tag si no esta asignado
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
            else
            {
                Debug.LogError("No se encuentra el player. Asigna el 'playerTransform' o ponle el Tag 'Player' al jugador");
                this.enabled = false; //Corta/ desactiva script si no hay jugador
                return;
            }
        }
        
        //Llama a la funcion EnemySpawn
        InvokeRepeating(nameof(EnemySpawn), initialDelay, spawnRateTime);
    }

    void EnemySpawn()
    {
        //guardar la posicion del jugador
        Vector3 targetPosition = playerTransform.position;
        
        //Calculo posicion aleatorio del spawn 
        // Plano XY
        float spawnX = Random.Range(xMin, xMax);
        float spawnY_relative = Random.Range(yMin, yMax); // Altura relativa
        // Plano Z (profundidad)
        float spawnZ = Random.Range(zMin, zMax); // <-- CAMBIO: Profundidad aleatoria
        float finalSpawnY = spawnY_relative + yOffset; // <-- CAMBIO: Altura final con offset

        
        //Creacion de la poosicion de spawn 3D, con profundidad fija
        //Vector3 spawnPosition = new Vector3(spawnX, finalSpawnY, zSpawnLevel); // <-- CAMBIO
        Vector3 spawnPosition = new Vector3(spawnX, finalSpawnY, spawnZ); // <-- CAMBIO

        //Crear al enemigo en la posicion del spawn  
        GameObject enemyGO = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
        //Obtencion del script Fairy 
        
        /*Fairy fairyScript = enemyGO.GetComponent<Fairy>();

        if (fairyScript != null)
        {
            //Le damos la posicon del jugadpr al script enemigo
            fairyScript.InitializeMovement(targetPosition);
        }
        else
        {
            Debug.LogWarning("El prefab del enemigo no tiene script 'Fairy. No se puede inicializar el movimiento");
        }
        */

    }
    
    //Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // Dibuja un cubo que representa el volumen de spawn usando los rangos min y max
        Vector3 center = new Vector3(
            (xMin + xMax) / 2f,
            (yMin + yMax) / 2f + yOffset,
            (zMin + zMax) / 2f
        );
        Vector3 size = new Vector3(
            Mathf.Abs(xMax - xMin),
            Mathf.Abs(yMax - yMin),
            Mathf.Abs(zMax - zMin)
        );

        Gizmos.DrawWireCube(center, size);

        /*
        //Esquinas de el rectanculo de spawn XY
        Vector3 corner1 = new Vector3(xMin, yMin + yOffset, zSpawnLevel); 
        Vector3 corner2 = new Vector3(xMax, yMin + yOffset, zSpawnLevel); 
        Vector3 corner3 = new Vector3(xMax, yMax + yOffset, zSpawnLevel); 
        Vector3 corner4 = new Vector3(xMin, yMax + yOffset, zSpawnLevel);
        
        //Lineas del rectangulo
        Gizmos.DrawLine(corner1, corner2);
        Gizmos.DrawLine(corner2, corner3);
        Gizmos.DrawLine(corner3, corner4);
        Gizmos.DrawLine(corner4, corner1);
        */
    }
}