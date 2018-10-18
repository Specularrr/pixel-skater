using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float force = 0;
	private Rigidbody2D m_player;

	// Use this for initialization
	void Awake () {
		m_player = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {


		// fix the movement
		if (Input.GetKeyDown("right"))
		{
			Debug.Log("Right pressed");
			m_player.AddForce(Vector3.right * force);
		}

		if (Input.GetKeyDown("left"))
		{
			Debug.Log("Left pressed");
			m_player.AddForce(Vector3.left * force);
		}

	}
}
