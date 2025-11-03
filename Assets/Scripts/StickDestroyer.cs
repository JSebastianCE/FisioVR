using UnityEngine;

public class StickDestroyer : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;

    void OnCollisionEnter(Collision collision)
    {
        // Guardar el objeto que golpea en una variable
        GameObject objectWeHit = collision.gameObject;

        // Intentamos obtener CUALQUIER componente que sea "Dañable"
        IDamageable enemy = objectWeHit.GetComponent<IDamageable>();

        // Comprobar si encontramos la interfaz
        if (enemy != null)
        {
            // Llamamos al método de la interfaz
            enemy.TakeDamage(damageAmount);
        }
    }
}

/*
public class StickDestroyer : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        //Guardar el objeto que golpea en una varible
        GameObject objectWeHit = collision.gameObject;
        
        // Intento obtener el componente "DestructibleFairy" de ese objeto
        Fairy fairy = objectWeHit.GetComponent<Fairy>();
        
        //Comprobar si encontramos (si 'fairy' No es null)
        if (fairy != null)
        {
            //El objeto si tiene script
            //Le decimos que destruya llamando a su funcion publica
            fairy.BeDestrouyed();
        }
        
        
    }
    
}
*/
