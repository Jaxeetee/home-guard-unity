using MyUtils;
using UnityEngine;

namespace Weapons {
    public class Projectile : MonoBehaviour
    {
        float _speed = 50f;

        // Update is called once per frame
        void Update()
        {
            transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);
        }


        //TODO: Use raycasts instead of collision enter
        void OnCollisionEnter(Collision other)
        {
            PooledObject.ReturnToPool(StringManager.BULLET_PISTOL ,this.gameObject);
        }

        public void SetSpeed(float speed) => _speed = speed;
    }
}

