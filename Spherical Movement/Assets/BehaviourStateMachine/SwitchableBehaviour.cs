using UnityEngine;
using System.Collections;

public abstract class SwitchableBehaviour<T> : MonoBehaviour
{

	/// <summary>
	/// called when switching behaviour types
	/// </summary>
	public virtual void Enter(T owner)
	{
	}

	public virtual void FSM_Update(T owner)
	{
	}

	public virtual void FSM_FixedUpdate(T owner)
	{

	}

	public virtual void FSM_LateUpdate(T owner)
	{

	}

	/// <summary>
	/// Called when unloading this behaviour type, before calling the load of the next behaviour type
	/// </summary>
	public virtual void Exit(T owner)
	{
	}
}
