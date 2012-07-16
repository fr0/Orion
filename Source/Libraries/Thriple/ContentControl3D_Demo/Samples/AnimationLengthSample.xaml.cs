using System.Windows.Controls;

namespace ContentControl3D_Demo.Samples
{
	public partial class AnimationLengthSample : UserControl, ISample
	{
		public AnimationLengthSample()
		{
			InitializeComponent();
		}

		public string Description
		{
			get
			{
				return 
					"This sample allows you to experiment with various values for the AnimationLength " +
					"property, which represents the duration of an animated rotation, in milliseconds.";
			}
		}
	}
}