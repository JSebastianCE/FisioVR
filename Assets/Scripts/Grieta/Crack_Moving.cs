using UnityEngine;
using UnityEngine.AI; // Importar

public class Crack_Moving : CrackBaseState
{
    private float _recalculateTimer;

    public Crack_Moving(CrackContext context, CrackStateMachine.ECrackStates eState) : base(context, eState) { }

    public override void EnterState()
    {
        context.PlaySound(context.SfxMoving);
        if (context.Agent.isOnNavMesh)
        {
            context.Agent.isStopped = false;
        }
        _recalculateTimer = 0f; // Recalcular ruta inmediatamente
    }

    public override void UpdateState()
    {
        _recalculateTimer -= Time.deltaTime;

        // (Si se acaba el timer O si ya llegamos a nuestro punto temporal)
        if (_recalculateTimer <= 0f || (context.Agent.isOnNavMesh && !context.Agent.pathPending && context.Agent.remainingDistance < 1.0f))
        {
            SetNewWanderDestination();
            _recalculateTimer = context.WanderInterval;
        }
    }

    private void SetNewWanderDestination()
    {
        // Un punto aleatorio EN UNA ESFERA alrededor del jugador
        Vector3 randomPoint = Random.insideUnitSphere * context.WanderRadius;
        randomPoint += context.Target.position;

        // Encontrar el punto más cercano en el NavMesh a ese punto aleatorio
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randomPoint, out navHit, context.WanderRadius * 1.5f, NavMesh.AllAreas))
        {
            // Va a ese punto para hacer wander sin ir directo al player
            if (context.Agent.isOnNavMesh)
            {
                context.Agent.SetDestination(navHit.position);
            }
        }
        else
        {
            // Si falla simplemente vamos hacia el jugador
            if (context.Agent.isOnNavMesh)
            {
                context.Agent.SetDestination(context.Target.position);
            }
        }
    }

    public override CrackStateMachine.ECrackStates GetNextState()
    {
        // Revisar si ya estamos en rango de ataque
        float distanceToPlayer = Vector3.Distance(context.Transform.position, context.Target.position);

        if (distanceToPlayer <= context.AttackRadius)
        {
            return CrackStateMachine.ECrackStates.Attacking;
        }

        // Si no, seguir en este estado
        return stateKey;
    }

    public override void ExitState()
    {
        // Detener al agente al salir del estado
        if (context.Agent.isOnNavMesh)
        {
            context.Agent.isStopped = true;
        }
    }

    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}