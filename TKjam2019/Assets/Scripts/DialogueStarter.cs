using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
	public Dialogue dialogue;

	private static DialogueStarter instance;

	public static DialogueStarter Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<DialogueStarter>();
			}
			return instance;
		}
	}

	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueHandler>().startDialogue(dialogue);
	}
}
