using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GalleryInformer.Models;
using GalleryInformer.Services;
using GalleryInformer.Views;
using System.Collections.ObjectModel;

namespace GalleryInformer.ViewModels;

public partial class GalleryPageViewModel : ObservableObject
{
    private readonly NavigationService _navigationService;

    public ObservableCollection<Photo> Photos { get; } = [];

    [ObservableProperty]
    private Photo _selectedPhoto;

    public GalleryPageViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;

        LoadImagesAsync();
    }

    private async Task LoadImagesAsync()
    {
        var cameraFolderPath = "/storage/emulated/0/Pictures";

        PermissionStatus status = await Permissions.RequestAsync<Permissions.Media>();

        if (Directory.Exists(cameraFolderPath))
        {
            var files = Directory.GetFiles(cameraFolderPath, "*jpg");

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);

                Photos.Add(new Photo(fileInfo.Name, fileInfo.FullName, fileInfo.CreationTime));
            }
        }
        else
        {
            await Shell.Current.DisplayAlertAsync("Ошибка", "Папка не найдена или нет разрешений.", "OK");
        }
    }

    [RelayCommand]
    private async Task Open()
    {
        if (SelectedPhoto == null) return;

        _navigationService.SelectedPhotoCache = SelectedPhoto;

        await Shell.Current.GoToAsync("//ImageDetailPage");
    }


    [RelayCommand]
    private async Task Delete()
    {
        if (SelectedPhoto == null)
        {
            await Shell.Current.DisplayAlertAsync("Ошибка", "Выберите изображение для удаления.", "ОК");
            return;
        }

        bool confirm = await Shell.Current.DisplayAlertAsync("Подтверждение", $"Удалить {SelectedPhoto.Name}?", "Да", "Нет");

        if (confirm)
        {
            try
            {
                File.Delete(SelectedPhoto.FullPath);
                Photos.Remove(SelectedPhoto);
                SelectedPhoto = null!;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Ошибка удаления", $"Не удалось удалить файл: {ex.Message}", "ОК");
            }
        }
    }
}