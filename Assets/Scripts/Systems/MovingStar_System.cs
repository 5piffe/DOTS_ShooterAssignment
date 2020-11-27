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
        float rnd = random.NextFloat(-18f, 18f); // TODO: Screen width fixa

        Entities.
            WithAny<Star_Tag>().
            ForEach((ref Translation position, in Movement_Data movementData) =>
            {
                position.Value.x -= 1f * movementData.moveSpeed * deltaTime;

				if (position.Value.x < -18f)
				{
                    position.Value.x = 18f;
                    position.Value.y = rnd;
				}

            }).ScheduleParallel(); // TODO: kolla parallel och schedule osv. Kolla hur fan man ser threads i profiler också.
    }
}