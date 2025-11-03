using System.Collections.Generic;
using UnityEngine;
using System;

public class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();

    protected BaseState<EState> currentState;

    bool isTransitioning = false;

    void Start() {
        currentState.EnterState();
    }

    void Update() {
        EState nextStateKey = currentState.GetNextState();

        if(!isTransitioning && nextStateKey.Equals(currentState.stateKey)) {
            currentState.UpdateState();
        }
        else if (!isTransitioning)
        {
            TransitionToState(nextStateKey);
        }
    }

    void TransitionToState(EState nextStateKey)
    {
        isTransitioning = true;
        currentState.ExitState();
        currentState = States[nextStateKey];
        currentState.EnterState();
        isTransitioning = false;
    }

    private void OnTriggerEnter(Collider other) {
        currentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other) {
        currentState.OnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other) { 
        currentState.OnTriggerExit(other);
    }
}
