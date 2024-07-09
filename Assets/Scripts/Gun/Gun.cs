using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using MyUtils;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

namespace Weapons {
    public class Gun : MonoBehaviour, IDebugger
    {
        
        [Header("Projectile")]
        [SerializeField] Projectile _projectile;
        [SerializeField] Transform _muzzlePoint;
        [SerializeField] float _projectileVelocity;

        [Space]
        [Header("Gun stats")]
        [SerializeField] float _msBetweenShots;
        [SerializeField] float _reloadTime;
        [SerializeField] int _magSize;
        [SerializeField] FireMode _fireMode;

        public FireMode fireMode
        {
            get => _fireMode;
            set => _fireMode = value;
        }
        int _currentBullets;

        float _fireRate;
        bool _isReloading = false;
        bool _isShooting = false;

        void Start()
        {
            PooledObject.NewObjectPool(StringManager.BULLET_PISTOL, _projectile.gameObject);
        }

        void OnEnable()
        {
            Warning(_currentBullets.ToString());
        }

        void OnDisable()
        {
            
        }

        public void InitStats()
        {
            _currentBullets = _magSize;
        }

        // void Update()
        // {
        //     if (_isShooting)
        //     {
        //         Shoot();
        //     }
        // }

        private IEnumerator Shoot()
        {
            while (_isShooting)
            {
                if (Time.time > _fireRate && !_isReloading && _currentBullets > 0)
                {
                    _fireRate = Time.time + _msBetweenShots / 1000;
                    GameObject bullet = PooledObject.GetObject(StringManager.BULLET_PISTOL);
                    bullet.transform.position = _muzzlePoint.position;
                    bullet.transform.rotation = _muzzlePoint.rotation;
                    bullet.GetComponent<Projectile>().SetSpeed(_projectileVelocity);
                    this._currentBullets--;
                }
                yield return null;

            }
        }

        public void onTriggerHold()
        {
            _isShooting = true;
            StartCoroutine(Shoot());
        }

        public void onTriggerRelease()
        {
            _isShooting = false;
        }

        public IEnumerator Reload()
        {
            float elapsedTime = 0f;

            while (elapsedTime < _reloadTime)
            {
                _isReloading = true;  
                elapsedTime += Time.deltaTime;
                Log(elapsedTime.ToString());
                yield return null;
            }
            _currentBullets = _magSize;
            _isReloading = false;
        }

        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void Warning(string message)
        {
            Debug.LogWarning(message);
        }

        public void Error(string message)
        {
            Debug.LogError(message);
        }
    }

}
