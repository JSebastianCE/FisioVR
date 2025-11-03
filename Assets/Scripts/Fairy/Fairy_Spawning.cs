using UnityEngine;

public class Fairy_Spawning : FairyBaseState
{
    private float _spawnTimer;
    private const float SPAWN_DURATION = 1.5f; // Duración del spawn en segundos

    public Fairy_Spawning(FairyContext context, FairyStateMachine.EFairyStates eState) : base(context, eState) { }

    public override void EnterState()
    {
        context.PlaySound(context.SfxSpawn);
        context.InstantiateVFX(context.VfxSpawn, context.Transform.position);
        _spawnTimer = SPAWN_DURATION;
    }

    public override void UpdateState()
    {
        _spawnTimer -= Time.deltaTime;
    }

    public override FairyStateMachine.EFairyStates GetNextState()
    {
        if (_spawnTimer <= 0f)
        {
            return FairyStateMachine.EFairyStates.Moving;
        }
        return stateKey;
    }

    public override void ExitState() { }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}