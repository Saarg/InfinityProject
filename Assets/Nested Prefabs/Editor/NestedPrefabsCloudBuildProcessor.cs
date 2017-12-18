#if UNITY_CLOUD_BUILD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualDesignCafe.Editor.Prefabs.Database;

public static class NestedPrefabsCloudBuildProcessor
{
    /// <summary>
    /// Preprocesses the project for a build on Unity Cloud Build.
    /// Will remove all nested prefab data from the project, without creating any backup or restore point.
    /// Set this method as Pre-Export Method in the Advanced Options of your Cloud Build config: NestedPrefabsCloudBuildProcessor.PreExportMethod
    /// </summary>
    public static void PreExportMethod()
    {
        PrefabDatabaseUtility.PreprocessCloudBuild();
    }
}
#endif