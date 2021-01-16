using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
	[SerializeField] private ResponsiveParticles particles;
	[SerializeField] [ColorUsage(true, true)] private Color32 particleColor;
	Player player;
	Vector3 tempPos;
	Vector3 prevPos;

	void Start() {
		player = GameObject.FindObjectOfType<Player>();
		Physics.gravity = new Vector3(0, -9.0f, 0);
		prevPos = transform.position;
	}

    void FixedUpdate() {
		prevPos = transform.position;
		transform.position = MousePos();
		RaycastHit[] hits = Physics.RaycastAll(new Ray(prevPos, (transform.position - prevPos).normalized), (transform.position - prevPos).magnitude);

		for (int i = 0; i < hits.Length; i++) {
			if (hits[i].collider.gameObject.GetComponent<Enemy>()) {
				hits[i].collider.gameObject.GetComponent<Enemy>().Die();
				ResponsiveParticles p = Instantiate(particles, transform.position, transform.rotation);
				p.SetColor(particleColor);
			}
		}
    }

	public Vector3 MousePos() {
		tempPos = Input.mousePosition;
		tempPos.z = 10f;
		return Camera.main.ScreenToWorldPoint(tempPos);
	}

	// private void OnTriggerEnter(Collider other) {
	// 	if (other.gameObject.GetComponent<Enemy>()) {
	// 		other.GetComponent<Enemy>().Die();
	// 	}
	// }
}
