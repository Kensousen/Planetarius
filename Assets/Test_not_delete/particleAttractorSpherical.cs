using System.Collections;
using UnityEngine;

public class particleAttractorSpherical : MonoBehaviour {
	
	ParticleSystem ps;
	ParticleSystem.Particle[] m_Particles;
	public Transform target;
	public float speed = 5f;
	int numParticlesAlive;
	bool startP = false;

	float step;
	void Start () {
		StartCoroutine(ParticleBurst());
	}
		
	IEnumerator ParticleBurst()
		{			
			yield return new WaitForSeconds(1);
			ps = GetComponent<ParticleSystem>();
			startP = true;
		}

	void Update () {
		if(startP){
			m_Particles = new ParticleSystem.Particle[ps.main.maxParticles];
			numParticlesAlive = ps.GetParticles (m_Particles);
			step = speed * Time.deltaTime;
			for (int i = 0; i < numParticlesAlive; i++) {
				step = speed * Time.deltaTime;
				m_Particles [i].position = Vector3.SlerpUnclamped (m_Particles [i].position, target.position, step);


			}
			ps.SetParticles (m_Particles, numParticlesAlive);
		}
	}

}