using Microsoft.JSInterop;

namespace MudBlazorWebApp1.Services
{
    public class SelectedChildService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly UserContext _userContext;
        public event Action? OnChange;
        private int? _selectedChildId;
        private string? _currentUserId;
        private static readonly string LocalStorageKeyPrefix = "selectedChildId_";

        public SelectedChildService(IJSRuntime jsRuntime, UserContext userContext)
        {
            _jsRuntime = jsRuntime;
            _userContext = userContext;
        }

        public int? SelectedChildId
        {
            get => _selectedChildId;
            set
            {
                if (_selectedChildId == value) return;
                _selectedChildId = value;

                if (value.HasValue)
                {
                    SaveToLocalStorage(value.Value);
                }
                else
                {
                    ClearFromLocalStorage();
                }

                OnChange?.Invoke();
            }
        }

        public async Task InitializeAsync()
        {
            try
            {
                // ابتدا userId را بگیرید
                _currentUserId = await _userContext.GetUserIdAsync();

                if (string.IsNullOrEmpty(_currentUserId))
                    return;

                // سپس بر اساس userId از localStorage بخوانید
                var key = $"{LocalStorageKeyPrefix}{_currentUserId}";
                var stored = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);

                if (!string.IsNullOrEmpty(stored) && int.TryParse(stored, out var childId))
                {
                    _selectedChildId = childId;
                }
            }
            catch
            {
                // اگر خطا بود، نادیده بگیرید
            }
        }

        private async void SaveToLocalStorage(int childId)
        {
            try
            {
                if (string.IsNullOrEmpty(_currentUserId))
                    _currentUserId = await _userContext.GetUserIdAsync();

                var key = $"{LocalStorageKeyPrefix}{_currentUserId}";
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, childId.ToString());
            }
            catch { }
        }

        private async void ClearFromLocalStorage()
        {
            try
            {
                if (string.IsNullOrEmpty(_currentUserId))
                    _currentUserId = await _userContext.GetUserIdAsync();

                var key = $"{LocalStorageKeyPrefix}{_currentUserId}";
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
            }
            catch { }
        }

        // متد جدید برای پاک کردن هنگام لاگ‌اوت
        public async Task ClearOnLogoutAsync()
        {
            _selectedChildId = null;
            await ClearFromLocalStorageAsync();
        }

        private async Task ClearFromLocalStorageAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(_currentUserId))
                    _currentUserId = await _userContext.GetUserIdAsync();

                var key = $"{LocalStorageKeyPrefix}{_currentUserId}";
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
            }
            catch { }
        }
    }
}