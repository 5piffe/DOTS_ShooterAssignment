using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class FaceInDirectionSystem : SystemBase // My game won't need the player to be able to face the direction since we're going with classic style shmup game, but check it out.
{
	protected override void OnUpdate()
	{
		Entities.
			ForEach((ref Rotation rotation, in Translation position, in MoveData moveData) =>
			{
				FaceInDirection(ref rotation, moveData);

			}).Schedule();
	}

	private static void FaceInDirection(ref Rotation rot, MoveData moveData) // Making a static method like this is typical for a system when you want to reuse code
	{
		if (!moveData.moveDirection.Equals(float3.zero)) // Check if it's better to do if it's zero then return, (is that an erly return statement?)
		{
			quaternion targetRotation = quaternion.LookRotationSafe(moveData.moveDirection, math.left()); // haha directions are fucked up too. would be cool to still tilt ship "up" or "down" sideways so you se the wings when going down and underside wing when going up
			rot.Value = math.slerp(rot.Value, targetRotation, moveData.turnSpeed);
		}
	}
}