using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public string sceneToLoad = "SampleScene Liza"; // <-- Put your main scene name here

    public void GoBack()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
