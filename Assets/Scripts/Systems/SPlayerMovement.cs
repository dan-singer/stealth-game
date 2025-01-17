using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

/// <summary>
/// Player Movement System.
/// </summary>
public class SPlayerMovement : ComponentSystem
{
    protected override void OnUpdate()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        Entities.ForEach((Entity entity, Transform transform, CharacterController controller, CPlayerMovement playerMovement, AudioSource audioSource) => {
            // Translation
            Vector3 localVel = new Vector3(horz, 0, vert);
            if (localVel.sqrMagnitude > 0) {
                playerMovement.bobTime += Time.deltaTime * playerMovement.bobSpeed;

                // Footsteps
                if (playerMovement.footstepTime == 0.0f) {
                    audioSource.PlayOneShot(playerMovement.footstepSounds[playerMovement.curFootstepIndex]);
                }
                else if (playerMovement.footstepTime > playerMovement.footstepSounds[playerMovement.curFootstepIndex].length) {
                    playerMovement.footstepTime = 0.0f;
                    playerMovement.curFootstepIndex = (playerMovement.curFootstepIndex + 1) % playerMovement.footstepSounds.Length;
                    audioSource.PlayOneShot(playerMovement.footstepSounds[playerMovement.curFootstepIndex]);
                }
                playerMovement.footstepTime += Time.deltaTime;
            } 
            else {
                playerMovement.bobTime %= Mathf.PI * 2;
                float resumeTarget;
                if (playerMovement.bobTime <= Mathf.PI) {
                    resumeTarget = Mathf.PI;
                } 
                else {
                    resumeTarget = Mathf.PI * 2;
                }
                playerMovement.bobTime = Mathf.Lerp(playerMovement.bobTime, resumeTarget, 0.1f);
                playerMovement.footstepTime = 0.0f;
            }

            // Sprinting
            if (Input.GetButtonDown("Sprint"))
            {
                playerMovement.moveSpeed *= 2;
            }
            else if (Input.GetButtonUp("Sprint"))
            {
                playerMovement.moveSpeed /= 2;
            }

            Vector3 worldVel = Quaternion.Euler(0, transform.eulerAngles.y, 0) * localVel;
            worldVel *= playerMovement.moveSpeed;

            // Prevent y acceleration from accumulation
            if (!controller.isGrounded) {
                playerMovement.acceleration += new Vector3(0, -9.81f, 0);
            }
            else {
                playerMovement.acceleration.y = 0;
            }

            // Forces
            if (Input.GetButtonDown("Jump") && controller.isGrounded)
            {
                playerMovement.acceleration += new Vector3(0, playerMovement.jumpForceMagnitude, 0);
            }



            // Uncomment for crouching ability
            /*
            if (Input.GetButtonDown("Crouch"))
            {
                playerMovement.isCrouched = true;
                controller.height /= 2;
                playerMovement.moveSpeed /= 2;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                playerMovement.isCrouched = false;
                controller.height *= 2;
                playerMovement.moveSpeed *= 2;
            }*/
  

            worldVel += playerMovement.acceleration * Time.deltaTime;
            controller.Move(worldVel * Time.deltaTime);

            // Rotation
            float yaw = Input.GetAxis("Mouse X");
            float pitch = Input.GetAxis("Mouse Y");

            playerMovement.pitch += -pitch * playerMovement.rotationSpeed * Time.deltaTime;
            playerMovement.pitch = Mathf.Clamp(playerMovement.pitch, playerMovement.minPitch, playerMovement.maxPitch);
            playerMovement.yaw += yaw * playerMovement.rotationSpeed * Time.deltaTime;

            // Head Bob
            float pitchOffset = Mathf.Sin(playerMovement.bobTime) * playerMovement.bobAmplitude;

            transform.rotation = Quaternion.Euler(playerMovement.pitch + pitchOffset, playerMovement.yaw, 0);
        });
    }
}
