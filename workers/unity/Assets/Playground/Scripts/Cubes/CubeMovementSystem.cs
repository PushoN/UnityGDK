using Generated.Improbable;
using Generated.Playground;
using Improbable.Gdk.Core;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

#region Diagnostic control

#pragma warning disable 649
// ReSharper disable UnassignedReadonlyField
// ReSharper disable UnusedMember.Global

#endregion

namespace Playground
{
    [UpdateBefore(typeof(UnityEngine.Experimental.PlayerLoop.FixedUpdate))]
    internal class CubeMovementSystem : ComponentSystem
    {
        private struct Data
        {
            public readonly int Length;
            public ComponentArray<Rigidbody> Rigidbody;
            public ComponentDataArray<SpatialOSCubeTargetVelocity> Cube;
            [ReadOnly] public ComponentDataArray<Authoritative<SpatialOSCubeTargetVelocity>> DenoteAuthority;
        }

        [Inject] private Data data;

        private Worker worker;

        protected override void OnCreateManager(int capacity)
        {
            base.OnCreateManager(capacity);

            worker = Worker.GetWorkerFromWorld(World);
        }

        protected override void OnUpdate()
        {
            for (var i = 0; i < data.Length; i++)
            {
                var rigidbodyComponent = data.Rigidbody[i];
                var cubeComponent = data.Cube[i];

                if (cubeComponent.TargetVelocity.X > 0 && rigidbodyComponent.position.x - worker.Origin.x > 10)
                {
                    cubeComponent.TargetVelocity = new Vector3f { X = -2.0f };
                    data.Cube[i] = cubeComponent;
                }
                else if (cubeComponent.TargetVelocity.X < 0 && rigidbodyComponent.position.x - worker.Origin.x < -10)
                {
                    cubeComponent.TargetVelocity = new Vector3f { X = 2.0f };
                    data.Cube[i] = cubeComponent;
                }

                var velocity = new Vector3(cubeComponent.TargetVelocity.X, cubeComponent.TargetVelocity.Y, cubeComponent.TargetVelocity.Z);
                rigidbodyComponent.MovePosition(rigidbodyComponent.position + Time.fixedDeltaTime * velocity);
            }
        }
    }
}
