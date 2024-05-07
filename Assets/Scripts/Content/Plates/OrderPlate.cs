using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OrderPlate : MonoBehaviour
{
    public Order properties;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Time;
    [SerializeField] private TextMeshProUGUI Status;

    [SerializeField] private Image TypeImage;

    [SerializeField] private ImageWithType[] Sprites;

    [SerializeField] private OrderPreview preview;
    private Transform previewSpawnPlace;
    public void Init(Order props, Transform previewSpawnPlace)
    {
        properties = props;
        if(props.Table == null)
        {
            DataProcessor.Instance.allData.Orders.Remove(props);
            Destroy(gameObject);
            return;
        }
        Name.text = props.Table.ProjectName;
        Name.gameObject.GetComponent<TruncateText>().Truncate();
        Time.text = props.Hours + " hours";
        Status.text = props.Status;
        this.previewSpawnPlace = previewSpawnPlace;

        foreach (var item in Sprites)
        {
            if (item.Name == props.Table.ComputerType)
            {
                TypeImage.sprite = item.Sprite;
                break;
            }
        }
    }

    internal void OpenPreview()
    {
        var obj = Instantiate(preview, previewSpawnPlace);
        obj.Init(properties);
    }
}
