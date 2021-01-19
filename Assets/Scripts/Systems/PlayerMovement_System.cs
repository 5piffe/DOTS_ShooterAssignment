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

        Entities.
            WithAny<Player_Tag>().
            ForEach((ref Translation position, in Movement_Data movementData) =>
            {
                float3 normalizedDirection = math.normalizesafe(movementData.moveDirection);
                position.Value.x = (math.clamp(position.Value.x + normalizedDirection.x * movementData.moveSpeed * deltaTime, -movementData.ScreenBounds().x, movementData.ScreenBounds().x));
                position.Value.y = (math.clamp(position.Value.y + normalizedDirection.y * movementData.moveSpeed * deltaTime, -movementData.ScreenBounds().y + 2, movementData.ScreenBounds().y));

            }).Run();
    }
}