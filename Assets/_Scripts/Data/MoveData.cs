using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent] // So we can drag script to object even tho still not monobehaviour <- Actually check out what this really is
// Rename to just like movedata or movement since this is data that could be used in different other objects, not just the player.
public struct MoveData : IComponentData
{
	public float3 moveDirection;
	public float moveSpeed;
	public float turnSpeed;
}
