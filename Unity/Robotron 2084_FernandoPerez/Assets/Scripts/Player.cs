﻿using UnityEngine;
using System.Collections;

public class Player : Living {
	// Transform to get player's positions.
	Transform tr;

	[Header ("Player Explodes Prefab")]
	public GameObject m_player_explodes;

	[Header ("Shot Positions")]
	public Transform m_shot_right;
	public Transform m_shot_left;
	public Transform m_shot_up;
	public Transform m_shot_down;

	public GameObject m_shot_prefab;

	// Configuration for player speed.
	[Header("Player Speed")]
	[Range(100f,300f)]
	public float m_speed = 150f;

	// Get the keyboard input.
	float m_horizontal = 0f;
	float m_vertical = 0f;

	[Header ("GameManager")]
	// Getting the game manager.
	public GameManager gm;

	// Setting the animator for the player.
	private Animator m_animator;

	void OnEnable() {
		EventManager.StartListening ("Explode",Explode);
		tr = GetComponent<Transform> () as Transform;
		m_animator = GetComponent<Animator> () as Animator;
		this.m_no_lives = 4;

		tr.position = tr.right * 0;
	}

	void Explode() {
		EventManager.StopListening ("Explode",Explode);
		StartCoroutine (ExplodeCoroutine ());
	}

	IEnumerator ExplodeCoroutine () {
		yield return new WaitForSeconds (0.01f);
		gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
		m_animator = GetComponent<Animator> () as Animator;
		this.m_no_lives = 4;

	}
	
	// Update is called once per frame
	void Update () {
		m_horizontal = Input.GetAxis ("Horizontal");
		m_vertical = Input.GetAxis ("Vertical");

		// Right Shooting.
		if (Input.GetKeyDown (KeyCode.L)) {
			GameObject go = ObjectPoolingManager.Instance.GetObject (m_shot_prefab.name);
			go.transform.position = m_shot_right.position;
			go.transform.rotation = m_shot_right.rotation;

			SoundManager.Instance.PlayerShots();


		}

		// Left shooting.
		if (Input.GetKeyDown (KeyCode.J)) {
			GameObject go = ObjectPoolingManager.Instance.GetObject (m_shot_prefab.name);
			go.transform.position = m_shot_left.position;
			go.transform.rotation = m_shot_left.rotation;

			SoundManager.Instance.PlayerShots ();
		}

		// Up shooting
		if (Input.GetKeyDown (KeyCode.I)) {
			GameObject go = ObjectPoolingManager.Instance.GetObject (m_shot_prefab.name);
			go.transform.position = m_shot_up.position;
			go.transform.rotation = m_shot_up.rotation;

			SoundManager.Instance.PlayerShots ();
		}

		// Down Shooting
		if (Input.GetKeyDown (KeyCode.K)) {
			GameObject go = ObjectPoolingManager.Instance.GetObject (m_shot_prefab.name);
			go.transform.position = m_shot_down.position;
			go.transform.rotation = m_shot_down.rotation;

			SoundManager.Instance.PlayerShots ();
		}
	}

	//
	void FixedUpdate () {
		tr.position += 
			m_horizontal * tr.right * Time.fixedDeltaTime * m_speed +
			m_vertical * tr.up * Time.fixedDeltaTime * m_speed;

//		if (tr.position.x > )

		// Up Movement
		if ( (Input.GetKey (KeyCode.UpArrow)) || (Input.GetKey (KeyCode.W)) ) {
			m_animator.SetInteger ("Direction", 2);
		}

		// Down Movement.
		if ( (Input.GetKey (KeyCode.DownArrow)) || (Input.GetKey (KeyCode.S)) ) {
			m_animator.SetInteger ("Direction", 1);
		}

		// Right Movement
		if ( (Input.GetKey (KeyCode.RightArrow)) || (Input.GetKey (KeyCode.D)) ) {
			m_animator.SetInteger ("Direction", 3);
		}

		// Left Movement
		if ((Input.GetKey (KeyCode.LeftArrow)) || (Input.GetKey (KeyCode.A)) ){
			m_animator.SetInteger ("Direction", 4);
		}
	}

	// Triggered when a collision happens.
	void OnTriggerEnter2D (Collider2D otherGameObject) {
		// Checking if the enemy is a hulk or a grunt.
		if (otherGameObject.gameObject.tag == "Grunt"){

			this.m_no_lives -= 1;
			gm.UpdateLives (1);

			if (this.m_no_lives == 0) {

				// Instantiation of the grunt explodes game object.
				GameObject go = ObjectPoolingManager.Instance.GetObject (m_player_explodes.name);

				// Setting the position and rotation of the grunt exploded in the same as the grunt already shoted.
				go.transform.position = otherGameObject.transform.position;
				go.transform.rotation = otherGameObject.transform.rotation;

				// Activating sound.
				SoundManager.Instance.PlayerExplodes ();

				// Disabling the player.
				EventManager.StopListening ("Explode",Explode);
				gameObject.SetActive (false);

				// Activating gameover sequency.
				gm.OnGameover ();
			}
		}

		if (otherGameObject.gameObject.tag == "Hulk") { 
			this.m_no_lives -= 1;
			gm.UpdateLives (1);

			if (this.m_no_lives == 0) {

				// Instantiation of the grunt explodes game object.
				GameObject go = ObjectPoolingManager.Instance.GetObject (m_player_explodes.name);

				// Setting the position and rotation of the grunt exploded in the same as the grunt already shoted.
				go.transform.position = otherGameObject.transform.position;
				go.transform.rotation = otherGameObject.transform.rotation;

				// Activating sound.
				SoundManager.Instance.PlayerExplodes ();

				// Disabling the player.
				EventManager.StopListening ("Explode",Explode);
				gameObject.SetActive (false);

				// Activating gameover sequency.
				gm.OnGameover ();
			}
		}
	}
}
