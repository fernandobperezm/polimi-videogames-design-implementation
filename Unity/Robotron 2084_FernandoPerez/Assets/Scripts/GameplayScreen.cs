using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameplayScreen : MonoBehaviour {

	[Header("Sprites of Lives.")]
	public Sprite m_filled_live; // Filled live.
	public Sprite m_empty_live; // Empty Live.

	[Header("On screen lives.")]
	public Image[] m_array_live; // Lives sprites on Screen.

	int m_no_lives = 4; // Number of lives of player.

	// Use this for initialization
	void Start () {
		FillLives ();
	}

	// Fill all lives.
	public void FillLives() {
		foreach (Image image in m_array_live){
			image.sprite = m_filled_live;
		}
		m_no_lives = 4;
	}

	// Fill fixed number of lives.
	public void FillFixedLives(int no_lives) {
		
	}

	// Empty fixed number of lives.
	public void EmptyFixedLives(int no_lives) {
		for (int i = m_no_lives; i > (m_no_lives - no_lives); i--){
			m_array_live [m_no_lives - 1].sprite = m_empty_live;
		}

		m_no_lives -= no_lives;
	}
}

