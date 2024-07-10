using MyUtils;
using UnityEngine;

namespace Weapons {
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private LayerMask _collisionMasks;
        private float _speed = 1f;
        private Vector3 _initialPosition;
        private float _maxDistance;
        private string _key;

        private void Start()
        {
            _initialPosition = transform.position;
        }

        private void OnDisable()
        {
            GetComponentInChildren<TrailRenderer>().Clear();
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            CheckCollisions(.5f);
            OnMaxDistanceReached();
        }

        private void OnMaxDistanceReached()
        {
            if (DidReachMaxDistance())
                PooledObject.ReturnToPool(_key ,this.gameObject);
        }

        private bool DidReachMaxDistance()
        {
            return TraveledDistance() > _maxDistance;
        }

        private float TraveledDistance()
        {
            return Vector3.Distance(_initialPosition, transform.position);
        }

        private void CheckCollisions(float moveDistance)
        {
            Ray forwardRay = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(forwardRay, out hit, moveDistance, _collisionMasks, QueryTriggerInteraction.Collide))
            {
                OnHitObject(hit);
            }
        }

        private void OnHitObject(RaycastHit hit)
        {
            PooledObject.ReturnToPool(_key ,this.gameObject);
        }

        public void InitializeProjectileStats(string key, float speed, float maxDistance, Material trailMaterial) 
        {
            _speed = speed;
            _maxDistance = maxDistance;
            GetComponentInChildren<Trail>().SetMaterial(trailMaterial);
            _key = key; 
        } 
    }
}

