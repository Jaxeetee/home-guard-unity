using MyUtils;
using UnityEngine;

namespace Weapons {
    public class Projectile : MonoBehaviour
    {
        [SerializeField] LayerMask _collisionMasks;

        float _speed = 1f;
        Vector3 _initialPosition;
        float _maxDistance;
        string _key;

        void Start()
        {
            _initialPosition = transform.position;
        }

        void OnDisable()
        {
            GetComponentInChildren<TrailRenderer>().Clear();
        }

        void Update()
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            CheckCollisions(.5f);
            OnMaxDistanceReached();
        }

        void OnMaxDistanceReached()
        {
            if (DidReachMaxDistance())
                PooledObject.ReturnToPool(_key ,this.gameObject);
        }

        bool DidReachMaxDistance()
        {
            return TraveledDistance() > _maxDistance;
        }

        float TraveledDistance()
        {
            return Vector3.Distance(_initialPosition, transform.position);
        }

        void CheckCollisions(float moveDistance)
        {
            Ray forwardRay = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(forwardRay, out hit, moveDistance, _collisionMasks, QueryTriggerInteraction.Collide))
            {
                OnHitObject(hit);
            }
        }

        void OnHitObject(RaycastHit hit)
        {
            PooledObject.ReturnToPool(_key ,this.gameObject);
        }

        public void InitializeProjectileStats(string key, float speed, float maxDistance, Material trailMaterial) 
        {
            _speed = speed;
            _maxDistance = maxDistance;
            GetComponentInChildren<Trail>().SetMaterial(trailMaterial);
            _key = key; 


            // Collider[] initialColliders = Physics.OverlapSphere(transform.position, .125f, _collisionMasks);
            // Debug.Log($"is there initial collision? {initialColliders.Length}");
            // if (initialColliders.Length > 0)
            // {
            //     RaycastHit hit;
            //     Physics.Raycast(transform.position, initialColliders[0].transform.position, out hit);
            //     Debug.Log(hit.collider.gameObject.name);
            //     OnHitObject(hit);
            // }
        } 
    }
}

