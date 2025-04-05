using Microsoft.AspNetCore.Mvc;
using UserAlertManagement.Services.Interfaces;
using UserAlertManagement.Services.Models;

namespace FlightManagementApi.Controllers;

[ApiController]
[Route("api/v1/alerts")]
public class AlertController : ControllerBase
{
    private readonly IUserAlertService _userAlertService;
    public AlertController(IUserAlertService userAlertService)
    {
        _userAlertService = userAlertService;
    }

    [HttpPost("user/{userId}")]
    public async Task<AlertModel> CreateAlert([FromRoute] Guid userId, [FromBody] AlertModel alert)
    {
        alert.UserId = userId;
        var res = await _userAlertService.CreateAlert(alert);
        return res;
    }

    [HttpGet("{alertId}")]
    public async Task<AlertModel> GetAlert([FromRoute] Guid alertId)
    {
        var res = await _userAlertService.GetAlert(alertId);
        return res;
    }


    [HttpPut("{alertId}")]
    public async Task<AlertModel> UpdateAlert([FromRoute] Guid alertId, [FromBody] AlertModel updatedAlert)
    {
        updatedAlert.Id = alertId;
        var res = await _userAlertService.UpdateAlert(updatedAlert);
        return res;
    }

    [HttpDelete("{alertId}")]
    public async Task<IActionResult> DeleteAlert([FromRoute] Guid alertId)
    {
        await _userAlertService.DeleteAlert(alertId);
        return NoContent();
    }

}