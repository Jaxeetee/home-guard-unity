using System.Collections;
using System.Collections.Generic;
using MyUtils;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public enum EnemyStates {
        IDLE,
        ATTACK,
        CHASE,
        DEATH
    }

    [SerializeField] private NavMeshAgent _agent;

    private Transform _target;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        SetNewTarget(GameObject.FindGameObjectWithTag("Player").transform);
        StartCoroutine(UpdateTargetPosition());
    }

    // private void Update()
    // {    
    //     _agent.SetDestination(_target.position);
    // }

    private IEnumerator UpdateTargetPosition()
    {
        float frequency = 0.25f;

        while (_target != null)
        {
            _agent.SetDestination(_target.position);
            
            yield return new WaitForSeconds(frequency);
        }
    }

    public void SetNewTarget(Transform newTarget)
    {
        _target = newTarget;
        
    }


}
