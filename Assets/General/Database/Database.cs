using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "ScriptableObjects/Database", order = 1)]
public class Database : ScriptableObject
{
    public static List<GameObject> Prefabs { get { return Instance.PrefabsField; } }
    public List<GameObject> PrefabsField = new List<GameObject>();

    public static AnimationCurve SmoothCurve { get { return Instance.SmoothCurveField; } }
    public AnimationCurve SmoothCurveField = new AnimationCurve();

    public static AnimationCurve PopupCurve { get { return Instance.PopupCurveField; } }
    public AnimationCurve PopupCurveField = new AnimationCurve();

    public static AnimationCurve FastSmoothCurve { get { return Instance.FastSmoothCurveField; } }
    public AnimationCurve FastSmoothCurveField = new AnimationCurve();

    public static AnimationCurve ElasticPopupCurve { get { return Instance.ElasticPopupCurveField; } }
    public AnimationCurve ElasticPopupCurveField = new AnimationCurve();

    public static AnimationCurve MountainCurve { get { return Instance.MountainCurveField; } }
    public AnimationCurve MountainCurveField = new AnimationCurve();


    private static Database Instance 
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = (Resources.Load("Database") as Database);
            }

            return m_instance;
        }
    }

    private static Database m_instance = null;
}
