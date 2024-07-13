using System.Collections;
using UnityEngine;
using MyUtils;

namespace Weapons{
    public class Ejector : MonoBehaviour
    {
        private Rigidbody _rb;

        private float _lifeTime = 0f;
        private string _poolKey;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _rb.AddForce(new Vector3(50f, 0, 0));
            StartCoroutine(Despawn());
        }

        private IEnumerator Despawn()
        {
            // float elapsedTime = 0f;

            //     elapsedTime += Time.deltaTime;
                yield return new WaitForSeconds(_lifeTime);
                ObjectPoolManager.ReturnToPool(_poolKey, this.gameObject);

            
        }

        public void InitializeEjector(float lifeTime, string poolKey)
        {
            _lifeTime = lifeTime;
            _poolKey = poolKey;
        }
    }
}
