using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_IOS
using UnityEngine.iOS;
#endif
public class ShowRateUs : MonoBehaviour
{
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Showed"))
        {
            PlayerPrefs.SetInt("Showed", 1);
            RateUs();
        }
    }
    private void RateUs()
    {
#if UNITY_IOS
        Device.RequestStoreReview();
#endif
    }
}
