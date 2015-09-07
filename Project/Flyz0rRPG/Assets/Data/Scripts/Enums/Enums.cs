/// <summary>
/// SCRIPT   NAME: Enums
/// CREATION DATE: 04/08/15
/// EDTION   DATE: 07/09/15
/// AUTHOR       : Alexandre "CaRaCrAzY" Petrassi Cardoso
/// </summary>

using UnityEngine;
using System.Collections;
/// <summary>
/// This script defines enums to be used throughly the code preventing string hardcoding to ensure code safety
/// </summary>
namespace CaRaCrAzY{
	public enum Tags{
		Player,
		Enemy,
		Platform
	}

	namespace InputManagement{
		public enum Axes{
			Shift,
			Ctrl,
			Spacebar,
			Horizontal,
			Vertical,
			Enter,
			Escape,
			_1,_2,_3,_4,_5,_6,_7,_8,_9,_0
		}
	}
}
