using UnityEngine;

public abstract class CrackBaseState : BaseState<CrackStateMachine.ECrackStates>
{
    protected CrackContext context;

    public CrackBaseState(CrackContext context, CrackStateMachine.ECrackStates stateKey) : base(stateKey)
    {
        this.context = context;
    }
}
