/// <summary>
/// SCRIPT   NAME: Jump
/// CREATION DATE: 22/08/15
/// EDTION   DATE: 07/08/15
/// AUTHOR       : Alexandre "CaRaCrAzY" Petrassi Cardoso
/// </summary>

using UnityEngine;
using System.Collections;
using CaRaCrAzY.CollisionManagement;
using CaRaCrAzY.InputManagement;

namespace CaRaCrAzY{
	namespace Player{
		[RequireComponent(typeof(Rigidbody2D))]
		public class Jump : Inputable{

			[Tooltip("Higher this value, higher the character jumps")]
			[SerializeField] float jumpPower = 10;
			[Tooltip("Maximum AirJump quantity this unity can perform")]
			[SerializeField] int   maxJump = 2;
			int jumpCount = 0; // Air jump counter

			Rigidbody2D rigidBody; // Character's rigidbody reference used to set jump velocity
			[Tooltip("Collider that will be recognized as the character's feet for landing purposes")]
			[SerializeField] BodyPart feet;

			/// <summary>
			/// Used as the Start() method from a MonoBehaviour
			/// Use this for initialization
			/// </summary>
			protected override void start () {
				rigidBody = GetComponent<Rigidbody2D>();
			}

			/// <summary>
			/// Defines a condition to switch between events that indepent from axis input
			/// ex. Jump requires input, but for the jump to be triggered the player must touch a platform.
			/// Limits the player's quantity of AirJumps
			/// </summary>
			protected override bool trigger(){
				if(jumpCount >= maxJump) return false;
				return true;
			}

			/// <summary>
			/// Used as the FixedUpdate() method from a MonoBehaviour
			/// Called every frame before input events
			/// </summary>
			protected override void fixedUpdate(){
				if(!feet.isColliding){ // if character is not on ground
					if(jumpCount == 0) jumpCount = 1; // and it didn't jump already, set its jump count to 1
				}else{ // if character is on ground
					jumpCount = 0; // set its jumpCount to 0
				}
			}

			/// <summary>
			/// Event called the instant the key is pressed if the trigger is true
			/// if the player hits the key, the character jumps
			/// </summary>
			protected override void onStarting(){
				rigidBody.velocity = new Vector2(rigidBody.velocity.x,jumpPower);
				++jumpCount;
			}

			/// <summary>
			/// Event called the instant the key is released if the trigger is true
			/// If the button is released while the player is ascending, sets it's Y speed by a quarter.
			/// </summary>
			protected override void onEnding(){
				if(rigidBody.velocity.y > 0)
					rigidBody.velocity = new Vector2(rigidBody.velocity.x,rigidBody.velocity.y/4);
			}

		}
	}
}