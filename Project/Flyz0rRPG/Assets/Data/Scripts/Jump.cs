using UnityEngine;
using System.Collections;
using Fireblizzard.InputManagement;

namespace Fireblizzard{
	namespace Player{
		[RequireComponent(typeof(Rigidbody2D))]
		public class Jump : MonoBehaviour {

			[SerializeField] float jumpPower = 10;
			Rigidbody2D rigidBody;

			// Use this for initialization
			void Start () {
				rigidBody = GetComponent<Rigidbody2D>();
			}
			
			// Update is called once per frame
			void FixedUpdate () {
				rigidBody.velocity = new Vector2(rigidBody.velocity.x,InputListener.getAxis("Jump").getState(AxisState.onAxisDown) ? jumpPower: rigidBody.velocity.y);
			}
		}
	}
}