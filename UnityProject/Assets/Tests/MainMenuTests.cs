using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuTests
{
    [Test]
    public void B1_LoadMainMenuScene()
    {
        string expectedSceneName = "MainMenuScene";

        EditorSceneManager.OpenScene("Assets/Scenes/" + expectedSceneName + ".unity");
        string actualSceneName = SceneManager.GetActiveScene().name;

        Assert.AreEqual(expectedSceneName, actualSceneName);
    }
}