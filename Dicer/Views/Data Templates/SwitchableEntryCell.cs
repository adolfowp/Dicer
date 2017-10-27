using System;

using Xamarin.Forms;

namespace Dicer.Views
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
            //enabler.SetBinding(CheckBox.IsCheckedProperty, nameof(IsChecked));
            //enabler.SetBinding(CheckBox.TextProperty, nameof(Text));

            #endregion
            #region Value Entry
            valueEntry = new Entry
            {
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.Fill,
            };
            //valueEntry.SetBinding(Entry.TextProperty, nameof(Value));
            #endregion

            var mainLayout = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star },
                }
            };
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
                defaultValue: 0m,
                propertyChanged: OnValuePropertyChanged);

        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(
                propertyName: nameof(IsChecked),
                returnType: typeof(bool),
                declaringType: typeof(SwitchableEntryCell),
                defaultBindingMode: BindingMode.TwoWay,
                defaultValue: false);
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

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
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

        static void OnIsCheckedPropertyChanged(BindableObject bindable,
                                            object oldValue, object newValue)
        {
            var control = bindable as SwitchableEntryCell;
            control.enabler.IsChecked = (bool)newValue;
        }
        #endregion
    }
}

