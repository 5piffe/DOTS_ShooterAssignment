using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public class BulletSpawner_System : ComponentSystem
{
	private float fireRate = 1.1f;

	protected override void OnUpdate()
	{
		float deltaTime = Time.DeltaTime;
		Entity playerEntity = GetSingletonEntity<Player_Tag>();
		Translation playerTranslation = EntityManager.GetComponentData<Translation>(playerEntity);

		Entities.
			WithAny<Gun_Tag>().
			ForEach((ref PrefabToEntity_Data prefabEntityComponent, ref Translation position) =>
			{
				fireRate -= fireRate * deltaTime;
				if (fireRate < 1)
				{
					Entity spawnedEntity = EntityManager.Instantiate(prefabEntityComponent.prefabToConvert);
					EntityManager.SetComponentData(spawnedEntity, new Translation { Value = playerTranslation.Value });
					
					fireRate = 1.1f;
				}
			});
	}
}