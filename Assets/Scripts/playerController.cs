using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector2 PositionInitial;
    Rigidbody2D _compRigidbody2D;
    [SerializeField] float xMin, xMax, yMin, yMax;
    [SerializeField] float currentX, currentY;
    [SerializeField] Slider sliderLife;
    [SerializeField] float maxLife;
    [SerializeField] float forceJump;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float longitudRayCast;
    [SerializeField] float ImpactIntensityHorizontal;
    [SerializeField] float ImpactIntensityVertical;
    [SerializeField] Sprite spriteMaskable;
    [SerializeField] float damageHumo;
    IEnumerator coroutineDamage;
    PlayerInput input;
    SpriteRenderer _compSpriteRenderer;
    RaycastHit2D hit;
    bool canJump;
    bool isGround;
    bool isInvunerability =false;
    float horizontal;
    float vertical;
    bool isSpacePressed;
    bool isJump;
    bool isTakeDoor;
    bool isSliderDoorComplete = false;
    Vector2 currentPosition;
    GameObject aux;
    public static event Action OnCollisionDoor;
    public static event Action OnCollisionDoorExit;
    public static event Action OnFillingSlider;
    public static event Action OnPlayerDeath;
    public static event Action OnStepExplosive;

    private void Awake()
    {
        _compRigidbody2D = GetComponent<Rigidbody2D>();
        _compSpriteRenderer = GetComponent<SpriteRenderer>();
        input = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        transform.position = PositionInitial;
        sliderLife.maxValue = maxLife;
        sliderLife.value = maxLife;
    }


    private void Update()
    {
        currentX = Mathf.Clamp(transform.position.x, xMin,xMax);
        currentY = Mathf.Clamp(transform.position.y, yMin,yMax);
        
        transform.position = new Vector2(currentX, currentY);  
    }

    public void AxisY(InputAction.CallbackContext context)
    {
       
        vertical = context.ReadValue<float>();
        
    }

    public void AxisX(InputAction.CallbackContext context)
    {
       
        horizontal = context.ReadValue<float>();
       
    } 

    public void Rellenar(InputAction.CallbackContext context)
    {
        if (context.performed && isTakeDoor == true){
            OnFillingSlider?.Invoke();
            if (isSliderDoorComplete ==true)
            {
                Destroy(aux);
            }
        }
      
    }

    public void Jump(InputAction.CallbackContext context)
    {
            isJump = context.performed;
        
    }

    private void FixedUpdate()
    {
       
        _compRigidbody2D.velocity = new Vector2(horizontal *speed, _compRigidbody2D.velocity.y);
        CheckRaycast();
        if (isJump)
        {
            if (canJump)
            {
                _compRigidbody2D.AddForce(new Vector2 (0,forceJump),ForceMode2D.Impulse);
                isJump = false; 
            }
        }

    }
    private void CheckRaycast()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRayCast, groundLayer);
        if (hit.collider != null)
        {
          canJump = true;
        }
        else
        {
            canJump = false;
        }
    }



    private void OnEnable()
    {
        PuzzlesManagerController.OnCompleteSliderDoor += CheckCompleteSliderDoor;
    }

    private void OnDisable()
    {
        PuzzlesManagerController.OnCompleteSliderDoor -= CheckCompleteSliderDoor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "door")
        {
            isTakeDoor = true;
            aux = collision.gameObject;
            OnCollisionDoor?.Invoke();
        }

        if (collision.gameObject.tag == "block")
        {
            _compRigidbody2D.AddForce(Vector2.left * ImpactIntensityHorizontal);
            // _compRigidbody2D.AddForce(Vector2.down * ImpactIntensityVertical);

        }

        if (collision.gameObject.tag == "mouse")
        {
            _compRigidbody2D.AddForce(Vector2.right * ImpactIntensityHorizontal);
            _compRigidbody2D.AddForce(Vector2.up * ImpactIntensityVertical);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "door")
        {
            isTakeDoor = false;
            OnCollisionDoorExit?.Invoke();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if( collision.gameObject.tag == "maskable")
        {
            Destroy(collision.gameObject);
            SetNewSprite(spriteMaskable);
            isInvunerability = true;
        }
        if( collision.gameObject.tag == "humo" && isInvunerability ==false)
        {
            coroutineDamage = GetDamageProgresive(damageHumo);
            StartCoroutine(coroutineDamage);
        }

        if(collision.gameObject.tag == "mina")
        {
            OnStepExplosive?.Invoke();
                
            DesactiveInput();
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "humo" && isInvunerability == false)
        {

            StopCoroutine(coroutineDamage);
           
        }
    }


    IEnumerator GetDamageProgresive(float damage)
    {
        while (true)
        {
            if (sliderLife.value > 0)
            {
                sliderLife.value -= damage;
                yield return new WaitForSeconds(0.5f);

            }
            if (sliderLife.value <= 0)
            {
                OnPlayerDeath?.Invoke();
                gameObject.SetActive(false);    
                break;
            }
        }
        
        

    }

    


void CheckCompleteSliderDoor(bool auxBool)
    {
        isSliderDoorComplete = auxBool;
    }


    void SetNewSprite(Sprite sprite)
    {
        _compSpriteRenderer.sprite = sprite;
    }


    void DesactiveInput()
    {
        input.enabled = false;
    }

    void ActiveInput()
    {
        input.enabled  = true;
    }
}



