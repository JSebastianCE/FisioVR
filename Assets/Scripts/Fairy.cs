using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fairy : MonoBehaviour
{
    [Header("Configuracion de Movimiento")]
    [SerializeField] private float ConstantVelocity = 5.0f;
    
    private Rigidbody rb;
    private Vector3 DirectionMovement; //Direccion recta calculada

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No tiene Rigidbody. No se podra mover!", this);
        }
        
        //Quitar gravedad al enemigo volador
        rb.useGravity = false;
        
        //Congelar rotacon para que no se voltee por las fisicas
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void InitializeMovement(Vector3 targetPosition)
    {
        //Calculo de la direccion recta
        
        //Vector de diferencia (del spawn obejtivo)
        Vector3 diference = targetPosition - transform.position;
        
        //Vector de direccion final (Normalizacion)
        DirectionMovement = diference.normalized;

        if (rb != null)
        {
            //Aplicacion de la velocidad constante en la direccion calculada
            rb.linearVelocity = DirectionMovement * ConstantVelocity;
        }
    }
    
    public void BeDestrouyed()
    {
        //Agregar efecto visual....
        
        Debug.Log(gameObject.name + " fue golpeado y destruido.");
        
        Destroy(gameObject);
    }
}