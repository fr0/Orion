using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace WpfDisciplesBlogRoll3D
{
	public partial class BloggerDetailsControl : UserControl
	{
		public BloggerDetailsControl()
		{
			InitializeComponent();

			this.AddHandler(
				Hyperlink.RequestNavigateEvent,
				(RequestNavigateEventHandler)((sender, e) => Process.Start(e.Uri.OriginalString)));
		}
	}
}