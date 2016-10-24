using UnityEngine;
using System.Collections;

public class Player : Living {
	// Transform to get player's positions.
	Transform tr;

	[Header ("Player Explodes")]
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

	// Flashing interval.
	float m_flashing_interval = 0.25f;

	// Get the keyboard input.
	float m_horizontal = 0f;
	float m_vertical = 0f;

	[Header ("GameManager")]
	// Getting the game manager.
	public GameManager gm;

	// Setting the animator for the player.
	private Animator m_animator;

	// Collider for player
	BoxCollider2D m_player_collider;



	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
		m_animator = GetComponent<Animator> () as Animator;
		m_player_collider = GetComponent <BoxCollider2D> () as BoxCollider2D;
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

		// Up Movement
		if (Input.GetKey (KeyCode.UpArrow)) {
			m_animator.SetInteger ("Direction", 2);
		}

		// Down Movement.
		if (Input.GetKey (KeyCode.DownArrow)) {
			m_animator.SetInteger ("Direction", 1);
		}

		// Right Movement
		if (Input.GetKey (KeyCode.RightArrow)) {
			m_animator.SetInteger ("Direction", 3);
		}

		// Left Movement
		if (Input.GetKey (KeyCode.LeftArrow)) {
			m_animator.SetInteger ("Direction", 4);
		}
	}

	// Triggered when a collision happens.
	void OnTriggerEnter2D (Collider2D otherGameObject) {
		Debug.Log ("Hit " + otherGameObject.gameObject.name);

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
				gameObject.SetActive (false);
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
				gameObject.SetActive (false);
			}
		}
	}
}
