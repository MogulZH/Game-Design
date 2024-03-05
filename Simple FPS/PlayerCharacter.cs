using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour {
	private int _health;
	[SerializeField] private Text healthText;
	[SerializeField] private GameObject gameOverTextObject;
	[SerializeField] private AudioSource BGMsource;

	void Start() {
		_health = 2;
		UpdateHealthDisplay();
		gameOverTextObject.SetActive(false);
		BGMsource = GetComponent<AudioSource>();
		BGMsource.volume = 0.2f;
		BGMsource.Play();
	}

	public void Hurt(int damage) {
		_health -= damage;
		Debug.Log("Health: " + _health);
		UpdateHealthDisplay();
		if(_health <= 0){
			GameOver();
		}
	}

	void UpdateHealthDisplay(){
        healthText.text = "Health: ";
        for (int i = 0; i < _health; i++){
            healthText.text += "* ";
        }
    }

	private void GameOver(){
		Debug.Log("Game Over!");
		gameOverTextObject.SetActive(true);
	}
}
