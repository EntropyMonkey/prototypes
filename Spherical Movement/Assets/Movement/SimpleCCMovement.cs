using UnityEngine;
using System.Collections;

public class SimpleCCMovement : MonoBehaviour
{

	public float floorSpeed = 50.0f;
	public float floorDeceleration = 4.0f;
	public float maxFloorSpeed = 25.0f;
	public float airSpeed = 30.0f;
	public float airDeceleration = 4.0f;
	public float maxAirSpeed = 40.0f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;

	Vector3 velocity = Vector3.zero;

	Vector2 moveDirection = Vector2.zero;
	bool jumping = false;
	const float inputDeadzone = 0.25f;

	void Update()
	{
		// inputstuff
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
			jumping = true;
		else if (Input.GetButtonUp("Jump"))
			jumping = false;

		// movement stuff
		CharacterController controller = GetComponent<CharacterController>();

		Vector3 inputMultiplier = new Vector3(moveDirection.x, 0, moveDirection.y);

		Vector3 worldDirection = transform.TransformDirection(inputMultiplier); // local to world

		if (controller.isGrounded)
		{
			if (worldDirection.magnitude < 0.05f)
				velocity = Vector3.Lerp(velocity, Vector3.zero, floorDeceleration * Time.deltaTime);

			velocity += worldDirection * floorSpeed * Time.deltaTime;

			if (velocity.sqrMagnitude > maxFloorSpeed * maxFloorSpeed)
			{
				velocity = velocity.normalized * maxFloorSpeed;
			}

			if (jumping)
			{
				velocity.y = jumpSpeed;
			}
			else
			{
				velocity.y = 0;
			}
		}
		else
		{
			velocity.x += worldDirection.x * airSpeed * Time.deltaTime;
			velocity.z += worldDirection.z * airSpeed * Time.deltaTime;

			Vector2 horizontal = new Vector2(velocity.x, velocity.z);
			if (horizontal.sqrMagnitude > maxAirSpeed * maxAirSpeed)
				horizontal = horizontal.normalized * maxAirSpeed;

			if (worldDirection.magnitude < 0.05f)
			{
				horizontal = Vector2.Lerp(horizontal, Vector2.zero, airDeceleration * Time.deltaTime);
			}

			velocity.x = horizontal.x;
			velocity.z = horizontal.y;
		}

		velocity.y -= gravity * Time.deltaTime;

		controller.Move(velocity * Time.deltaTime);
	}
}