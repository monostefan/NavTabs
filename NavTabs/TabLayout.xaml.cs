using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace NavTabs
{
	public partial class TabLayout : Grid
	{
		public event EventHandler<int> TabChanged;

		private Color inactiveColor = Color.Black;
		public Color InactiveColor
		{
			get { return inactiveColor; }
			set { inactiveColor = value; }
		}

		private Color activeColor = Color.Blue;
		public Color ActiveColor
		{
			get { return activeColor; }
			set { activeBox.BackgroundColor = activeColor = value; }
		}

		private readonly List<string> titles;

		public IEnumerable<string> Titles
		{
			get { return titles; }
		}

		public TabLayout()
		{
			this.titles = new List<string>();

			InitializeComponent();

			activeBox.BackgroundColor = ActiveColor;
		}

		public void AddTab(string title)
		{
			titles.Add(title);

			int column = titles.IndexOf(title);

			var tabLabel = new Label
			{
				Text = title,
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = column == 0 ? ActiveColor : InactiveColor
			};

			tabLabel.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command(() => OnTabSelected(column, tabLabel))
			});

			Children.Add(tabLabel, column, 0);
		}

		private void OnTabSelected(int column, Label selectedLabel)
		{
			foreach (var label in Children.OfType<Label>())
			{
				label.TextColor = InactiveColor;
			}

			selectedLabel.TextColor = ActiveColor;

			TabChanged?.Invoke(this, column);

			ViewExtensions.CancelAnimations(activeBox);
			activeBox.TranslateTo(selectedLabel.X, 0, 100, Easing.CubicInOut);
		}
	}
}
