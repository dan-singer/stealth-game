using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

public class SPlayer : ComponentSystem
{
    protected override void OnUpdate()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        Entities.ForEach((Entity entity, Transform transform, CharacterController controller, CPlayerMovement playerMovement, CPitchYaw pitchYaw) => {
            // Translation
            Vector3 localVel = new Vector3(horz, 0, vert);
            Vector3 worldVel = Quaternion.Euler(0, transform.eulerAngles.y, 0) * localVel;
            worldVel *= playerMovement.moveSpeed;
            controller.Move(worldVel * Time.deltaTime);

            // Rotation
            float yaw = Input.GetAxis("Mouse X");
            float pitch = Input.GetAxis("Mouse Y");

            pitchYaw.pitch += -pitch * playerMovement.rotationSpeed * Time.deltaTime;
            pitchYaw.yaw += yaw * playerMovement.rotationSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(pitchYaw.pitch, pitchYaw.yaw, 0);
        });
    }
}
