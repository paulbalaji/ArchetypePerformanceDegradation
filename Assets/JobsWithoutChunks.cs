using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;

namespace Playground
{
    [DisableAutoCreation]
    [AlwaysUpdateSystem]
    public class JobsWithoutChunks : JobComponentSystem
    {
        private struct AddToSomeNumberJob : IJobProcessComponentData<SomeCommonComponent>
        {
            public void Execute(ref SomeCommonComponent data)
            {
                ++data.SomeNumber;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            inputDeps = new AddToSomeNumberJob().Schedule(this, inputDeps);
            return base.OnUpdate(inputDeps);
        }
    }
}
