using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class SpiritStateMachine : BaseEnemyStateMachine<SpiritStateMachine.ESpiritStates>
{
    public enum ESpiritStates
    {
        Spawning,
        Moving,
        Attacking,
        Dying
    }

    [Header("Especifico de Espíritu de Fuego")]
    [SerializeField] public GameObject projectilePrefab;

    public NavMeshAgent Agent { get; private set; }
    private SpiritContext _spiritContext;

    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();
        _spiritContext = new SpiritContext(this);

        Agent.speed = velocity;
        Agent.stoppingDistance = attackRadius;

        InitializeStates();
        ValidateParameters();
    }

    // Start se usa después de que todo está creado
    protected override void Start()
    {
        // (Esto inicializa la vida)
        base.Start();

        currentState.EnterState();
    }

    private void ValidateParameters()
    {
        Assert.IsNotNull(target, "El Target (Player) no se encontró.");
        Assert.IsNotNull(Agent, "NavMeshAgent component not found.");
        Assert.IsNotNull(vfxSpawn, "vfxSpawn (en BaseEnemyStateMachine) no está asignado.");
    }

    private void InitializeStates()
    {
        States.Add(ESpiritStates.Spawning, new FireSpirit_Spawning(_spiritContext, ESpiritStates.Spawning));
        States.Add(ESpiritStates.Moving, new FireSpirit_Moving(_spiritContext, ESpiritStates.Moving));
        States.Add(ESpiritStates.Attacking, new FireSpirit_Attacking(_spiritContext, ESpiritStates.Attacking));
        States.Add(ESpiritStates.Dying, new FireSpirit_Dying(_spiritContext, ESpiritStates.Dying));

        currentState = States[ESpiritStates.Spawning];
    }

    protected override ESpiritStates GetDeathStateEnum()
    {
        return ESpiritStates.Dying;
    }
}
