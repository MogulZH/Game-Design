using UnityEngine;
using System.Collections;
using UnityEngine.UI; /* Required for controlling Canvas UI system */


public class RayShooter : MonoBehaviour {
	private Camera _camera;
	[SerializeField] private GameObject reticle;
	[SerializeField] private GameObject fireballPrefab;
	[SerializeField] public AudioClip crash;
	[SerializeField] private AudioSource source;
	private GameObject _fireball;

	void Start() {
		source = GetComponent<AudioSource>();
		_camera = GetComponent<Camera>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		reticle = GameObject.Find("Reticle");
		reticle.GetComponent<Text>().text = "+";
		reticle.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
		reticle.GetComponent<RectTransform>().position =
            new Vector3(_camera.pixelWidth / 2.0f,
                        _camera.pixelHeight / 2.0f,
                        0.0f);
	}

    /** Deprecated in Unity 2018 
	void OnGUI() {
		int size = 12;
		float posX = _camera.pixelWidth/2 - size/4;
		float posY = _camera.pixelHeight/2 - size/2;
		GUI.Label(new Rect(posX, posY, size, size), "*");
	}
    **/
    

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			_fireball = Instantiate(fireballPrefab) as GameObject;
			_fireball.transform.position = transform.TransformPoint(Vector3.forward*5f);
			_fireball.transform.rotation = transform.rotation;
			source.PlayOneShot(crash);
		}
	}
}
	