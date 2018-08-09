using System;
using Generated.Playground;
using Improbable.Gdk.Core;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Random = System.Random;

namespace Playground
{
    [UpdateInGroup(typeof(SpatialOSUpdateGroup))]
    public class TriggerColorChangeSystem : ComponentSystem
    {
        private struct CubeColorData
        {
            public readonly int Length;
            [ReadOnly] public ComponentDataArray<EventSender<SpatialOSCubeColor>> EventSenders;
        }

        [Inject] private CubeColorData cubeColorData;

        private Array colorValues;

        protected override void OnCreateManager(int capacity)
        {
            base.OnCreateManager(capacity);

            colorValues = Enum.GetValues(typeof(Color));
        }

        private const float SendRateHz = 0.5f;
        private float timeSinceLastSend = 0.0f;

        protected override void OnUpdate()
        {
            // Send update at SendRateHz.
            timeSinceLastSend += Time.deltaTime;
            if (timeSinceLastSend < 1.0f / SendRateHz)
            {
                return;
            }

            timeSinceLastSend = 0.0f;

            var newColor = (Generated.Playground.Color) colorValues.GetValue(new Random().Next(colorValues.Length));

            for (var i = 0; i < cubeColorData.Length; i++)
            {
                var colorData = new Generated.Playground.ColorData
                {
                    Color = newColor
                };

                cubeColorData.EventSenders[i].SendChangeColorEvent(colorData);
            }
        }
    }
}
