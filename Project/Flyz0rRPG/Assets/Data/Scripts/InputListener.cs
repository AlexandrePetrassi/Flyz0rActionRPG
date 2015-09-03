/// <summary>
/// SCRIPT   NAME: Input listener
/// CREATION DATE: 22/08/15
/// EDTION   DATE: 29/08/15
/// AUTHOR       : Alexandre "CaRaCrAzY" "Fireblizzard" Petrassi Cardoso
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Fireblizzard{
	namespace InputManagement{
		/// <summary>
		/// Manages input events like "onAxisDown", "onAxisUp", "onAxisDoubleTap" using Unity's InputManager information
		/// </summary>
		public class InputListener : Singleton<InputListener> {

			// Fields
			[Tooltip("Axes that will have its events listened by this script")]
			[SerializeField] Axis[] axes;

			// Methods
			void Awake(){
				DontDestroyOnLoad(gameObject);
			}

			/// <summary>
			/// Iterate through axes listening events
			/// </summary>
			void FixedUpdate () {
				foreach(Axis axis in axes)
					axis.update();
			}

			/// <summary>
			/// Returns an axis
			/// </summary>
			public static Axis getAxis(string axisName){
				foreach(Axis axis in Instance.axes)
					if(axis.name == axisName) return axis;
				return null;
			}
		}

		/// <summary>
		/// Axis possible states
		/// </summary>
		public enum AxisState{
			onAxisUnpressed,
			onAxisDown,
			onAxisPressed,
			onAxisUp,
		}

		/// <summary>
		/// Axis listened
		/// </summary>
		[System.Serializable]
		public class Axis{

			// Fields
			[Tooltip("This name MUST match the InputManager Axis' name")]
			[SerializeField] public string name;
			[Tooltip("Tolerance time for triggering an OnDoubleTap event between the last two key presses")]
			[SerializeField] float doubleTapMaxInterval = 0.4f;
			// Axis' state
			AxisState state;
			// Flags indicating this axis raw state in the current and previous frame respectively
			bool isPressed, wasPressed;
			// Moment in time that the last two taps occurred
			float _lastTapTime, previousTapTime;
			float lastTapTime{
				get{
					return _lastTapTime;
				}
				set{
					previousTapTime = _lastTapTime;
					_lastTapTime = value;
				}
			}

			// Methods

			/// <summary>
			/// Update this axis information about events
			/// </summary>
			public void update(){
				wasPressed = isPressed;
				isPressed = (Input.GetAxis(name) != 0);
				updateState();
			}

			/// <summary>
			/// Updates this axis state based in the raw states
			/// </summary>
			void updateState(){
				if(wasPressed){
					if(isPressed){
						state = AxisState.onAxisPressed;
					}else{
						state = AxisState.onAxisUp;
						lastTapTime = Time.time;
					}
				}else{
					if(isPressed){
						state = AxisState.onAxisDown;
					}else{
						state = AxisState.onAxisUnpressed;
					}
				}
			}

			/// <summary>
			/// Returns true if this axis is on the specified state
			/// </summary>
			public bool getState(AxisState axisState){
				return state == axisState;
			}

			/// <summary>
			/// returns true when this axis is registering a double tap event
			/// </summary>
			public bool onDoubleTap(){
				return (getState(AxisState.onAxisUp) && lastTapTime - previousTapTime < doubleTapMaxInterval);
			}

		}
	}
}
