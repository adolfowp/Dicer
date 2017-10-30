using Xamarin.Forms;

namespace Dicer
{
	public class ValueExposer : ContentView
	{
        #region Fields
        private readonly Image _icon;
        private readonly Label _valueLabel;
        private readonly Label _titleLabel;
        #endregion

        #region Costruttore
        public ValueExposer()
        {
            #region Icona
            _icon = new Image
            {
                WidthRequest = 64,
                HeightRequest = 64,
            };
            Grid.SetColumn(_icon, 0);
            Grid.SetRow(_icon, 0);
            Grid.SetRowSpan(_icon, 2);
            #endregion
            #region Title Label
            _titleLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = TitleColor,
            };
            Grid.SetColumn(_titleLabel, 1);
            Grid.SetRow(_titleLabel, 1);
            #endregion
            #region Value Label
            _valueLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.Black,
            };
            Grid.SetColumn(_valueLabel, 1);
            Grid.SetRow(_valueLabel, 0);
            #endregion
            #region Layout
            var mainLayout = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = new GridLength(28) },
                    new ColumnDefinition { Width = GridLength.Auto },
                },
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = GridLength.Star },
                    new RowDefinition { Height = GridLength.Star },
                },
                Children =
                {
                    _icon,
                    _valueLabel,
                    _titleLabel,
                },
            };
            #endregion

            Content = mainLayout;
        }
        #endregion

        #region Properties
        public string Image
        {
            get
            {
                return base.GetValue(ImageProperty).ToString();
            }
            set
            {
                base.SetValue(ImageProperty, value);
            }
        }
        public decimal Value
        {
            get
            {
                return (decimal)base.GetValue(ValueProperty);
            }
            set
            {
                base.SetValue(ValueProperty, value);
            }
        }
        public string Title
        {
            get
            {
                return base.GetValue(TitleProperty).ToString();
            }
            set
            {
                base.SetValue(TitleProperty, value);
            }
        }
        public Color TitleColor
        {
            get
            {
                return (Color)base.GetValue(TitleColorProperty);
            }
            set
            {
                base.SetValue(TitleColorProperty, value);
            }
        }
        #endregion

        #region Bindable properties
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
            propertyName: nameof(Value),
            returnType: typeof(decimal),
            declaringType: typeof(ValueExposer),
            defaultValue: 0m,
            propertyChanged: ValuePropertyChanged);

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            propertyName: nameof(Image),
            returnType: typeof(string),
            declaringType: typeof(ValueExposer),
            defaultValue: null,
            propertyChanged: ImagePropertyChanged);

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            propertyName: nameof(Title),
            returnType: typeof(string),
            declaringType: typeof(ValueExposer),
            defaultValue: string.Empty,
            propertyChanged: TitleTextPropertyChanged);

        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(
            propertyName: nameof(TitleColor),
            returnType: typeof(Color),
            declaringType: typeof(ValueExposer),
            defaultValue: Color.Black,
            propertyChanged: TitleColorPropertyChanged);
        #endregion

        #region Methods
        private static void ImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            var control = (ValueExposer)bindable;
            control._icon.Source = ImageSource.FromFile(newValue.ToString());
        }

        private static void ValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ValueExposer)bindable;
            control._valueLabel.Text = newValue.ToString();
        }

        private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ValueExposer)bindable;
            control._titleLabel.Text = newValue.ToString();
        }

        private static void TitleColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ValueExposer)bindable;
            control._titleLabel.TextColor = (Color)newValue;
        }
        #endregion

    }
}
