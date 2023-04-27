using UnityEngine;
[CreateAssetMenu(fileName = "APIConfig", menuName = "API Config", order = 1)]
public class APILoader : ScriptableObject
{
  [HideInInspector]
  public string apiKey;
}
