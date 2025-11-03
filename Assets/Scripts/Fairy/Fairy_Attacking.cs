using UnityEngine;

public class Fairy_Attacking : FairyBaseState
{
    private bool _hasHit; // Para evitar múltiples golpes

    public Fairy_Attacking(FairyContext context, FairyStateMachine.EFairyStates eState) : base(context, eState) { }

    public override void EnterState()
    {
        context.PlaySound(context.SfxAttack);
        _hasHit = false;
    }

    public override void UpdateState()
    {
        // Es un ataque kamikaze, así que deja de deambular (wander)
        // y se lanza directo al jugador.
        context.Transform.position = Vector3.MoveTowards(
            context.Transform.position,
            context.Target.position,
            context.Velocity * 1.5f * Time.deltaTime // Un extra de velocidad para el ataque
        );
        context.Transform.LookAt(context.Target.position);
    }

    public override FairyStateMachine.EFairyStates GetNextState()
    {
        // Este estado solo se abandona al morir (manejado por TakeDamage)
        // o al golpear al jugador (manejado por OnTriggerEnter)
        return stateKey;
    }

    public override void OnTriggerEnter(Collider other)
    {
        // Si ya golpeamos o no es el jugador, no hacemos nada
        if (_hasHit || !other.CompareTag("Player")) return;

        _hasHit = true;

        // 1. Instanciar VFX y SFX de "golpe al jugador"
        context.InstantiateVFX(context.VfxPlayerDamage, context.Transform.position);
        context.PlaySound(context.SfxPlayerDamage);

        // 2. Hacer daño al jugador (asumiendo que el jugador también tiene IDamageable)
        IDamageable player = other.GetComponent<IDamageable>();
        player?.TakeDamage(context.AttackDamage);

        // 3. Auto-destruirse (transicionando a Muerte)
        // Damos un valor alto de daño para asegurar la muerte
        context.TakeDamage(9999f);
    }

    public override void ExitState() { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}