using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace ContentControl3D_Demo.Samples
{
	public partial class BindingToViewModelSample : UserControl, ISample
	{
		public BindingToViewModelSample()
		{
			InitializeComponent();

			base.DataContext = new PersonViewModel(
				"Josh",
				"Smith",
				"Images/JoshSmith.jpg",
				28,
				"Johann Sebastian Bach");
		}

		public string Description
		{
			get
			{
				return 
					"This sample shows how to bind both sides of ContentControl3D to a ViewModel object. " +
					"It disables the rotation button by binding the CanRotate property to the PersonViewModel's " +
					"IsValid property.  The back side of the surface is an edit form, with input validation.";
			}
		}
	}

	public class PersonViewModel : INotifyPropertyChanged, IDataErrorInfo
	{
		#region Fields

		string _age;
		string _favoriteComposer;
		string _firstName;
		bool _isValidating;
		string _lastName;

		static readonly string[] ValidatedProperties = new string[] 
		{
			"Age",
			"FirstName", 
			"LastName"			
		};

		#endregion // Fields

		#region Constructor

		public PersonViewModel(string firstName, string lastName, string photoPath, int age, string favoriteComposer)
		{
			this.FirstName = firstName;
			this.LastName = lastName;
			this.PhotoUri = new Uri(photoPath, UriKind.Relative);
			this.Age = age.ToString();
			this.FavoriteComposer = favoriteComposer;
		}

		#endregion // Constructor

		#region Properties

		public string Age
		{
			get { return _age; }
			set
			{
				if (value == _age)
					return;

				_age = value;

				this.OnPropertyChanged("Age");
			}
		}

		public string FavoriteComposer
		{
			get { return _favoriteComposer; }
			set
			{
				if (value == _favoriteComposer)
					return;

				_favoriteComposer = value;

				this.OnPropertyChanged("FavoriteComposer");
			}
		}

		public string FirstName
		{
			get { return _firstName; }
			set
			{
				if (value == _firstName)
					return;

				_firstName = value;

				this.OnPropertyChanged("FirstName");
				this.OnPropertyChanged("FullName");
			}
		}

		public bool IsValid
		{
			get
			{
				try
				{
					_isValidating = true;

					foreach (string propertyName in ValidatedProperties)
						if ((this as IDataErrorInfo)[propertyName] != null)
							return false;
				}
				finally
				{
					_isValidating = false;
				}

				return true;
			}
		}

		public string LastName
		{
			get { return _lastName; }
			set
			{
				if (value == _lastName)
					return;

				_lastName = value;

				this.OnPropertyChanged("LastName");
				this.OnPropertyChanged("FullName");
			}
		}

		public string FullName
		{
			get { return String.Format("{0}, {1}", this.LastName, this.FirstName); }
		}

		public Uri PhotoUri { get; private set; }

		#endregion // Properties

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion // INotifyPropertyChanged Members

		#region IDataErrorInfo Members

		public string Error
		{
			get { return null; }
		}

		public string this[string propertyName]
		{
			get
			{
				string error = null;
				switch (propertyName)
				{
					case "FirstName":
						if (this.FirstName == null || this.FirstName.Trim().Length == 0)
							error = "First name is missing.";
						break;

					case "LastName":
						if (this.LastName == null || this.LastName.Trim().Length == 0)
							error = "Last name is missing.";
						break;

					case "Age":
						int age;
						if (Int32.TryParse(this.Age, out age))
						{
							if (age < 0 || 120 < age)
								error = "Age must be between 0 and 120.";
						}
						else
						{
							error = "Age must be a number.";
						}
						break;
				}

				if (!_isValidating)
					this.OnPropertyChanged("IsValid");

				return error;
			}
		}

		#endregion // IDataErrorInfo Members
	}
}