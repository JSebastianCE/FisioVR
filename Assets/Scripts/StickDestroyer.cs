using UnityEngine;

public class StickDestroyer : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        //Guardar el objeto que golpea en una varible
        GameObject objectWeHit = collision.gameObject;
        
        // Intento obtener el componente "DestructibleFairy" de ese objeto
        DestructibleFairy fairy = objectWeHit.GetComponent<DestructibleFairy>();
        
        //Comprobar si encontramos (si 'fairy' No es null)
        if (fairy != null)
        {
            //El objeto si tiene script
            //Le decimos que destruya llamando a su funcion publica
            fairy.BeDestrouyed();
        }
        
        
    }
}
