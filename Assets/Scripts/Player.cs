using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float rotationSpeed = 10.0f;
    [SerializeField]
    private Mesh mesh;
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        EntityManager entityManager = World.Active.EntityManager;
        EntityArchetype playerArchetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(Rotation),
            typeof(CPlayerMovement)
        );
        Entity player = entityManager.CreateEntity(playerArchetype);
        entityManager.SetComponentData(player, new CPlayerMovement{moveSpeed = moveSpeed, rotationSpeed = rotationSpeed});

    }
}
