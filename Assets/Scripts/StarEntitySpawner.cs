using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class StarEntitySpawner : ComponentSystem
{
	private Random random;
	protected override void OnStartRunning()
	{
		random = new Random(48);
		Entities.ForEach((ref PrefabEntityComponent prefabEntityComponent, ref Translation position) =>
		{
			for (int i = 0; i < prefabEntityComponent.numberOfObjectsToInstatiate; i++)
			{
				float3 randomPos = new float3(random.NextFloat(-18f, 18f), random.NextFloat(-11f, 11f), 0f);
				Entity spawnedEntity = EntityManager.Instantiate(prefabEntityComponent.prefabToConvert);
				EntityManager.SetComponentData(spawnedEntity, new Translation { Value = randomPos });
			}
		});
	}

	protected override void OnUpdate()
	{
	}
}