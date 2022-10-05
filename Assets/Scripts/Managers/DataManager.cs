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
            IgnoreComments = true,      // �ּ� ����
            IgnoreWhitespace = true     // ���� ����
        };

        using (XmlReader r = XmlReader.Create("Assets/Resources/Data/StartData.xml", xmlReaderSettings))
        {
            // ���� r.Dispose()�� �ؼ� �ݾ���� �ϴµ� using�� ����ϸ� Dispose�� �ڵ����� ���ش�.
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
