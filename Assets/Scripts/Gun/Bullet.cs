using System.Collections;
using System.Collections.Generic;
using MyUtils;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 50f;

    // void OnEnable()
    // {
    //     transform.rotation = Quaternion.identity;
    //     Debug.Log("Bullet rotation after reset: " + transform.rotation.eulerAngles.ToString());
    // }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter(Collision other)
    {
        PooledObject.ReturnToPool(StringManager.BULLET_PISTOL ,this.gameObject);
    }

    public void SetSpeed(float speed) => _speed = speed;
}
