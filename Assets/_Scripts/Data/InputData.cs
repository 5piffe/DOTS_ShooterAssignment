using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct InputData : IComponentData
{
	public KeyCode up;
	public KeyCode down;
	public KeyCode left;
	public KeyCode right;

	// Axis for mouse & shit if you gonna shoot with mouse maybe
}