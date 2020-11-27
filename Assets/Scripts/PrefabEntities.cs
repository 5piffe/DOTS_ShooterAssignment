using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class PrefabEntities : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
	public static Entity prefabEntity;
	public GameObject prefabGameObject;

	public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
	{
		Entity prefabEntity = conversionSystem.GetPrimaryEntity(prefabGameObject);
		PrefabEntities.prefabEntity = prefabEntity;
	}

	public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
	{
		referencedPrefabs.Add(prefabGameObject);
	}
}
