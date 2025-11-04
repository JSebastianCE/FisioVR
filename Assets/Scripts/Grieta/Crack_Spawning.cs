using UnityEngine;

public class Crack_Spawning : CrackBaseState
{
    private float _spawnTimer;
    private const float SPAWN_DURATION = 1.5f;

    public Crack_Spawning(CrackContext context, CrackStateMachine.ECrackStates eState) : base(context, eState) { }

    public override void EnterState()
    {
        // Usamos el Contexto para reproducir sonido y VFX
        context.PlaySound(context.SfxSpawn);
        context.InstantiateVFX(context.VfxSpawn, context.Transform.position);

        _spawnTimer = SPAWN_DURATION;
    }

    public override void UpdateState()
    {
        // Contamos hacia atrás
        _spawnTimer -= Time.deltaTime;
    }

    public override CrackStateMachine.ECrackStates GetNextState()
    {
        // Si el timer llega a cero, transicionamos a Moving
        if (_spawnTimer <= 0f)
        {
            return CrackStateMachine.ECrackStates.Moving;
        }

        // Si no, nos quedamos en este estado
        return stateKey;
    }

    public override void ExitState() { }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}