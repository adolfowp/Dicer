using System;

using Xamarin.Forms;

namespace Dicer.Views.DataTemplates
{
    public class SwitchableEntryCell : ViewCell
    {
        #region Fields
        readonly CheckBox enabler;
        readonly Entry valueEntry;
        #endregion
        public SwitchableEntryCell()
        {
            #region Switch
            enabler = new CheckBox
            {
                HorizontalOptions = LayoutOptions.Start,
            };

            #endregion
            #region Value Entry
            valueEntry = new Entry
            {
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.End,
            };
            #endregion

            var mainLayout = new Grid();
            mainLayout.Children.Add(enabler, 0, 0);
            mainLayout.Children.Add(valueEntry, 1, 0);

            View = mainLayout;
        }

        #region Binding Properties
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                propertyName: nameof(Text),
                returnType: typeof(string),
                declaringType: typeof(SwitchableEntryCell),
                defaultValue: string.Empty,
                propertyChanged: OnTextPropertyChanged);

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(
                propertyName: nameof(Value),
                returnType: typeof(decimal),
                declaringType: typeof(SwitchableEntryCell),
                defaultValue: string.Empty,
                propertyChanged: OnValuePropertyChanged);
        #endregion

        #region Properties
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public decimal Value
        {
            get { return (decimal)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        #endregion

        #region Methods
        static void OnTextPropertyChanged(BindableObject bindable,
                                            object oldValue, object newValue)
        {
            var control = bindable as SwitchableEntryCell;
            control.enabler.Text = (string)newValue;
        }

        static void OnValuePropertyChanged(BindableObject bindable,
                                            object oldValue, object newValue)
        {
            var control = bindable as SwitchableEntryCell;
            control.valueEntry.Text = newValue.ToString();
        }
        #endregion
    }
}

