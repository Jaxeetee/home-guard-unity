using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyUtils{
    public abstract class BaseState<EnumState> where EnumState : Enum 
    {
        public BaseState(EnumState key)
        {
            stateKey = key;
        }

        public EnumState stateKey {get; private set; }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract EnumState GetNextState();
        public abstract void OnTriggerEnter(Collider other);
        public abstract void OnTriggerExit(Collider other);
        public abstract void OnTriggerStay(Collider other);
    }
}
