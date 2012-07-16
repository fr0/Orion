using System.Windows.Controls;

namespace ContentControl3D_Demo.Samples
{
	public partial class DirectContentSample : UserControl, ISample
	{
		public DirectContentSample()
		{
			InitializeComponent();
		}

		public string Description
		{
			get 
			{ 
				return 
					"This sample shows how to add visual elements directly to both sides of ContentControl3D," +
					"via the Content and BackContent properties."; 
			}
		}
	}
}