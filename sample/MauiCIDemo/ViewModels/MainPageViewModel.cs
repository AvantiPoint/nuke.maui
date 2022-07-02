namespace MauiCIDemo.ViewModels;

public class MainPageViewModel : BindableBase
{
    private ISemanticScreenReader _screenReader { get; }
    private int _count;

    public MainPageViewModel(ISemanticScreenReader screenReader)
    {
        _screenReader = screenReader;
        CountCommand = new DelegateCommand(OnCountCommandExecuted);
    }

    public string Title => "Main Page";

    private string _text = "Click me";
    public string Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }

    public DelegateCommand CountCommand { get; }

    private void OnCountCommandExecuted()
    {
        _count++;
        if (_count == 1)
            Text = "Clicked 1 time";
        else if (_count > 1)
            Text = $"Clicked {_count} times";

        _screenReader.Announce(Text);
    }
}
