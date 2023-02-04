using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera gameCamera;
    private void Awake()
    {
        gameCamera = Camera.main;
    }

    public void MoveCamera()
    {
        gameCamera.transform.position = new Vector3(GameController.Instance.Level, GameController.Instance.Level, -10.0f);
    }

    public IEnumerator ChangeCameraSize()
    {
        float targetSize = gameCamera.orthographicSize + 1.0f;

        while (gameCamera.orthographicSize < targetSize)
        {
            gameCamera.orthographicSize += 0.004f;
            yield return new WaitForEndOfFrame();
        }
    }
}
