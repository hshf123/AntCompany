using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DataManager
{
    public void Init()
    {
        // TODO
        ReadXMLData();
    }

    void ReadXMLData()
    {
        XmlReaderSettings xmlReaderSettings = new XmlReaderSettings()
        {
            IgnoreComments = true,      // 주석 무시
            IgnoreWhitespace = true     // 공백 무시
        };

        using (XmlReader r = XmlReader.Create("Assets/Resources/Data/StartData.xml", xmlReaderSettings))
        {
            // 원래 r.Dispose()를 해서 닫아줘야 하는데 using을 사용하면 Dispose를 자동으로 해준다.
            r.MoveToContent();

            while (r.Read())
            {
                if (r.Depth == 1 && r.NodeType == XmlNodeType.Element)
                {
                    for (int i = 0; i < r.AttributeCount; i++)
                    {
                        Debug.Log($"{r.GetAttribute(i)}");
                    }
                }
            }
        }

    }
}
