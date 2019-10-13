using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using Unity.Rendering;

/// <summary>
/// Day-night cycle adapted from http://twiik.net/articles/simplest-possible-day-night-cycle-in-unity-5
/// </summary>
public class SDayNight : ComponentSystem
{
    protected override void OnUpdate()
    {
        EntityManager entityManager = World.Active.EntityManager;

        Entities.ForEach((Entity entity, Light light, CDayNight dayNight) => {
            light.transform.rotation = Quaternion.Euler((dayNight.timeOfDay * 360.0f) - 90.0f, 170.0f, 0.0f);

            float intensityMultiplier = 1;
            if (dayNight.timeOfDay <= 0.23f || dayNight.timeOfDay >= 0.75f) {
                intensityMultiplier = 0;
            }
            else if (dayNight.timeOfDay <= 0.25f) {
                intensityMultiplier = Mathf.Clamp01((dayNight.timeOfDay - 0.23f) * (1 / 0.02f));
            }
            else if (dayNight.timeOfDay >= 0.73f) {
                intensityMultiplier = Mathf.Clamp01(1 - ((dayNight.timeOfDay - 0.73f) * (1 / 0.02f)));
            }
    
            light.intensity = intensityMultiplier;
        });
    }
}
