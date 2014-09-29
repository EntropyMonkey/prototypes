using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public enum Movement { SPHERICAL, JETPACK3D }

	public Vector2 MoveDirection
	{
		get
		{
			return moveDirection;
		}
	}

	public bool Jumping
	{
		get;
		private set;
	}

	[SerializeField]
	private Movement movementScheme;

	Vector2 moveDirection = Vector2.zero;
	const float inputDeadzone = 0.25f;

	MovementController movementFSM;

	Jetpack3DMovement jetpack3DMovement;
	SphericalMovement sphericalMovement;

	// Use this for initialization
	void Awake()
	{
		movementFSM = gameObject.AddComponent<MovementController>();

		if (!(jetpack3DMovement = GetComponent<Jetpack3DMovement>()))
			jetpack3DMovement = gameObject.AddComponent<Jetpack3DMovement>();

		if (!(sphericalMovement = GetComponent<SphericalMovement>()))
			sphericalMovement = gameObject.AddComponent<SphericalMovement>();

		switch(movementScheme)
		{
			case Movement.JETPACK3D:
				movementFSM.Configure(this, jetpack3DMovement);
				break;
			case Movement.SPHERICAL:
				movementFSM.Configure(this, sphericalMovement);
				break;
		}
	}

	void Update()
	{
		moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if (moveDirection.magnitude < inputDeadzone)
		{
			moveDirection = Vector2.zero;
		}
		else
		{
			// scaled radial deadzone
			moveDirection = moveDirection.normalized * (moveDirection.magnitude - inputDeadzone) * (1 - inputDeadzone);
		}

		if (Input.GetButtonDown("Jump"))
			Jumping = true;
		if (Input.GetButtonUp("Jump"))
			Jumping = false;

		movementFSM.FSM_Update();
	}

	void FixedUpdate()
	{
		movementFSM.FSM_FixedUpdate();
	}

	void LateUpdate()
	{
		movementFSM.FSM_LateUpdate();
	}
}
