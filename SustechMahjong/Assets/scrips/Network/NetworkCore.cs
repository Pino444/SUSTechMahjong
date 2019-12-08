using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class NetworkCore : MonoBehaviour {
    public string serverAddress;
    public int serverPort;
    
    private TcpClient _client;
    private NetworkStream _stream;  // C#中采用NetworkStream的方式, 可以类比于python网络编程中的socket
    private Thread _thread;
    private byte[] _buffer = new byte[1024];  // 接收消息的buffer
    private string receiveMsg = "";
    private bool isConnected = false;
    

    public void OnApplicationQuit() {
        SendData("Close");  // 退出的时候先发一个退出的信号给服务器, 使得连接被正确关闭
        Debug.Log("exit sent!");
        CloseConnection ();
    }
    
    public void Test() {
        SetupConnection();
        SendData("test");
        Debug.Log("start!");
    }

    // -----------------------private---------------------
    private void SetupConnection() {
        try {
            _thread = new Thread(ReceiveData);  // 传入函数ReceiveData作为thread的任务
            _thread.IsBackground = true;
            _client = new TcpClient(serverAddress, serverPort);
            _stream = _client.GetStream();
            _thread.Start();  // background thread starts working while loop
            isConnected = true;

        } catch (Exception e) {
            Debug.Log (e.ToString());
            CloseConnection ();
        }
    }

    private void ReceiveData() {  // 这个函数被后台线程执行, 不断地在while循环中跑着
        Debug.Log ("Entered ReceiveData function...");
        if (!isConnected)  // stop the thread
            return;
        int numberOfBytesRead = 0;
        while (isConnected && _stream.CanRead) {
            try {
                numberOfBytesRead = _stream.Read(_buffer, 0, _buffer.Length);
                receiveMsg = Encoding.ASCII.GetString(_buffer, 0, numberOfBytesRead);
                _stream.Flush();
                Debug.Log(receiveMsg);
                receiveMsg = "";
            } catch (Exception e) {
                Debug.Log (e.ToString ());
                CloseConnection ();
            }
        }
    }

    private void SendData(String msgToSend)
    {
        byte[] bytesToSend = Encoding.ASCII.GetBytes(msgToSend);
        if (_stream.CanWrite)
        {
            _stream.Write(bytesToSend, 0, bytesToSend.Length);
        }
    }

    private void CloseConnection() {
        if (isConnected) {
            _thread.Interrupt ();  // 这个其实是多余的, 因为isConnected = false后, 线程while条件为假自动停止
            _stream.Close ();
            _client.Close ();
            isConnected = false;
            receiveMsg = "";
        }
    }

}
