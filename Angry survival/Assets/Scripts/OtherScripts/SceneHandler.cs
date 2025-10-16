using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Scener = UnityEngine.SceneManagement.SceneManager;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void LoadScene(int sceneIndex) => Scener.LoadScene(sceneIndex);
    public void QuitGame() => Application.Quit();
    public void ReloadScene() => Scener.LoadScene(Scener.GetActiveScene().buildIndex);
}
