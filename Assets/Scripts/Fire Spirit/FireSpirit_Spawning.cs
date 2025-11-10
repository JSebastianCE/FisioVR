using UnityEngine;

public class FireSpirit_Spawning : FireSpiritBaseState
{
    public FireSpirit_Spawning(SpiritContext context, SpiritStateMachine.ESpiritStates stateKey) : base(context, stateKey) { }

    public override void EnterState() { }
    public override void UpdateState() { }
    public override void ExitState() { }
    public override SpiritStateMachine.ESpiritStates GetNextState() { return stateKey; }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}
