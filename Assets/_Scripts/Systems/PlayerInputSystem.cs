using System;
using UnityEngine;
using Unity.Entities;

public class PlayerInputSystem : SystemBase
{
	protected override void OnUpdate() // Execute every frame
	{
		// Entities.ForEach takes a "Lambda expression" as argument ( params ) => { expression }   <-Kolla up lambda expression mer. Who dis?
		// Here we could have filter Entities.WithAny<PlayerTag>(). But I don't think well put inputcomponent on other stuff anyway
		Entities.ForEach((ref MoveData moveData, in InputData inputData) => // in instead of ref keyword, for readonly on the inputdata (small optimization)
		{
			bool upKey = Input.GetKey(inputData.up);
			bool downKey = Input.GetKey(inputData.down);
			bool rightKey = Input.GetKey(inputData.right);
			bool leftKey = Input.GetKey(inputData.left);

			//Converting bool to int32 (kolla om vi kan göra en int() som returnar 1 eller 0 om boolen e true false eller nå istället kanske
			// Han körde: moveData.Direction.x = (rightKey) ? 1 : 0; vilket är mer som jag tänkte, men gör det ännu snyggare med en funktion grej.
			moveData.moveDirection.z = Convert.ToInt32(upKey);
			moveData.moveDirection.z -= Convert.ToInt32(downKey);
			moveData.moveDirection.x = Convert.ToInt32(rightKey);
			moveData.moveDirection.x -= Convert.ToInt32(leftKey); // Märkligt att de inte funkar om man har de i fel ordning :S

		}).Run(); // Run or Scedule thing. Here we use input thing so that needs to happen on the main thread, which Run will guarantee.
		
	}
}