using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToScreenSpaceUI : MonoBehaviour
{
    [SerializeField, Tooltip("-1 = instantaneous")] private float _speed = -1;
    private GameObject _target;
    private RectTransform _canvasRect;
    private Camera _camera;
    private RectTransform _transform;

    private void Start()
    {
        _camera = Camera.main;
        _canvasRect =  FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        _transform = (RectTransform)transform;
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null) return;

        var pos = FindLocation();
        
        if (Mathf.Approximately(_speed , -1)) _transform.anchoredPosition = pos;
        else _transform.anchoredPosition = Vector2.Lerp(_transform.anchoredPosition, pos,_speed * Time.deltaTime);
        
    }

    Vector2 FindLocation()
    {
        var position = _target.transform.position;

        Vector2 viewportPosition = _camera.WorldToViewportPoint(position + Vector3.up);
        var sizeDelta = _canvasRect.sizeDelta;
        Vector2 worldObjectScreenPosition=new Vector2(
            ((viewportPosition.x*sizeDelta.x)-(sizeDelta.x*0.5f)),
            ((viewportPosition.y*sizeDelta.y)-(sizeDelta.y*0.5f)));

        return worldObjectScreenPosition;
    }
}
