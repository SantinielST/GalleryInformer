using GalleryInformer.ViewModels;

namespace GalleryInformer.Views;

public partial class GalleryPage : ContentPage
{
    public GalleryPage(GalleryPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}