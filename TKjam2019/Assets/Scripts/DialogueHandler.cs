using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour {

	public Text speakerText;
	public Text sentenceText;

	private Queue<string> sentences;
	private Queue<string> speakers;
    
	protected DialogueHandler() { }

	// Start is called before the first frame update
    void Start() {
		sentences= new Queue<string>();
		speakers= new Queue<string>();
    }

	public void startDialogue(Dialogue dialogue){

		sentences.Clear();
		speakers.Clear();

		foreach(string sentence in dialogue.sentences){
			sentences.Enqueue(sentence);
		}

		foreach(string speaker in dialogue.speakers){
			speakers.Enqueue(speaker);
		}

		nextSentence();
	}

	public void nextSentence(){

		if(sentences.Count == 0){
			//TODO: zamykanie systemu dialogowego
			return;
		}

		string speaker= speakers.Dequeue();
		speakerText.text= speaker;


		string sentence= sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(typeSentence(sentence));
	}

	IEnumerator typeSentence(string sentence){
	
		sentenceText.text= "";

		foreach(char letter in sentence.ToCharArray()){
		
			sentenceText.text += letter;
			yield return null;
		}
	}
}
