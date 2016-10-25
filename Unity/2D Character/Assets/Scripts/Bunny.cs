using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D)) ] // Adds a rigidbody2D if the gameobject doesn't have it. This is for Start Get component not to crash.
public class Bunny : MonoBehaviour {

	float m_horizontal = 0f;
	float m_speed = 150f;

	bool m_jump;
	float m_jump_force = 5f;

	bool facing_right = true;

	Animator anim;

	Rigidbody2D rb; // RigidBody.
	Transform tr;
	//bool m_go_left = false; // this is for 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> () as Rigidbody2D;
		tr = GetComponent<Transform> () as Transform;
		anim = GetComponent<Animator> () as Animator;
	}
	
	// Update is called once per frame
	void Update () {
		m_horizontal = Input.GetAxis ("Horizontal");
		m_jump = Input.GetButtonDown ("Jump");
		if (m_horizontal <0 && facing_right) {
			FlipBunny ();
		} else if (m_horizontal > 0 && !facing_right) {
			FlipBunny ();
		}
	}

	void FixedUpdate () {
		float vy = rb.velocity.y; // Moving on 
		rb.velocity = new Vector2 (m_horizontal * m_speed * Time.fixedDeltaTime, vy);

		anim.SetFloat("Speed",Mathf.Abs (m_speed));

		if (m_jump) {
			rb.AddForce (new Vector2(0f,m_jump_force),ForceMode2D.Impulse);
		}
	}

	void FlipBunny() {
		Vector3 ls = tr.localScale;

		ls.x = -1f * ls.x;
		tr.localScale = ls;
		facing_right = !facing_right;
	}
}
