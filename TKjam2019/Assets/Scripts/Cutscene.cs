using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DialogueTrigger.Instance.TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
