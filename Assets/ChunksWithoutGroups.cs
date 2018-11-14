using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Playground
{
    [DisableAutoCreation]
    [AlwaysUpdateSystem]
    public class ChunksWithoutGroups : ComponentSystem
    {
        private EntityArchetypeQuery query = new EntityArchetypeQuery
        {
            All = new ComponentType[] {typeof(SomeCommonComponent)},
            Any = Array.Empty<ComponentType>(),
            None = Array.Empty<ComponentType>()
        };
        
        protected override void OnUpdate()
        {
            var commonComponentType = GetArchetypeChunkComponentType<SomeCommonComponent>();
            var chunkArray = EntityManager.CreateArchetypeChunkArray(query, Allocator.TempJob);

            foreach (var chunk in chunkArray)
            {
                var commonComponents = chunk.GetNativeArray(commonComponentType);
                for (int i = 0; i < commonComponents.Length; ++i)
                {
                    var someNumber = commonComponents[i];
                    ++someNumber.SomeNumber;
                    commonComponents[i] = someNumber;
                }
            }
            chunkArray.Dispose();
        }
    }
}
