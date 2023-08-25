using Cinemachine;
using DG.Tweening;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BattleCameraController : MonoBehaviour
{
    private BattleManager _battleManager;

    [SerializeField] private float transitionTime = 2;
    [SerializeField] private Transform mostLeftPoint;
    [SerializeField] private Transform currentMostRightPoint;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float minSize = 4.5f;
    [SerializeField] private float maxSize = 5;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxPointDistance;
    private Sequence _cameraSizeAnimation;
    
    public void Initialize(BattleManager battleManager)
    {
        _battleManager = battleManager;
        _battleManager.OnSquandChanged.AddListener(SquadChanged);
        _cameraSizeAnimation = DOTween.Sequence();
        SquadChanged();
    }

    private void SquadChanged()
    {
        currentMostRightPoint = _battleManager.EnemyPoints[Mathf.Max(2, _battleManager.MaxEnemyCount-1)].transform;
        UpdateValues();
    }

    void UpdateValues()
    {
        var leftPoint = mostLeftPoint.position;
        centerPoint.DOKill();
        centerPoint.DOMove(new Vector3(leftPoint.x + (currentMostRightPoint.position.x - leftPoint.x) / 2, 0, 0), transitionTime);

        float d = Vector3.Distance(leftPoint, currentMostRightPoint.transform.position) - minDistance;
        float percentage = d / (maxPointDistance - minDistance);

        _cameraSizeAnimation.Kill();
        _cameraSizeAnimation.Append(DOVirtual.Float(virtualCamera.m_Lens.OrthographicSize,
            Mathf.Lerp(minSize, maxSize, percentage), transitionTime, UpdateCameraSize));
    }

    private void UpdateCameraSize(float value)
    {
        virtualCamera.m_Lens.OrthographicSize = value;
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        var bM = FindObjectOfType<BattleManager>();
        var leftPosition = mostLeftPoint.position;
        minDistance = Vector3.Distance(leftPosition, bM.EnemyPoints[2].transform.position);
        maxPointDistance = Vector3.Distance(leftPosition, bM.EnemyPoints[4].transform.position);
        
        EditorUtility.SetDirty(this);
        
        UpdateValuesNow();
    }

    private void UpdateValuesNow()
    {
        var leftPoint = mostLeftPoint.position;
        centerPoint.transform.position =
            new Vector3(leftPoint.x + (currentMostRightPoint.position.x - leftPoint.x) / 2, 0, 0);

        float d = Vector3.Distance(leftPoint, currentMostRightPoint.transform.position) - minDistance;
        float percentage = d / (maxPointDistance - minDistance);
        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(minSize, maxSize, percentage);
    }
#endif
}
