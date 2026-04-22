using System.Net.Http.Json;
using GoodHamburger.Web.Models;

namespace GoodHamburger.Web.Services;

public class ApiService(HttpClient http)
{
    public async Task<List<MenuItemModel>> GetMenuAsync() =>
        await http.GetFromJsonAsync<List<MenuItemModel>>("api/menu") ?? new();

    public async Task<List<OrderModel>> GetOrdersAsync() =>
        await http.GetFromJsonAsync<List<OrderModel>>("api/orders") ?? new();

    public async Task<OrderModel?> GetOrderAsync(Guid id) =>
        await http.GetFromJsonAsync<OrderModel>($"api/orders/{id}");

    public async Task<OrderModel?> CreateOrderAsync(CreateOrderModel model)
    {
        var response = await http.PostAsJsonAsync("api/orders", model);
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<OrderModel>()
            : null;
    }

    public async Task<OrderModel?> UpdateOrderAsync(Guid id, CreateOrderModel model)
    {
        var response = await http.PutAsJsonAsync($"api/orders/{id}", model);
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<OrderModel>()
            : null;
    }

    public async Task<bool> DeleteOrderAsync(Guid id)
    {
        var response = await http.DeleteAsync($"api/orders/{id}");
        return response.IsSuccessStatusCode;
    }
}