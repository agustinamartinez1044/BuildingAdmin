using IDataAcess;
using IServices;
using Domain;

namespace Services;

public class ReportsService : IReportServices
{
    private readonly IGenericRepository<Ticket> _ticketRepository;
    private readonly IGenericRepository<Building> _buildingRepository;
    private readonly IGenericRepository<MaintenanceOperator> _maintenanceOperatorRepository;

    public ReportsService(IGenericRepository<Ticket> ticketRepository, IGenericRepository<Building> buildingRepository, IGenericRepository<MaintenanceOperator> maintenanceOperatorRepository)
    {
        _ticketRepository = ticketRepository;
        _buildingRepository = buildingRepository;
        _maintenanceOperatorRepository = maintenanceOperatorRepository;
    }

    public ICollection<TicketByBuilding> GetTicketsByBuilding(string? name = null)
    {
        var buildings = _buildingRepository.GetAll<Building>();
        var ticketDataList = new List<TicketByBuilding>();

        if (name != null)
        {
            buildings = buildings.Where(b => b.Name == name);
        }

        foreach (var building in buildings)
        {
            var ticketData = new TicketByBuilding
            {
                BuildingName = building.Name,
                TicketsOpen = building.Tickets.Count(t => t.Status == Domain.DataTypes.Status.Open),
                TicketsInProgress = building.Tickets.Count(t => t.Status == Domain.DataTypes.Status.InProgress),
                TicketsClosed = building.Tickets.Count(t => t.Status == Domain.DataTypes.Status.Closed)

            };

            ticketDataList.Add(ticketData);
        }

        return ticketDataList;
    }

    public ICollection<TicketsByMaintenanceOperator> GetTicketsByMaintenanceOperator(string buildingName, string? operatorName = null)
    {
        var building = _buildingRepository.GetByCondition(b => b.Name == buildingName);
        var tickets = building.Tickets;

        var result = new List<TicketsByMaintenanceOperator>();

        if (operatorName != null)
        {
            tickets = (List<Ticket>)tickets.Where(t => t.AssignedTo.Name == operatorName);

            var reportData = new TicketsByMaintenanceOperator
            {
                OperatorName = operatorName,
                TicketsOpen = tickets.Count(t => t.Status == Domain.DataTypes.Status.Open),
                TicketsInProgress = tickets.Count(t => t.Status == Domain.DataTypes.Status.InProgress),
                TicketsClosed = tickets.Count(t => t.Status == Domain.DataTypes.Status.Closed),
                AverageTimeToClose = CalculateAverage(tickets.Where(t => t.Status == Domain.DataTypes.Status.Closed).ToList()).ToString(@"hh\:mm")
            };

            result.Add(reportData);
        }
        else 
        {
            var ticketsAgrupedByOperator = tickets.GroupBy(t => t.AssignedTo.Name);

            foreach (var ticketGroup in ticketsAgrupedByOperator)
            {
                var reportData = new TicketsByMaintenanceOperator
                {
                    OperatorName = ticketGroup.Key,
                    TicketsOpen = ticketGroup.Count(t => t.Status == Domain.DataTypes.Status.Open),
                    TicketsInProgress = ticketGroup.Count(t => t.Status == Domain.DataTypes.Status.InProgress),
                    TicketsClosed = ticketGroup.Count(t => t.Status == Domain.DataTypes.Status.Closed),
                    AverageTimeToClose = CalculateAverage(ticketGroup.Where(t => t.Status == Domain.DataTypes.Status.Closed).ToList()).ToString(@"hh\:mm")
                };  

                result.Add(reportData);
            }

        }
        return result;
    }

    private TimeSpan CalculateAverage(List<Ticket> tickets)
    {
        DateTime totalHours = new DateTime(0);
        int count = 0;
        foreach (var ticket in tickets)
        {
            totalHours += ticket.ClosingDate - ticket.AttentionDate;
            count++;
        }

        if (count == 0) return new TimeSpan(0);

        TimeSpan result = new TimeSpan(totalHours.Ticks / count);
        return result;
    }

    public ICollection<TicketsByCategory> GetTicketsByCategory(string buildingName, string? categoryName = null)
    {
        Building building = _buildingRepository.GetByCondition(b => b.Name == buildingName);
        var tickets = building.Tickets;

        var result = new List<TicketsByCategory>();

        if(categoryName != null)
        {
            tickets = tickets.Where(t => t.Category.Name == categoryName).ToList();

            foreach (var ticket in tickets)
            {
                var reportData = new TicketsByCategory
                {
                    CategoryName = categoryName,
                    TicketsOpen = tickets.Count(t => t.Status == Domain.DataTypes.Status.Open),
                    TicketsInProgress = tickets.Count(t => t.Status == Domain.DataTypes.Status.InProgress),
                    TicketsClosed = tickets.Count(t => t.Status == Domain.DataTypes.Status.Closed)
                };

                result.Add(reportData);
            }
        }
        else
        {
            var ticketsAgrupedByCategory = tickets.GroupBy(t => t.Category.Name);

            foreach (var ticketGroup in ticketsAgrupedByCategory)
            {
                var reportData = new TicketsByCategory
                {
                    CategoryName = ticketGroup.Key,
                    TicketsOpen = ticketGroup.Count(t => t.Status == Domain.DataTypes.Status.Open),
                    TicketsInProgress = ticketGroup.Count(t => t.Status == Domain.DataTypes.Status.InProgress),
                    TicketsClosed = ticketGroup.Count(t => t.Status == Domain.DataTypes.Status.Closed)
                };  

                result.Add(reportData);
            }
        }
        return result;
    }


}

