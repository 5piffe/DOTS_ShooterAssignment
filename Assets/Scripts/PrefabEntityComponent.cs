using Unity.Entities;

[GenerateAuthoringComponent] 
public struct PrefabEntityComponent : IComponentData
{
	public Entity prefabToConvert;
	public int numberOfObjectsToInstatiate;
}