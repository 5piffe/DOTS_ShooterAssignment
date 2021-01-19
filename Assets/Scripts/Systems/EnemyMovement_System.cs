using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class EnemyMovement_System : ComponentSystem
{
    protected override void OnUpdate()
    {
		float deltaTime = Time.DeltaTime;

		Entities.
			WithAny<Enemy_Tag>().
			ForEach((ref Translation position, ref Rotation rotation, ref Enemy_Data enemyData) =>
			{
				float yPositioning = enemyData.formationYAmplitude * math.sin((float)Time.ElapsedTime * enemyData.formationYSpeed
					+ position.Value.x * enemyData.formationYOffset);

				float zPositioning = enemyData.formationZAmplitude * math.sin((float)Time.ElapsedTime * enemyData.formationZSpeed
					+ position.Value.x * enemyData.formationZOffset);

				position.Value = new float3(position.Value.x -= enemyData.flySpeed * deltaTime, yPositioning, zPositioning);
			});
	}
}