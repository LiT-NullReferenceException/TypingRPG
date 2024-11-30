using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectGenerator : MonoBehaviour
{
    [SerializeField] private int _screenButtonCount = 6;
    
    [SerializeField] private StageData _stageData;

    [SerializeField] private Button _nextPageButton;
    [SerializeField] private Button _backPagebutton;
    [SerializeField] private TextMeshProUGUI _pageText;
    [SerializeField] private RectTransform _stageGroupParent;
    [SerializeField] private StageSelectNode _stageSelectNode;
    [SerializeField] private StageGroup _stageGroup;
    [SerializeField] private StageHelper _stageHelper;
    //[SerializeField] private AudioManager audioManager = null;

    private List<GameObject> _stageGroups = new List<GameObject>();
    private int _pageInProgress = 0;

    private void Awake()
    {
        Generate();
        _nextPageButton.onClick.AddListener(() => NextPage(1));
        _backPagebutton.onClick.AddListener(() => NextPage(-1));
        _backPagebutton.interactable = false;
        _pageText.text = $"{_pageInProgress + 1} / {_stageGroups.Count}";
        
        // AudioManager を参照する
        //audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }

    /// <summary>
    /// ステージ選択ボタンを生成する
    /// </summary>
    public void Generate()
    {
        int buttonCount = 0;
        while (buttonCount < _stageData.Enemies.Count)
        {
            StageGroup stageGroup = Instantiate(_stageGroup.gameObject, _stageGroupParent).GetComponent<StageGroup>();
            _stageGroups.Add(stageGroup.gameObject);
            List<StageData.TypingEnemy> enemies = new List<StageData.TypingEnemy>();
            
            for (int i = 0; i < _screenButtonCount; i++)
            {
                enemies.Add(_stageData.Enemies[buttonCount]);
                buttonCount++;
                if(buttonCount >= _stageData.Enemies.Count) break;
            }
            
            stageGroup.Init(enemies, _stageSelectNode, _stageHelper);
            if (buttonCount != _screenButtonCount) stageGroup.SetActive(false);
        }
    }

    /// <summary>
    /// ページをめくる
    /// </summary>
    /// <param name="pageTurnCount">めくるページ数</param>
    /// <returns>めくれるか否か</returns>
    public bool NextPage(int pageTurnCount)
    {
        AudioManager.instance_AudioManager.PlaySE(4);
        if (_pageInProgress + pageTurnCount >= _stageGroups.Count) return false;
        if (_pageInProgress + pageTurnCount < 0) return false;
            
        _stageGroups[_pageInProgress].SetActive(false);
        _pageInProgress += pageTurnCount;
        _stageGroups[_pageInProgress].SetActive(true);

        _nextPageButton.interactable = _pageInProgress != _stageGroups.Count - 1;
        _backPagebutton.interactable = _pageInProgress != 0;

        _pageText.text = $"{_pageInProgress + 1} / {_stageGroups.Count}";
        return true;
    }
}
