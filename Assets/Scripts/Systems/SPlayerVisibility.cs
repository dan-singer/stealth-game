using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

/// <summary>
/// System that modifies a post process profile to indicate player's visibility state
/// </summary>
public class SPlayerVisibility : ComponentSystem
{

    Vector3 nearestSpawnLocation = Vector3.zero;

    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, Transform transform, CPlayerVisibility playerVisibility) => {
            if (!playerVisibility.initialized) {
                StateController.SawTarget += () => {
                    ++playerVisibility.numEnemiesSeenBy;
                    if (playerVisibility.Seen != null) {
                        playerVisibility.Seen();
                    }
                };
                StateController.LostTarget += () => {
                    --playerVisibility.numEnemiesSeenBy;
                };
                StateController.BeganSearching += () => {
                    ++playerVisibility.numEnemiesSearchingFor;
                    if (playerVisibility.numEnemiesSeenBy == 0 && playerVisibility.SearchedFor != null)
                        playerVisibility.SearchedFor();
                };
                StateController.FinishedSearching += () => {
                    --playerVisibility.numEnemiesSearchingFor;
                    if (playerVisibility.numEnemiesSearchingFor == 0 && playerVisibility.numEnemiesSeenBy == 0 && playerVisibility.Hidden != null) {
                        playerVisibility.Hidden();
                    }
                };
                StateController.HitTarget += () => {
                    playerVisibility.isDead = true;
                    // Find the nearest spawn location
                    nearestSpawnLocation = playerVisibility.spawnTransforms[0].position;
                    for (int i = 1; i < playerVisibility.spawnTransforms.Length; ++i) {
                        if (Vector3.SqrMagnitude(playerVisibility.spawnTransforms[i].position - transform.position) < Vector3.SqrMagnitude(nearestSpawnLocation - transform.position)) {
                            Debug.Log("Checking " + i);
                            nearestSpawnLocation = playerVisibility.spawnTransforms[i].position;
                        }
                    }
                    --playerVisibility.lives;
                    if (playerVisibility.lives <= 0) {
                        
                    }
                    playerVisibility.deathTimer = 0.0f;
                    if (playerVisibility.Hidden != null) {
                        playerVisibility.Hidden();
                    }
                };
                playerVisibility.initialized = true;
            }
            if (playerVisibility.isDead) {
                if (playerVisibility.lives <= 0) {
                    SceneManager.LoadScene("DeathScene");
                }
                else {
                    if (Mathf.Approximately(playerVisibility.deathTimer, 0.0f)) {
                        transform.position = nearestSpawnLocation + new Vector3(0, 2.0f, 0);
                    }
                    playerVisibility.deathTimer += Time.deltaTime;
                    if (playerVisibility.deathTimer > playerVisibility.deathDuration) {
                        playerVisibility.isDead = false;
                    }
                }

            }

            Color targetColor = new Color(0, 0, 0, 0);
            float targetIntensity = 0.0f;
            Vector2 targetCenter = new Vector2(0.5f, 0.5f);
            if (playerVisibility.isDead) {
                targetColor = Color.red;
                targetIntensity = 1.0f;
                targetCenter.x = 2.0f;
            }
            else if (playerVisibility.numEnemiesSeenBy > 0) {
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
            vignette.center.value = targetCenter;
        });
    }
}
