using UnityEditor;
using VisualDesignCafe.NestedPrefabs;

public class NestedPrefabsPreferences
{
    [InitializeOnLoadMethod]
    public static void Initialize()
    {

        // Nested Prefabs automatically reports exceptions by default. 
        // These exceptions are sent anonymously and do not contain any private data (paths and data outside of the Nested Prefabs namespace are stripped from the stacktrace).
        // If you want to disable sending of this analytics data you can set the following setting to 'false'.
        NestedPrefabsConfig.ENABLE_ANALYTICS = false;

        // Nested Prefabs automatically checks for updates at editor startup. 
        // It is recommended to check for updates and keep your installed version of Nested Prefabs up to date in order to receive the latest bug fixes and features.
        // If you do not want to check for updates you can set the following setting to 'false'.
        NestedPrefabsConfig.CHECK_FOR_UPDATES = false;

    }
}
