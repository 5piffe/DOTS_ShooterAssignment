using Unity.Entities;

[GenerateAuthoringComponent] 
public struct PrefabToEntity_Data : IComponentData
{
	public Entity prefabToConvert;
	public int numberOfObjectsToInstatiate;
}