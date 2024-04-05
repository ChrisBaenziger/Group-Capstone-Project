﻿using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.RouteStop
{
    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// DATE: 2024-03-05
    /// Interaction Logic for Route Stop
    /// </summary>
    public interface IRouteStopManager
    {
        IEnumerable<RouteStopVM> GetRouteStopByRouteId(int routeId);
        int AddRouteStop(RouteStopVM routeStopVM);
        int EditRouteStop(RouteStopVM existingRouteStop, RouteStopVM newRouteStop);
        int DeactivateRouteStop(int routeStopId);
        int ActivateRouteStop(int routeStopId);
    }
}