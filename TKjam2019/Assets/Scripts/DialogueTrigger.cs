using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    private static DialogueTrigger instance;

    public static DialogueTrigger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<DialogueTrigger>();
            }
            return instance;
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
