using UnityEngine;
using UnityEngine.Assertions;

public class CrackStateMachine : StateManager<CrackStateMachine.ECrackStates>
{
    public enum ECrackStates
    {
        Spawning,
        Moving,
        Attacking,
        Dying
    }

    [SerializeField] private GameObject crackSpawnVFX;

    private void Awake()
    {
        ValidateParameters();
    }

    private void ValidateParameters()
    {
        Assert.IsNotNull(crackSpawnVFX, "crackSpawnVFX has not a prefab assign to it.");
    }
}
