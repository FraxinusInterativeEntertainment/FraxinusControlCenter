using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance;

    [SerializeField]
    private Transform m_UIContentRoot;
    [SerializeField]
    private Transform m_UIPopupRoot;

    private UIForm m_currentForm;
    private GameObject m_uiRoot;
    private ResourcesService m_resourcesService;

    public static UIManager instance { get { return m_instance; } }
    /*
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new GameObject(typeof(UIManager).Name).AddComponent<UIManager>();
            }

            return m_instance;
        }
    }*/

    
    void Awake()
    {
        DontDestroyOnLoad(this);

        m_instance = this;

        DontDestroyOnLoad(this.gameObject);
        m_resourcesService = new ResourcesService();
    }
    
    public void ShowForm(string _formName)
    {
        if (m_currentForm != null)
        {
            Destroy(m_currentForm.gameObject);
        }

        GameObject uiFormGO = m_resourcesService.Load<GameObject>(_formName);
        UIFormBase formGO = GameObject.Instantiate(uiFormGO).GetComponent<UIFormBase>();
        formGO.transform.SetParent(m_UIContentRoot);
        formGO.Anchor(0, 0, 0);
    }

    public void ShowView(string _viewName)
    {
        m_currentForm.ShowView(_viewName);
    }
}
