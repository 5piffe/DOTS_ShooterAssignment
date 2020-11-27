﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct HealthData : IComponentData
{
	public int currentValue;
	public bool isDead;
}
