using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class PlayerMovementSystem : SystemBase
{
	protected override void OnUpdate()
	{
		float deltaTime = Time.DeltaTime;

		Entities.WithNone<ChaserTag>().
			ForEach((ref Translation position, in MoveData moveData) =>
		{
			float3 normalizedDirection = math.normalizesafe(moveData.moveDirection); // So we don't move faster if moving diagonally // Note normalizeSAFE
			position.Value += normalizedDirection * moveData.moveSpeed * deltaTime;
		}).Run();
	}
}