using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace NavTabs
{
    public partial class TabsView : Grid
    {
		private static readonly Color activeColor = Color.FromHex("#001155");
		private static readonly Color inactiveColor = Color.FromHex("#bbbbbb");
		private static readonly Color currentTabColor = Color.FromHex("#1781e3");

        public event EventHandler<int> TabChanged;

        private readonly List<string> titles;

		private Label currentTab;

		private bool isActive = true;
		public bool IsActive
		{
			get { return isActive; }
			set
			{
				this.isActive = value;

				foreach (var label in Children.OfType<Label>().Where(l => l != currentTab))
				{
					label.TextColor = isActive ? activeColor : inactiveColor;
				}
			}
		}

        public IEnumerable<string> Titles
        {
            get { return titles; }
        }

        public TabsView()
        {
            this.titles = new List<string>();

            InitializeComponent();

            activeBox.BackgroundColor = currentTabColor;
        }

        public void AddTab(string title)
        {
            titles.Add(title);

            int column = titles.IndexOf(title);

            var tabLabel = new Label
            {
                Text = title,
                FontSize = 17,
                TextColor = column == 0 ? currentTabColor : activeColor,
                VerticalOptions = LayoutOptions.Fill,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            tabLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => OnTabSelected(column, tabLabel))
            });

			if (currentTab == null)
				currentTab = tabLabel;

            SetColumnSpan(seperator, titles.Count);

            Children.Add(tabLabel, column, 0);
        }

        public void RemoveTab(string title)
        {
            titles.Remove(title);

            var labelsToRemove = Children.OfType<Label>().Where(l => l.Text == title).ToArray();
            foreach (var tabLabel in labelsToRemove)
            {
                tabLabel.GestureRecognizers.RemoveAt(0);
                Children.Remove(tabLabel);
            }

            SetColumnSpan(seperator, titles.Count);
        }

        private void OnTabSelected(int column, Label selectedLabel)
        {
			if (!IsActive)
				return;

            foreach (var label in Children.OfType<Label>())
            {
                label.TextColor = activeColor;
            }

			currentTab = selectedLabel;

			currentTab.TextColor = currentTabColor;

            TabChanged?.Invoke(this, column);

            ViewExtensions.CancelAnimations(activeBox);
            activeBox.TranslateTo(selectedLabel.X, 0, 100, Easing.CubicInOut);
        }
    }
}
