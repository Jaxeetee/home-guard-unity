using System.Collections;
using System.Collections.Generic;
using MyUtils;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour,IDebugger
{
    
    [SerializeField]
    private Bullet _bullet;

    [SerializeField]
    private Transform _bulletSpawnPoint;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private float _fireRate;

    [SerializeField]
    private float _reloadTime;

    [SerializeField]
    private int _magSize;
    private int _currentBullets;
    private bool _canShoot = true;

    void Start()
    {
        PooledObject.NewObjectPool(StringManager.BULLET_PISTOL, _bullet.gameObject);
    }

    void OnEnable()
    {
        Warning(_currentBullets.ToString());
    }

    public void InitStats()
    {
        _currentBullets = _magSize;
    }

    public void Shoot()
    {
        if (Time.time > _fireRate && _canShoot && _currentBullets > 0)
        {
            GameObject bullet = PooledObject.GetObject(StringManager.BULLET_PISTOL);
            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.rotation = _bulletSpawnPoint.rotation;
            bullet.GetComponent<Bullet>().SetSpeed(_bulletSpeed);
            this._currentBullets--;
        }
    }

    public IEnumerator Reload()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _reloadTime)
        {
            _canShoot = false;  
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _canShoot = true;
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
