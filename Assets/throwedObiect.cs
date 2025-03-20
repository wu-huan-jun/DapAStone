using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Video;

public class throwedObiect : MonoBehaviour
{
    [SerializeField] private float forceMutiplier;//输入值变化率和rb加速度之间的乘算系数
    [SerializeField, Range(0, 1)] private float inputLerp;//输入值平滑度，（0，1）
    public Vector3 inputValue;//平滑后的输入向量
    [SerializeField] private Vector3 inputValueLastFrame;//上一帧的平滑输入向量
    public bool inputInitialized;//输入值是否已经归零
    Rigidbody rb;
    public Vector3 startpos;

    [Header("Grass")]
    [SerializeField] private GameObject grass;
    [SerializeField] private float grassPerSecond;
    [SerializeField] private float t;
    // [Header("Debug sliders")]
    // [SerializeField] private Slider mouseXSlider;
    // [SerializeField] private Slider mouseYSlider;
    // [SerializeField] private Text inputValueText;


    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        inputValueLastFrame = inputValue;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(LateStart());
    }
    // void Debug1()
    // {
    //     mouseXSlider.value = inputValue.x;
    //     mouseYSlider.value = inputValue.y;
    //     inputValueText.text = $"Input:{Input.GetAxis("Mouse X")},{Input.GetAxis("Mouse Y")} \nLerpedInput: ({inputValue.x}, {inputValue.y})";
    // }
    IEnumerator LateStart()//等待两秒，等待输入值稳定
    {
        yield return new WaitForSeconds(2);
        inputInitialized = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (inputInitialized)
        {
            inputValue = Vector3.Lerp(inputValue, new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y")), inputLerp);
            if (inputValue.magnitude > inputValueLastFrame.magnitude)
            {
                rb.AddForce((inputValue - inputValueLastFrame) * forceMutiplier, ForceMode.Acceleration);//当鼠标加速移动时，给刚体施加力
            }
            inputValueLastFrame = inputValue;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startpos;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        t += Time.deltaTime;
        if (t >= 1 / grassPerSecond)
        {
            if (grass != null&&transform.position.y<-10)
            {
               Grassland grassland = Instantiate(grass, new Vector3((int)transform.position.x,(int)transform.position.y,(int)transform.position.z) 
               + transform.up * -0.5f, Quaternion.identity).GetComponent<Grassland>();
               grassland.enabled = true;
                t = 0;
            }
        }
    }
}
