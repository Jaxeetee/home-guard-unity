using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Weapons {
public class GunController : MonoBehaviour
{

    [SerializeField] MyPlayerInputManager _input;
    [SerializeField] LayerMask _collisions;
    [SerializeField] Gun _starterGun;
    Gun _equippedGun;

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

    void Update()
    {
        // Ray ray = new Ray(transform.position, transform.forward);
        // if (Physics.Raycast(ray, out var hit, 1f, _collisions))
        // {
        //     Vector3 incomingVec = hit.point - transform.position;

        //     Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);

        //     Debug.DrawLine(transform.position, hit.point, Color.red);
        //     Debug.DrawRay(hit.point, reflectVec, Color.green);
        //     var direction = reflectVec - transform.position;
        //     direction.y = 0;
        //     transform.forward = direction;
        // }
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
