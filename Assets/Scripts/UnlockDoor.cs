using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour {

	public GameObject player;
	PickUpKey pickupKey;
	public Rigidbody rb;


	// Use this for initialization
	void Start () {
		pickupKey = player.GetComponent<PickUpKey>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire1") && pickupKey.hasKey == true) {
			rb.isKinematic = false;
			pickupKey.hasKey = false;
		}
	}
}
