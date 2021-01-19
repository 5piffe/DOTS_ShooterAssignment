using Unity.Entities;

[GenerateAuthoringComponent]
public struct EnemySpawner_Data : IComponentData
{
	public Entity enemyPrefab;
	public int initialNumberOfEnemies;
	public float spawnRate, spawnXOffset;
}