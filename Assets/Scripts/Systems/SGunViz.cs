using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using Unity.Rendering;

/// <summary>
/// </summary>
public class SGunViz : ComponentSystem
{
    protected override void OnUpdate()
    {
        EntityManager entityManager = World.Active.EntityManager;
        Entities.ForEach((Entity entity, Transform transform, CGun gun, CGunViz gunViz, LineRenderer lineRenderer) => {

            if (Input.GetButton("Fire2")) {
                lineRenderer.enabled = true;
                Vector3 launchDir = gun.transform.forward;
                // s = .5at^2 + v0t + h0
                Vector3 a = new Vector3(0, -9.81f, 0);
                Vector3 v0 = launchDir * gun.launchForceMagnitude * Time.fixedDeltaTime;
                Vector3 h0 = gun.socket.position;

                // Figure out what time we'll hit the ground
                float height = 1.8f;
                float targetHeight = gun.cam.transform.position.y - height;

                float timeOfImpact = (-v0.y - Mathf.Sqrt((v0.y * v0.y) - (2 * a.y) * (h0.y - targetHeight))) / a.y;
                
                Vector3[] positions = new Vector3[gunViz.resolution + 1];
                for (int i = 0; i < positions.Length; ++i) {
                    float t = (i / (float)gunViz.resolution) * timeOfImpact;
                    positions[i] =  (.5f * a * t * t) + (v0 * t) + h0; 
                    positions[i] = gun.transform.InverseTransformPoint(positions[i]);
                }
                lineRenderer.startWidth = lineRenderer.endWidth = 0.1f;
                lineRenderer.positionCount = positions.Length;
                lineRenderer.SetPositions(positions);
            } 
            else {
                lineRenderer.enabled = false;
            }

        });
    }
}
