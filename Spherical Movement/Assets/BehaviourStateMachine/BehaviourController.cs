using UnityEngine;
using System.Collections;

public class BehaviourController<T> : MonoBehaviour
{
	protected SwitchableBehaviour<T> currentBehaviour;
	protected SwitchableBehaviour<T> lastBehaviour;
	protected T owner;

	public void Configure(T owner, SwitchableBehaviour<T> startBehaviour)
	{
		this.owner = owner;
		ChangeBehaviour(startBehaviour);
	}

	public void ChangeBehaviour(SwitchableBehaviour<T> newBehaviour)
	{
		if (currentBehaviour != newBehaviour && newBehaviour != null)
		{
			if (currentBehaviour != null) currentBehaviour.Exit(owner);
			lastBehaviour = currentBehaviour;
			currentBehaviour = newBehaviour;
			currentBehaviour.Enter(owner);
		}
	}

	public void FSM_Update()
	{
		currentBehaviour.FSM_Update(owner);
	}

	public void FSM_FixedUpdate()
	{
		currentBehaviour.FSM_FixedUpdate(owner);
	}

	public void FSM_LateUpdate()
	{
		currentBehaviour.FSM_LateUpdate(owner);
	}
}
