using GalleryInformer.Models;

namespace GalleryInformer.Services;

public class NavigationService
{
    // Это свойство будет временно хранить выбранное фото
    public Photo SelectedPhotoCache { get; set; }
}