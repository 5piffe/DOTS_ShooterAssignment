using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct Input_Data : IComponentData
{
    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode shoot;
}