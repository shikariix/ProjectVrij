using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : MonoBehaviour {

	private Transform aim;
	public bool hasKey;

	public GameObject key;
	public float pickupRange;
	public Camera cam;

	public GameObject item;
	public Transform heldPos;
	private bool holdItem;
	public Rigidbody rbItem;

	// Use this for initialization
	void Start () {
		aim = GameObject.Find ("FirstPersonCharacter").transform;
		hasKey = false;
		holdItem = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp ("Fire1")) {

			Vector3 rayOrigin = cam.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 0.0f));

			RaycastHit hit;

			if (Physics.Raycast (rayOrigin, cam.transform.forward, out hit, pickupRange)) {
				if (hit.collider.gameObject == key) {
					key.SetActive (false);
					hasKey = true;
				} else if (hit.collider.gameObject == item && holdItem == false) {
					holdItem = true;
				}
			} 
		}

		if (holdItem == true) {
			item.transform.position = heldPos.position;
			item.transform.rotation = heldPos.rotation;
			rbItem.useGravity = false;
			rbItem.velocity = new Vector3 (0, 0, 0);
			if (Input.GetButtonDown ("Fire1")) {
				rbItem.AddForce(aim.transform.forward * 500);
				rbItem.useGravity = true;
				holdItem = false;
			}
		}
	}
}
