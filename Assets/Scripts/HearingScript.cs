using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingScript : MonoBehaviour {

	public float range;
	UnlockDoor doorNoise;
	public GameObject item;
	public float dist;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		dist = Vector3.Distance(item.transform.position, transform.position);
		//Debug.Log (dist);
	}
}
