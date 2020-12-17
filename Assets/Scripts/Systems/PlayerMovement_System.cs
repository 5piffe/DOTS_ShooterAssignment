using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class PlayerMovement_System : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        float xBounds = 18f;
        float yBounds = 11f;

        Entities.
            WithAny<Player_Tag>().
            ForEach((ref Translation position, in Movement_Data movementData, in Input_Data input) =>
            {
                float3 normalizedDirection = math.normalizesafe(movementData.moveDirection);
                position.Value.x = (math.clamp(position.Value.x + normalizedDirection.x * movementData.moveSpeed * deltaTime, -xBounds, xBounds));
                position.Value.y = (math.clamp(position.Value.y + normalizedDirection.y * movementData.moveSpeed * deltaTime, -yBounds + 2, yBounds));
            }).Run();
    }
}