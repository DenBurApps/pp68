using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasePlate : MonoBehaviour
{
    public Properties properties;

    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Price;
    [SerializeField] private TextMeshProUGUI Type;
    [SerializeField] private TextMeshProUGUI ComputersCount;

    [SerializeField] private Image TypeImage;

    [SerializeField] private ImageWithType[] Sprites;

    [SerializeField] private Preview preview;
    private Transform previewSpawnPlace;
    public void Init(Properties props,Transform previewSpawnPlace)
    {
        properties = props;
        this.previewSpawnPlace = previewSpawnPlace;

        Name.text = props.ProjectName;
        Name.gameObject.GetComponent<TruncateText>().Truncate();
        Price.text = "$" + props.Price + " an hour";
        Type.text = props.ComputerType;
        ComputersCount.text = (props.ComputersCount - props.OcupiedComputers).ToString() + " free computers";
        foreach (var item in Sprites)
        {
            if(item.Name == props.ComputerType)
            {
                TypeImage.sprite = item.Sprite;
                break;
            }    
        }
    }

    public void OpenPreview()
    {
        var obj = Instantiate(preview,previewSpawnPlace);
        obj.Init(properties);
    }
}
