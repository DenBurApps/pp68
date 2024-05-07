using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditPlate : MonoBehaviour
{
    public Properties properties = new Properties();
    [Header("Base Window")]
    public InputFieldChanger ProjectName;
    public InputFieldChanger ProjectPrice;
    public InputFieldChanger ComputersCount;


    [Header("Services")]
    public Transform ServiceSpawnPlace;
    public ServicePlate ServicePlate;
    public List<ServicePlate> ServicePlates = new List<ServicePlate>();

    public Button ContinueButton;
    private Preview preview;
    public void Init(Properties props,Preview preview)
    {
        DeleteServicePlate(ServicePlates[0]);
        properties = props;
        this.preview = preview;

        ProjectName.ChangeText(props.ProjectName);
        ProjectPrice.ChangeText(props.Price.ToString());
        ComputersCount.ChangeText(props.ComputersCount.ToString());
        ServicePlates = FillServicePlatesList(props.Services, ServiceSpawnPlace);

        ContinueButton.onClick.RemoveAllListeners();
        ContinueButton.onClick.AddListener(() => 
        {
            SavePlateData();
            Destroy(gameObject);
        });
    }

    private void Awake()
    {
        AddnewServicePlate();
        ContinueButton.onClick.AddListener(() =>
        {
            CreatePlateData();
            Destroy(gameObject);
        });
    }
    public void SavePlateData()
    {
        FillPlateData();
        DataProcessor.Instance.EditPlate(properties);
        preview.Init(properties);
    }

    public void CreatePlateData()
    {
        FillPlateData();
        DataProcessor.Instance.AddNewPlate(properties);

    }

    private void FillPlateData()
    {
        properties.ProjectName = ProjectName.text;
        float.TryParse(ProjectPrice.text, out float price);
        properties.Price = price;
        int.TryParse(ComputersCount.text, out int computersCount);
        if(0 >= computersCount)
            computersCount = 1;
        
        properties.ComputersCount = computersCount;

        properties.Services = GetInfoFromServicePlates(ServicePlates);
    }
    private List<Service> GetInfoFromServicePlates(List<ServicePlate> list)
    {
        List<Service> dataList = new List<Service>();

        foreach (var item in list)
        {
            dataList.Add(item.GetData());
        }
        return dataList;
    }
    private List<ServicePlate> FillServicePlatesList(List<Service> dataList, Transform spawnPlace)
    {
        List<ServicePlate> list = new List<ServicePlate>();

        foreach (var item in dataList)
        {
            var obj = Instantiate(ServicePlate, spawnPlace);
            obj.GetComponent<RectTransform>().SetSiblingIndex(1);
            obj.Init(item,this);
            list.Add(obj);
        }
        return list;
    }

    public void AddnewServicePlate()
    {
        var obj = Instantiate(ServicePlate, ServiceSpawnPlace);
        ServicePlates.Add(obj);
        obj.GetComponent<RectTransform>().SetSiblingIndex(1);
        obj.Init(new Service(), this);
    }
    public void DeleteServicePlate(ServicePlate plate)
    {
        foreach (var obj in ServicePlates)
        {
            if (obj == plate)
            {
                Destroy(plate.gameObject);
                properties.Services.Remove(obj.properties);
                ServicePlates.Remove(obj);
                return;
            }
        }
    }
    public void ChangeTableType(string type)
    {
        properties.ComputerType = type;
    }
    private void FixedUpdate()
    {
        float cost = 0;

        foreach(var obj in ServicePlates)
        {
            float.TryParse(obj.Info.text, out float c);
            cost += c; 
        }
    }
}
