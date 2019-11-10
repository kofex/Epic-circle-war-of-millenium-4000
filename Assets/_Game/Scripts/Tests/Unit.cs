using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Tests
{
	public class Unit : MonoBehaviour
	{
		public float SpeedMultyplayer = 1.0f;
		public float Radius => 0.5f;
		public bool IsMoving;
	
		private SpriteRenderer _thisSpriteRenderer;
		private Vector2 _collisionReflection;
		private Vector2 _speedVector; 
		private Vector2 _newSpeedVector;
		private float _bounceForce = 10f;
		private SpriteRenderer SpriteRenderer =>
			_thisSpriteRenderer ? _thisSpriteRenderer : (_thisSpriteRenderer = GetComponent<SpriteRenderer>());

		private float timer = 0;

		private void Start()
		{
			_speedVector = new Vector2(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
			_newSpeedVector = _speedVector;
		}

		private void Update()
		{
			if(IsMoving)
				transform.Translate(_newSpeedVector*Time.deltaTime * SpeedMultyplayer);
		
			timer += Time.deltaTime;
			if (timer < 0.1)
				return;
		
			timer = 0f;
			SpriteRenderer.color = Color.white;
			_collisionReflection = Vector2.zero;
			//_newSpeedVector = Vector2.zero;
		}

		public void OnCollision(Vector2 collisionReflection)
		{
			_collisionReflection.x = collisionReflection.x;
			_collisionReflection.y = collisionReflection.y;
		
			_newSpeedVector = _collisionReflection * _speedVector.magnitude;
		
			timer = 0;
		}

		private void OnDrawGizmos()
		{
			var position = transform.position;
		
			Gizmos.color = Color.green;
			Gizmos.DrawLine(position, _collisionReflection == Vector2.zero ? (Vector2) position : _collisionReflection + (Vector2) position);
		
			Gizmos.color = Color.cyan;
			var speed = _speedVector + (Vector2) position;
			Gizmos.DrawLine(position, speed);
		
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(position, _newSpeedVector == Vector2.zero ? (Vector2) position : _newSpeedVector + (Vector2) position);
		}
	}
}