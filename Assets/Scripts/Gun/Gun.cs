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
        [SerializeField] private string _projectileKey;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private Transform _muzzlePoint;
        [SerializeField] private float _projectileVelocity;
        [SerializeField] private Material _trailMaterial;
        [SerializeField] private float _maxProjectileDistance;

        [Space]
        [Header("Gun stats")]
        [SerializeField] float _msBetweenShots;
        [SerializeField] private float _reloadTime;
        [SerializeField] private int _magSize;
        [SerializeField] private FireMode _fireMode;

        [Space]
        [Header("Gun Collision Mask")]
        [SerializeField] private LayerMask _collisions;

        public FireMode fireMode
        {
            get => _fireMode;
            set => _fireMode = value;
        }
        private int _currentBullets;

        private float _fireRate;
        private bool _isReloading = false;
        private bool _isShooting = false;
        private bool _pressedTrigger = false;
        private RaycastHit? cachedRaycastHit;
        private float cachedRaycastTime;

        private GameObject _player;

        private void Start()
        {
            PooledObject.NewObjectPool(_projectileKey, _projectile.gameObject);
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void OnEnable()
        {
            InitStats();
        }

        public void InitStats()
        {
            _currentBullets = _magSize;
        }

        private bool IsGunClipping()
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
