using UnityEngine;

public class DestructibleFairy : MonoBehaviour
{
    public void BeDestrouyed()
    {
        //Agregar efecto visual....
        
        Debug.Log(gameObject.name + " fue golpeado y destruido.");
        
        Destroy(gameObject);
    }
}