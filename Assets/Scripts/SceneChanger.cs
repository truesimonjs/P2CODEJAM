using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadSceneByName(string EmailGameStart)
    {
        SceneManager.LoadScene(EmailGameStart);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(1);
    }
}
