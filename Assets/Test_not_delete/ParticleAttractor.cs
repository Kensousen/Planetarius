using System.Collections;
using UnityEngine;


public class ParticleAttractor : MonoBehaviour {

	ParticleSystem ps;
	ParticleSystem.Particle[] m_Particles;
	public Transform target;
	int numParticlesAlive;

	void Start () {
		ps = GetComponent<ParticleSystem>();
		if (!GetComponent<Transform>()){
			GetComponent<Transform>();
		}
	}
	void Update () {
		m_Particles = new ParticleSystem.Particle[ps.main.maxParticles];
		numParticlesAlive = ps.GetParticles(m_Particles);
		for (int i = 0; i < numParticlesAlive; i++) {
			m_Particles[i].position = Vector3.Slerp(m_Particles[i].position, target.position, 5f * Time.deltaTime);
		}
		ps.SetParticles(m_Particles, numParticlesAlive);
	}
}
