using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Playground
{
    public class ArchetypeExplosionBehaviour : MonoBehaviour
    {
        private int TargetArchetypeCount = 0;
        private int ActualArchetypeCount = 0;
        
        public Text ArchetypeCount;
        public Text TestOutput;
        
        private EntityManager manager;
        private Entity entity;
        
        private int lastFrameCount;
        
        private DateTime timeOfNextUpdate;
        private DateTime timeOfLastUpdate;

        private StringBuilder finalOutput;

        private readonly Dictionary<int, Type> types = new Dictionary<int, Type>
        {
            {0, typeof(C0)},
            {1, typeof(C1)},
            {2, typeof(C2)},
            {3, typeof(C3)},
            {4, typeof(C4)},
            {5, typeof(C5)},
            {6, typeof(C6)},
            {7, typeof(C7)},
            {8, typeof(C8)},
            {9, typeof(C9)},
            {10, typeof(C10)},
            {11, typeof(C11)},
            {12, typeof(C12)},
            {13, typeof(C13)},
            {14, typeof(C14)},
            {15, typeof(C15)},
            {16, typeof(C16)},
            {17, typeof(C17)},
            {18, typeof(C18)},
            {19, typeof(C19)},
            {20, typeof(C20)},
            {21, typeof(C21)},
            {22, typeof(C22)},
            {23, typeof(C23)},
            {24, typeof(C24)},
            {25, typeof(C25)},
            {26, typeof(C26)},
            {27, typeof(C27)},
            {28, typeof(C28)},
            {29, typeof(C29)},
            {30, typeof(C30)},
            {31, typeof(C31)}
        };

        private void Start()
        {
            finalOutput = new StringBuilder();
            
            World world = new World("Silly world");
            manager = world.GetOrCreateManager<EntityManager>();

            Debug.Log($"API: ChunksWithGroups");
            finalOutput.AppendLine($"API: ChunksWithGroups");
            
            world.GetOrCreateManager<GroupsFromQueries>();

            entity = manager.CreateEntity(typeof(SomeCommonComponent));

            ScriptBehaviourUpdateOrder.UpdatePlayerLoop(World.AllWorlds.ToArray());
            
            lastFrameCount = Time.frameCount;
            
            UpdateTimes();
        }

        private void UpdateTimes()
        {
            timeOfLastUpdate = DateTime.Now;
            timeOfNextUpdate = timeOfLastUpdate.AddSeconds(30);
        }
        
        private void Update()
        {
            TestOutput.text = $"{manager.GetComponentData<SomeCommonComponent>(entity).SomeNumber}";
            
            if (DateTime.Now >= timeOfNextUpdate)
            {
                TargetArchetypeCount += 50000;
                
                string lineOut = $"{ActualArchetypeCount}, {GetFps()}";
                finalOutput.AppendLine(lineOut);
                Debug.Log(lineOut);
                
                if (TargetArchetypeCount > 1000000)
                {
                    Debug.Log(finalOutput.ToString());
                    System.IO.File.WriteAllText($"ChunksWithGroups.txt", finalOutput.ToString());
                    Application.Quit();
                }
            }
            
            // Maximally inefficient archetype creation extravaganza
            if (ActualArchetypeCount < TargetArchetypeCount)
            {
                while (ActualArchetypeCount < TargetArchetypeCount)
                {
                    ++ActualArchetypeCount;
                
                    ArchetypeCount.text = $"{ActualArchetypeCount} of {TargetArchetypeCount} Archetypes";
                
                    var entity = manager.CreateEntity(typeof(SomeCommonComponent));
                
                    for (int j = 0; j < 20; ++j)
                    {
                        if (((ActualArchetypeCount >> j) & 1) == 1)
                        {
                            manager.AddComponent(entity, types[j]);
                        }
                    }

                    manager.DestroyEntity(entity);
                }

                GetFps();
                UpdateTimes();
            }
        }
        
        private double GetFps()
        {
            var frameCount = Time.frameCount - lastFrameCount;
            lastFrameCount = Time.frameCount;
            return frameCount / (DateTime.Now - timeOfLastUpdate).TotalSeconds;
        }
    }
}
