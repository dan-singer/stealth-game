using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// System that modifies a post process profile to indicate player's visibility state
/// </summary>
public class SPlayerVisibility : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, Transform transform, CPlayerVisibility playerVisibility) => {
            if (!playerVisibility.initialized) {
                StateController.SawTarget += () => {
                    ++playerVisibility.numEnemiesSeenBy;
                };
                StateController.LostTarget += () => {
                    --playerVisibility.numEnemiesSeenBy;
                };
                StateController.BeganSearching += () => {
                    ++playerVisibility.numEnemiesSearchingFor;
                };
                StateController.FinishedSearching += () => {
                    --playerVisibility.numEnemiesSearchingFor;
                };
                playerVisibility.initialized = true;
            }
            Color targetColor = new Color(0, 0, 0, 0);
            float targetIntensity = 0.0f;
            if (playerVisibility.numEnemiesSeenBy > 0) {
                targetColor = Color.red;
                targetIntensity = playerVisibility.targetIntensity;
            }
            else if (playerVisibility.numEnemiesSearchingFor > 0) {
                targetColor = Color.yellow;
                targetIntensity = playerVisibility.targetIntensity;
            }
            Vignette vignette = playerVisibility.profile.GetSetting<Vignette>();
            Color lerpedColor = Color.Lerp(vignette.color.value, targetColor, 0.1f);
            float lerpedIntensity = Mathf.Lerp(vignette.intensity.value, targetIntensity, 0.1f);

            vignette.color.value = lerpedColor;
            vignette.intensity.value = lerpedIntensity;
        });
    }
}
