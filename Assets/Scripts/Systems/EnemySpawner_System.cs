using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class EnemySpawner_System : ComponentSystem
{
	float initSpawnIn = 1;
	float spawnX;
	int enemiesSpawned;
	bool waveSpawned = false; //TODO: Wavetimer

	protected override void OnUpdate()
	{
		float deltaTime = Time.DeltaTime;
		
		Entities.
			WithAny<EnemySpawner_Tag>().
			ForEach((ref EnemySpawner_Data enemySpawnData, ref Translation position) =>
			{
				if (waveSpawned)
					return;

				initSpawnIn -= deltaTime;

				if (initSpawnIn < 0f && enemiesSpawned < enemySpawnData.initialNumberOfEnemies)
				{
					Entity enemy = EntityManager.Instantiate(enemySpawnData.enemyPrefab);
					EntityManager.SetComponentData(enemy, new Translation { Value = new float3(spawnX, 0, 0) });
					
					initSpawnIn = enemySpawnData.spawnRate;
					spawnX += enemySpawnData.spawnXOffset;
					enemiesSpawned++;
				}
			});
	}
}