/// <summary>
/// SCRIPT   NAME: Jump
/// CREATION DATE: 22/08/15
/// EDTION   DATE: 29/08/15
/// AUTHOR       : Alexandre "CaRaCrAzY" "Fireblizzard" Petrassi Cardoso
/// </summary>

using UnityEngine;
using System.Collections;
using Fireblizzard.InputManagement;

namespace Fireblizzard{
	namespace Player{
		[RequireComponent(typeof(Rigidbody2D))]
		public class Jump : MonoBehaviour{

			//[SerializeField] string actiavationAxisName;
			//[SerializeField] 

			[SerializeField] float jumpPower = 10;
			[SerializeField] int   maxJump = 2;
			int jumpCount = 0;

			Rigidbody2D rigidBody;
			BodyCollision bodyCollision;

			// Use this for initialization
			void Start () {
				rigidBody = GetComponent<Rigidbody2D>();
				bodyCollision = GetComponent<BodyCollision>();
			}
			
			// Update is called once per frame
			void FixedUpdate () {
				update();
				if(condition())
					execute();
			}

			void execute(){
				rigidBody.velocity = new Vector2(rigidBody.velocity.x,jumpPower);
				++jumpCount;
			}

			bool condition(){
				if(!(InputListener.getAxis("Jump").onDoubleTap())) return false;
				if(jumpCount >= maxJump) return false;
				return true;
			}

			void update(){
				if(!bodyCollision.foot.isColliding){
					if(jumpCount == 0) jumpCount = 1;
				}else{
					jumpCount = 0;
				}
			}
		}
	}
}