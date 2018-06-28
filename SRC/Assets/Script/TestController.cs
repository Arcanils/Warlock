using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestController : MonoBehaviour {

	public GameObject PrefabSpell;
	public Transform Target;
	private NavMeshAgent _nma;
	private Camera _cam;

	private void Awake()
	{
		_nma = Target.GetComponent<NavMeshAgent>();
		_nma.updatePosition = true;
		_nma.updateRotation = true;
		_cam = Camera.main;
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			ComputeMoveFromMouse();
		}

		if (Input.GetButtonDown("Fire2"))
		{
			CastSpell();
		}
	}
	
	private void ComputeMoveFromMouse()
	{
		var mousePos = Input.mousePosition;
		mousePos.z = 0f;
		var rayFromCam = _cam.ScreenPointToRay(mousePos);
		RaycastHit hitInfo;
		if (!Physics.Raycast(rayFromCam, out hitInfo))
			return;

		TryMoveHere(hitInfo.point);
	}

	private void TryMoveHere(Vector3 TargetPosition)
	{
		_nma.SetDestination(TargetPosition);
	}

	private void CastSpell()
	{
		var mousePos = Input.mousePosition;
		mousePos.z = 0f;
		var rayFromCam = _cam.ScreenPointToRay(mousePos);
		RaycastHit hitInfo;
		if (!Physics.Raycast(rayFromCam, out hitInfo))
			return;

		var dirSpell = hitInfo.point - Target.position;
		dirSpell.y = 0f;
		dirSpell.Normalize();
		var rotationSpell = -(Mathf.Atan2(dirSpell.z , dirSpell.x) * Mathf.Rad2Deg - 90f);

		Object.Instantiate(PrefabSpell, Target.position, Quaternion.Euler(0f, rotationSpell, 0f));
	}
}
