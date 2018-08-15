using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Improbable.Gdk.Core.GameObjectRepresentation;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Playground.Editor
{
    [InitializeOnLoad]
    public class PrefabPreprocessor : IPreprocessBuildWithReport
    {
        // Needed for IPreprocessBuildWithReport
        public int callbackOrder => 0;

        static PrefabPreprocessor()
        {
            EditorApplication.playModeStateChanged += PlaymodeStateChanged;
        }

        void IPreprocessBuildWithReport.OnPreprocessBuild(BuildReport report)
        {
            PreprocessPrefabs();
        }

        private static void PlaymodeStateChanged(PlayModeStateChange playModeStateChange)
        {
            if (playModeStateChange == PlayModeStateChange.ExitingEditMode)
            {
                PreprocessPrefabs();
            }
        }

        private static void PreprocessPrefabs()
        {
            if (!DoPrefabsNeedFixing())
            {
                return;
            }

            FixPrefabs();

            AssetDatabase.SaveAssets();
        }

        private static bool DoPrefabsNeedFixing()
        {
            return AssetDatabase.FindAssets("t:Prefab")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<GameObject>)
                .Any(PrefabNeedsFixing);
        }

        private static bool PrefabNeedsFixing(GameObject prefabObject)
        {
            return prefabObject != null
                && prefabObject.GetComponents<MonoBehaviour>()
                    .Any(BehaviourNeedsFixing);
        }

        private static void FixPrefabs()
        {
            var prefabsToFix = new List<GameObject>();
            var monoBehavioursToDisable = new List<MonoBehaviour>();

            var allPrefabObjectsInProject = AssetDatabase.FindAssets("t:Prefab")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<GameObject>)
                .Where(prefabObject => prefabObject != null);

            foreach (var prefabObject in allPrefabObjectsInProject)
            {
                var prefabNeedsFixing = false;

                foreach (var monoBehaviour in prefabObject
                    .GetComponents<MonoBehaviour>()
                    .Where(BehaviourNeedsFixing))
                {
                    if (!prefabNeedsFixing)
                    {
                        prefabNeedsFixing = true;
                        prefabsToFix.Add(prefabObject);
                    }

                    monoBehavioursToDisable.Add(monoBehaviour);
                }
            }

            Undo.RecordObjects(monoBehavioursToDisable.Cast<Object>().ToArray(),
                "Disable monobehaviours with [Require] fields on prefabs");

            foreach (var monoBehaviour in monoBehavioursToDisable)
            {
                monoBehaviour.enabled = false;
            }

            prefabsToFix.ForEach(EditorUtility.SetDirty);
        }

        private static bool IsBehaviourEnabledInEditor(Object obj)
        {
            return !ReferenceEquals(obj, null)
                && EditorUtility.GetObjectEnabled(obj) == 1;
        }

        private static bool RequiresReadersOrWriters(Type targetType)
        {
            return targetType
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Any(field => Attribute.IsDefined(field, typeof(RequireAttribute), false));
        }

        private static bool BehaviourNeedsFixing(MonoBehaviour monoBehaviour)
        {
            return IsBehaviourEnabledInEditor(monoBehaviour) && RequiresReadersOrWriters(monoBehaviour.GetType());
        }
    }
}
