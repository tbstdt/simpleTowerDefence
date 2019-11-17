using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CameraControl: MonoBehaviour
{
    [Range(0, 1)] public float CameraSpeed = 0.1f;
    [Range(1, 10)] public int Sensitivity = 5;
 
    public Bounds CameraBounds;

    void Update()
    {
       CameraMove();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(CameraBounds.center, CameraBounds.size);
    }
    
    private void CameraMove()
    {
        var finalPos = transform.position;
        var mousePos = Input.mousePosition;
   
        var x = GetNextPosition(mousePos.x, Screen.width);
        var z = GetNextPosition(mousePos.y, Screen.height);

        var deltaPos = new Vector3(x,0,z);
        if(deltaPos == Vector3.zero) return;
 
        finalPos += deltaPos;
        if (CameraBounds.Contains(finalPos))
            transform.DOMove(finalPos,0f);
    }

    private float GetNextPosition(float mousePosition, float maxPosition)
    {
        var result = 0f;
        
        if (mousePosition < Sensitivity)
            result = -CameraSpeed;
        else if (mousePosition > maxPosition - Sensitivity)
            result = CameraSpeed;
        return result;
    }
}