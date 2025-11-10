using UnityEngine;

public abstract class FireSpiritBaseState : BaseState<SpiritStateMachine.ESpiritStates>
{
    protected SpiritContext _context;

    public FireSpiritBaseState(SpiritContext context, SpiritStateMachine.ESpiritStates stateKey) : base(stateKey)
    {
        this._context = context; 
    }
}
