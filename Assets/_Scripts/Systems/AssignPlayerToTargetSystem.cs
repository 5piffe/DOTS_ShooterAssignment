using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(SimulationSystemGroup))] // Look in entity debugger under default world systems thing to se these groups
[UpdateBefore(typeof(TargetToDirectionSystem))] // So you can basically change in what order the systems happen
public class AssignPlayerToTargetSystem : SystemBase
{
	protected override void OnStartRunning()
	{
        base.OnStartRunning();
        AssignPlayer();
	}

	protected override void OnUpdate()
    {
       
    }

    private void AssignPlayer()
	{
        // EntityQuery playerQuery = GetEntityQuery(typeof(PlayerTag));
        EntityQuery playerQuery = GetEntityQuery(ComponentType.ReadOnly<PlayerTag>()); // <- same as above but readonly, use readonly when you can, it'll speed up your jobs

        //Entity playerEntity = playerQuery.ToEntityArray(Allocator.Temp)[0];
        Entity playerEntity = playerQuery.GetSingletonEntity();

        Entities.
            WithAll<ChaserTag>().
            ForEach((ref TargetData targetData) =>
            {
                if (playerEntity != Entity.Null) // Nullcheck with entities is Entity.Null rather than just null
                {
                    targetData.targetEntity = playerEntity;
                }
            }).Schedule();
    }
}
