using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using Unity.Rendering;

/// <summary>
/// Gun System
/// </summary>
public class SGun : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, Transform transform, CGun gun, AudioSource audioSource) => {
            if (!gun.initialized) {
                gun.CurAmmo = gun.startAmmo;
                gun.initialized = true;
            }
            if (Input.GetButtonDown("Fire1")) {
                Vector3 launchDir = gun.transform.forward;
                GameObject bullet = GameObject.Instantiate(gun.bullet, gun.socket.position, Quaternion.identity);
                bullet.GetComponent<CInitialForce>().force = launchDir * gun.launchForceMagnitude;
                audioSource.PlayOneShot(gun.fireSound);
                --gun.CurAmmo;
            }
        });
    }
}
