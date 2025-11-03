using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float lifetime = 4f; // Cuántos segundos vive antes de auto-destruirse

    private float _damage;
    private bool _hasHit;

    // El StateMachine llama a esto justo después de Instanciar
    public void Initialize(Transform target, float damage)
    {
        _damage = damage;
        _hasHit = false;

        // Apuntar al jugador
        transform.LookAt(target.position);

        // Destruirse si no golpea nada
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Moverse hacia adelante (ya está apuntando al jugador)
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (_hasHit || !other.CompareTag("Player")) return;

        _hasHit = true;

        // Hacer daño al jugador
        IDamageable player = other.GetComponent<IDamageable>();
        player?.TakeDamage(_damage);

        // (Aquí podrías instanciar un VFX de impacto)

        // Destruir el proyectil al impactar
        Destroy(gameObject);
    }
}