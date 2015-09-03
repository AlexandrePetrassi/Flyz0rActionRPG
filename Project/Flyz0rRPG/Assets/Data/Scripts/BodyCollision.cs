/// <summary>
/// SCRIPT   NAME: Body Collision
/// CREATION DATE: 25/08/15
/// EDTION   DATE: 28/08/15
/// AUTHOR       : Alexandre "CaRaCrAzY" "Fireblizzard" Petrassi Cardoso
/// </summary>

using UnityEngine;
using System.Collections;

/// <summary>
/// Body Collision.
/// Handles the references of all body parts from the object
/// 
/// IMPORTANT NOTE: this script MUST BE configured to run its update function AFTER the 
/// BodyPart script at the tab Edit>ProjectSettings>ScriptExecutionOrder, otherwise it will not function properly
/// </summary>
namespace Fireblizzard{
	namespace Player{
		public class BodyCollision : MonoBehaviour {

			[SerializeField] BodyPart[] bodyParts;

			// Use this for initialization
			void Start () {
				
			}
			
			// Update is called once per frame
			void FixedUpdate () {
				//foreach(BodyPart bodyPart in bodyParts)
					//bodyPart.update();
			}

			public BodyPart foot{
				get{
					return bodyParts[0];
				}
			}
		}
	}
}