using System.Collections.Generic;
using UnityEngine;
using System;

public class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> currentState;
    bool isTransitioning = false;

    // Eliminamos el método Start() de aquí.
    // La clase hija se encargará de llamar a EnterState()

    void Update()
    {
        // Asegurarnos que haya un estado antes de hacer nada
        if (currentState == null) return;

        EState nextStateKey = currentState.GetNextState();

        if (!isTransitioning && nextStateKey.Equals(currentState.stateKey))
        {
            currentState.UpdateState();
        }
        else if (!isTransitioning)
        {
            TransitionToState(nextStateKey);
        }
    }

    protected void TransitionToState(EState nextStateKey)
    {
        isTransitioning = true;
        currentState.ExitState();
        currentState = States[nextStateKey];
        currentState.EnterState();
        isTransitioning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentState != null) currentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (currentState != null) currentState.OnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentState != null) currentState.OnTriggerExit(other);
    }
}