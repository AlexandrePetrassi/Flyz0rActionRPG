/// <summary>
/// SCRIPT   NAME: Ability
/// CREATION DATE: 29/08/15
/// EDTION   DATE: 29/08/15
/// AUTHOR       : Alexandre "CaRaCrAzY" "Fireblizzard" Petrassi Cardoso
/// </summary>

using UnityEngine;
using System.Collections;

namespace Fireblizzard{
	namespace Player{
		public abstract class Ability : MonoBehaviour {

			bool _isTrue, wasTrue;
			bool isTrue{
				get{
					return _isTrue;
				}
				set{
					wasTrue = _isTrue;
					_isTrue = value;
				}
			}

			abilityState state{
				get{
					if(isTrue){
						if(wasTrue)	return abilityState.started;
						else		return abilityState.starting;
					}else{
						if(wasTrue)	return abilityState.ending;
						else		return abilityState.ended;
					}
				}
			}

			void FixedUpdate(){
				isTrue = condition();
				switchEvents();
			}

			bool switchEvents(){
				switch(state){
				case abilityState.starting:
					return onStarting();
				case abilityState.started:
					return onStarted();
				case abilityState.ending:
					return onEnding();
				default:
					return onEnded();
				}
			}

			protected virtual void update(){}

			protected virtual bool condition(){return false;}

			protected virtual bool onStarting(){return false;}

			protected virtual bool onStarted(){return false;}

			protected virtual bool onEnding(){return false;}

			protected virtual bool onEnded(){return false;}

			enum abilityState{
				starting,
				started,
				ending,
				ended
			}
		}
	}
}