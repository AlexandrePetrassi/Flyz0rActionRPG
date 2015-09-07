/// <summary>
/// SCRIPT   NAME: Body Part
/// CREATION DATE: 28/08/15
/// EDTION   DATE: 07/09/15
/// AUTHOR       : Alexandre "CaRaCrAzY" Petrassi Cardoso
/// </summary>

using UnityEngine;
using System.Collections;

/// <summary>
/// Body part.
/// Handles a especific body part's collision flags
/// 
/// IMPORTANT NOTE: this script MUST BE configured to run its update function BEFORE the 
/// "Inputable.cs" script at the tab Edit>ProjectSettings>ScriptExecutionOrder, otherwise it will not function properly
/// </summary>
namespace CaRaCrAzY{
	namespace CollisionManagement{
		public class BodyPart : MonoBehaviour {

			bool _isColliding; // Marked as false every frame. Marked as true if a collision occured
			public bool isColliding{
				get{
					return _isColliding;
				}
			}

			/// <summary>
			/// Sets the collision flag as true if a collision above a platform occured
			/// </summary>
			void OnTriggerStay2D(Collider2D col){
				if(col.tag == Tags.Platform.ToString())
					_isColliding = this.isAbove(col.transform);
			}
			
			/// <summary>
			/// Sets the flag as false every frame.
			/// If this object is still colliding, the flag will be set again as true by 
			/// trigger, except if the execution order isn't set properly (Read IMPORTANT NOTE at the top)
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
