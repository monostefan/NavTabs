using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace NavTabs
{
    public partial class TabLayout : Grid
    {
        private static readonly Color inactiveColor = Color.FromHex("#001155");
        private static readonly Color activeColor = Color.FromHex("#1781e3");

        public event EventHandler<int> TabChanged;

        private readonly List<string> titles;

        public IEnumerable<string> Titles
        {
            get { return titles; }
        }

        public TabLayout()
        {
            this.titles = new List<string>();

            InitializeComponent();

            activeBox.BackgroundColor = activeColor;
        }

        public void AddTab(string title)
        {
            titles.Add(title);

            int column = titles.IndexOf(title);

            var tabLabel = new Label
            {
                Text = title,
                FontSize = 17,
                TextColor = column == 0 ? activeColor : inactiveColor,
                VerticalOptions = LayoutOptions.Fill,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            tabLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => OnTabSelected(column, tabLabel))
            });

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
            foreach (var label in Children.OfType<Label>())
            {
                label.TextColor = inactiveColor;
            }

            selectedLabel.TextColor = activeColor;

            TabChanged?.Invoke(this, column);

            ViewExtensions.CancelAnimations(activeBox);
            activeBox.TranslateTo(selectedLabel.X, 0, 100, Easing.CubicInOut);
        }
    }
}
