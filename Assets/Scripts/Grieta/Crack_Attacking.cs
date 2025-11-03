using UnityEngine;

public class Crack_Attacking : CrackBaseState
{
    private float _attackTimer;

    public Crack_Attacking(CrackContext context, CrackStateMachine.ECrackStates eState) : base(context, eState) { }

    public override void EnterState()
    {
        // Al entrar, nos detenemos (ya lo hizo Moving.ExitState)
        // y reseteamos el timer para atacar de inmediato.
        _attackTimer = 0f;
        context.Transform.LookAt(context.Target.position);
    }

    public override void UpdateState()
    {
        // Siempre mirar al jugador
        context.Transform.LookAt(context.Target.position);

        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0f)
        {
            Shoot();
            _attackTimer = context.AttackCooldown; // Reiniciar cooldown
        }
    }

    private void Shoot()
    {
        // 1. Reproducir sonido de ataque
        context.PlaySound(context.SfxAttack);

        // 2. Instanciar el proyectil
        // (Asegúrate de que el "rayo" prefab tenga el script EnemyProjectile)
        GameObject projGO = Object.Instantiate(
            context.ProjectilePrefab,
            context.Transform.position, // Salir desde la grieta
            context.Transform.rotation  // Mirando en la misma dirección
        );

        // 3. Pasar datos al script del proyectil
        EnemyProjectile projectileScript = projGO.GetComponent<EnemyProjectile>();
        if (projectileScript != null)
        {
            projectileScript.Initialize(context.Target, context.AttackDamage);
        }
    }

    public override CrackStateMachine.ECrackStates GetNextState()
    {
        // Si el jugador se aleja demasiado, volvemos a perseguirlo
        float distanceToPlayer = Vector3.Distance(context.Transform.position, context.Target.position);
        if (distanceToPlayer > context.AttackRadius + 1.0f) // (Añadimos un "buffer" de 1m)
        {
            return CrackStateMachine.ECrackStates.Moving;
        }

        return stateKey;
    }

    public override void ExitState() { }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}