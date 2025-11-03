using UnityEngine;

public class Crack_Moving : CrackBaseState
{
    public Crack_Moving(CrackContext context, CrackStateMachine.ECrackStates eState) : base(context, eState)
    {
        this.context = context;
    }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }

    public override CrackStateMachine.ECrackStates GetNextState()
    {
        return stateKey;
    }

    public override void OnTriggerEnter(Collider other)
    {

    }

    public override void OnTriggerStay(Collider other)
    {

    }

    public override void OnTriggerExit(Collider other)
    {

    }
}
