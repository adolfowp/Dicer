using System;

using Xamarin.Forms;

namespace Dicer.Views
{
    public class CheckBox : ContentView
    {
        #region Fields
        readonly Frame insideFrame;
        readonly Label textLabel;
        #endregion

        public CheckBox()
        {
            #region Outline Frame
            var outlineFrame = new Frame
            {
                OutlineColor = Color.Black,
                HasShadow = false,
                WidthRequest = 20,
                HeightRequest = 20,
                Padding = new Thickness(5),
                Margin = new Thickness(14),
            };
            var tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += (object sender, EventArgs e) => 
            {
                IsChecked = !IsChecked;
            };
            outlineFrame.GestureRecognizers.Add(tapRecognizer);
            Grid.SetColumn(outlineFrame, 0);
            #endregion

            #region Inseide Frame
            insideFrame = new Frame
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = SelectionColor,
                IsVisible = IsChecked,
                HasShadow = false,
            };
            #endregion
            #region Text Label
            textLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.Black,
                Text = this.Text,
                VerticalOptions = LayoutOptions.Center,
            };
            Grid.SetColumn(textLabel, 1);
            #endregion

            outlineFrame.Content = insideFrame;
            Content = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Star },
                },

                Children = 
                {
                    outlineFrame,
                    textLabel,
                }
            };
        }

        #region Binding Properties
        public static readonly BindableProperty SelectionColorProperty =
            BindableProperty.Create(
                propertyName: nameof(SelectionColor),
                declaringType: typeof(CheckBox),
                returnType: typeof(Color),
                defaultValue: Color.Black,
                propertyChanged: OnSelectionColorPropertyChanged);

        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(
                propertyName: nameof(IsChecked),
                declaringType: typeof(CheckBox),
                returnType: typeof(bool),
                defaultBindingMode: BindingMode.TwoWay,
                defaultValue: false,
                propertyChanged: OnIsCheckedPropertyChanged);

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                propertyName: nameof(Text),
                declaringType: typeof(CheckBox),
                returnType: typeof(string),
                defaultValue: string.Empty,
                propertyChanged: OnTextPropertyChanged);
        #endregion

        #region Properties
        public Color SelectionColor
        {
            get { return (Color)GetValue(SelectionColorProperty); }
            set { SetValue(SelectionColorProperty, value); }
        }

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region Methods
        static void OnIsCheckedPropertyChanged(BindableObject bindable, 
                                               object oldValue, object newValue)
        {
            var control = bindable as CheckBox;
            control.insideFrame.IsVisible = (bool)newValue;
        }

        static void OnTextPropertyChanged(BindableObject bindable, 
                                          object oldValue, object newValue)
        {
            var control = bindable as CheckBox;
            control.textLabel.Text = (string)newValue;
        }

        static void OnSelectionColorPropertyChanged(BindableObject bindable, 
                                                    object oldValue, object newValue)
        {
            var control = bindable as CheckBox;
            control.insideFrame.BackgroundColor = (Color)newValue;
        }
        #endregion
    }
}

