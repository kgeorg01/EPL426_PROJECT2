using UnityEngine;
using System.Collections;

// Creates unique IDs (GUID) for objects 
public class UniqueIdentifierAttribute : PropertyAttribute { }

public class UniqueID : MonoBehaviour
{
    [UniqueIdentifier]
    public string uniqueId;
}