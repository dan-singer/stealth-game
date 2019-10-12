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
                Vector3 launchDir = gun.transform.forward;
                if (Vector3.Angle(Vector3.forward, launchDir) < 20.0f)
                {
                    launchDir = Quaternion.AngleAxis(gun.pitchOffset, gun.transform.right) * launchDir;
                }

                GameObject bullet = GameObject.Instantiate(gun.bullet, gun.socket.position, Quaternion.identity);
                bullet.GetComponent<CInitialForce>().force = launchDir * gun.launchForceMagnitude;
            }
        });
    }
}
