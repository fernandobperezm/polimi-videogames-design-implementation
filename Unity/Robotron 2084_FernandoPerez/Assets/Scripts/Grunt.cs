using UnityEngine;
using System.Collections;

public class Grunt : Living {

	Transform tr;

	[Header ("Grunt Explodes Prefab")]
	public GameObject m_grunt_explodes;

	GameObject player; // As the grunts follows the player, we need their directions.

	[Header ("Grunt Speed")]
	[Range(25f,200f)]
	public float m_speed = 50f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		tr = GetComponent<Transform> () as Transform;
		this.m_no_lives = 1;
	}

	void OnEnable () {
		EventManager.StartListening ("Explode",Explode);
		player = GameObject.FindGameObjectWithTag ("Player");
		tr = GetComponent<Transform> () as Transform;
		this.m_no_lives = 1;
	}

	void Explode() {
		EventManager.StopListening ("Explode",Explode);
		StartCoroutine (ExplodeCoroutine ());
	}

	IEnumerator ExplodeCoroutine () {
		yield return new WaitForSeconds (0.01f);
		GameObject go = ObjectPoolingManager.Instance.GetObject (m_grunt_explodes.name);
		go.transform.position = gameObject.transform.position;
		go.transform.rotation = gameObject.transform.rotation;
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		// To chase the player, we must know its position to follow that direction.
		Vector3 direction = 
			(player.transform.position - tr.position).normalized;

		// Following the player.
		tr.position = tr.position +  direction * m_speed * Time.deltaTime;
		
	}
}
