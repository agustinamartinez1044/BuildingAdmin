using IServices;
using Domain;
using IDataAccess;
using IDataAcess;

namespace Services;

public class BuildingService : IBuildingService
{

    private IGenericRepository<Building> _buildingRepository;
    private ISessionService _sessionService;

    public BuildingService(IGenericRepository<Building> buildingRepository, ISessionService sessionService)
    {
        _buildingRepository = buildingRepository;
        _sessionService = sessionService;
    }

    public Building CreateBuilding(Building building)
    {
        var buildingAlreadyExist = _buildingRepository.GetByCondition(b => b.Name == building.Name);

        if (buildingAlreadyExist != null)
        {
            throw new ArgumentException("Building already exist");
        }

        _buildingRepository.Insert(building);
        return building;

    }

    public void DeleteBuilding(int buildingId)
    {
        var currentUser = _sessionService.GetCurrentUser() as Manager;
        if (currentUser == null)
        {
            throw new InvalidOperationException("Current user is not a manager");
        }

        var building = currentUser.Buildings.FirstOrDefault(b => b.Id == buildingId);
        if (building == null)
        {
            throw new ArgumentNullException("Building not found");
        }

        _buildingRepository.Delete(building);
    }

    public Building ModifyBuilding(int buildingId, Building modifiedBuilding)
    {
        Building buildingToModify = _buildingRepository.Get(buildingId) ?? throw new ArgumentNullException("Building not found");

        buildingToModify.Name = modifiedBuilding.Name;
        buildingToModify.Address = modifiedBuilding.Address;
        buildingToModify.Location = modifiedBuilding.Location;
        buildingToModify.ConstructionCompany = modifiedBuilding.ConstructionCompany;
        buildingToModify.Expenses = modifiedBuilding.Expenses;
        buildingToModify.Apartments = modifiedBuilding.Apartments;
        buildingToModify.Tickets = modifiedBuilding.Tickets;


        _buildingRepository.Update(buildingToModify);

        return buildingToModify;
    }
}