using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour {

	public GameObject player;
	static Animator anim;

	public GameObject[] points;
	public NavMeshAgent agent;
	private int destPoint = 0;
	private bool isCoroutineExecuting = false;

	private float hp;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();

		points = GameObject.FindGameObjectsWithTag("PatrolPoint");

		StartCoroutine(GoToNextPoint (5));

		hp = 5;
	}

	IEnumerator GoToNextPoint(float time)
	{
		if (isCoroutineExecuting)
			yield break;

		isCoroutineExecuting = true;

		yield return new WaitForSeconds(time);

		// Set the agent to go to the currently selected destination.
		agent.destination = points[destPoint].transform.position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		destPoint = (destPoint + 1) % points.Length;

		isCoroutineExecuting = false;
	}

	void Update () {
		Vector3 direction = player.transform.position - transform.position;
		float angle = Vector3.Angle (direction, transform.forward);

		if (!agent.pathPending && agent.remainingDistance < 0.5f) {
			StartCoroutine(GoToNextPoint (5));
		}

		if (Vector3.Distance (player.transform.position, transform.position) < 20 && angle < 60) {
			direction.y = 0;
			agent.speed = 5;
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
			agent.speed = 3;
			agent.acceleration = 8;
			anim.SetBool("isWalking",true);
			anim.SetBool("isChasing",false);
			anim.SetBool("isAttacking",false);
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
