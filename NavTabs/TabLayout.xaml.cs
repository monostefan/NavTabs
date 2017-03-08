using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace NavTabs
{
	public partial class TabLayout : Grid
	{
		public event EventHandler TabChanged;

		private readonly List<string> titles;

		public IEnumerable<string> Titles
		{
			get { return titles; }
		}

		public TabLayout()
		{
			this.titles = new List<string>();

			InitializeComponent();
		}

		protected override void OnSizeAllocated(double width, double height)
		{
			base.OnSizeAllocated(width, height);

			ResizeActiveBox();
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
				TextColor = column == 0 ? Color.Blue : Color.Black
			};

			tabLabel.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command(() => OnTabSelected(column, tabLabel))
			});

			Children.Add(tabLabel, column, 0);
		}

		void OnTabSelected(int column, Label selectedLabel)
		{
			foreach (var label in Children.OfType<Label>())
			{
				label.TextColor = Color.Black;
			}

			selectedLabel.TextColor = Color.Blue;

			Children.Add(activeBox, column, 1);
		}

		private void ResizeActiveBox()
		{
			if (Width < 0 || titles == null)
				return;
			
			activeBox.WidthRequest = Width / titles.Count;
		}
	}
}
