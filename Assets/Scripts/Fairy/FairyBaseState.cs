using UnityEngine;

public abstract class FairyBaseState : BaseState<FairyStateMachine.EFairyStates>
{
    protected FairyContext context;

    public FairyBaseState(FairyContext context, FairyStateMachine.EFairyStates stateKey) : base(stateKey)
    {
        this.context = context;
    }
}