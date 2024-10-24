using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    [SerializeField] private GameObject managerSetting;

    public void OpenSetting()
    {
        if (managerSetting != null)
        {
            managerSetting.SetActive(true);
            Debug.Log("Open Setting Called"); 
        }
    }

    public void CloseSetting()
    {
        if (managerSetting != null)
        {
            managerSetting.SetActive(false);
            Debug.Log("Close Setting Called"); 
        }
    }
}
