using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance = null; // Singleton.

	public AudioSource m_background_music;

	[Header ("Player Game Effects")]
	public AudioSource m_player_effects; // Manager of audio clips for the player actions.
	public AudioClip m_shot_audioclip; // audio clip for the shot.
	public AudioClip m_grunt_explodes_audioclip; // audio clip for the explotion of grunts
	public AudioClip m_hulk_explodes_audioclip; // audio clip for the explotion of hulks.
	public AudioClip m_player_explodes_audioclip; // Audio clip for the player explotion.

	[Header("Background Music")]
	public AudioClip m_menu_background;

	void Awake() {
		// Singleton.
		if (Instance == null){
			Instance = this;
			DontDestroyOnLoad (gameObject);
		}
		else {
			Destroy(gameObject);
		}
	}

	void Start() {
		m_background_music.clip = m_menu_background;
		m_background_music.Play();
	}

	public void PlayerShots () {
		m_player_effects.PlayOneShot (m_shot_audioclip);
	}

	public void GruntExplodes () {
		m_player_effects.PlayOneShot (m_grunt_explodes_audioclip);
	}

	public void HulkExplodes () {
		m_player_effects.PlayOneShot (m_hulk_explodes_audioclip);
	}

	public void PlayerExplodes () {
		m_player_effects.PlayOneShot (m_player_explodes_audioclip);
	}
}
