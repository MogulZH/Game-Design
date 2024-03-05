using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System;
using System.Numerics;

public class Tombstone : MonoBehaviour {
    void Update(){
		transform.Rotate(0, 40f * Time.deltaTime, 0);
	}
}