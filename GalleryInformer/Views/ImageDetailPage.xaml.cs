using GalleryInformer.ViewModels;

namespace GalleryInformer.Views;

public partial class ImageDetailPage : ContentPage
{
    public ImageDetailPage(ImageDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}