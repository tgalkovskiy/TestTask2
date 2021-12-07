
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    public Button _addForce;
    public Button _changeObject;
    public Button _backMenu;
    
    private void Awake()
    {
        ApplyButtonForScene(SceneManager.GetActiveScene().buildIndex != 0);
    }
    private void ApplyButtonForScene(bool isActive)
    {
        _addForce.gameObject.SetActive(isActive);
        _changeObject.gameObject.SetActive(isActive);
        _backMenu.GetComponentInChildren<Text>().text = isActive ? "MainMenu" : "Start";
    }
    
}


