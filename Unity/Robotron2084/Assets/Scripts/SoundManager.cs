using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {


	public static SoundManager Instance = null;

	public AudioSource m_background_music;

	[Header("Player Sound Effects")]
	public AudioSource m_player_effects;
	public AudioClip m_shoot_audioclip;
	public AudioClip m_grunt_explodes_audioclip;

	[Header("Background Music")]
	public AudioClip m_menu_background;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	void Start() {
		m_background_music.clip = m_menu_background;
		m_background_music.Play ();
	}

	public void PlayerShoots() {
		m_player_effects.PlayOneShot (m_shoot_audioclip);
	}

	public void GruntExplodes() {
		m_player_effects.PlayOneShot (m_grunt_explodes_audioclip);
	}
}
