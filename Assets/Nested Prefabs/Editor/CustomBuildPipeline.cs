using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using VisualDesignCafe.Editor.Prefabs.Database;


public class CustomBuildPipeline
{

    /// <summary>
    /// Example of a custom build pipeline with Nested Prefabs.
    /// </summary>
    [MenuItem( "Window/Visual Design Cafe/Nested Prefabs/Build Example" )]
    public static void Build()
    {
        // Create a 'Builds' folder in the root project folder to export the build to.
        if( !Directory.Exists( "Builds" ) )
            Directory.CreateDirectory( "Builds" );

        // Call PreprocessBuild before starting the build to remove all nested prefab data from the project.
        PrefabDatabaseUtility.PreprocessBuild();
        {
            // Create a build.
            BuildPipeline.BuildPlayer(
                new string[]
                {
                    "Assets/test.unity"
                    },
                "builds/build.exe",
                BuildTarget.StandaloneWindows,
                BuildOptions.None );

            // Switch to Android and create another build
            BuildPipeline.BuildPlayer(
                new string[]
                {
                    "Assets/test.unity"
                    },
                "builds/build.apk",
                BuildTarget.Android,
                BuildOptions.None );
        }
        // Call PostprocessBuild after building to restore all nested prefab data.
        PrefabDatabaseUtility.PostprocessBuild();
    }
}
