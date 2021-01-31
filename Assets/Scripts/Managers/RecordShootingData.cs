using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RecordShootingData 
{
    public static Dictionary<float, List<RecordedData>> m_ShootRecordData=new Dictionary<float, List<RecordedData>>();

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

        Debug.Log("time: " + time+ " data:" + data.instrument + ", " + data.duration);

    }
}
