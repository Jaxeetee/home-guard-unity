using UnityEngine;
using System;
using System.Collections.Generic;

namespace MyUtils
{
    public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
    {
        protected Dictionary<EState, BaseState<EState>> states = new ();
        protected BaseState<EState> currentState;
        protected bool isTransitioningState = false;

        void Start() => currentState.EnterState();

        void Update() 
        {
            EState nextStateKey = currentState.GetNextState();

            if (isTransitioningState) return;

            if (nextStateKey.Equals(currentState.stateKey))
            {
                currentState.UpdateState();
            }
            else
            {
                TransitionToState(nextStateKey);
            }
        }

        public void TransitionToState(EState key)
        {
            isTransitioningState = true;
            currentState.ExitState();
            currentState = states[key];
            currentState.EnterState();
            isTransitioningState = false;

        }

        void OnTriggerEnter(Collider other) => currentState.OnTriggerEnter(other);
        void OnTriggerStay(Collider other) => currentState.OnTriggerStay(other);
        void OnTriggerExit(Collider other) => currentState.OnTriggerExit(other);
    }
}

