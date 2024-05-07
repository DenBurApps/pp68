using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderPreview : MonoBehaviour
{
    public Order order;
    public BasePlate plate;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Hours;
    public TextMeshProUGUI Status;

    [Header("Resources")]
    public ServicePlate ResourcePlate;
    public Transform ServiceSpawnPlace;

    public void Init(Order props)
    {
        order = props;
        plate.Init(order.Table, null);
        Cost.text = order.Table.Price.ToString();
        Hours.text = order.Hours.ToString();
        Status.text = order.Status;

        FillServicePlatesList(props.Table.Services, ServiceSpawnPlace);
    }
    public void DeleteOrder()
    {
        order.Table.OcupiedComputers--;
        DataProcessor.Instance.allData.Orders.Remove(order);
        Parser.StartSave();
        SpawnManager.Instance.SpawnAllPlates();
        Destroy(gameObject);
    }

    private void FillServicePlatesList(List<Service> dataList, Transform spawnPlace)
    {
        foreach (var item in dataList)
        {
            var obj = Instantiate(ResourcePlate, spawnPlace);
            obj.GetComponent<RectTransform>().SetSiblingIndex(0);
            obj.Init(item, null);

            obj.Blocker.SetActive(true);
        }
    }
}
