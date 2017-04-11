using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class FpsWalkerController: MonoBehaviour {

	public float walkSpeed = 6.0f;
	public float runSpeed = 11.0f;
	public bool limitDiagonalSpeed = true;
	public bool toggleRun = false;

	[Space(10)]
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;
	public float fallingDamageThreshold = 10.0f;
	public bool airControl = false;
	public float antiBumpFactor = .75f;
	private bool canDoubleJump = false;

	[Space(10)]
	public bool slideWhenOverSlopeLimit = false;
	public bool slideOnTaggedObjects = false;
	public float slideSpeed = 12.0f;

	public CharacterController controller;

	[Space(10)]
	[SerializeField] public MouseLook mouseLook;


	[HideInInspector]public bool playerControl = true;
	private Vector3 moveDirection = Vector3.zero;
	private bool grounded = false;
	private Camera playerCam;
	private Transform myTransform;
	private float speed;
	private RaycastHit hit;
	private float fallStartLevel;
	private bool falling;
	private float slideLimit;
	private float rayDistance;
	private Vector3 contactPoint;
	/*[HideInInspector]*/ public bool doubleJumpActive = false;

	private float inputX;
	private float inputY;
	private bool inputJump;

	public bool CanDoubleJump {
		get {
			return this.canDoubleJump;
		}
		set {
			this.canDoubleJump = value;
		}
	}

	void Start() {
		this.controller = this.GetComponent<CharacterController>();
		this.playerCam = this.GetComponentInChildren<Camera>();
		this.mouseLook.Init(this.transform, this.playerCam.transform);
		this.myTransform = this.transform;
		this.speed = this.walkSpeed;
		this.rayDistance = this.controller.height * .5f + controller.radius;
		this.slideLimit = this.controller.slopeLimit - .1f;
	}

	void FixedUpdate() {
		this.mouseLook.UpdateCursorLock();

		if (!this.playerControl)
			return;
		// Limit diagonal speed if both input are pressed simultaneously
		float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f && limitDiagonalSpeed) ? .7071f : 1.0f;

		if (grounded) {
			bool sliding = false;
			// See if surface immediately below should be slid down. We use this normally rather than a ControllerColliderHit point,
			// because that interferes with step climbing amongst other annoyances
			if (Physics.Raycast(myTransform.position, -Vector3.up * 2, out hit, rayDistance)) {
				if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
					sliding = true;
			}
			// However, just raycasting straight down from the center can fail when on steep slopes
			// So if the above raycast didn't catch anything, raycast down from the stored ControllerColliderHit point instead
			else {
				Physics.Raycast(contactPoint + Vector3.up, -Vector3.up, out hit);
				if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
					sliding = true;
			}

			// If we were falling, and we fell a vertical distance greater than the threshold, run a falling damage routine
			if (falling) {
				falling = false;
				if (myTransform.position.y < fallStartLevel - fallingDamageThreshold)
					FallingDamageAlert(fallStartLevel - myTransform.position.y);
			}

			// If running isn't on a toggle, then use the appropriate speed depending on whether the run button is down
			if (!toggleRun)
				speed = Input.GetButton("Run") ? runSpeed : walkSpeed;

			// If sliding (and it's allowed), or if we're on an object tagged "Slide", get a vector pointing down the slope we're on
			if ((sliding && slideWhenOverSlopeLimit) || (slideOnTaggedObjects && hit.collider.tag == "Slide")) {
				Vector3 hitNormal = hit.normal;
				moveDirection = new Vector3((inputX * inputModifyFactor) + hitNormal.x, -hitNormal.y, (inputY * inputModifyFactor) + hitNormal.z);
				Vector3.OrthoNormalize(ref hitNormal, ref moveDirection);
				moveDirection *= slideSpeed;
			}
			// Otherwise recalculate moveDirection directly from axes, adding a bit of -y to avoid bumping down inclines
			else {
				moveDirection = new Vector3(inputX * inputModifyFactor, -antiBumpFactor, inputY * inputModifyFactor);
				moveDirection = myTransform.TransformDirection(moveDirection) * speed;
			}

			if (!this.doubleJumpActive)
				this.doubleJumpActive = true;

			} else {
			// If we stepped over a cliff or something, set the height at which we started falling
			if (!falling) {
				falling = true;
				fallStartLevel = myTransform.position.y;
			}

			// If air control is allowed, check movement but don't touch the y component
			if (airControl) {
				moveDirection.x = inputX * speed * inputModifyFactor;
				moveDirection.z = inputY * speed * inputModifyFactor;
				moveDirection = myTransform.TransformDirection(moveDirection);
			}



		}

		moveDirection.y = Mathf.Clamp(moveDirection.y - (this.gravity * Time.deltaTime), -this.gravity * 0.6f, this.gravity);

		// Move the controller, and set grounded true or false depending on whether we're standing on something

	}

	void Update() {
		this.inputX = Input.GetAxis("Horizontal");
		this.inputY = Input.GetAxis("Vertical");
		this.inputJump = Input.GetButtonDown("Jump");

		if (grounded) {
			if (this.inputJump) {
				moveDirection.y = jumpSpeed;
				//StartCoroutine(this.OnLongJump());
			}
		} else if (this.canDoubleJump && this.doubleJumpActive) {

			if (this.inputJump) {
				moveDirection.y = jumpSpeed;
				this.doubleJumpActive = false;
			}
		}

		grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;

	
		// If the run button is set to toggle, then switch between walk/run speed. (We use Update for this...
		// FixedUpdate is a poor place to use GetButtonDown, since it doesn't necessarily run every frame and can miss the event)
		if (toggleRun && grounded && Input.GetButtonDown("Run"))
			speed = (speed == walkSpeed ? runSpeed : walkSpeed);
	}

	IEnumerator OnLongJump() {
		yield return new WaitForSeconds(0.2f);
		float timer = Time.time;
		while (timer < Time.time + 2f) {
			if (Input.GetButton("Jump")) {
				moveDirection.y += jumpSpeed / 2;
				break;
			}
			yield return null;
		}
	}

	void LateUpdate() {
		this.mouseLook.LookRotation(this.transform, this.playerCam.transform);
	}

	// Store point that we're in contact with for use in FixedUpdate if needed
	void OnControllerColliderHit(ControllerColliderHit hit) {
		contactPoint = hit.point;
	}

	// If falling damage occured, this is the place to do something about it. You can make the player
	// have hitpoints and remove some of them based on the distance fallen, add sound effects, etc.
	void FallingDamageAlert(float fallDistance) {
		print("Ouch! Fell " + fallDistance + " units!");
	}
}