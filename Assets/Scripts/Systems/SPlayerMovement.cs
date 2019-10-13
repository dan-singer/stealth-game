using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

/// <summary>
/// Player Movement System. Handles Translation and Rotation for the First Person character
/// </summary>
public class SPlayerMovement : ComponentSystem
{
    protected override void OnUpdate()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        Entities.ForEach((Entity entity, Transform transform, CharacterController controller, CPlayerMovement playerMovement) => {
            // Translation
            Vector3 localVel = new Vector3(horz, 0, vert);
            if (localVel.sqrMagnitude > 0) {
                playerMovement.bobTime += Time.deltaTime * playerMovement.bobSpeed;
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
            if (Input.GetButtonDown("Jump"))
            {
                playerMovement.acceleration += new Vector3(0, playerMovement.jumpForceMagnitude, 0);
            }

            // Crouching
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
            }
  

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
