using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
	public string[] lines;
	public float speedText;
	public TMPro.TextMeshProUGUI dialogText;
    public GameObject dialogWin;
    public GameObject powersWin;

	int index;

	// Use this for initialization
	private void Start ()
    {
        powersWin.SetActive(false);

    	index = 0;
		StartDialog ();
	}

	private void StartDialog()
    {
		dialogText.text = string.Empty;
		StartCoroutine (TypeLine ());
	}
	
	IEnumerator TypeLine()
	{
		foreach (char c  in lines[index].ToCharArray()) {
			dialogText.text += c;
			yield return new WaitForSeconds (speedText);
		}
	}

	public void scipTextClick()
    {
		if (dialogText.text == lines [index]) {
			NextLines ();
		} else {
			StopAllCoroutines ();
			dialogText.text = lines [index];
		}
	}

	private void NextLines()
    {
		if (index < lines.Length - 1) {
			index++;
			StartDialog ();
		}
        else
        {
			index = 0;
			dialogWin.SetActive(false);
            powersWin.SetActive(true);
		}
	}
}
