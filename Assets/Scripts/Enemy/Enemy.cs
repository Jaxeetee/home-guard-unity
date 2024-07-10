using System.Collections;
using System.Collections.Generic;
using MyUtils;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public class Enemy : StateManager<Enemy.EnemyStates> 
{
    public enum EnemyStates {
        IDLE,
        ATTACK,
        CHASE,
        DEATH
    }

    private NavMeshAgent _agent;

    private Transform _target;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        NavMeshAgent navMeshAgent = _agent as NavMeshAgent;
        

    }

    private void Start()
    {
        SetNewTarget(GameObject.FindGameObjectWithTag("Player").transform);
    }


    private void OnEnable()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Debug.Log(_agent.agentTypeID);
        
        _agent.SetDestination(_target.position);
    }

    public void SetNewTarget(Transform newTarget)
    {
        _target = newTarget;
        
    }


}
