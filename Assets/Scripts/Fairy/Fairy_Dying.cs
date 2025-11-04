using UnityEngine;

public class Fairy_Dying : FairyBaseState
{
    private const float DESTROY_DELAY = 2.0f; // 2 segundos para el VFX de explosión

    public Fairy_Dying(FairyContext context, FairyStateMachine.EFairyStates eState) : base(context, eState) { }

    public override void EnterState()
    {
        // Reportar el puntaje al GameManager/HUD
        context.ReportScore();

        // Desactivar el colisionador
        Collider collider = context.Transform.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Destruir el GameObject después del delay
        Object.Destroy(context.Transform.gameObject, DESTROY_DELAY);
    }

    public override void UpdateState()
    {
        
    }

    public override FairyStateMachine.EFairyStates GetNextState()
    {
        // Estado final
        return stateKey;
    }

    public override void ExitState() { }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}