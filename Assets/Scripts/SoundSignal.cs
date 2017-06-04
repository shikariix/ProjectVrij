using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSignal : MonoBehaviour {

	public GameObject enemy;
	HearingScript enemyHearing;

	// Use this for initialization
	void Start () {
		enemyHearing = enemy.GetComponent<HearingScript> ();
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.name != "Monster") {
			enemyHearing.HearNoise (gameObject);
		}
	}
}
