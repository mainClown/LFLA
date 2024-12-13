using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

// public class MainMenuTest
// {
//     [UnityTest]
//     public IEnumerator Test_OpenNonExistentScene()
//     {
//         // Создаем объект MainMenu для тестирования
//         GameObject mainMenuObject = new GameObject();
//         MainMenu mainMenu = mainMenuObject.AddComponent<MainMenu>();

//         // Перехватываем вызов SceneManager.LoadScene
//         LogAssert.Expect(LogType.Error, "Scene 'NonExistentScene' couldn't be loaded because it has not been added to the build settings or the AssetBundle has not been loaded.");

//         // Вызываем метод StartNewGame с недоступной сценой
//         SceneManager.LoadScene("NonExistentScene");

//         // Ждем один кадр, чтобы убедиться, что ошибка была записана
//         yield return null;

//         // Проверяем, что приложение продолжает работать
//         Assert.IsTrue(Application.isPlaying);
//     }
// }

public class MainMenuTest
{
    [Test]
    public void B4a_Test_ExitButton_HandlesError()
    {
        // Создаем объект MainMenu для тестирования
        GameObject mainMenuObject = new GameObject();
        MainMenu mainMenu = mainMenuObject.AddComponent<MainMenu>();

        // Перехватываем вывод в лог
        LogAssert.Expect(LogType.Error, "Error during quit: Cannot quit: active processes detected");

        // Вызываем метод Exit
        mainMenu.Exit();

        // Проверяем, что приложение продолжает работать
        Assert.IsTrue(Application.isPlaying);
    }
}