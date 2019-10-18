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
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, Transform transform, CPlayerVisibility playerVisibility) => {
            if (!playerVisibility.initialized) {
                // Subscribe to AI events
                StateController.SawTarget = () => {
                    ++playerVisibility.numEnemiesSeenBy;
                    playerVisibility.Seen?.Invoke();
                };
                StateController.LostTarget = () => {
                    --playerVisibility.numEnemiesSeenBy;
                };
                StateController.BeganSearching = () => {
                    ++playerVisibility.numEnemiesSearchingFor;
                    if (playerVisibility.numEnemiesSeenBy == 0 && playerVisibility.SearchedFor != null) {
                        playerVisibility.SearchedFor();
                    }
                };
                StateController.FinishedSearching = () => {
                    --playerVisibility.numEnemiesSearchingFor;
                    if (playerVisibility.numEnemiesSearchingFor == 0 && playerVisibility.numEnemiesSeenBy == 0 && playerVisibility.Hidden != null) {
                        playerVisibility.Hidden();
                    }
                };
                StateController.HitTarget = () => {
                    playerVisibility.isDead = true;
                    // Find the nearest spawn location
                    playerVisibility.nearestSpawnLocation = playerVisibility.spawnTransforms[0].position;
                    for (int i = 1; i < playerVisibility.spawnTransforms.Length; ++i) {
                        if (Vector3.SqrMagnitude(playerVisibility.spawnTransforms[i].position - transform.position) < 
                            Vector3.SqrMagnitude(playerVisibility.nearestSpawnLocation - transform.position)) {
                            playerVisibility.nearestSpawnLocation = playerVisibility.spawnTransforms[i].position;
                        }
                    }
                    --playerVisibility.lives;
                    playerVisibility.deathTimer = 0.0f;
                    if (playerVisibility.lives > 0) {
                        playerVisibility.deathMessage.SetActive(true);
                    }
                    playerVisibility.Hidden?.Invoke();
                };
                playerVisibility.initialized = true;
            }

            // Vignette calculations
            Color targetColor = new Color(0, 0, 0, 0);
            float targetIntensity = 0.0f;
            Vector2 targetCenter = new Vector2(0.5f, 0.5f);

            if (playerVisibility.isDead) {
                if (playerVisibility.lives <= 0) {
                    SceneManager.LoadScene("DeathScene");
                }
                else {
                    // Determine nearest spawn location
                    if (Mathf.Approximately(playerVisibility.deathTimer, 0.0f)) {
                        transform.position = playerVisibility.nearestSpawnLocation + new Vector3(0, 2.0f, 0);
                    }
                    playerVisibility.deathTimer += Time.deltaTime;
                    if (playerVisibility.deathTimer > playerVisibility.deathDuration) {
                        playerVisibility.deathMessage.SetActive(false);
                        playerVisibility.isDead = false;
                    }
                    targetColor = Color.red;
                    targetIntensity = 1.0f;
                    targetCenter.x = 2.0f;
                }

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
