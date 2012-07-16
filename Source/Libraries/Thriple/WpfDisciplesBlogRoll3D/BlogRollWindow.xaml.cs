using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using Thriple.Controls;
using Thriple.Panels;

namespace WpfDisciplesBlogRoll3D
{
	public partial class BlogRollWindow : Window
	{
		Panel3D _panel3D;
		readonly TimeSpan ANIMATION_LENGTH = TimeSpan.FromSeconds(.7);

		public BlogRollWindow()
		{
			InitializeComponent();

			this.listBox.PreviewKeyDown += new KeyEventHandler(listBox_PreviewKeyDown);
		}

		void listBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			// This is necessary to workaround a bug in WPF that arises
			// when you press the Tab key when keyboard focus is in the
			// ScrollViewer.  This bug does not arise under normal circumstances.
			e.Handled = e.Key == Key.Tab;
		}

		void OnPanel3DLoaded(object sender, RoutedEventArgs e)
		{
			// Grab a reference to the Panel3D when it loads.
			_panel3D = sender as Panel3D;
		}

		void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		void Open_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			// The ApplicationCommands.Open command is used for a variety of purposes here.
			// It's a dirty hack, but who cares...this is just a demo.

			var elem = e.OriginalSource as FrameworkElement;
			int childIndex = this.listBox.Items.IndexOf(elem.DataContext);
			int visibleIndex = _panel3D.GetVisibleIndexFromChildIndex(childIndex);
			if (0 < visibleIndex)
			{
				// The user clicked on an item that was not in the front of the scene,
				// so tell the Panel3D to animate it to the front.
				if (!_panel3D.IsMovingItems)
					this.MoveItems(visibleIndex, true);
			}
			else
			{
				XmlAttribute attr = e.Parameter as XmlAttribute;
				if (attr != null)
				{
					// The user clicked on the button that opens up a blog.
					string url = attr.Value;
					if (!String.IsNullOrEmpty(url))
						Process.Start(url);
				}
				else
				{
					// The user clicked on a blogger's image, so rotate the item.
					ContentControl3D.RotateCommand.Execute(null, elem);
				}
			}
		}

		void OnListBoxItemUnselected(object sender, RoutedEventArgs e)
		{
			// When the user brings a new ListBoxItem to the front of the 3D scene
			// it becomes the selected item in the ListBox.  At that time, we must
			// verify that the previously selected item's front side is facing forward.
			DependencyObject depObj = e.OriginalSource as DependencyObject;
			while (depObj != null)
			{
				if (VisualTreeHelper.GetChildrenCount(depObj) == 0)
					break;

				depObj = VisualTreeHelper.GetChild(depObj, 0);
				ContentControl3D cc3D = depObj as ContentControl3D;
				if (cc3D != null)
				{
					cc3D.BringFrontSideIntoView();
					break;
				}
			}
		}

		void MoveForwardButtonClicked(object sender, RoutedEventArgs e)
		{
			this.MoveItems(1, true);
		}

		void MoveBackButtonClicked(object sender, RoutedEventArgs e)
		{
			this.MoveItems(1, false);
		}

		void PageForwardButtonClicked(object sender, RoutedEventArgs e)
		{
			this.MoveItems(3, true);
		}

		void PageBackButtonClicked(object sender, RoutedEventArgs e)
		{
			this.MoveItems(3, false);
		}

		void MoveItems(int itemCount, bool forward)
		{
			_panel3D.MoveItems(itemCount, forward, ANIMATION_LENGTH);
		}
	}
}