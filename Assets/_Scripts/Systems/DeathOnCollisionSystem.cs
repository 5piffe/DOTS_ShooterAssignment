using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;

public class DeathOnCollisionSystem : JobComponentSystem
{
	private BuildPhysicsWorld buildPhysicsWorld;
	private StepPhysicsWorld stepPhysicsWorld;

	protected override void OnCreate()
	{
		base.OnCreate();
		buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
		stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
	}

	[BurstCompile]
	struct DeathOnCollisionSystemJob : ICollisionEventsJob
	{
		[ReadOnly] public ComponentDataFromEntity<DeathColliderTag> deathColliderGroup;
		[ReadOnly] public ComponentDataFromEntity<ChaserTag> chaserGroup;

		public ComponentDataFromEntity<HealthData> healthGroup;

		public void Execute(CollisionEvent collisionEvent)
		{
			Entity entityA = collisionEvent.EntityA;
			Entity entityB = collisionEvent.EntityB;

			bool entityAIsChaser = chaserGroup.HasComponent(entityA);
			bool entityAIsDeathCollider = deathColliderGroup.HasComponent(entityA);
			bool entityBIsChaser = chaserGroup.HasComponent(entityB);
			bool entityBIsDeathCollider = deathColliderGroup.HasComponent(entityB);

			if (entityAIsDeathCollider && entityBIsChaser)
			{
				HealthData modifiedHealth = healthGroup[entityB];
				modifiedHealth.isDead = true;
				healthGroup[entityB] = modifiedHealth;
			}

			if (entityAIsChaser && entityBIsDeathCollider)
			{
				HealthData modifiedHealth = healthGroup[entityA];
				modifiedHealth.isDead = true;
				healthGroup[entityA] = modifiedHealth;
			}
		}
	}
	protected override JobHandle OnUpdate(JobHandle inputDeps)
	{
		var job = new DeathOnCollisionSystemJob();
		job.deathColliderGroup = GetComponentDataFromEntity<DeathColliderTag>(true);
		job.chaserGroup = GetComponentDataFromEntity<ChaserTag>(true);
		job.healthGroup = GetComponentDataFromEntity<HealthData>(false);

		JobHandle jobHandle = job.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld,
			inputDeps);
		jobHandle.Complete();

		return jobHandle;
	}
}
