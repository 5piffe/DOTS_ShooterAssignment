using Unity.Entities;
using Unity.Transforms;

public class BulletSpawner_System : ComponentSystem
{
	private float fireRate = 0f;

	protected override void OnUpdate()
	{
		float deltaTime = Time.DeltaTime;
		Entity playerEntity = GetSingletonEntity<Player_Tag>();
		Translation playerTranslation = EntityManager.GetComponentData<Translation>(playerEntity);
		Movement_Data playerMovementData = EntityManager.GetComponentData<Movement_Data>(playerEntity);

		Entities.
			WithAny<Gun_Tag>().
			ForEach((ref PrefabToEntity_Data prefabToEntity, ref Translation position) =>
			{
				if (!playerMovementData.shooting)
					return;

				fireRate -= fireRate * deltaTime;

				if (fireRate < 1)
				{
					Entity bullet = EntityManager.Instantiate(prefabToEntity.prefabToConvert);
					EntityManager.SetComponentData(bullet, new Translation { Value = playerTranslation.Value });
					fireRate = 1.1f;
				}
			});
	}
}