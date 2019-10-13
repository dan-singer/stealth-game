using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using Unity.Rendering;

/// <summary>
/// General initial force applier
/// </summary>
public class SInitialForce : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, Transform transform, Rigidbody rigidbody, CInitialForce initialForce) => {
            if (!initialForce.launched) {
                rigidbody.AddForce(initialForce.force);
                initialForce.launched = true;
            }
        });
    }
}
