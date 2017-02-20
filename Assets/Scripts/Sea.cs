using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour {

    new ParticleSystem particleSystem;
    ParticleSystem.Particle[] particlesArray;
    public int seaRosolution = 50;

    public float spacing = 0.5f;

    public float noiseScale = 0.5f;
    public float heightScale = 3f;

    public Gradient colorGradient;

    float perlinNoiseAnimX = 0.01f;
    float perlinNoiseAnimY = 0.01f;

	// Use this for initialization
	void Start () {
        particleSystem = GetComponent<ParticleSystem>();
        particlesArray = new ParticleSystem.Particle[seaRosolution * seaRosolution];
        particleSystem.maxParticles = seaRosolution * seaRosolution;
        particleSystem.Emit(seaRosolution * seaRosolution);
        particleSystem.GetParticles(particlesArray);
	}
	
	// Update is called once per frame
	void Update () {
        float zPos = 0;
        for (int i = 0; i < seaRosolution; ++i)
        {
            for (int j = 0; j < seaRosolution; ++j)
            {
                zPos = Mathf.PerlinNoise(i * noiseScale + perlinNoiseAnimX, j * noiseScale + perlinNoiseAnimY);
                particlesArray[i * seaRosolution + j].position = new Vector3(i * spacing, zPos * heightScale, j * spacing);
                particlesArray[i * seaRosolution + j].startColor = colorGradient.Evaluate(zPos);
            }
        }
        perlinNoiseAnimX += 0.01f;
        perlinNoiseAnimY += 0.01f;
        particleSystem.SetParticles(particlesArray, particlesArray.Length);
    }
}
