/// <summary>
/// SCRIPT   NAME: Input listener
/// CREATION DATE: 22/08/15
/// EDTION   DATE: 06/09/15
/// AUTHOR       : Alexandre "CaRaCrAzY" Petrassi Cardoso
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

/// <summary>
/// Manages input events like "onAxisDown", "onAxisUp", "onAxisDoubleTap" using Unity's InputManager information
/// </summary>
namespace CaRaCrAzY{
	namespace InputManagement{
		public class InputListener : Singleton<InputListener> {

			// Fields
			[Tooltip("Axes that will have its events listened by this script")]
			[SerializeField] Axis[] axes;

			// Methods
			void Awake(){
				DontDestroyOnLoad(gameObject);
				foreach(Axis axis in axes)
					axis.name = axis.axis.ToString();
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
			public static Axis getAxis(Axes axis){
				foreach(Axis _axis in Instance.axes)
					if(_axis.axis == axis) return _axis;
				return null;
			}
		}

		/// <summary>
		/// Axis possible states
		/// </summary>
		public enum State{
			starting,
			started,
			ending,
			ended
		}

		/// <summary>
		/// Axis listened
		/// </summary>
		[System.Serializable]
		public class Axis{

			// Fields
			[HideInInspector] public string name;

			[SerializeField]  Axes myAxis;
			public Axes axis{
				get{
					return myAxis;
				}
			}
			[Tooltip("Tolerance time for triggering an OnDoubleTap event between the last two key presses")]
			[SerializeField] float doubleTapMaxInterval = 0.4f;
			// Axis' state
			State myState;
			public State state{
				get{
					return myState;
				}
			}

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
						myState = State.started;
					}else{
						myState = State.ending;
						lastTapTime = Time.time;
					}
				}else{
					if(isPressed){
						myState = State.starting;
					}else{
						myState = State.ended;
					}
				}
			}

			/// <summary>
			/// Returns true if this axis is on the specified state
			/// </summary>
			public bool getState(State axisState){
				return myState == axisState;
			}

			/// <summary>
			/// returns true when this axis is registering a double tap event
			/// </summary>
			public bool onDoubleTap(){
				return (getState(State.starting) && lastTapTime - previousTapTime < doubleTapMaxInterval);
			}

		}
	}
}
