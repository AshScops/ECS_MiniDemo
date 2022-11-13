using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class Test : MonoBehaviour
{
    public GameObject m_Prefab;

    void Start()
    {
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var entityFromPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(m_Prefab, settings);
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var instance = entityManager.Instantiate(entityFromPrefab);
    }

}
