namespace Thriple.Controls
{
	public enum RotationEasingMode
	{
		/// <summary>
		/// No easing mode is used.
		/// </summary> 
		None,

		/// <summary>
		/// The surface dips back on its edge. 
		/// </summary>
		Dip,

		/// <summary>
		/// The surface shakes as if it is being electrified.
		/// </summary>
		Electrified,

		/// <summary>
		/// The surface goes back smoothly, and then quickly rotates as it approaches the user.
		/// </summary>
		Pounce,

		/// <summary>
		/// The surface goes back smoothly, quickly rotates, and then approaches the user smoothly.
		/// </summary>
		RoundhouseKick,

		/// <summary>
		/// The surface quickly rotates, then goes back and approaches to the user smoothly.
		/// </summary> 
		Slap,

		/// <summary>
		/// The surface rotates very quickly, and swings back into place. 
		/// </summary>
		Whiplash
	}
}