using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System;
using UnityEngine.UI;
using System.Collections;


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