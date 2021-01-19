using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct Movement_Data : IComponentData
{
    [System.NonSerialized] public float3 moveDirection;
    [System.NonSerialized] public bool shooting;
    public float moveSpeed;
    
    public float2 ScreenBounds()
	{
        return new float2(18,11);
	}
}