using UnityEngine;
using UnityEngine.UI;

public class StarController : MonoBehaviour
{
    [SerializeField] private Image _starImage;
    [SerializeField] private Transform _starParentTransform;

    /// <summary>
    /// 難易度の星を表示する
    /// </summary>
    /// <param name="level">難易度</param>
    public void Init(int level)
    {
        foreach (Transform t in _starParentTransform)
        {
            t.gameObject.SetActive(false);
        }
        
        for (int i = 0; i < level; i++)
        {
            UpdateStar();
        }
    }

    private void UpdateStar()
    {
        //アクティブでないオブジェクトを探索
        foreach(Transform t in _starParentTransform)
        {
            if (!t.gameObject.activeSelf)
            {
                t.gameObject.SetActive(true);
                return;
            }
        }
        
        //非アクティブなオブジェクトがない場合は新規生成
        Instantiate(_starImage.gameObject, _starParentTransform);
    }
}
