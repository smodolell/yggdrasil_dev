namespace Yggdrasil.Blazor.Services;

public interface IAppState
{
    string CurrentTitle { get; set; }
    bool IsProcessing { get; set; }
    event Action OnChange;
    void NotifyStateChanged();
}
