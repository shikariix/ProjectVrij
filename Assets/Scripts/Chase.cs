using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour {

	public GameObject player;
	public GameObject destination;
	public GameObject newDestination;
	static Animator anim;
	public NavMeshAgent agent;

	private float hp;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		agent.SetDestination (destination.transform.position);
		hp = 5;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = player.transform.position - transform.position;
		float angle = Vector3.Angle (direction, transform.forward);

		if (Vector3.Distance (player.transform.position, transform.position) < 20 && angle < 60) {
			direction.y = 0;
			agent.SetDestination (player.transform.position);
			agent.speed = 3;
			agent.acceleration = 8;

			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), 0.1f);

			anim.SetBool("isWalking",false);
			if (direction.magnitude > 5) {
				anim.SetBool ("isChasing", true);
				anim.SetBool("isAttacking",false);
			} else {
				agent.speed = 15;
				agent.acceleration = 14;
				anim.SetBool("isAttacking",true);
				anim.SetBool("isChasing",false);
			}
		} else {
			agent.speed = 1;
			agent.acceleration = 8;
			anim.SetBool("isWalking",true);
			anim.SetBool("isChasing",false);
			anim.SetBool("isAttacking",false);
		}

		if (transform.position == destination.transform.position) {
			agent.SetDestination (newDestination.transform.position);
		}

		if (hp <= 0) {
			//player dead
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject == player) {
			hp -= 1;
		}
	}
}
