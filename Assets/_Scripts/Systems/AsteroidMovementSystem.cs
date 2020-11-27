using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class AsteroidMovementSystem : SystemBase
{
	protected override void OnUpdate()
	{
		float deltaTime = Time.DeltaTime;
		//float moveSpeed = 1f; // Don't do this. Use a data for it. Like the playerMoveData that you've got. Gotta reuse it, and can rename it to just movement or something

		// If we used data for direction we would also use a ref inside these parameters to that data :)
		Entities.
			WithAll<ChaserTag>().
			//WithAll<AsteroidTag>().
			WithNone<PlayerTag>().
			ForEach((ref Translation position, in MoveData moveData, in Rotation rotation) =>
			{
				//float3 forwardDirection = math.forward(rotation.Value); //Fucked up movement directions
				//position.Value += forwardDirection * moveData.moveSpeed * deltaTime;

				position.Value += new float3(-1, 0, 0) * moveData.moveSpeed * deltaTime;

			}).ScheduleParallel();
	}
}
