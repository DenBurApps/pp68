using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    private List<BasePlate> AllPlates = new List<BasePlate>();

    [SerializeField] private BasePlate plate;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform spawnPreviewPoint;
    [SerializeField] private GameObject emptyObject;

    [SerializeField] private EditPlate CreateDataWindow;
    [SerializeField] private CreateOrder CreateOrderWindow;

    [SerializeField] private Transform CreateDataSpawnPlace;

    [Header("Orders")]
    public OrderPlate OrderPlate;
    private List<OrderPlate> orderPlates = new List<OrderPlate>();
    public void SpawnCreateDataWindow()
    {
        Instantiate(CreateDataWindow, CreateDataSpawnPlace);
    }
    public void SpawnCreateOrderWindow()
    {
        Instantiate(CreateOrderWindow, CreateDataSpawnPlace);
    }
    public void Start()
    {
        if (DataProcessor.Instance != null)
            SpawnAllPlates();
    }

    private void Awake()
    {
        Instance = this;
    }
    public void SpawnAllPlates()
    {
        ClearPlates();
        if (SpawnState)
            SpawnAllOrders();
        else
            SpawnAllTables();
    }
    public void SpawnAllTables()
    {
        foreach (var item in DataProcessor.Instance.allData.properties)
        {
            if (Filter(item))
            {
                var obj = Instantiate(plate, spawnPoint);
                obj.Init(item, spawnPreviewPoint);
                AllPlates.Add(obj);
                obj.gameObject.GetComponent<RectTransform>().SetSiblingIndex(0);
                obj.GetComponent<Button>().onClick.AddListener(obj.OpenPreview);
            }
        }
        if (AllPlates.Count == 0) emptyObject.SetActive(true); 
        else emptyObject.SetActive(false);

    }
    public void SpawnAllOrders()
    {
        foreach (var item in DataProcessor.Instance.allData.Orders)
        {
            var obj = Instantiate(OrderPlate, spawnPoint);
            obj.Init(item, spawnPreviewPoint);
            orderPlates.Add(obj);
            obj.gameObject.GetComponent<RectTransform>().SetSiblingIndex(0);
            obj.GetComponent<Button>().onClick.AddListener(obj.OpenPreview);
        }
        if (orderPlates.Count == 0) emptyObject.SetActive(true);
        else emptyObject.SetActive(false);
    }
    private void ClearPlates()
    {
        foreach (var plate in AllPlates)
            Destroy(plate.gameObject);
        AllPlates.Clear();

        foreach (var plate in orderPlates)
            Destroy(plate.gameObject);
        orderPlates.Clear();
    }
    public void ChangeSpawnState(bool state)
    {
        SpawnState = state;
        SpawnAllPlates();
    }
    public bool SpawnState = false;
    public TMP_InputField searchField;
    private bool Filter(Properties props)
    {
        if (!props.ProjectName.Contains(searchField.text))
            return false;

        return true;
    }
}
