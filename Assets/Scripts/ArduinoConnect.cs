/*using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ArduinoConnect : MonoBehaviour
{
    public SerialPort serialPort;
    private string dataFlow;
    private string[] dataSet;
    private int? xRaw = null;
    private int? yRaw = null;
    private int? zRaw = null;
    private List<int?> xyzCoordinateRaw;
    // Start is called before the first frame update
    void Start()
    {
        serialPort = new SerialPort("COM3", 9600);
        serialPort.Open();
        xyzCoordinateRaw = new List<int?> { xRaw, yRaw, zRaw };
    }

    // Update is called once per frame
    void Update()
    {
        GetCoordinate();
        UpdateCoordinateList();
    }

    // Get the current coordinate
    void GetCoordinate()
    {
        if (serialPort.IsOpen)
        {
            dataFlow = serialPort.ReadLine();
            //Debug.Log(dataFlow);
            dataSet = dataFlow.Split(' ');
            if (dataSet.Length == 3 && dataSet[0].Length < 4)
            {
                for (int i = 0; i < dataSet.Length; i++)
                {
                    if (i == 0)
                    {
                        xRaw = int.Parse(dataSet[i]);
                    }
                    else if (i == 1)
                    {
                        yRaw = int.Parse(dataSet[i]);
                    }
                    else if (i == 2)
                    {
                        zRaw = int.Parse(dataSet[i]);
                    }
                }
            }
        }
    }

    // Update the xyz list
    void UpdateCoordinateList()
    {
        xyzCoordinateRaw[0] = xRaw;
        xyzCoordinateRaw[1] = yRaw;
        xyzCoordinateRaw[2] = zRaw;
    }

    // Convert the y into vertical input number
    public float ConvertYCoordinate()
    {
        if (yRaw == null)
        {
            return 0.0f;
        }
        else if (yRaw > 153)
        {
            return 1.0f;
        }
        else if (yRaw < 103)
        {
            return -1.0f;
        }
        else
        {
            return ((float)(yRaw - 128) / 25.0000f);
        }
    }

    // Get the coordinate list
    public List<int?> GetCurrentCoordinate()
    {
        return xyzCoordinateRaw;
    }
}
*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;

public class ArduinoConnect : MonoBehaviour
{
    public SerialPort serialPort;
    private string dataFlow;
    private string[] dataSet;
    private int? xRaw = null;
    private int? yRaw = null;
    private int? zRaw = null;
    private List<int?> xyzCoordinateRaw;
    // Start is called before the first frame update
    void Start()
    {
        serialPort = new SerialPort("COM3", 9600);
        serialPort.Open();
        xyzCoordinateRaw = new List<int?> { xRaw, yRaw, zRaw };
    }

    // Update is called once per frame
    void Update()
    {
        GetCoordinate();
        UpdateCoordinateList();
    }

    // Get the current coordinate
    void GetCoordinate()
    {
        if (serialPort.IsOpen)
        {
            dataFlow = serialPort.ReadLine();
            //Debug.Log(dataFlow);
            dataSet = dataFlow.Split(' ');
            if (dataSet.Length == 3 && dataSet[0].Length < 4)
            {
                for (int i = 0; i < dataSet.Length; i++)
                {
                    if (i == 0)
                    {
                        xRaw = int.Parse(dataSet[i]);
                    }
                    else if (i == 1)
                    {
                        yRaw = int.Parse(dataSet[i]);
                    }
                    else if (i == 2)
                    {
                        zRaw = int.Parse(dataSet[i]);
                    }
                }
            }
        }
    }

    // Update the xyz list
    void UpdateCoordinateList()
    {
        if (serialPort.IsOpen)
        {
            xyzCoordinateRaw[0] = xRaw;
            xyzCoordinateRaw[1] = yRaw;
            xyzCoordinateRaw[2] = zRaw;
        }
            
    }

    // Convert the y into vertical input number
    public float ConvertYCoordinate()
    {
        if (yRaw == null)
        {
            return 0.0f;
        }
        else if (yRaw > 153)
        {
            return 1.0f;
        }
        else if (yRaw < 103)
        {
            return -1.0f;
        }
        else
        {
            return ((float)(yRaw - 128) / 25.0f);
        }
    }

    // Get the coordinate list
    public List<int?> GetCurrentCoordinate()
    {
        return xyzCoordinateRaw;
    }
}
