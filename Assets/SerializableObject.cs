using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializableObject : MonoBehaviour
{
    public virtual CSSerializedObject Save()
    {
        return new CSSerializedObject();
    }
    public virtual void Load(CSSerializedObject serializedObject)
    {

    }
}
