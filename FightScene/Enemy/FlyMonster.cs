using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(enemy))]
public class FlyMonster : MonoBehaviour {

	private enemy enemy;
	private Transform Destination;
	private Animator MoveAnimate;
	public float StartSpeed;
	public float speed;
	public bool isOwl;

	// Use this for initialization
	void Start () {
		MoveAnimate = GetComponent<Animator>();
		enemy = GetComponent<enemy>();
		Destination = enemy.Destination;
		speed = StartSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = Destination.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
		if(!isOwl)
			speed = StartSpeed;
		MoveAnimate.SetFloat("Speed", speed);
	}
}
