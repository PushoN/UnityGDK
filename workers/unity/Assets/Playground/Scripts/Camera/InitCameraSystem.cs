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
    [UpdateInGroup(typeof(SpatialOSUpdateGroup))]
    public class InitCameraSystem : ComponentSystem
    {
        private struct Data
        {
            public readonly int Length;
            public EntityArray Entites;
            [ReadOnly] public ComponentDataArray<Authoritative<SpatialOSPlayerInput>> PlayerInput;
            [ReadOnly] public ComponentDataArray<AuthorityChanges<SpatialOSPlayerInput>> PlayerInputAuthority;
        }

        [Inject] private Data data;

        protected override void OnUpdate()
        {
            for (var i = 0; i < data.Length; i++)
            {
                var entity = data.Entites[i];
                PostUpdateCommands.AddComponent(entity, CameraComponentDefaults.Input);
                PostUpdateCommands.AddComponent(entity, CameraComponentDefaults.Transform);

                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
