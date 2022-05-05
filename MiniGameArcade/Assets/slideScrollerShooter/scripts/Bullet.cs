using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;

using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Transform firepoint;
    public GameObject bulletPrefab;

     void Update()
    {
     
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


     void Shoot()
     {
         Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
     }
}
