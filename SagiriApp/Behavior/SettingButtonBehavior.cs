using System;
using System.Windows.Controls;

using Microsoft.Xaml.Behaviors;

using SagiriApp.Views;

namespace SagiriApp.Behavior
{
    class SettingButtonBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Click += _OnClick;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.Click -= this._OnClick;
        }

        private void _OnClick(object sender, EventArgs e) => SettingWindow.GetInstance?.Show();
    }
}
