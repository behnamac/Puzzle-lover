using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [Header("Panels")]
        [SerializeField] private PanelController[] panels;
        private Dictionary<string, PanelController> _panelDic;

        [Header("Texts")]
        [SerializeField] private Text levelNumberText;

        [Header("Floats")]
        [SerializeField] private float showCompeletPanelDelay;
        [SerializeField] private float showFailPanelDelay;

        #region Unity Function
        private void Awake()
        {
            Instance = this;

            GameManager.onLevelStart += onLevelStart;
            GameManager.onLevelCompelet += onLevelCompelet;
            GameManager.onLevelFail += onLevelFail;

            _panelDic = new Dictionary<string, PanelController>();
            for (int i = 0; i < panels.Length; i++)
            {
                _panelDic.Add(panels[i].panelName, panels[i]);
            }
        }
        // Start is called before the first frame update
        private void Start()
        {
            if (levelNumberText != null)
                levelNumberText.text = "Level " + PlayerPrefsManager.GetLevelNumber();
            else
                Debug.LogWarning("Set Level_text");

            ActivePanel("Start");
        }
        private void OnDestroy()
        {
            GameManager.onLevelStart -= onLevelStart;
            GameManager.onLevelCompelet -= onLevelCompelet;
            GameManager.onLevelFail -= onLevelFail;
        }
        #endregion

        #region Private Functions
        private void ActiveCompeletPanel() 
        {
            ActivePanel("Compelet");
        }
        private void ActiveFailPanel()
        {
            ActivePanel("Fail");
        }
        #endregion

        #region Events
        private void onLevelStart()
        {
            ActivePanel("GamePlay");
        }
        private void onLevelCompelet()
        {
            Invoke(nameof(ActiveCompeletPanel), showCompeletPanelDelay);
        }
        private void onLevelFail()
        {
            Invoke(nameof(ActiveFailPanel), showFailPanelDelay);
        }
        #endregion

        #region Public Functions
        public void ActivePanel(string panelname)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].panel.SetActive(false);
            }

            _panelDic[panelname].panel.SetActive(true);
        }
        //Buttons
        public void StartButton()
        {
            GameManager.Instance.LevelStart();
        }
        public void ResetLevelButton()
        {
            GameManager.Instance.ResetLevel();
        }
        #endregion
    }
    [System.Serializable]
    public class PanelController
    {
        public string panelName;
        public GameObject panel;
    }
}
