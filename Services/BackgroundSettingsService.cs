using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
public class BackgroundSettingsService
{
    public bool backgroundIsShown = true;
    private readonly ProtectedLocalStorage _storage;
    private const string StorageKey = "backgroundEnabled";
    public BackgroundSettingsService(ProtectedLocalStorage storage)
    { 
        _storage = storage;
    }
    public async Task InitializeAsync()
    {
        var result = await _storage.GetAsync<bool>(StorageKey);
        if (result.Success)
        {
            backgroundIsShown = result.Value;
        }
    }
    public async Task SetBackgroundOnOff(bool enabled)
    {
        backgroundIsShown = enabled;
        await _storage.SetAsync(StorageKey, enabled);
    }

}