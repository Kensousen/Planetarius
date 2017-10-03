using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxPlayerController : MonoBehaviour {

	public float moveSpeed = 8;
	private Vector3 moveDir;
	Rigidbody myRigidBody;
	public Transform transF;
	Animator animator;
	bool isWalking = false;

	public GameObject laser;
	Light laserLight;
	bool laserBoom=false;



	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator>();
		laserLight = laser.GetComponentInChildren<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		moveDir = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized;
		//transF.LookAt(Vector3.zero);
		//transF.Rotate(Vector3.right, -90);


		var lookPos = Vector3.zero - transform.position;
		lookPos.y = 0;
		var rotation = Quaternion.LookRotation(lookPos);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);


		animator.SetBool("IDLE", true);
		animator.SetBool("SHOOT", false);
		animator.SetBool("WALK", false);


	
		if (Input.GetKey ("a") || Input.GetKey ("w") || Input.GetKey ("s") || Input.GetKey ("d")) {
			animator.SetBool("IDLE", false);
			animator.SetBool("SHOOT", false);
			animator.SetBool("WALK", true);

		}
			
		if (Input.GetKey ("w")) {
			
			Vector3 auxPos;
			auxPos = transform.position;
			auxPos += ( transform.TransformDirection(moveDir) * 0.12f);
			transform.position = auxPos;


		}

		if (Input.GetKey ("s")) {
			
			Vector3 auxPos;
			auxPos = transform.position;
			auxPos -= ( transform.TransformDirection(moveDir) * -0.12f);
			transform.position = auxPos;
		}


		if (Input.GetKey ("d")) {
			transF.RotateAround(transF.transform.position, Vector3.up, 350 * Time.deltaTime);
		}


		if (Input.GetKey ("a")) {
			Vector3 aux;
			aux = transF.eulerAngles;
			aux.y -= 8;
			transF.eulerAngles = aux;
		}


		if (Input.GetButton("Fire1")) { 
			animator.SetBool ("IDLE", false);
			animator.SetBool ("SHOOT", true);
			animator.SetBool ("WALK", false);
			StartCoroutine (LaserBeamShot ());
			if (laserBoom) {
				laser.SetActive (true);
				laserLight.intensity = Random.Range (1, 5);
			}

		} else {
			laser.SetActive (false);
			laserBoom = false;
		}

	}

	IEnumerator LaserBeamShot()
	{			
		yield return new WaitForSeconds(0.2f);
		laserBoom = true;
	}

}
