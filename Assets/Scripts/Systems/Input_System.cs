using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;
using System;

public class Input_System : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Movement_Data moveData, in Input_Data inputData) =>
        {
            bool up = Input.GetKey(inputData.moveUp);
            bool down = Input.GetKey(inputData.moveDown);
            bool left = Input.GetKey(inputData.moveLeft);
            bool right = Input.GetKey(inputData.moveRight);
            bool shoot = Input.GetKey(inputData.shoot);

            moveData.moveDirection.y = Convert.ToInt32(up);
            moveData.moveDirection.y -= Convert.ToInt32(down);
            moveData.moveDirection.x = Convert.ToInt32(right);
            moveData.moveDirection.x -= Convert.ToInt32(left);
            moveData.shooting = shoot;

        }).Run();
    }
}