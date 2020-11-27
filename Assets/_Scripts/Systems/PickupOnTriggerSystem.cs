using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

//[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class PickupOnTriggerSystem : JobComponentSystem
{
	private BuildPhysicsWorld buildPhysicsWorld; // setting up
	private StepPhysicsWorld stepPhysicsWorld; // running simulation

	private EndSimulationEntityCommandBufferSystem commandBufferSystem;

	protected override void OnCreate()
	{
		base.OnCreate();
		buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
		stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
		commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
	}

	// [BurstCompatible] // Burst doesn't run on the main thread so that doesn't work here
	struct PickupOnTriggerSystemJob : ITriggerEventsJob
	{
		[ReadOnly] public ComponentDataFromEntity<PickupTag> allPickups;
		[ReadOnly] public ComponentDataFromEntity<PlayerTag> allPlayers;

		public EntityCommandBuffer entityCommandBuffer;

		public void Execute(TriggerEvent triggerEvent)
		{
			Entity entityA = triggerEvent.EntityA;
			Entity entityB = triggerEvent.EntityB;

			if (allPickups.HasComponent(entityA) && allPickups.HasComponent(entityB))
			{
				return;
			}

			if (allPickups.HasComponent(entityA) && allPlayers.HasComponent(entityB))
			{
				entityCommandBuffer.DestroyEntity(entityA);
			}
			else if (allPlayers.HasComponent(entityA) && allPickups.HasComponent(entityB))
			{
				entityCommandBuffer.DestroyEntity(entityB);
			}

		}
	}
	protected override JobHandle OnUpdate(JobHandle inputDeps)
	{
		var job = new PickupOnTriggerSystemJob();
		job.allPickups = GetComponentDataFromEntity<PickupTag>(true);
		job.allPlayers = GetComponentDataFromEntity<PlayerTag>(true);
		job.entityCommandBuffer = commandBufferSystem.CreateCommandBuffer();

		JobHandle jobHandle = job.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, inputDeps);


		//jobHandle.Complete();
		commandBufferSystem.AddJobHandleForProducer(jobHandle);
		return jobHandle;
	}
}
