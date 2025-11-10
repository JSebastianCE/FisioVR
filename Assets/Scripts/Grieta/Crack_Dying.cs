using UnityEngine;

public class Crack_Dying : CrackBaseState
{
    private const float delay = 2.0f; // 2 segundos para cualquier animación/VFX de muerte

    public Crack_Dying(CrackContext context, CrackStateMachine.ECrackStates eState) : base(context, eState) { }

    public override void EnterState()
    {
        // Reportar el puntaje al GameManager/HUD
        context.ReportScore();

        // context.PlaySound(context.SfxMuerte);

        // Detener el NavMeshAgent para que no siga intentando moverse
        if (context.Agent.isOnNavMesh)
        {
            context.Agent.isStopped = true;
            context.Agent.ResetPath(); // Limpiar su ruta
        }

        // Desactivar el colisionador para no recibir más golpes
        Collider collider = context.Transform.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Destruir el GameObject después del delay
        // Usamos Object.Destroy porque esta clase no es un MonoBehaviour
        Object.Destroy(context.Transform.gameObject, delay);
    }

    public override void UpdateState()
    {
        
    }

    public override CrackStateMachine.ECrackStates GetNextState()
    {
        // Este es un estado final. Nunca transiciona a otro estado.
        return stateKey;
    }

    public override void ExitState() { }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}