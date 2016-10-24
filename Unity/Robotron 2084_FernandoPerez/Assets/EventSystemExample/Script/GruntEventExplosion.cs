using UnityEngine;
using System.Collections;

public class GruntEventExplosion : MonoBehaviour {

	[Range(0.1f,2f)]
	public float m_time = 0.25f;

	// Use this for initialization
	void OnEnable () {
		StartCoroutine ("WaitForExplosionEnd");
	}

	void OnDisable() {
		//StopCoroutine("WaitForExplosionEnd");
	}

	IEnumerator WaitForExplosionEnd() {
		//Debug.Log ("START DYING");
		yield return new WaitForSeconds(0.25f);
		this.gameObject.SetActive (false);
		//Debug.Log ("FINISH DYING");
		yield return null;
	}
}
