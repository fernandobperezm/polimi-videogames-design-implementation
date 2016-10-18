using UnityEngine;
using System.Collections;

public class GruntExplodes : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		StartCoroutine (WaitForAnimation ());
	}
	
	IEnumerator WaitForAnimation() {
		yield return new WaitForSeconds (.5f);
		gameObject.SetActive(false);
	}
}
