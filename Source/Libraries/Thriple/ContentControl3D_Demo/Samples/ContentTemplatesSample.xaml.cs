using System.Windows.Controls;

namespace ContentControl3D_Demo.Samples
{
	public partial class ContentTemplatesSample : UserControl, ISample
	{
		public ContentTemplatesSample()
		{
			InitializeComponent();
		}

		public string Description
		{
			get
			{
				return
					"This sample shows how to use data objects as the BackContent and Content " +
					"of ContentControl3D, and then provide DataTemplates to the BackContentTemplate " +
					"and ContentTemplate properties to specify how to display those data objects.";
			}
		}
	}
}