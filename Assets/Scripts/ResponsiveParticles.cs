using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveParticles : MonoBehaviour {
    private Color32 color;
    private ParticleSystem ps;

    void Start() {
        ParticleSystem.MainModule settings = GetComponent<ParticleSystem>().main;
        settings.startColor = new ParticleSystem.MinMaxGradient(color);
    }

    public void SetColor(Color32 c) { color = c; }
}
