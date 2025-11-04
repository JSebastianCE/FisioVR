using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.AI; // 1. Importar NavMeshAgent

// 2. Requerir el componente
[RequireComponent(typeof(NavMeshAgent))]
public class CrackStateMachine : BaseEnemyStateMachine<CrackStateMachine.ECrackStates>
{
    public enum ECrackStates
    {
        Spawning,
        Moving,
        Attacking,
        Dying
    }

    [Header("Especifico de Grieta")]
    [SerializeField] public GameObject projectilePrefab;

    public NavMeshAgent Agent { get; private set; }
    private CrackContext _crackContext;

    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();
        _crackContext = new CrackContext(this);

        Agent.speed = velocity;
        Agent.stoppingDistance = attackRadius;

        InitializeStates();
        ValidateParameters();
    }

    // Start se usa después de que todo está creado
    protected override void Start()
    {
        //    (Esto inicializa la vida)
        base.Start();

        currentState.EnterState();
    }

    private void ValidateParameters()
    {
        Assert.IsNotNull(target, "El Target (Player) no se encontró.");
        Assert.IsNotNull(Agent, "NavMeshAgent component not found.");
        Assert.IsNotNull(vfxSpawn, "vfxSpawn (en BaseEnemyStateMachine) no está asignado.");
        Assert.IsNotNull(projectilePrefab, "projectilePrefab (en CrackStateMachine) no está asignado.");
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