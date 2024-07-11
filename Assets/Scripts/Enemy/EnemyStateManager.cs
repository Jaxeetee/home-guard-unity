using MyUtils;
using UnityEngine;

public class EnemyStateManager : StateManager<EnemyStateManager.EnemyStates>
{
    public enum EnemyStates {
        IDLE,
        CHASE,
        ATTACK
    }

    private void Awake()
    {
        currentState = states[EnemyStates.IDLE]; 
    }
}