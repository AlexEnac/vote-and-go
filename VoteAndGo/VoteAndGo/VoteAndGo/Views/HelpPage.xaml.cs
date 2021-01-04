using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteAndGo.Models;
using VoteAndGo.ViewModels;
using VoteAndGo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VoteAndGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpPage : ContentPage
    {
        HelpViewModel _viewModel;
        public HelpPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new HelpViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}