﻿using DataObjects.HelperObjects;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.RouteAssignment
{
    /// <summary>
    /// AUTHOR: Steven Sanchez
    /// DATE: 2024-03-24
    /// Interaction Logic for Route Assignments
    /// </summary>
    /// <br /><br />
    ///    UPDATER: 
    /// <br />
    ///    UPDATED: 
    /// <br />
    ///     Update Comments
    /// </remarks>
    public interface IRouteAssignmentManager
    {
        IEnumerable<Route_Assignment_VM> GetAllRouteAssignmentByDriverID(int Driver_ID);
        Task<BingMapsResponse> getRouteLineForRouteAssignmentVM(IEnumerable<Route_Assignment_VM> route);

    }
}
