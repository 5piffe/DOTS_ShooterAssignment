using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class StarSpawner_System : ComponentSystem
{
	Movement_Data md = new Movement_Data();
	private Random random;

	protected override void OnStartRunning()
	{
		random = new Random(48);

		Entities.
			WithAny<Star_Tag>().
			ForEach((ref PrefabToEntity_Data prefabToEntity, ref Translation position) =>
			{
				for (int i = 0; i < prefabToEntity.numberOfObjectsToInstatiate; i++)
				{
					float3 randomPos = new float3(random.NextFloat(-md.ScreenBounds().x, md.ScreenBounds().x), random.NextFloat(-md.ScreenBounds().y, md.ScreenBounds().y), 0f);
					Entity spawnedStar = EntityManager.Instantiate(prefabToEntity.prefabToConvert);
					EntityManager.SetComponentData(spawnedStar, new Translation { Value = randomPos });
				}
			});
	}

	protected override void OnUpdate()
	{
	}
}