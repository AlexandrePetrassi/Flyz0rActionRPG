/// <summary>
/// SCRIPT   NAME: Walk
/// CREATION DATE: 22/08/15
/// EDTION   DATE: 23/08/15
/// AUTHOR       : Alexandre "CaRaCrAzY" "Fireblizzard" Petrassi Cardoso
/// </summary>

using UnityEngine;
using System.Collections;

namespace Fireblizzard{
	namespace Player{
		[RequireComponent(typeof(Rigidbody2D))]
		public class Walk : MonoBehaviour{

			[SerializeField] float moveSpeed = 5;
			Rigidbody2D rigidBody;

			// Use this for initialization
			void Start () {
				rigidBody = GetComponent<Rigidbody2D>();
			}

			void FixedUpdate () {
				rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed,rigidBody.velocity.y);
			}
		}
	}
}
