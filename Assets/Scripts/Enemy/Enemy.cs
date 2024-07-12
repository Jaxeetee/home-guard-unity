using System.Collections;
using System.Collections.Generic;
using MyUtils;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public class Enemy : DamageableEntity
{
    private NavMeshAgent _agent;
    private Transform _target;
    private string _key;

    protected override void Awake()
    {
        base.Awake();
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

    private IEnumerator UpdateTargetPosition()
    {
        float frequency = 0.25f;

        while (_target != null)
        {
            _agent.SetDestination(_target.position);
            
            yield return new WaitForSeconds(frequency);
        }
    }

    private void SetNewTarget(Transform newTarget)
    {
        _target = newTarget;
        
    }

    protected override void Die()
    {
        base.Die();
        PooledObject.ReturnToPool(_key, this.gameObject);
    }

    public void Initialize(string key)
    {
        _key = key;
    }


}
