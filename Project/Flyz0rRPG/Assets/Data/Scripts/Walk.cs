using UnityEngine;
using System.Collections;

namespace Fireblizzard{
	namespace Player{
		[RequireComponent(typeof(Rigidbody2D))]
		public class Walk : MonoBehaviour {
			
			[SerializeField] float moveSpeed = 5;
			Rigidbody2D rigidBody;

			// Use this for initialization
			void Start () {
				rigidBody = GetComponent<Rigidbody2D>();
			}
			
			// Update is called once per frame
			void FixedUpdate () {
				rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed,rigidBody.velocity.y);
			}
		}
	}
}
