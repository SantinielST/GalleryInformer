using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GalleryInformer.Models;
using GalleryInformer.Services;

namespace GalleryInformer.ViewModels;

public partial class ImageDetailViewModel : ObservableObject
{
    [ObservableProperty]
    private Photo selectedPhoto;

    public string FormattedDate => SelectedPhoto?.CreationDate.ToString("dd MMMM yyyy, HH:mm") ?? "Дата неизвестна";

    public ImageDetailViewModel(NavigationService navigationService)
    {
        SelectedPhoto = navigationService.SelectedPhotoCache;

        navigationService.SelectedPhotoCache = null;
    }

    [RelayCommand]
    private async Task Back()
    {
        await Shell.Current.GoToAsync("//GalleryPage");
    }
}