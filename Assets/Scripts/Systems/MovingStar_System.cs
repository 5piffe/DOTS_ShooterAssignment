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
    protected override void OnCreate()
	{
        random = new Unity.Mathematics.Random(24);
    }

	protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        float rnd = random.NextFloat(-18 + 2, 11);

        Entities.
            WithAny<Star_Tag>().
            ForEach((ref Translation position, in Movement_Data movementData) =>
            {
                position.Value.x -= 1f * movementData.moveSpeed * deltaTime;

				if (position.Value.x < -movementData.ScreenBounds().x)
				{
                    position.Value.x = movementData.ScreenBounds().x;
                    position.Value.y = rnd;
				}

            }).ScheduleParallel();
    }
}