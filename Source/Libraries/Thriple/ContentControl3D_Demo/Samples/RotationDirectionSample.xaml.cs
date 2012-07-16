using System;
using System.Windows.Controls;
using System.Windows.Input;
using Thriple.Controls;

namespace ContentControl3D_Demo.Samples
{
	public partial class RotationDirectionSample : UserControl, ISample
	{
		public RotationDirectionSample()
		{
			InitializeComponent();

			base.DataContext = Enum.GetValues(typeof(RotationDirection));
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
					"This sample shows how to specify in which direction ContentControl3D " +
					"rotates, by setting the RotationDirection property.";
			}
		}
	}
}