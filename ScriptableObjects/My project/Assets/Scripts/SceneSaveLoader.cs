using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SceneSaveLoader : MonoBehaviour
{
    public static void SaveScene(string savePath)
    {
        // Convert the current scene to JSON string
        Scene currentScene = SceneManager.GetActiveScene();
        SceneData sceneData = new SceneData(currentScene);
        string jsonData = JsonUtility.ToJson(sceneData);

        // Save the JSON string to a file
        File.WriteAllText(savePath, jsonData);
    }

    public static SceneData LoadScene(string savePath)
    {
        if (File.Exists(savePath))
        {
            // Read the JSON string from the file
            string jsonData = File.ReadAllText(savePath);

            // Convert the JSON string to SceneData object
            SceneData sceneData = JsonUtility.FromJson<SceneData>(jsonData);
            return sceneData;
        }

        return null;
    }
}
[System.Serializable]
public class SceneData
{
    public string sceneName;

    public SceneData(Scene scene)
    {
        sceneName = scene.name;
    }
}