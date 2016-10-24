using UnityEngine;
using System.Collections;

public class PlayerExplodes : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		EventManager.StartListening ("Explode",Explode);
	}

	void Explode() {
		EventManager.StopListening ("Explode",Explode);
		StartCoroutine (ExplodeCoroutine ());
	}

	IEnumerator ExplodeCoroutine() {
		yield return new WaitForSeconds (.5f);
		gameObject.SetActive(false);
	}
}
