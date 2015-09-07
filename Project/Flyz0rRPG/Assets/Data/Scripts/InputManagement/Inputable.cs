/// <summary>
/// SCRIPT   NAME: Inputable
/// CREATION DATE: 29/08/15
/// EDTION   DATE: 07/09/15
/// AUTHOR       : Alexandre "CaRaCrAzY" Petrassi Cardoso
/// </summary>

using UnityEngine;
using System.Collections;
using CaRaCrAzY.InputManagement;

/// <summary>
/// Inputable.
/// This script defines an Inputable component. That means it can handle input from a 
/// virtual axis and manage its different events
/// </summary>
namespace CaRaCrAzY{
	namespace InputManagement{
		public abstract class Inputable : MonoBehaviour {

			[Tooltip("Virtual Axis used to perform this action")]
			[SerializeField] Axes key;
			Axis _axis; // Eixo associado
			public Axes axis{
				set{
					_axis = InputListener.getAxis(key);
				}
			}

			/// <summary>
			/// Called at initialization 
			/// </summary>
			void Start(){
				axis = key; // Sets the axis based on the key name
				start();    // Calls the start() method from the subclass as if it was a MonoBehaviour's Start()
			}

			/// <summary>
			/// FixedUpdate, called every frame, inhenrited from MonoBehaviour
			/// </summary>
			void FixedUpdate(){
				fixedUpdate();
				switchEvent();
				postFixedUpdate();
			}

			/// <summary>
			/// Switches to the right event for the current frame
			/// </summary>
			void switchEvent(){
				if(trigger()){
					switch(_axis.state){
					case State.starting: onStarting(); return;
					case State.started:  onStarted();  return;
					case State.ending:   onEnding();   return;
					default:             onEnded();    return;
					}
				}else{
					switch(_axis.state){
					case State.starting: onNegativeStarting(); return;
					case State.started:  onNegativeStarted();  return;
					case State.ending:   onNegativeEnding();   return;
					default:             onNegativeEnded();    return;
					}
				}
			}

			/// <summary>
			/// Used as the Start() method from a MonoBehaviour
			/// </summary>
			protected virtual void start(){}

			/// <summary>
			/// Used as the FixedUpdate() method from a MonoBehaviour
			/// Called every frame before input events
			/// </summary>
			protected virtual void fixedUpdate(){}

			/// <summary>
			/// Used as the FixedUpdate() method from a MonoBehaviour
			/// Called every frame after input events
			/// </summary>
			protected virtual void postFixedUpdate(){}

			/// <summary>
			/// Defines a condition to switch between events that indepent from axis input
			/// ex. Jump requires input, but for the jump to be triggered the player must touch a platform.
			/// </summary>
			protected virtual bool trigger(){return true;}

			/// <summary>
			/// Event called the instant the key is pressed if the trigger is true
			/// </summary>
			protected virtual void onStarting(){}

			/// <summary>
			/// Event called while the key is pressed if the trigger is true
			/// </summary>
			protected virtual void onStarted(){}

			/// <summary>
			/// Event called the instant the key is released if the trigger is true
			/// </summary>
			protected virtual void onEnding(){}

			/// <summary>
			/// Event called while the key is not pressed if the trigger is true
			/// </summary>
			protected virtual void onEnded(){}

			/// <summary>
			/// Event called the instant the key is pressed if the trigger is false
			/// </summary>
			protected virtual void onNegativeStarting(){}

			/// <summary>
			/// Event called while the key is pressed if the trigger is false
			/// </summary>
			protected virtual void onNegativeStarted(){}

			/// <summary>
			/// Event called the instant the key is released if the trigger is false
			/// </summary>
			protected virtual void onNegativeEnding(){}

			/// <summary>
			/// Event called while the key is not pressed if the trigger is false
			/// </summary>
			protected virtual void onNegativeEnded(){}
		}
	}
}