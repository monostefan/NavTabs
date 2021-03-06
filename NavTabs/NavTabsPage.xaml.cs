﻿using System;
using Xamarin.Forms;

namespace NavTabs
{
    public partial class NavTabsPage : ContentPage
    {
        public NavTabsPage()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.iOS)
                Padding = new Thickness(0, 20, 0, 0);

            tabLayout.AddTab("Von mir");
            tabLayout.AddTab("Für mich");
            tabLayout.AddTab("Für sie");
            tabLayout.RemoveTab("Für sie");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            tabLayout.TabChanged += OnTabChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            tabLayout.TabChanged -= OnTabChanged;
        }

        private void OnTabChanged(object sender, int tabId)
        {
            view1.IsVisible = tabId == 0;
            view2.IsVisible = tabId == 1;
        }

		private void SetTabActive(object sender, EventArgs args)
		{
			tabLayout.IsActive = true;
		}

		private void SetTabInactive(object sender, EventArgs args)
		{
			tabLayout.IsActive = false;
		}
    }
}
