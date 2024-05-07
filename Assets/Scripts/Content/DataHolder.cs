using System;
using System.Collections.Generic;

[Serializable]
public class Root
{
    public bool onBoarding;

    public List<Properties> properties = new List<Properties>();
    public List<Order> Orders = new List<Order>();
}

[Serializable]
public class Properties
{
    public string ProjectName;
    public string Description;
    public string ComputerType = "Gaming computers";
    public int ComputersCount;
    public int OcupiedComputers;
    public float Price;
    public int ID;

    public List<Service> Services = new List<Service>();
}
[Serializable]
public class Service
{
    public string Name;
    public string Info1;
    public int ID;
}
[Serializable]
public class Order
{
    public Properties Table;
    public float Hours;
    public string Status;
}
