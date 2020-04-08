using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

// Client app is the one sending messages to a Server/listener.   
// Both listener and client can send messages back and forth once a   
// communication is established.  
public class mountClient
{
    public static Socket SocketConnection;

    public static void sendMessage(string message)
    {
        byte[] msg = new byte[50];
        msg = Encoding.ASCII.GetBytes(message);

        // Send the data through the commPort.    
        int bytesSent = SocketConnection.Send(msg);
    }

    public static void receiveMessage()
    {
        byte[] messageReceived = new byte[50];
        int byteRecv = SocketConnection.Receive(messageReceived);
        //string msg = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
        //string [] SplittedMsg  = System.Text.RegularExpressions.Regex.Split(msg, "#");
        /*
        switch ( ( command ) Int32.Parse( SplittedMsg[0] )  )
        {
            case command.AbortSlewDone :
                ASCOM.astromakerMount.Telescope.Slewing = false ;
                break;

            case command.TrackDone :
                ASCOM.astromakerMount.Telescope.Tracking = true;
                break;

            case command.StopTrackDone:
                ASCOM.astromakerMount.Telescope.Tracking = false;
                break;

            default:
                return;
        }*/
    }

    public static bool StartClient(string[] buffer)
    {
        Console.WriteLine("client starting");
        byte[] bytes = new byte[1024];

        try
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = IPAddress.Parse("192.168.1.2");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
            SocketConnection = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            // Connect the commPort to the remote endpoint. Catch any errors.    
            try
            {
                SocketConnection.Connect(remoteEP);
                Console.WriteLine("connection established");
                return true;
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                return false;
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
                return false;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return false;
        }
    }

    public static void stopClient()
    {
        // Release the commPort.  
        string msg = "closeConnection#";
        sendMessage(msg);
        SocketConnection.Shutdown(SocketShutdown.Both);
        SocketConnection.Close();
    }
}