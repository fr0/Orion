using System;
using System.Windows.Controls;
using System.Windows.Input;
using Thriple.Controls;

namespace ContentControl3D_Demo.Samples
{
	public partial class EasingModesSample : UserControl, ISample
	{
		public EasingModesSample()
		{
			InitializeComponent();

			base.DataContext = Enum.GetValues(typeof(RotationEasingMode));
		}

		void ListBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			// This is necessary to workaround a bug in WPF that arises
			// when you press the Tab key when keyboard focus is in the
			// ScrollViewer.  This bug does not arise under normal circumstances.
			e.Handled = e.Key == Key.Tab;
		}

		public string Description
		{
			get
			{
				return
					"This sample shows that setting the EasingMode property affects how ContentControl3D rotates. " +
					"The default easing mode is 'None', meaning that an easing mode is not used.";
			}
		}
	}
}