using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using Thriple.Panels;

namespace Panel3D_Demo
{
    public partial class BoundWindow : Window
    {
        ObservableCollection<string> _dataItems;
        int _newItemCount = 0;
        Panel3D _panel3D;

        public BoundWindow()
        {
            InitializeComponent();

            this.ResetDataSource();

            this.itemLayoutDirectionEditor.AddHandler(Slider.ValueChangedEvent, (RoutedEventHandler)this.OnApplyItemLayoutDirection);

            this.listBox.AddHandler(Panel3D.ItemsHostLoadedEvent, (RoutedEventHandler)this.OnPanel3DLoaded);

            this.panelConfigSelector.ItemsSource = this.Resources.Keys;
            this.panelConfigSelector.SelectedItem = "Default";
            this.panelConfigSelector.SelectionChanged += panelConfigSelector_SelectionChanged;

            this.chkAutoAdjustOpacity.Click += new RoutedEventHandler(chkAutoAdjustOpacity_Click);
            this.chkAllowTransparency.Click += new RoutedEventHandler(chkAllowTransparency_Click);
        }

        void chkAutoAdjustOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (_panel3D != null)
                _panel3D.AutoAdjustOpacity = this.chkAutoAdjustOpacity.IsChecked ?? false;
        }

        void chkAllowTransparency_Click(object sender, RoutedEventArgs e)
        {
            if (_panel3D != null)
                _panel3D.AllowTransparency = this.chkAllowTransparency.IsChecked ?? false;
        }

        void panelConfigSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var panelConfig = this.TryFindResource(this.panelConfigSelector.SelectedItem) as ItemsPanelTemplate;
            if (panelConfig != null)
                this.listBox.ItemsPanel = panelConfig;
        }

        void OnPanel3DLoaded(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Panel3D)
            {
                // Grab a reference to the Panel3D when it loads.
                _panel3D = e.OriginalSource as Panel3D;
                _panel3D.AllowTransparency = this.chkAllowTransparency.IsChecked ?? false;
                _panel3D.AutoAdjustOpacity = this.chkAutoAdjustOpacity.IsChecked ?? false;

                this.itemLayoutDirectionEditor.DataContext = _panel3D.ItemLayoutDirection;
            }
        }

        void OnItemClick(object sender, RoutedEventArgs e)
        {
            if (this.chkMoveItemToFrontOnClick.IsChecked ?? false)
            {
                // Move the item that was clicked to the front of the Panel3D scene.
                var elem = e.Source as FrameworkElement;
                int childIndex = this.listBox.Items.IndexOf(elem.DataContext);
                int visibleIndex = _panel3D.GetVisibleIndexFromChildIndex(childIndex);
                if (0 < visibleIndex && !_panel3D.IsMovingItems)
                {
                    _panel3D.MoveItems(visibleIndex, true);
                }
            }
            else
            {
                MessageBox.Show("You clicked on " + (e.Source as Button).DataContext);
            }
        }

        void OnApplyItemLayoutDirection(object sender, RoutedEventArgs e)
        {
            Vector3D dir = new Vector3D(
                this.itemLayoutDirectionEditorX.Value,
                this.itemLayoutDirectionEditorY.Value,
                this.itemLayoutDirectionEditorZ.Value);

            _panel3D.ItemLayoutDirection = dir;

            this.itemLayoutDirectionValue.Text = String.Format(
                "{0},  {1},  {2}",
                dir.X.ToString("##.###"),
                dir.Y.ToString("##.###"),
                dir.Z.ToString("##.###"));
        }

        #region Navigation Buttons

        void MoveForwardButtonClicked(object sender, RoutedEventArgs e)
        {
            if (_panel3D != null)
                _panel3D.MoveItems(1, true);
        }

        void MoveBackButtonClicked(object sender, RoutedEventArgs e)
        {
            if (_panel3D != null)
                _panel3D.MoveItems(1, false);
        }

        void PageForwardButtonClicked(object sender, RoutedEventArgs e)
        {
            if (_panel3D != null)
                _panel3D.MoveItems(3, true);
        }

        void PageBackButtonClicked(object sender, RoutedEventArgs e)
        {
            if (_panel3D != null)
                _panel3D.MoveItems(3, false);
        }

        #endregion // Navigation Buttons

        #region Data Source Buttons

        void AddItemButtonClicked(object sender, RoutedEventArgs e)
        {
            string newValue = "item " + _newItemCount++;
            _dataItems.Add(newValue);

            this.UpdateSlider();
        }

        void Append100ItemsButtonClicked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; ++i)
            {
                string newValue = "item " + _newItemCount++;
                _dataItems.Add(newValue);
            }

            this.UpdateSlider();
        }

        void InsertItemButtonClicked(object sender, RoutedEventArgs e)
        {
            if (0 < _dataItems.Count)
            {
                string newValue = "item " + _newItemCount++;
                _dataItems.Insert(1, newValue);
            }

            this.UpdateSlider();
        }

        void RemoveItemButtonClicked(object sender, RoutedEventArgs e)
        {
            if (0 < _dataItems.Count)
                _dataItems.RemoveAt(0);

            this.UpdateSlider();
        }

        void ResetDataSourceButtonClicked(object sender, RoutedEventArgs e)
        {
            this.ResetDataSource();
        }

        #endregion // Data Source Buttons

        #region Helper Methods

        void ResetDataSource()
        {
            this.Cursor = Cursors.Wait;

            _newItemCount = 0;

            if (_dataItems == null)
            {
                _dataItems = new ObservableCollection<string>();
                base.DataContext = _dataItems;
            }
            else
            {
                _dataItems.Clear();
            }

            this.UpdateSlider();

            this.Cursor = Cursors.Arrow;
        }

        void UpdateSlider()
        {
            this.selectedIndexSlider.Maximum = _dataItems.Count - 1;
        }

        #endregion // Helper Methods
    }
}