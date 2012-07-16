using System.Windows.Controls;

namespace ContentControl3D_Demo.Samples
{
	public partial class ZoomDestinationSample : UserControl, ISample
	{
		public ZoomDestinationSample()
		{
			InitializeComponent();
		}

		public string Description
		{
			get
			{
				return
					"This sample shows how to customize the animated rotation, by specifying " +
					"where the 3D scene's camera should move to.  The camera reaches its zoom " +
					"destination halfway through the rotation, and then returns to its original " +
					"location by the end of the animation.";
			}
		}
	}
}