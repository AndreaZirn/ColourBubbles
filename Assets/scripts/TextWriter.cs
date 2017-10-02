// -----------------------------------------------------------------------------------------------
//  Creation date :  13.06.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer, Patrick Del Conte
// -----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Display the instruction scene text delayed
/// </summary>
public class TextWriter : MonoBehaviour {

	public float letterPause = 0.08f;
	string message;
	public Text textComp;
	public Button button;
	public GameObject neil;
	public GameObject laberlaber;
	public GameObject nextbutton;

	int counter = 0;

	void Awake(){
		button.gameObject.SetActive(false);
	}

	void Start () {
		StartCoroutine (StartText());
	}

	IEnumerator StartText(){
		laberlaber.SetActive (false);
		message = textComp.text;
		textComp.text = "";
		button.gameObject.SetActive (true);
		yield return new WaitForSeconds(6);
		laberlaber.SetActive (true);
		yield return StartCoroutine (TypeText ());
		button.gameObject.SetActive(true);
	}

	IEnumerator TypeText () {
		button.enabled = false;
		foreach (char letter in message.ToCharArray()) {
			textComp.text += letter;
			yield return new WaitForSeconds (letterPause);
		}
		button.enabled = true;
	}
		
	public void writeText (){
		counter++;
		if (counter == 1) {
			textComp.text = "";
			message = "Die Schwerkraft macht das Zeichnen richtig schwierig, da alle Farben frei in der Luft herumfliegen. Bitte hilf mir die Farben auf die Farbpalette zu bringen.";
			StartCoroutine (TypeText ());
		} else if (counter == 2) {
			textComp.text = "";
			message = "Da ich im Raumschiff nur Platz für 3 Farben habe, müssen die anderen Farben gemischt werden durch Zusammenstossen.";
			StartCoroutine (TypeText ());
		} else if (counter > 2) {
			SceneManager.LoadScene ("LevelSelection");
		}
	}
}
