using System;
using Reactive.Bindings.Interactivity;

namespace SagiriApp.Interop
{
    public class MouseEventConverter : DelegateConverter<EventArgs, (object sender, EventArgs e)>
    {
        protected override (object sender, EventArgs e) OnConvert(EventArgs source) => (AssociateObject, source);
    }
}
