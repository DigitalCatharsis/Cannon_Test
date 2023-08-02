using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Vector3 _screenPosition;
    private Vector3 _worldPosition;
    private GameObject _pointer;

    private void Start()
    {

        var obj = Resources.Load("JackPointer") as GameObject;
        _pointer = GameObject.Instantiate(obj);

    }

    private void Update()
    {
        ImitatePointer();
        //RotatePlayerMesh();
    }

    private void ImitatePointer()
    {
        _screenPosition = Input.mousePosition;
        _screenPosition.z = Camera.main.nearClipPlane + 1;

        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);

        _pointer.transform.position = _worldPosition;
        Debug.Log(_pointer.transform.position);
    }

    private void RotatePlayerMesh()
    {
        this.transform.root.transform.LookAt(_pointer.transform);
    }

}
