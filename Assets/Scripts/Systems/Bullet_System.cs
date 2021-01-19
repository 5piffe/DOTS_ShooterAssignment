using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using System;
using Unity.Mathematics;
using Unity.Transforms;

public class Bullet_System : SystemBase
{
	EndSimulationEntityCommandBufferSystem m_EndSimulationEcbSystem; /* TODO: if usig this more places (like enemies) make "destroy"system instead or something */

	protected override void OnCreate()
	{
		m_EndSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
	}

	protected override void OnUpdate()
	{
		float deltaTime = Time.DeltaTime;
		var ecb = m_EndSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();

		Entities.
			WithAny<Bullet_Tag>().
			ForEach((Entity entity, int entityInQueryIndex, ref Translation position, in Movement_Data movementData) =>
			{
				position.Value.x += 1f * movementData.moveSpeed * deltaTime;

				if (position.Value.x > movementData.ScreenBounds().x)
				{
					ecb.DestroyEntity(entityInQueryIndex, entity);
				}


			}).ScheduleParallel();
	}
}