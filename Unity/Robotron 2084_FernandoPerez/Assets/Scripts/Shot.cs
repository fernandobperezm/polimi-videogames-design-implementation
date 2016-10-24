using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	Transform tr;

	// Prefab of enemies explotions. With this, we can handle the explotion.
	public GameObject m_grunt_explodes;
	public GameObject m_hulk_explodes;

	[Header ("Shoot speed")]
	[Range (250f,500f)]
	public float m_shot = 300f;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
	}
	
	// Update is called once per frame
	void Update () {
		//Checking if the shot goes width farther.
		if (Mathf.Abs (tr.position.x) > Screen.width) {
			gameObject.SetActive (false);
		}

		// Checking if the shot goes height farther.
		if (Mathf.Abs (tr.position.y) > Screen.height) {
			gameObject.SetActive (false);
		}
	}

	// Movement of the shot.
	void FixedUpdate () {
		tr.position += tr.right * Time.fixedDeltaTime * m_shot;
	}

	// Triggered when a collision happens.
	void OnTriggerEnter2D (Collider2D otherGameObject) {
		Debug.Log ("Hit " + otherGameObject.gameObject.name);

		// Checking if the enemy is a hulk or a grunt.
		if (otherGameObject.gameObject.tag == "Grunt"){
			// Instantiation of the grunt explodes game object.
			GameObject go = ObjectPoolingManager.Instance.GetObject (m_grunt_explodes.name);

			// Setting the position and rotation of the grunt exploded in the same as the grunt already shoted.
			go.transform.position = otherGameObject.transform.position;
			go.transform.rotation = otherGameObject.transform.rotation;

			// Activating sound.
			SoundManager.Instance.GruntExplodes ();

			// Scoring for the player.
			GameManager.Instance.Scored (100);

			// Disabling the shot and the grunt.
			otherGameObject.gameObject.SetActive (false);
			gameObject.SetActive (false);
		}

		if (otherGameObject.gameObject.tag == "Hulk") {
			// Gettings
			Living hk = otherGameObject.gameObject.GetComponent<Hulk> () as Hulk;

			// Reducing the lives by 1.
			hk.m_no_lives -= 1;

			// Checking if died.
			if (hk.m_no_lives == 0){
				GameObject go = ObjectPoolingManager.Instance.GetObject (m_hulk_explodes.name);
				// Setting the position and rotation of the grunt exploded in the same as the grunt already shoted.
				go.transform.position = otherGameObject.transform.position;
				go.transform.rotation = otherGameObject.transform.rotation;

				// Activating sound.
				SoundManager.Instance.GruntExplodes ();

				// Scoring for the player.
				GameManager.Instance.Scored (200);

				// Disabling the shot and the grunt.
				otherGameObject.gameObject.SetActive (false);
			}

			gameObject.SetActive (false);
		}
	}
}
