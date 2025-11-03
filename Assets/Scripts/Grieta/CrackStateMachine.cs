using UnityEngine;
using UnityEngine.Assertions;
using static CrackStateMachine;

public class CrackStateMachine : BaseEnemyStateMachine<CrackStateMachine.ECrackStates>
{
    public enum ECrackStates
    {
        Spawning,
        Moving,
        Attacking,
        Dying
    }

    private CrackContext _crackContext;

    [SerializeField] private GameObject crackSpawnVFX;

    private void Awake()
    {
        ValidateParameters();

        _crackContext = new CrackContext();

        InitializeStates(); 
    }

    private void ValidateParameters()
    {
        Assert.IsNotNull(crackSpawnVFX, "crackSpawnVFX has not a prefab assign to it.");
    }

    private void InitializeStates()
    {
        States.Add(ECrackStates.Spawning, new Crack_Spawning(_crackContext, ECrackStates.Spawning));
        States.Add(ECrackStates.Moving, new Crack_Moving(_crackContext, ECrackStates.Moving));
        States.Add(ECrackStates.Attacking, new Crack_Attacking(_crackContext, ECrackStates.Attacking));
        States.Add(ECrackStates.Dying, new Crack_Dying(_crackContext, ECrackStates.Dying));
        currentState = States[ECrackStates.Spawning];
    }

    protected override ECrackStates GetDeathStateEnum()
    {
        return ECrackStates.Dying;
    }
}
