using Microsoft.AspNetCore.Mvc;
using UserAlertManagement.Services.Interfaces;
using UserAlertManagement.Services.Models;

namespace FlightManagementApi.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly IUserAlertService _userAlertService;
    public UserController(IUserAlertService userAlertService)
    {
        _userAlertService = userAlertService;
    }

    [HttpPost]
    public async Task<UserModel> CreateUser([FromBody] UserModel user)
    {
        var res = await _userAlertService.CreateUser(user);
        return res;
    }
   

    [HttpGet("{userId}")]
    public async Task<UserModel> GetUser(Guid userId)
    {
        var res = await _userAlertService.GetUser(userId);
        return res;
    }
    
    [HttpGet]
    public async Task<List<UserModel>> GetAlert([FromQuery] string? from, [FromQuery] string? to, [FromQuery] DateTime? departureDate, [FromQuery] decimal? maxPrice)
    {
        var res = await _userAlertService.GetUsersMatchingAlerts(from, to, departureDate, maxPrice);
        return res;
    }
    
    [HttpPut("{userId}")]
    public async Task<UserModel> GetUser(Guid userId, [FromBody] UserModel updatedUser)
    {
        updatedUser.Id = userId;
        var res = await _userAlertService.UpdateUser(updatedUser);
        return res;
    }


    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        await _userAlertService.DeleteUser(userId);
        return NoContent();
    }
}