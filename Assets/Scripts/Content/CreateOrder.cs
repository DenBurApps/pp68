using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateOrder : MonoBehaviour
{
    public BasePlate ChoosedPlate;
    private bool plateChoosed = false;
    public CheckMark[] checkMarks;
    public Order order;
    public InputFieldChanger Hours;


    private void Awake()
    {
        foreach (CheckMark item in checkMarks)
        {
            item.GetComponent<Button>().onClick.AddListener(() => { ChangeOrderStatus(item); });
        }
        CreateOrderButton.onClick.AddListener(CreateNewOrder);
        SpawnPlates();
    }
    public Button CreateOrderButton;
    private void FixedUpdate()
    {
        if (!plateChoosed)
        {
            CreateOrderButton.interactable = false;
            return;
        }
        if (Hours.text == "" || Hours.text == "0")
        {
            CreateOrderButton.interactable = false;
            return;
        }
        if (order.Status == "")
        {
            CreateOrderButton.interactable = false;
            return;
        }

        CreateOrderButton.interactable = true;
    }
    private void CreateNewOrder()
    {
        ChoosedPlate.properties.OcupiedComputers++;
        order.Table = ChoosedPlate.properties;

        float.TryParse(Hours.text, out float hours);
        order.Hours = hours;
        DataProcessor.Instance.allData.Orders.Add(order);
        Parser.StartSave();
        SpawnManager.Instance.SpawnAllPlates();
        Destroy(gameObject);
    }

    public void ChangeOrderStatus(CheckMark check)
    {
        foreach (CheckMark item in checkMarks)
        {
            if(item == check)
                order.Status = item.ReturnStatus();
            else
                item.DeactivateCheck();
        }
    }
    public BasePlate PlatePrefab;
    [SerializeField] private Transform spawnPoint;
    public GameObject PlatesHolder;
    public GameObject OpenButton;
    private void SpawnPlates()
    {
        foreach (var item in DataProcessor.Instance.allData.properties)
        {
            if (item.ComputersCount-item.OcupiedComputers > 0)
            {
                var obj = Instantiate(PlatePrefab, spawnPoint);
                obj.Init(item, null);
                obj.gameObject.GetComponent<RectTransform>().SetSiblingIndex(0);
                obj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    plateChoosed = true;
                    ChoosedPlate.Init(item, null);
                    PlatesHolder.SetActive(false);
                    OpenButton.SetActive(false);
                });
            }
        }
    }
}
