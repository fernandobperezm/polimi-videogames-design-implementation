using UnityEngine;
using System.Collections;

public class FakePlayer: MonoBehaviour {
	
	private Animator m_animator;
	// Use this for initialization
	void Start () {
		m_animator = GetComponent<Animator> () as Animator;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.DownArrow)){
			m_animator.SetInteger ("Direction", 1);
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)){
			m_animator.SetInteger ("Direction", 2);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)){
			m_animator.SetInteger ("Direction", 3);
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)){
			m_animator.SetInteger ("Direction", 4);
		}
	}
}
