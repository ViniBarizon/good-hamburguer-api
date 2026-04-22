using System.Net.Http.Json;
using GoodHamburger.Web.Models;

namespace GoodHamburger.Web.Services;

public class ApiService(HttpClient http)
{
    public async Task<List<MenuItemModel>> GetMenuAsync()
    {
        try
        {
            var result = await http.GetFromJsonAsync<List<MenuItemModel>>("api/menu");
            return result ?? new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GetMenuAsync error: {ex.Message}");
            return new();
        }
    }

    public async Task<List<OrderModel>> GetOrdersAsync()
    {
        try
        {
            var result = await http.GetFromJsonAsync<List<OrderModel>>("api/orders");
            return result ?? new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GetOrdersAsync error: {ex.Message}");
            return new();
        }
    }

    public async Task<OrderModel?> GetOrderAsync(Guid id)
    {
        try
        {
            return await http.GetFromJsonAsync<OrderModel>($"api/orders/{id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GetOrderAsync error: {ex.Message}");
            return null;
        }
    }

    public async Task<OrderModel?> CreateOrderAsync(CreateOrderModel model)
    {
        try
        {
            var response = await http.PostAsJsonAsync("api/orders", model);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"CreateOrder response: {response.StatusCode} — {content}");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<OrderModel>()
                : null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CreateOrderAsync error: {ex.Message}");
            return null;
        }
    }

    public async Task<OrderModel?> UpdateOrderAsync(Guid id, CreateOrderModel model)
    {
        try
        {
            var response = await http.PutAsJsonAsync($"api/orders/{id}", model);
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<OrderModel>()
                : null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"UpdateOrderAsync error: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> DeleteOrderAsync(Guid id)
    {
        try
        {
            var response = await http.DeleteAsync($"api/orders/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DeleteOrderAsync error: {ex.Message}");
            return false;
        }
    }
}