using UnityEngine;

public class NPCTalkTrigger : MonoBehaviour
{
    private bool isPlayerInRange = false;
    [SerializeField] private DialogueTrigger dialogueTrigger;
    private bool dialogueActive = false;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (!dialogueActive)
            {
                dialogueTrigger.TriggerDialogue();
                dialogueActive = true;
            }
            else
            {
                FindFirstObjectByType<DialogueManager>().DisplayNextSentence();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Press Space to talk.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueActive = false;
        }
    }
}
