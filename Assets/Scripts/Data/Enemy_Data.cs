using Unity.Entities;

[GenerateAuthoringComponent]
public struct Enemy_Data : IComponentData
{
	public float formationYAmplitude, formationYOffset,
				formationZAmplitude, formationZOffset,
				formationYSpeed, formationZSpeed, flySpeed;

}