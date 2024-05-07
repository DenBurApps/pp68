using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_IOS
using UnityEngine.iOS;
#endif
public class ShowRateUs : MonoBehaviour
{
    public GameObject RateUsObj;
    public Button RateButton;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Showed"))
        {
            PlayerPrefs.SetInt("Showed", 1);
            RateUsObj.SetActive(true);
            RateButton.onClick.AddListener(RateUs);
        }
        else
            RateUsObj.SetActive(false);
    }
    private void RateUs()
    {
#if UNITY_IOS
        Device.RequestStoreReview();
#endif
    }
}
