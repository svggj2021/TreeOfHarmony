using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class RecordShootingData 
{
    public static Dictionary<float, List<RecordedData>> m_ShootRecordData=new Dictionary<float, List<RecordedData>>();
    public static Dictionary<float, List<RecordedData>> allRecordedData =new Dictionary<float, List<RecordedData>>();

    public static void AddEntry(float time,RecordedData data)

    {

        //Don't need to check for repeated notes on an eigth next to impossible
        if(m_ShootRecordData.ContainsKey(time))
        {
            if (m_ShootRecordData[time] == null)
                m_ShootRecordData[time] = new List<RecordedData>();
            m_ShootRecordData[time].Add(data);
        }
        else
        {
            m_ShootRecordData[time] = new List<RecordedData>() { data };
        }

    /*    Debug.Log("time: " + time+ " data:" + data.instrument + ", " + data.duration);*/

    }

    public static void MakeCopy()
    {
       foreach(var kvp in m_ShootRecordData)
        {
            if (allRecordedData.ContainsKey(kvp.Key))
            {
                allRecordedData[kvp.Key].AddRange(kvp.Value);
            }
            else
            {
                allRecordedData[kvp.Key] = kvp.Value;
            }
        }
        m_ShootRecordData = new Dictionary<float, List<RecordedData>>();
    }
}
