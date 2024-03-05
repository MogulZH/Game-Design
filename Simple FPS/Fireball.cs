using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Diagnostics;

public class Fireball : MonoBehaviour {
	[SerializeField] private GameObject burstPrefab;
	private GameObject _burst;
	public float speed = 80.0f;
	public int damage = 1;

	void Update() {
		transform.Translate(0, 0, speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		PlayerCharacter player = other.GetComponent<PlayerCharacter>();
		ReactiveTarget enemy = other.GetComponent<ReactiveTarget>();	
		if (player != null) {
			player.Hurt(damage);
		}
		else if (enemy != null) {
			enemy.ReactToHit();
		}
		Destroy(this.gameObject);
		_burst = Instantiate(burstPrefab) as GameObject;
		_burst.transform.position = this.transform.position;
		_burst.GetComponent<ParticleSystem>().Play();
	}
}
