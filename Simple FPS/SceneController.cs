using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;

public class SceneController : MonoBehaviour {
	[SerializeField] private GameObject enemyPrefab;
	private GameObject enemy;  
	private List<GameObject> enemies = new List<GameObject>();
	 
	private int bodyCount = 1;

    void Update() {
		bool enemyDestroyed = enemies.RemoveAll(item => item == null) > 0;
		if(enemyDestroyed){
			bodyCount++;
		}
		enemies.RemoveAll(item => item == null);
		while (enemies.Count < bodyCount && enemies.Count < 10){
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.transform.position = new Vector3(Random.Range(1, 10), 1, Random.Range(1, 10));
			float angle = Random.Range(0, 360);
			enemy.transform.Rotate(0, angle, 0);
			enemies.Add(enemy);
		}
	}
}
