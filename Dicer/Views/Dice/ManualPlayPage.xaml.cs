using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dicer
{
    public partial class ManualPlayPage : ContentPage
    {
        #region Fields
        readonly PlayViewModel viewmodel;
        const string _titleText = "Manual";
        double StepValue;
        #endregion

        #region Constructors
        public ManualPlayPage()
        {
            InitializeComponent();

            StepValue = 1.0;
            sldChance.ValueChanged += OnSliderValueChanged;
        }

        public ManualPlayPage(DiceSite Site)
            : this()
        {
            BindingContext = viewmodel = new MartingaleViewModel(Site);
            viewmodel.Title = _titleText;

        }
		#endregion

		#region Methods
		void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
		{
			var newStep = Math.Round(e.NewValue / StepValue);

			sldChance.Value = newStep * StepValue;
		}

        #endregion


    }
}
