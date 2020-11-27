using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class SpinnerSystem : SystemBase
{
	protected override void OnUpdate()
	{
		float deltaTime = Time.DeltaTime;

		Entities.
			WithAll<SpinnerTag>(). // So instead of enteties.ForEach we apply filters of what this is gonna affect. we've got WithAll, WithNone and WithAny
			WithNone<PlayerTag>().								// In the parameter you can have filters if you'd like. but then you can't have the ones above, since you would apply the same filter twice
			ForEach((ref Rotation rotation, in MoveData moveData/*, in SpinnerTag spinnerTag*/) =>
			{
				quaternion normalizedRotation = math.normalize(rotation.Value);
				quaternion angleToRotate = quaternion.AxisAngle(math.up(), moveData.turnSpeed * deltaTime);

				rotation.Value = math.mul(normalizedRotation, angleToRotate); // <- math.mul vadå kan man inte göra bara *. kolla vad den gör

			}).ScheduleParallel();
	}
}
