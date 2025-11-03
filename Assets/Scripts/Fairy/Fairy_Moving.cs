using UnityEngine;

public class Fairy_Moving : FairyBaseState
{
    private Vector3 _wanderTargetPosition;
    private float _recalculateTimer;
    private float _hoverHeight = 2.0f; // Qué tan alto sobre el jugador queremos flotar

    public Fairy_Moving(FairyContext context, FairyStateMachine.EFairyStates eState) : base(context, eState) { }

    public override void EnterState()
    {
        context.PlaySound(context.SfxMoving);
        _recalculateTimer = 0f; // Recalcular ruta inmediatamente
        SetNewWanderDestination();
    }

    public override void UpdateState()
    {
        _recalculateTimer -= Time.deltaTime;

        // 1. ¿Toca recalcular?
        // (Si se acaba el timer O si ya llegamos a nuestro punto temporal)
        if (_recalculateTimer <= 0f || Vector3.Distance(context.Transform.position, _wanderTargetPosition) < 1.0f)
        {
            SetNewWanderDestination();
            _recalculateTimer = context.WanderInterval;
        }

        // 2. Moverse hacia el PUNTO TEMPORAL
        context.Transform.position = Vector3.MoveTowards(
            context.Transform.position,
            _wanderTargetPosition,
            context.Velocity * Time.deltaTime
        );

        // 3. Mirar al jugador (o al punto, como prefieras)
        context.Transform.LookAt(context.Target.position);
    }

    private void SetNewWanderDestination()
    {
        // 1. Un punto aleatorio en una esfera
        Vector3 randomPoint = Random.insideUnitSphere * context.WanderRadius;

        // 2. Queremos que vuele, así que nos aseguramos de que el punto
        //    esté *cerca* del jugador pero *sobre* él.
        Vector3 playerPos = context.Target.position;

        _wanderTargetPosition = new Vector3(
            playerPos.x + randomPoint.x,
            playerPos.y + _hoverHeight + Mathf.Abs(randomPoint.y), // Forzarlo a estar arriba
            playerPos.z + randomPoint.z
        );
    }

    public override FairyStateMachine.EFairyStates GetNextState()
    {
        // Revisar si ya estamos en rango de ataque (distancia al jugador)
        float distanceToPlayer = Vector3.Distance(context.Transform.position, context.Target.position);

        if (distanceToPlayer <= context.AttackRadius)
        {
            return FairyStateMachine.EFairyStates.Attacking;
        }

        // Si no, seguir en este estado
        return stateKey;
    }

    public override void ExitState() { }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}