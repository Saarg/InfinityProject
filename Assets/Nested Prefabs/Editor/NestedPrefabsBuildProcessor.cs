// The build processor will remove all internal nested prefab components from the project before a build, and restore them again after.
// This increases performance in the build as the internal components have a small overhead. However it will increase build time and requires all prefabs to be reimported.
// Any modifications made to prefabs while in build mode will be discarded when post processing the build.
// It is recommended to only use the processor for a release build.
// For more information visit: https://www.visualdesigncafe.com/products/nestedprefabs/documentation/building.php

// Uncomment the following line to use the automatic build processor. 
//#define USE_BUILD_PROCESSOR

#if UNITY_5_6 && !UNITY_CLOUD_BUILD && USE_BUILD_PROCESSOR
using UnityEditor;
using UnityEditor.Build;
using VisualDesignCafe.Editor.Prefabs.Database;

public class NestedPrefabsBuildProcessor : IPreprocessBuild, IPostprocessBuild
{

    /// <summary>
    /// The order in which the processors are called. Lower numbers are called first.
    /// </summary>
    public int callbackOrder
    {
        get
        {
            return int.MinValue;
        }
    }

    /// <summary>
    /// Called by Unity after building.
    /// Restores all nested prefab data.
    /// </summary>
    public void OnPostprocessBuild( BuildTarget target, string path )
    {
        PrefabDatabaseUtility.PostprocessBuild();
    }

    /// <summary>
    /// Called by Unity before a build is started.
    /// Prepares the project for building by (temporarily) removing all nested prefab data.
    /// </summary>
    public void OnPreprocessBuild( BuildTarget target, string path )
    {
        PrefabDatabaseUtility.PreprocessBuild();
    }

}
#endif