using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GravityBody))]
public class FirstPersonController : MonoBehaviour {

	// public vars
	public float mouseSensitivityX = 1;
	public float mouseSensitivityY = 1;
	public float walkSpeed = 6;
	public float jumpForce = 220;
	public LayerMask groundedMask;

	// System vars
	bool grounded;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	Transform cameraTransform;
	Rigidbody rigidbody;

	Animator animator;
	public GameObject laser;
	Light laserLight;
	bool laserBoom=false;

	void Awake() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		cameraTransform = Camera.main.transform;
		rigidbody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator>();
		laserLight = laser.GetComponentInChildren<Light>();
	}

	void Update() {

		animator.SetBool("IDLE", true);
		animator.SetBool("SHOOT", false);
		animator.SetBool("WALK", false);

		// Look rotation:
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);

		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation,-30	,-10);
		cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

		// Calculate movement:
		float inputX = Input.GetAxisRaw("Horizontal");
		float inputY = Input.GetAxisRaw("Vertical");

		Vector3 moveDir = new Vector3(inputX,0, inputY).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;
		moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,.15f);

		// Jump
		/*
		if (Input.GetButtonDown("space")) {
			if (grounded) {
				rigidbody.AddForce(transform.up * jumpForce);
			}
		}*/

		if (Input.GetKey ("a") || Input.GetKey ("w") || Input.GetKey ("s") || Input.GetKey ("d")) {
			animator.SetBool("IDLE", false);
			animator.SetBool("SHOOT", false);
			animator.SetBool("WALK", true);

		}

		if (Input.GetKeyDown ("space")) {
			if (grounded) {
				rigidbody.AddForce(transform.up * jumpForce);
			}
		
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

		// Grounded check
		Ray ray = new Ray(transform.position, -transform.up);

		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask)) {
			grounded = true;
		}
		else {
			grounded = false;
		}

	}

	IEnumerator LaserBeamShot(){			
		yield return new WaitForSeconds(0.2f);
		laserBoom = true;
	}

	void FixedUpdate() {
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
		rigidbody.MovePosition(rigidbody.position + localMove);
	}
}