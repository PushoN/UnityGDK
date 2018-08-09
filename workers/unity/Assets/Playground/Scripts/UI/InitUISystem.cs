using Generated.Playground;
using Improbable.Gdk.Core;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Playground
{
    [UpdateInGroup(typeof(SpatialOSUpdateGroup))]
    public class InitUISystem : ComponentSystem
    {
        private struct Data
        {
            public readonly int Length;
            public EntityArray Entities;
            [ReadOnly] public ComponentDataArray<SpatialOSLauncher> Launcher;
            [ReadOnly] public ComponentDataArray<SpatialOSScore> Score;
            [ReadOnly] public ComponentDataArray<Authoritative<SpatialOSPlayerInput>> PlayerInput;
            [ReadOnly] public ComponentArray<AuthoritiesChanged<SpatialOSPlayerInput>> PlayerInputAuthority;
        }

        [Inject] private Data data;

        protected override void OnUpdate()
        {
            for (var i = 0; i < data.Length; i++)
            {
                var ui = Resources.Load("Prefabs/UIGameObject");
                var inst = (GameObject) Object.Instantiate(ui, Vector3.zero, Quaternion.identity);
                var uiComponent = inst.GetComponent<UIComponent>();
                UIComponent.Main = uiComponent;
                uiComponent.TestText.text = $"Energy: {data.Launcher[i].EnergyLeft}";
                uiComponent.ScoreText.text = $"Score: {data.Score[i].Score}";
            }

            // Disable system after first run.
            Enabled = false;
        }
    }
}
