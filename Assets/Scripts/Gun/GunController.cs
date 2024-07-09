using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Weapons {
public class GunController : MonoBehaviour
{
    [SerializeField] MyPlayerInputManager _input;
    [SerializeField] Gun _starterGun;
    [SerializeField] Gun _gun;

    Gun _equippedGun;
    bool _canShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        EquipGun(_starterGun);
    }

    private void OnEnable()
    {
        _input.onShootHold += TriggerHold;
        _input.onShootRelease += TriggerRelease;
        _input.onReload += ReloadGun;
    }


    private void OnDisable()
    {
        _input.onShootHold -= TriggerHold;
        _input.onShootRelease -= TriggerRelease;
        _input.onReload -= ReloadGun;
    }

    private void EquipGun(Gun gun)
    {
        if (_equippedGun != null)
        {
            Destroy(_equippedGun.gameObject);
        }

        _equippedGun = Instantiate(gun, transform.position, transform.rotation) as Gun;
        _equippedGun.transform.parent = this.transform;
        _equippedGun.InitStats();
    }

    private void TriggerHold()
    {
        if (_equippedGun == null) return;
            Debug.Log("SHOOTING");
            _equippedGun.onTriggerHold();
        
    }

    private void TriggerRelease()
    {
        if (_equippedGun == null) return;
            _equippedGun.onTriggerRelease();
    }

    private void ReloadGun()
    {
        if (_equippedGun == null) return;
        StartCoroutine(_equippedGun.Reload());
    }



}

}
