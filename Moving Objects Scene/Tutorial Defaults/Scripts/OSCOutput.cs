using System.Collections.Generic;
using UnityEngine;
using OscJack;

public class OSCOutput : MonoBehaviour
{
    #region Singleton
    private static OSCOutput _instance;

    public static OSCOutput Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new OSCOutput();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public string IPAddress = "127.0.0.1"; // IP address for OSC
    public int oscPortOut = 9000;
    OscClient client;

    [SerializeField] GameObject _mainCamera;
    [SerializeField] GameObject _audioSourceObject1;

    void Start()
    {
        client = new OscClient(IPAddress, oscPortOut);
    }

    void OnDestroy()
    {
        client.Dispose();
    }

    void Update()
    {
        float sourcePosX = _audioSourceObject1.transform.position.z;
        float sourcePosY = _audioSourceObject1.transform.position.x * -1;
        float sourcePosZ = _audioSourceObject1.transform.position.y - 1.5f;
        client.Send("/RoomEncoder/sourceX", sourcePosX);
        client.Send("/RoomEncoder/sourceY", sourcePosY);
        client.Send("/RoomEncoder/sourceZ", sourcePosZ);
    }
}
