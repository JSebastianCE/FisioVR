using UnityEngine;
using UnityEngine.Assertions;

public class FairyStateMachine : BaseEnemyStateMachine<FairyStateMachine.EFairyStates>
{
    public enum EFairyStates
    {
        Spawning,
        Moving,
        Attacking,
        Dying
    }

    private FairyContext _fairyContext;

    // No necesitamos una referencia al NavMeshAgent

    // Awake se usa para obtener referencias y crear objetos
    protected override void Awake()
    {
        // 1. LLAMAR AL AWAKE DE LA CLASE BASE PRIMERO!
        //    (Esto obtiene el AudioSource y el Target)
        base.Awake();

        // 2. Pasar "this" (este script) al Contexto
        _fairyContext = new FairyContext(this);

        // 3. No hay agente que configurar, así que inicializamos estados
        InitializeStates();
        ValidateParameters();
    }

    // Start se usa después de que todo está creado
    protected override void Start()
    {
        // 4. LLAMAR AL START DE LA CLASE BASE!
        //    (Esto inicializa la vida)
        base.Start();

        // 5. Ahora que todo está listo, activamos el primer estado
        currentState.EnterState();
    }

    private void ValidateParameters()
    {
        // Validamos que los componentes base se encontraron
        Assert.IsNotNull(target, "El Target (Player) no se encontró. Asegúrate de tener un GameObject con el Tag 'Player'.");
        Assert.IsNotNull(vfxSpawn, "vfxSpawn (en BaseEnemyStateMachine) no está asignado.");
    }

    private void InitializeStates()
    {
        States.Add(EFairyStates.Spawning, new Fairy_Spawning(_fairyContext, EFairyStates.Spawning));
        States.Add(EFairyStates.Moving, new Fairy_Moving(_fairyContext, EFairyStates.Moving));
        States.Add(EFairyStates.Attacking, new Fairy_Attacking(_fairyContext, EFairyStates.Attacking));
        States.Add(EFairyStates.Dying, new Fairy_Dying(_fairyContext, EFairyStates.Dying));

        // Asignamos el estado inicial (pero no lo activamos hasta Start)
        currentState = States[EFairyStates.Spawning];
    }

    protected override EFairyStates GetDeathStateEnum()
    {
        return EFairyStates.Dying;
    }
}