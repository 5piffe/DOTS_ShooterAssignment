using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using System;
using Unity.Mathematics;
using Unity.Transforms;

public class MovingStar_System : SystemBase
{
    private Unity.Mathematics.Random random;
    private const float boundsX = 18f; // TODO: Screen bounds fixa
    private const float boundsY = 11f;
    protected override void OnCreate()
	{
        random = new Unity.Mathematics.Random(24);
    }
	protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        float rnd = random.NextFloat(-boundsY + 2, boundsY);

        Entities.
            WithAny<Star_Tag>().
            ForEach((ref Translation position, in Movement_Data movementData) =>
            {
                position.Value.x -= 1f * movementData.moveSpeed * deltaTime;

				if (position.Value.x < -boundsX)
				{
                    position.Value.x = boundsX;
                    position.Value.y = rnd;
				}

            }).ScheduleParallel();
    }
}