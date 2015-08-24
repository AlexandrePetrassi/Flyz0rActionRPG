/// <summary>
/// SCRIPT   NAME: Input listener
/// CREATION DATE: 22/08/15
/// EDTION   DATE: 23/08/15
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
		public class InputListener : MonoBehaviour {

			// Fields
			[Tooltip("Axes that will have its events listened by this script")]
			[SerializeField] Axis[] axes;
			static InputListener self; // SelfReference

			// Methods

			/// <summary>
			/// Self reference itself
			/// </summary>
			void Start () {
				if(self != null) Destroy(gameObject);
				else self = this;
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
				foreach(Axis axis in self.axes)
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
			[Tooltip("Tolerance time for triggering an OnMultiTap event between the last four key presses")]
			[SerializeField] float multiTapMaxInterval = 1.2f;
			// Axis' state
			AxisState state;
			// Flags indicating this axis raw state in the current and previous frame respectively
			bool isPressed, wasPressed;
			// Moment in time that the last four taps occurred
			// these initial values are vaguely sparced to guarantee that a doubleTap or multiTap isn't registered at initialization
			float[] tapTime = new float[4]{-3000,-2000,-1000,-1};

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
						registerTapTime();
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
			/// Registers a new tap
			/// </summary>
			void registerTapTime(){
				tapTime[0] = tapTime[1];
				tapTime[1] = tapTime[2];
				tapTime[2] = tapTime[3];
				tapTime[3] = Time.time;
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
				return (getState(AxisState.onAxisUp) && tapTime[3] - tapTime[2] < doubleTapMaxInterval);
			}

			/// <summary>
			/// returns true when this axis is registering a multi tap event
			/// </summary>
			public bool onMultiTap(){
				return (Time.time - tapTime[0] < multiTapMaxInterval);
			}
		}
	}
}
