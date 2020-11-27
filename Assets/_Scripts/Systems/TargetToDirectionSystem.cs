using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class TargetToDirectionSystem : SystemBase
{
    protected override void OnUpdate()
    {
		Entities.
			WithNone<PlayerTag>().
			WithAll<ChaserTag>().
			ForEach((ref MoveData moveData, ref Rotation rotation, in Translation position, in TargetData targetData) =>
			{
				ComponentDataFromEntity<Translation> allTranslations = GetComponentDataFromEntity<Translation>(true); // Gets for all entities using Translation?
				if (!allTranslations.HasComponent(targetData.targetEntity)) // allTranslations.Exists() <- Deprecated
				{
					return;
				}

				Translation targetPos = allTranslations[targetData.targetEntity];
				float3 directionToTarget = targetPos.Value - position.Value;
				moveData.moveDirection = directionToTarget;

			}).Run();
	}
}
