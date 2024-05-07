using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServicePlate : MonoBehaviour
{
    public Service properties;
    public GameObject Blocker;
    public InputFieldChanger Name;
    public InputFieldChanger Info;

    public Button DeleteButton;

    public void Init(Service props,EditPlate editPlate)
    {
        if (editPlate == null)
        {
            DeleteButton.gameObject.SetActive(false);
        }
        else
        {
            DeleteButton.onClick.AddListener(() =>
            {
                editPlate.DeleteServicePlate(this);

            });
        }
        Name.ChangeText(props.Name);
        Info.ChangeText(props.Info1);
    }
    public Service GetData()
    {
        properties.Name = Name.text;
        properties.Info1 = Info.text;
        return properties;
    }
}
