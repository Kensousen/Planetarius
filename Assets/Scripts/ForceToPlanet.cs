using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ForceToPlanet : MonoBehaviour {
	private GameObject planetObjectPrefab;
    private GameObject[] planetObjects;
	public GameObject spawner;
	int numero = 0;
	float planetRadius = 8.95f;
	float objectHeight = 11.1f;
	public Transform planetPosition;

	void Start () {
		
		numero = 0;
	}
	

	void Update () {
		 //Instantiate objects randomly around the sphere-world

		if (numero < 100) {
			Vector3 spawnPosition = Random.onUnitSphere * (planetRadius + objectHeight  ) + planetPosition.position ;
			Instantiate(spawner, spawnPosition, Quaternion.identity); //as GameObject
		numero++;
		}

			
		planetObjects = GameObject.FindGameObjectsWithTag("PlanetObject");
        foreach (GameObject planetObj in planetObjects){
           	planetObj.transform.LookAt(Vector3.zero);
			planetObj.transform.Rotate(Vector3.right, -90);
        }
	}
}
