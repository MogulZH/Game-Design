using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System;
using System.Numerics;

public class ReactiveTarget : MonoBehaviour {

	bool isRotating = false;
	float rotationAmount = 0f; 
	[SerializeField] private GameObject tombstonePrefab;
	private GameObject tombstone;

	public void ReactToHit() {
		WanderingAI behavior = GetComponent<WanderingAI>();
		if (behavior != null) {
			behavior.SetAlive(false);
		}
		StartCoroutine(Die());
	}

    private IEnumerator Die(){
		isRotating = true;

        yield return new WaitForSeconds(1.5f);
		
		isRotating = false;

		if(tombstonePrefab != null){
			tombstone = Instantiate(tombstonePrefab) as GameObject;
			tombstone.transform.position = transform.position;
		}

		Destroy(this.gameObject);
    }

	private void Update(){
		if(isRotating == true){
			float rotationPerFrame = 90f * Time.deltaTime;
			if(rotationAmount + rotationPerFrame > 90f){
				rotationPerFrame = 90f - rotationAmount;
				isRotating = false;
			}
			this.transform.Rotate(rotationPerFrame, 0, 0);
			rotationAmount += rotationPerFrame;
		}
	}
}
