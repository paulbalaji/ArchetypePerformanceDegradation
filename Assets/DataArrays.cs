using Unity.Collections;
using Unity.Entities;
using UnityEngine.UI;

namespace Playground
{
    [DisableAutoCreation]
    public class DataArrays : ComponentSystem
    {
        
        private struct Data
        {
            public readonly int Length;
            public ComponentDataArray<SomeCommonComponent> SomeComponent;
        }

        [Inject] private Data data;

        protected override void OnUpdate()
        {
            for (int i = 0; i < data.Length; ++i)
            {
                var someNumber = data.SomeComponent[i];
                ++someNumber.SomeNumber;
                data.SomeComponent[i] = someNumber;
            }
        }
    }
}
