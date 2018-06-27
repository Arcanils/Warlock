using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

	public float Speed;
	public float Duration;
	public float ExplosionRadius;
	public float ExplosionForce;
	public LayerMask LayerToProject;
	public GameObject PrefabExplosion;

	private Transform _trans;
	private Vector3 _dir;

	private void Awake()
	{
		_trans = transform;
		_dir = _trans.forward * Speed;
		Destroy(gameObject, Duration);
	}

	private void OnEnable()
	{

	}

	private void Update()
	{
		_trans.Translate(Vector3.forward * Time.deltaTime * Speed, Space.Self);
	}

	private void OnCollisionEnter(Collision collision)
	{
		Destroy(gameObject);
		OnExplosion();
	}

	private void OnExplosion()
	{
		GameObject.Instantiate(PrefabExplosion, _trans.position, _trans.rotation);
		var entities = Physics.OverlapSphere(_trans.position, ExplosionRadius, LayerToProject);

		for (int i = 0; i < entities.Length; i++)
		{
			var entity = entities[i];
			var entityRigidbody = entity.GetComponent<Rigidbody>();
			if (!entityRigidbody)
				continue;

			entityRigidbody.AddExplosionForce(ExplosionForce, _trans.position, ExplosionRadius);
		}
	}
}
