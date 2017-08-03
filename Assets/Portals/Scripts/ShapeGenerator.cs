using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    public PrimitiveType Shape;

    public Material ShapeMaterial;

    [Tooltip("Select only 1 layer")]
    public LayerMask layer;

    public int HowMany = 4;

    private Vector3 scaleSize = Vector3.one / 4;

    IEnumerator Start()
    {
        GameObject currentShape;
        for (int i = 0; i < HowMany; i++)
        {
            currentShape = GameObject.CreatePrimitive(Shape);
            currentShape.transform.SetParent(this.transform);
            currentShape.transform.localPosition = new Vector3(Random.value, 0, Random.value);
            currentShape.transform.localScale = scaleSize;
            currentShape.GetComponent<Renderer>().material = ShapeMaterial;
            currentShape.name += i;
            var rigid = currentShape.AddComponent<Rigidbody>();
            rigid.useGravity = Random.value >= 0.5;
            currentShape.layer = ToLayer(layer.value);
            yield return null;
        }
    }

    void Update()
    {
    }

    public static int ToLayer(int bitmask)
    {
        int result = bitmask > 0 ? 0 : 31;
        while (bitmask > 1)
        {
            bitmask = bitmask >> 1;
            result++;
        }
        return result;
    }
}
