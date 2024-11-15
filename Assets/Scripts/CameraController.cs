using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;


   

    private void Update()
    {
        CalculatePositionCamera();
    }

    void CalculatePositionCamera()
    {
      if(player.position.x > 9 && player.position.x < 28)
        {
            transform.position = new Vector3(19,transform.position.y, transform.position.z);
        }
        else if(player.position.x <=9)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
      else if (player.position.x > 28 && player.position.x < 47)
        {
            transform.position = new Vector3(38, transform.position.y, transform.position.z);
        }

      else if (player.position.x <= 28)
        {
            transform.position = new Vector3(19, transform.position.y, transform.position.z);
        }

        else if (player.position.x > 47 && player.position.x < 66)
        {
            transform.position = new Vector3(57, transform.position.y, transform.position.z);
        }

        else if (player.position.x <= 47)
        {
            transform.position = new Vector3(38, transform.position.y, transform.position.z);
        }
      else if(player.position.x > 66){
            transform.position = new Vector3(74.3f,transform.position.y,transform.position.z);
        }


    }
}
