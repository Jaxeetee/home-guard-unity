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
        [SerializeField] string _projectileKey;
        [SerializeField] Projectile _projectile;
        [SerializeField] Transform _muzzlePoint;
        [SerializeField] float _projectileVelocity;
        [SerializeField] Material _trailMaterial;
        [SerializeField] float _maxProjectileDistance;

        [Space]
        [Header("Gun stats")]
        [SerializeField] float _msBetweenShots;
        [SerializeField] float _reloadTime;
        [SerializeField] int _magSize;
        [SerializeField] FireMode _fireMode;

        [Space]
        [Header("Gun Collision Mask")]
        [SerializeField] LayerMask _collisions;

        public FireMode fireMode
        {
            get => _fireMode;
            set => _fireMode = value;
        }
        int _currentBullets;

        float _fireRate;
        bool _isReloading = false;
        bool _isShooting = false;
        bool _pressedTrigger = false;
        RaycastHit? cachedRaycastHit;
        float cachedRaycastTime;

        GameObject _player;

        void Start()
        {
            PooledObject.NewObjectPool(StringManager.BULLET_PISTOL, _projectile.gameObject);
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        void OnEnable()
        {
            InitStats();
        }

        public void InitStats()
        {
            _currentBullets = _magSize;
        }

        bool IsGunClipping()
        {
            float currentTime = Time.time;

            // Check if cached raycast is valid (within a reasonable time threshold)
            if (cachedRaycastHit.HasValue && currentTime - cachedRaycastTime < 0.1f) // Adjust threshold as needed
            {
                return cachedRaycastHit.Value.collider != null;
            }
            return Physics.Raycast(_player.transform.position, transform.forward, out var hit, 1.5f, _collisions);
        }

        private IEnumerator Shoot()
        {
            while (_isShooting)
            {
                
                if (Time.time > _fireRate)
                {
                    if (_fireMode == FireMode.SEMI)
                    {
                        if (_pressedTrigger) break;
                    }
                    _fireRate = Time.time + _msBetweenShots / 1000;

                    if (IsGunClipping())
                    {
                        _currentBullets--;
                    }
                    else 
                    {
                        GameObject bullet = PooledObject.GetObject(_projectileKey);
                        bullet.transform.position = _muzzlePoint.position;
                        bullet.transform.rotation = _muzzlePoint.rotation;
                        bullet.GetComponent<Projectile>().InitializeProjectileStats(_projectileKey,_maxProjectileDistance, _projectileVelocity, _trailMaterial);
                        _currentBullets--;
                    }
                }
                yield return null;

            }
        }

        public void onTriggerHold()
        {
            if (_isReloading || _currentBullets < 1) return;

            _isShooting = true;         
            StartCoroutine(Shoot());
            _pressedTrigger = true;
        }

        public void onTriggerRelease()
        {
            _isShooting = false;
            _pressedTrigger = false;
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
