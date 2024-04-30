﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IReportServices
    {
        ICollection<TicketByBuilding> GetTicketsByBuilding(string? id);

        ICollection<TicketsByMaintenanceOperator> GetTicketsByMaintenanceOperator(string buildingName, string? operatorName);
        
        ICollection<TicketsByCategory> GetTicketsByCategory(string buildingName, string? categoryName);
    }

    public struct TicketByBuilding
    {
        public string BuildingName { get; set; }
        public int TicketsOpen { get; set; }
        public int TicketsInProgress { get; set; }
        public int TicketsClosed { get; set; }
    }

    public struct TicketsByMaintenanceOperator
    {
        public string OperatorName { get; set; }
        public int TicketsOpen { get; set; }
        public int TicketsInProgress { get; set; }
        public int TicketsClosed { get; set; }
        public string AverageTimeToClose { get; set; }
    }

    public struct TicketsByCategory
    {
        public string CategoryName { get; set; }
        public int TicketsOpen { get; set; }
        public int TicketsInProgress { get; set; }
        public int TicketsClosed { get; set; }
    }
}
