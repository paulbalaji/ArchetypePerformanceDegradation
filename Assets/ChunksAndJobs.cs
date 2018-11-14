using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine.UI;

namespace Playground
{
    [DisableAutoCreation]
    public class ChunksAndJobs : JobComponentSystem
    {
        [BurstCompile]
        private struct CommonComponentJob : IJobChunk
        {
            public ArchetypeChunkComponentType<SomeCommonComponent> someCommonComponentType;

            public void Execute(ArchetypeChunk chunk, int chunkIndex)
            {
                var component = chunk.GetNativeArray(someCommonComponentType);
                var c = component[chunkIndex];
                ++c.SomeNumber;
                component[chunkIndex] = c;
            }
        }

        private EntityArchetypeQuery query = new EntityArchetypeQuery
        {
            All = new ComponentType[] {typeof(SomeCommonComponent)},
            Any = Array.Empty<ComponentType>(),
            None = Array.Empty<ComponentType>()
        };

        private ComponentGroup group;

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            group = GetComponentGroup(query);

            var job = new CommonComponentJob
            {
                someCommonComponentType = GetArchetypeChunkComponentType<SomeCommonComponent>(false)
            };
            inputDeps = job.Schedule(group, inputDeps);

            return base.OnUpdate(inputDeps);
        }
    }
}
