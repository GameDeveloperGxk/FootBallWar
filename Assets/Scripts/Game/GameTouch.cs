using UnityEngine;

using System.Collections;

using UnityEngine.UI;

//不手动添加ScrollRect组件，让脚本继承与ScrollRect，从而获得ScrollRect的属性、方法

public class GameTouch : ScrollRect
{
    private float mRadius;

    private Vector3 offsetVector3 = Vector3.zero;//摇杆的偏移

    protected override void Start()
    {
        base.Start();
        //计算摇杆移动半径
        mRadius = (transform as RectTransform).sizeDelta.x * 0.5f;
    }

    void Update()
    {
        CalculateOffect();
    }

    public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {

        //获得父类属性

        base.OnDrag(eventData);

        base.content.anchoredPosition = Vector3.ClampMagnitude(base.content.anchoredPosition, mRadius);

    }

    //计算偏移
    private void CalculateOffect()
    {

        offsetVector3 = content.localPosition / mRadius;

    }

    public Vector3 GetoffsetVector3()
    {

        return offsetVector3;

    }

}