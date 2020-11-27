using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct Movement_Data : IComponentData
{
    [System.NonSerialized] public float3 moveDirection;
    public float moveSpeed;
}