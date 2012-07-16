using System;
using System.Windows.Controls;
using Thriple.Controls;

namespace ContentControl3D_Demo.Samples
{
	public partial class PropertyExplorerSample : UserControl, ISample
	{
		public PropertyExplorerSample()
		{
			InitializeComponent();
			this.rotationDirectionSelector.ItemsSource = Enum.GetValues(typeof(RotationDirection));
			this.easingModeSelector.ItemsSource = Enum.GetValues(typeof(RotationEasingMode));
		}

		public string Description
		{
			get
			{
				return
					"This sample allows you to experiment with several properties of ContentControl3D " +
					"to see how the various values can work together to create different visual effects.";
			}
		}
	}
}