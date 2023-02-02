using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// honestly this entire class could be moved into MenuManager
public class SelectionMenu : MonoBehaviour
{
    public SelectionData data2;
    public GameObject selectionBox;

    // automatic getters and setters, read only outside class
    public bool inMenu { get; private set;}

    public void Start() 
    {
        inMenu = false;
        selectionBox.SetActive(false);
    }

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape)){
			Quit();
		}
    }	

    public void OpenMenu()
    {
        inMenu = true;
        selectionBox.SetActive(true);
        staticVariables.immobile = true;
    }

    public void StoreChoice(TextMeshProUGUI textMesh)
	{
        staticVariables.lastGuess = textMesh.text;
        staticVariables.guesses += 1;
        staticVariables.immobile = false;
        selectionBox.SetActive(false);
        StartCoroutine(TransitionScene());
	}

    public void Quit() {
        selectionBox.SetActive(false);
        staticVariables.immobile = false;
    }

    public void GoToCutscene()
	{
        inMenu = false;
        SceneManager.LoadScene("Accuse Cutscene");
	}

    public IEnumerator TransitionScene()
    {
        GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Accuse Cutscene");
    }
}
