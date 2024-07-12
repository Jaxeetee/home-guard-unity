using UnityEngine;
using System;
using System.Collections.Generic;

namespace MyUtils
{
    public abstract class StateManager<EnumState> : MonoBehaviour where EnumState : Enum
    {
        protected Dictionary<EnumState, BaseState<EnumState>> states = new ();
        protected BaseState<EnumState> currentState;
        protected bool isTransitioningState = false;

        void Start() => currentState.EnterState();

        void Update() 
        {
            if (isTransitioningState) return;

            EnumState nextStateKey = currentState.GetNextState();

            if (nextStateKey.Equals(currentState.stateKey))
            {
                currentState.UpdateState();
            }
            else
            {
                TransitionToState(nextStateKey);
            }
        }

        public void TransitionToState(EnumState key)
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

