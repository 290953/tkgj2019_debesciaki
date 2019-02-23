using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
	public Dialogue dialogue;

	//TODO: podpiac pod cos sensownego
	public void startDialogue(){
	
		FindObjectOfType<DialogueHandler>().startDialogue(dialogue);


	}
}
