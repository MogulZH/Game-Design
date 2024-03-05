using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class SceneTwoController : MonoBehaviour {
	[SerializeField] private GameObject congratsText;
	[SerializeField] private Button startButton;
	[SerializeField] private Button endButton;

	void Start(){
		startButton.onClick.AddListener(StartGame);
        endButton.onClick.AddListener(EndGame);
		StartCoroutine(Handler());
	}

	private IEnumerator Handler(){
		congratsText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);

        // Hide the congratulations text and show the buttons
        congratsText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
        endButton.gameObject.SetActive(true);
	}
	public void StartGame() {
		SceneManager.LoadScene(0);
	}

	public void EndGame(){
		Application.Quit();
	}
}
