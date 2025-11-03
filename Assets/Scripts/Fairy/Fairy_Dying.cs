using UnityEngine;

public class Fairy_Dying : FairyBaseState
{
    public Fairy_Dying(FairyContext context, FairyStateMachine.EFairyStates eState) : base(context, eState) { }

    public override void EnterState()
    {
        // 1. Avisar al GameManager/HUD que sume puntos
        context.ReportScore();

        // (Aquí podrías añadir un VFX de muerte o un sonido)

        // 2. Desactivar colisionadores para que no reciba más daño
        Collider collider = context.Transform.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // 3. Destruir el GameObject después de 2 segundos
        Object.Destroy(context.Transform.gameObject, 2f);
    }

    public override void UpdateState()
    {
        // No hacer nada mientras morimos
    }

    public override FairyStateMachine.EFairyStates GetNextState()
    {
        // Quedarse en este estado hasta ser destruido
        return stateKey;
    }

    public override void ExitState() { }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}