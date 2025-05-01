using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuTransition : MonoBehaviour
{

    [SerializeField] private string targetScene;

    
    public void TransitionScene()
    {
        SceneManager.LoadScene(targetScene);
    }

}
