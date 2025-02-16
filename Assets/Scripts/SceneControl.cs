using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public void loadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
