using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

public class PythonSocketListener : MonoBehaviour
{
    private TcpListener listener;
    private Thread listenThread;
    public BigFishController fishController; // Assign this in Inspector

    private ConcurrentQueue<string> commandQueue = new ConcurrentQueue<string>();

    void Start()
    {
        listenThread = new Thread(ListenThread);
        listenThread.IsBackground = true;
        listenThread.Start();
    }

    void ListenThread()
    {
        listener = new TcpListener(System.Net.IPAddress.Any, 5005);
        listener.Start();

        while (true)
        {
            using (var client = listener.AcceptTcpClient())
            using (var stream = client.GetStream())
            {
                byte[] buffer = new byte[256];
                int bytes = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytes).Trim().ToLower();

                Debug.Log("Received from Python: " + message);
                commandQueue.Enqueue(message);
            }
        }
    }

    void Update()
    {
        while (commandQueue.TryDequeue(out string command))
        {
            if (command.Contains("bite"))
            {
                fishController.Bite();
            }
            else if (command.Contains("up"))
            {
                fishController.MoveUp();
            }
            else if (command.Contains("down"))
            {
                fishController.MoveDown();
            }
            else if (command.Contains("block"))
            {
                fishController.Block(); // 🛡️ Add this line
            }
        }
    }

}
