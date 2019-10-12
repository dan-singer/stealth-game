using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using Unity.Rendering;

/// <summary>
/// </summary>
public class SGun : ComponentSystem
{
    protected override void OnUpdate()
    {
        EntityManager entityManager = World.Active.EntityManager;

        Entities.ForEach((Entity entity, Transform transform, CGun gun) => {
            if (Input.GetButtonDown("Fire1")) {
                Vector3 worldCenter = gun.cam.transform.position;

                Vector3 launchDir = gun.transform.forward;
                launchDir = Quaternion.AngleAxis(-75.0f, gun.transform.right) * launchDir;

                Debug.DrawRay(gun.socket.position, launchDir, Color.red, 3.0f);
                
                GameObject bullet = GameObject.Instantiate(gun.bullet, gun.socket.position, Quaternion.identity);
                bullet.GetComponent<CInitialForce>().force = launchDir * gun.launchForceMagnitude;
            }
        });
    }
}
