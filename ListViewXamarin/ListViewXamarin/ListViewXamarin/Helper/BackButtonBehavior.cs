
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class BackButtonBehavior : Behavior<Button>
    {
        protected override void OnAttachedTo(Button bindable)
        {
            bindable.Clicked += Bindable_Clicked;
            base.OnAttachedTo(bindable);
        }

        private void Bindable_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopAsync();
        }

        protected override void OnDetachingFrom(Button bindable)
        {
            bindable.Clicked -= Bindable_Clicked;
        }
    }
}
