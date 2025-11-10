using Unity.AI;

public class SpiritContext
{
    private readonly SpiritStateMachine _stateMachine;
    public SpiritContext(SpiritStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }
}
