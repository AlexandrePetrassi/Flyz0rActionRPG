/// <summary>
/// SCRIPT   NAME: Body Part
/// CREATION DATE: 28/08/15
/// EDTION   DATE: 28/08/15
/// AUTHOR       : Alexandre "CaRaCrAzY" "Fireblizzard" Petrassi Cardoso
/// </summary>

using UnityEngine;
using System.Collections;

/// <summary>
/// Body part.
/// Handles a especific body part's collision flags from the object its attached
/// 
/// IMPORTANT NOTE: this script MUST BE configured to run its update function BEFORE the 
/// BodyCollision script at the tab Edit>ProjectSettings>ScriptExecutionOrder, otherwise it will not function properly
/// </summary>
namespace Fireblizzard{
	namespace Player{
		public class BodyPart : MonoBehaviour {
			
			[SerializeField]bool _isColliding; // Setado como false a cada frame pelo BodyCollision
			public bool isColliding{
				get{
					return _isColliding;
				}
			}
			
			void OnTriggerStay2D(Collider2D col){
				if(col.tag == "Platform")
					_isColliding = this.isAbove(col.transform);
			}
			
			/// <summary>
			/// This object is always updated by a BodyCollision script attached to a parent gameObject
			/// </summary>
			public void FixedUpdate(){
				_isColliding = false;
			}
			
			/// <summary>
			/// Returns true if this object is above the platform
			/// </summary>
			bool isAbove(Transform platform){
				return (platform.position.y < transform.position.y);
			}
		}
	}
}
