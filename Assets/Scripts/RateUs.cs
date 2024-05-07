using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUs : MonoBehaviour
{
    public string androidUrl;
    public string IOSUrl;

    public void Rate()
    {
#if UNITY_ANDROID
        Application.OpenURL(androidUrl);
#elif UNITY_IOS
        Application.OpenURL(IOSUrl);
#endif
    }
}
