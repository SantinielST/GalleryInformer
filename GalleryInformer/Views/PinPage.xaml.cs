using GalleryInformer.ViewModels;

namespace GalleryInformer.Views;

public partial class PinPage : ContentPage
{
	public PinPage(PinPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}