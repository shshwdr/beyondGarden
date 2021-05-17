using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializableObject : MonoBehaviour
{
    public virtual SerializedObject Save()
    {
        return new SerializedObject();
    }
    public virtual void Load(SerializedObject serializedObject)
    {

    }
}
