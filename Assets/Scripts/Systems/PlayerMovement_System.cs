using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

//[AlwaysSynchronizeSystem] // TODO: Need this? check up
public class PlayerMovement_System : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        float xBounds = 18f;
        float yBounds = 11f; // TODO: Either get screen.width/height * 0.5f somehow (not supported, dots?), or put these values in some gameMgr or something

        Entities.
            WithAny<Player_Tag>().
            ForEach((ref Translation position, in Movement_Data movementData) =>
            {
                float3 normalizedDirection = math.normalizesafe(movementData.moveDirection);
                position.Value.x = (math.clamp(position.Value.x + normalizedDirection.x * movementData.moveSpeed * deltaTime, -xBounds, xBounds));
                position.Value.y = (math.clamp(position.Value.y + normalizedDirection.y * movementData.moveSpeed * deltaTime, -yBounds, yBounds));

            }).Run();
    }
}