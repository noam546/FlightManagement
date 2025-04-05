using AutoMapper;
using UserAlertManagement.Data.Interfaces;
using UserAlertManagement.Data.Models;
using UserAlertManagement.Services.Interfaces;
using UserAlertManagement.Services.Models;

namespace UserAlertManagement.Services;

public class UserAlertService : IUserAlertService
{
    private readonly IUserRepository _userRepository;
    private IMapper _mapper;
    private readonly IAlertRepository _alertRepository;
    
    public UserAlertService(IUserRepository userRepository, IMapper mapper, IAlertRepository alertRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _alertRepository = alertRepository;
    }
    public async Task<UserModel> CreateUser(UserModel userModel)
    {
        var user = _mapper.Map<User>(userModel);
        var userRes = await _userRepository.AddUser(user);
        var userModelRes = _mapper.Map<UserModel>(userRes);
        return userModelRes;
    }

    public async Task<UserModel> UpdateUser(UserModel userModel)
    {
        var user = _mapper.Map<User>(userModel);
        var userRes = await _userRepository.UpdateUser(user);
        var userModelRes = _mapper.Map<UserModel>(userRes);
        return userModelRes;
    }

    public async Task DeleteUser(Guid userId)
    {
        await _userRepository.DeleteUser(userId);
    }

    public async Task<UserModel> GetUser(Guid userId)
    {
        var user = await _userRepository.GetUser(userId);
        var userModel = _mapper.Map<UserModel>(user);
        return userModel;
    }

    public async Task<AlertModel> CreateAlert(AlertModel alert)
    {
        var alertEntity = _mapper.Map<Alert>(alert);
        var alertRes = await _alertRepository.AddAlert(alertEntity);
        var alertModelRes = _mapper.Map<AlertModel>(alertRes);
        return alertModelRes;
    }

    public async Task<AlertModel> UpdateAlert(AlertModel alert)
    {
        var alertEntity = _mapper.Map<Alert>(alert);
        var alertRes = await _alertRepository.UpdateAlert(alertEntity);
        var alertModelRes = _mapper.Map<AlertModel>(alertRes);
        return alertModelRes;
    }

    public async Task<List<UserModel>> GetUsersMatchingAlerts(string from, string to, DateTime? departureDate, decimal? price)
    {
        var users = await _alertRepository.GetUsersByMatchingAlerts(from, to, departureDate, price);
        var userModels = users.Select(u => _mapper.Map<UserModel>(u)).ToList();
        return userModels;
    }

    public async Task DeleteAlert(Guid alertId)
    {
        await _alertRepository.DeleteAlert(alertId);
    }

    public async Task<AlertModel> GetAlert(Guid alertId)
    {
        var alert = await _alertRepository.GetAlert(alertId);
        var alertModel = _mapper.Map<AlertModel>(alert);
        return alertModel;
    }
}