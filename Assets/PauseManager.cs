using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    float previousTimeScale = 1;
    public TextMeshProUGUI pauseLabel;
    public static bool isPaused;

    public Animator anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !DialogueManager.IsDialogueActive) 
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (Time.timeScale > 0)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            pauseLabel.enabled = true;
            anim.SetBool("isPaused", true);
        }
        else if (Time.timeScale == 0) 
        {
            Time.timeScale = previousTimeScale;
            pauseLabel.enabled = false;
            anim.SetBool("isPaused", false);
        }
    }
}
