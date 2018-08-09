using Generated.Playground;
using Improbable.Gdk.Core;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Playground
{
    [UpdateInGroup(typeof(SpatialOSUpdateGroup))]
    internal class LocalPlayerInputSync : ComponentSystem
    {
        private struct PlayerInputData
        {
            public readonly int Length;
            public ComponentDataArray<SpatialOSPlayerInput> PlayerInput;
            [ReadOnly] public ComponentDataArray<CameraTransform> CameraTransform;
            [ReadOnly] public ComponentDataArray<Authoritative<SpatialOSPlayerInput>> PlayerInputAuthority;
        }

        [Inject] private PlayerInputData playerInputData;

        protected override void OnUpdate()
        {
            for (var i = 0; i < playerInputData.Length; i++)
            {
                var cameraTransform = playerInputData.CameraTransform[i];
                var forward = cameraTransform.Rotation * Vector3.up;
                var right = cameraTransform.Rotation * Vector3.right;
                var input = Input.GetAxisRaw("Horizontal") * right + Input.GetAxisRaw("Vertical") * forward;

                var newPlayerInput = new SpatialOSPlayerInput
                {
                    Horizontal = input.x,
                    Vertical = input.z,
                    Running = Input.GetKey(KeyCode.LeftShift)
                };

                playerInputData.PlayerInput[i] = newPlayerInput;
            }
        }
    }
}
