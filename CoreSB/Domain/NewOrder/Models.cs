
using System.Collections.Generic;
using CoreSB.Universal;

namespace CoreSB.Domain.NewOrder
{
    public class PhysicalUnitConvertions : EntityIntIdDAL
    {
        public PhysicalUnitDAL UnitFrom { get; set; }
        public PhysicalUnitDAL UnitTo { get; set; }
        public double Coefficient { get; set; }
    }

    /*Kg,Pnd,sm,meter, m3 etc*/
    public class PhysicalUnitDAL : EntityIntIdDAL
    {
        public string Name { get; set; }
    }

    /*Length, Height, Weight or density exmpl*/
    public class PhysicalDimensionDAL : EntityIntIdDAL
    {
        /*Length goes here*/
        public string ParameterName { get; set; }

        /*Amount*/
        public double Amount { get; set; }

        /*And sm, kg, m/sq3 goes here*/
        public PhysicalUnitDAL DimensionUnit { get; set; }
    }
    public class GoodsDAL : EntityIntIdDAL
    {
        public string ProductName { get; set; }

        /*
            Can be a bulk of corn, and it has only volume and weigth
            but can be a pack of corn, with dimensions added to volume and weight
        */
        List<PhysicalDimensionDAL> Dimensions { get; set; }
    }


    public class AddressDAL : EntityIntIdDAL
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Province { get; set; }
        public string StreetName { get; set; }
        public int Code { get; set; }
    }
    public class RouteVertexDAL : EntityIntIdDAL
    {
        public int InRouteMoveOrder { get; set; }
        public AddressDAL From { get; set; }
        public AddressDAL To { get; set; }
        public double Distance { get; set; }
        public int PriorityWeigth { get; set; }
    }
    public class RouteDAL : EntityIntIdDAL
    {
        public string Name { get; set; }

        public List<RouteVertexDAL> RouteVertexes { get; set; }
    }


    public class DeliveryItemDAL : EntityGuidIdDAL
    {
        public string DeliveryName { get; set; }
        public int DeliveryNumber { get; set; }

        List<GoodsDAL> GoodsToDeliver { get; set; }
        List<RouteDAL> Routes { get; set; }
    }

    public class ClientDAL : EntityGuidIdDAL
    {
        public string ClientName { get; set; }
        List<OrderDAL> Orders { get; set; }
    }

    public class OrderDAL : EntityGuidIdDAL
    {
        public ClientDAL Client { get; set; }
        List<DeliveryItemDAL> OrderedItems { get; set; }
    }

}

