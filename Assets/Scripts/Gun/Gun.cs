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
        [SerializeField] private string _projectilePoolKey;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private Transform _muzzlePoint;
        [SerializeField] private float _projectileVelocity;
        [SerializeField] private Material _trailMaterial;
        [SerializeField] private float _maxProjectileDistance;
        [SerializeField] private float _damage;

        [Header("Projectile Casings")]
        [SerializeField] private string _ejectorPoolKey;
        [SerializeField] private Ejector _ejector;
        [SerializeField] private Transform _ejectorPoint;
        [SerializeField] private float _lifeTime;

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
            ObjectPoolManager.CreatePool(_ejectorPoolKey, _ejector.gameObject);
            ObjectPoolManager.CreatePool(_projectilePoolKey, _projectile.gameObject);
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

                    if (!IsGunClipping())
                    {
                        GameObject bullet = ObjectPoolManager.GetObject(_projectilePoolKey);
                        bullet.transform.position = _muzzlePoint.position;
                        bullet.transform.rotation = _muzzlePoint.rotation;
                        bullet.GetComponent<Projectile>().InitializeProjectileStats(
                            _projectilePoolKey,
                            _maxProjectileDistance, 
                            _projectileVelocity, 
                            _damage,    
                            _trailMaterial);
                        GameObject casing = ObjectPoolManager.GetObject(_ejectorPoolKey);
                        casing.transform.position = _ejectorPoint.position;
                        casing.transform.rotation = _ejectorPoint.rotation;
                        casing.GetComponent<Ejector>().InitializeEjector(_lifeTime, _ejectorPoolKey);
                    }
                    _currentBullets--;
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
