using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEditor;

public class MemoryCard : MonoBehaviour {
	[SerializeField] private GameObject cardBack;
	[SerializeField] private SceneController controller;

	private int _id;
	public int id {
		get {return _id;}
	}

	public void SetCard(int id, Sprite image) {
		_id = id;
		GetComponent<SpriteRenderer>().sprite = image;
	}

	public void OnMouseDown() {
		if (!EventSystem.current.IsPointerOverGameObject() &&
			cardBack.activeSelf && controller.canReveal) {
			cardBack.SetActive(false);
			controller.CardRevealed(this);
		}
	}

	public void Unreveal() {
		cardBack.SetActive(true);
	}

	public IEnumerator Shake(GameObject card) {
    Vector3 originalPosition = card.transform.position;
    float duration = 0.5f; // Duration of the shake
    float magnitude = 0.1f; // Magnitude of the shake

    float elapsed = 0.0f;
    while (elapsed < duration) {
        float x = originalPosition.x + Random.Range(-1f, 1f) * magnitude;
        float y = originalPosition.y + Random.Range(-1f, 1f) * magnitude;

        card.transform.position = new Vector3(x, y, originalPosition.z);
        elapsed += Time.deltaTime;

        yield return null; // Wait until the next frame
    }

    card.transform.position = originalPosition; // Reset to the original position
}
}
