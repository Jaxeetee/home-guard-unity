using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private MyPlayerInputManager _input;

    [SerializeField]
    private Gun _gun;

    Gun _equippedGun;


    // Start is called before the first frame update
    void Start()
    {
        _equippedGun = Instantiate(_gun, transform.position, transform.rotation) as Gun;
        _equippedGun.transform.parent = this.transform;
        _equippedGun.InitStats();
    }

    private void OnEnable()
    {
        _input.onShoot += ShootGun;
        _input.onReload += ReloadGun;
    }


    private void OnDisable()
    {
        _input.onShoot -= ShootGun;
        _input.onReload -= ReloadGun;
    }
    private void ShootGun()
    {
        if (_equippedGun == null) return;

       _equippedGun.GetComponent<Gun>().Shoot();
    }

    private void ReloadGun()
    {
        if (_equippedGun == null) return;

        StartCoroutine(_equippedGun.GetComponent<Gun>().Reload());
    }



}
