using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GalleryInformer.ViewModels;

public partial class PinPageViewModel : ObservableObject
{
    private const string PinKey = "user_pin";

    [ObservableProperty]
    private string _inputPin;

    [ObservableProperty]
    private string _titleText;

    [ObservableProperty]
    private string _buttonText;

    private bool _isFirstRun;

    public PinPageViewModel()
    {
        CheckPinStatus();
    }

    private async void CheckPinStatus()
    {
        var savedPin = await SecureStorage.Default.GetAsync(PinKey);
        _isFirstRun = string.IsNullOrEmpty(savedPin);

        TitleText = _isFirstRun ? "Установите PIN-код (4 цифры)" : "Введите PIN-код";
        ButtonText = _isFirstRun ? "Сохранить" : "Войти";
    }

    [RelayCommand]
    private async Task SubmitPin()
    {
        if (string.IsNullOrWhiteSpace(InputPin) || InputPin.Length != 4)
        {
            await Shell.Current.DisplayAlertAsync("Ошибка", "PIN должен состоять из 4 цифр", "OK");
            return;
        }

        if (_isFirstRun)
        {
            await SecureStorage.Default.SetAsync(PinKey, InputPin);
            await Shell.Current.GoToAsync("//GalleryPage");
        }
        else
        {
            var savedPin = await SecureStorage.Default.GetAsync(PinKey);
            if (InputPin == savedPin)
            {
                await Shell.Current.GoToAsync("//GalleryPage");
            }
            else
            {
                await Shell.Current.DisplayAlertAsync("Ошибка", "Неверный PIN", "OK");
                InputPin = string.Empty;
            }
        }
    }
}