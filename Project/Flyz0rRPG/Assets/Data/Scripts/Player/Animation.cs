/// <summary>
/// SCRIPT   NAME: Animation
/// CREATION DATE: 22/08/15
/// EDTION   DATE: 28/08/15
/// AUTHOR       : Alexandre "CaRaCrAzY" Petrassi Cardoso
/// </summary>

using UnityEngine;
using System.Collections;
using CaRaCrAzY.CollisionManagement;

namespace CaRaCrAzY{
	namespace Player{
		[RequireComponent(typeof(Animator))]
		[RequireComponent(typeof(Rigidbody2D))]
		public class Animation : MonoBehaviour {

			Animator animator;
			Rigidbody2D rigidBody;

			// Use this for initialization
			void Start () {
				animator = GetComponent<Animator>();
				rigidBody = GetComponent<Rigidbody2D>();
			}
			
			// Update is called once per frame
			void Update () {
				setDirection();
				setAnimatorFlags();
			}

			void setAnimatorFlags(){
				animator.SetInteger("Velocity_X",(int)rigidBody.velocity.x);
				animator.SetInteger("Velocity_Y",(int)rigidBody.velocity.y);
			}

			void setDirection(){
				if(rigidBody.velocity.x == 0) return;
				float velocitySign = Mathf.Sign(rigidBody.velocity.x);
				float scaleSign = Mathf.Sign(transform.localScale.x);
				if(velocitySign != scaleSign) 
					transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1,1,1));
			}
		}
	}
}