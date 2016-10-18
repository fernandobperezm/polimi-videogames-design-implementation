using UnityEngine;
using System.Collections;

public class GruntEvent : MonoBehaviour {
	public GameObject m_grunt_explosion;

	// Use this for initialization
	void OnEnable () {
		EventManager.StartListening ("Explode",Explode);
		EventManager.StartListening ("RunAway",RunAway);
	}

	void Explode () {
		EventManager.StopListening ("Explode", Explode);
		Debug.Log ("Explode");
		StartCoroutine (ExplodeCoroutine ());
	}

	void RunAway () {
		EventManager.StopListening ("RunAway", RunAway);
		Debug.Log ("RunAway");
	}

	IEnumerator ExplodeCoroutine() {
		yield return new WaitForSeconds (Random.value * 1 + 0.2f);
		Instantiate (m_grunt_explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}
		
}
