using UnityEngine;
using TMPro;

public class QTEStoryController : MonoBehaviour
{
    public TMP_Text dialogueText;
    public QTEManager qteManager;
    [SerializeField] private string targetScene;

    [TextArea(2, 5)]
    public string[] dialogueBeforeEachQTE;

    [TextArea(2, 5)]
    public string finalDialogue = "You did it! You've won!";

    [Header("Timing")]
    public float qteDelay = 2f; // Adjustable delay between dialogue and QTE

    private int currentStep = 0;

    void Start()
    {
        qteManager.OnQTESuccess += OnQTESuccess;
        qteManager.OnQTEFailure += OnQTEFailure;

        qteManager.HideQTE();
        ShowDialogue();
    }

    void ShowDialogue()
    {
        dialogueText.gameObject.SetActive(true);
        qteManager.HideQTE();

        if (currentStep < dialogueBeforeEachQTE.Length)
        {
            dialogueText.text = dialogueBeforeEachQTE[currentStep];
            Invoke(nameof(StartQTEAfterDelay), qteDelay); // Use adjustable delay
        }
        else
        {
            dialogueText.text = finalDialogue;
            Invoke(nameof(Win), 3f);
        }
    }

    void StartQTEAfterDelay()
    {
        dialogueText.gameObject.SetActive(false);
        qteManager.ShowQTE();
        qteManager.StartQTE();
    }

    void OnQTESuccess()
    {
        currentStep++;
        Invoke(nameof(ShowDialogue), 2f);
    }

    void OnQTEFailure()
    {
        dialogueText.text = "<color=red>You failed the QTE. Restarting...</color>";
        dialogueText.gameObject.SetActive(true);
        qteManager.HideQTE();

        currentStep = 0;
        Invoke(nameof(ShowDialogue), 3f);
    }

    [System.Obsolete]
    void Win()
    {
        dialogueText.text = "<color=green>You survived!</color>";
        SceneTransitionManager.Instance.TransitionToScene(targetScene);
        // TODO: Trigger win condition logic
    }
}
