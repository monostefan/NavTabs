using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace NavTabs
{
    public partial class TabLayout : Grid
    {
        public event EventHandler<int> TabChanged;

        private Color inactiveColor = Color.FromHex("#001155");
        public Color InactiveColor
        {
            get { return inactiveColor; }
            set { inactiveColor = value; }
        }

        private Color activeColor = Color.FromHex("#1781e3");
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
                FontSize = 17,
                TextColor = column == 0 ? ActiveColor : InactiveColor,
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
                label.TextColor = InactiveColor;
            }

            selectedLabel.TextColor = ActiveColor;

            TabChanged?.Invoke(this, column);

            ViewExtensions.CancelAnimations(activeBox);
            activeBox.TranslateTo(selectedLabel.X, 0, 100, Easing.CubicInOut);
        }
    }
}
