using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField] private ResponsiveParticles particles;
	[SerializeField] private int layers;
	[SerializeField] [ColorUsage(true, true)] private Color32 particleColor;
	[SerializeField] private bool isBomb;
	Player player;
	bool died;

    void Start() {
        player = GameObject.FindObjectOfType<Player>();
    }

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "DeathBox" && !isBomb) {
			player.LoseLife();
			Destroy(gameObject);
		}
	}

	public void Die() {
		layers -= 1;
		if (!isBomb) {
			player.AddScore(1);
			player.ResetCombo();
		} else {
			player.LoseLife();
		}
		if (layers <= 0) { Particles(); }
	}

	public void Particles() {
		ResponsiveParticles p = Instantiate(particles, transform.position, transform.rotation);
		p.SetColor(particleColor);
		Destroy(gameObject);
	}
}
