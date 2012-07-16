using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Thriple.Controls.Internal
{
	[ValueConversion(typeof(DependencyObject), typeof(DependencyObject))]
	internal class ContentValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// This horrendous hack is necessary because the Content/BackContent of a ContentControl3D
			// is a logical child of the ContentControl3D, not the ContentPresenters that host the content.
			// In order to inform the Content/BackContent elements of which side they reside on, we must
			// attach the IsOnFrontSide property value to them via a value converter that is set on the
			// TemplateBinding of the ContentPresenters' Content property.  If we do not do this, then the
			// IsOnFrontSide property value does not get inherited by the elements shown in the control.
			// This is important when BringIntoView is called on an element in the ContentControl3D and we
			// need to figure out on which side of the control it exists.  This workaround is only for
			// the Content and BackContent, but not the elements created by the ContentTemplate or
			// BackContentTemplate properties.

			DependencyObject depObj = value as DependencyObject;
			string side = parameter as string;
			if (depObj != null && !String.IsNullOrEmpty(side))
			{
				bool? isOnFrontSide = side == "FRONT";
				ContentControl3D.SetIsOnFrontSide(depObj, isOnFrontSide);
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}