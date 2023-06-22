using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    public class SocketClient
    {
        private string? clientName;
        public ConsoleColor clientColor;
        public void StartClient()
        {
            Console.WriteLine("Input server ip to connect: ");
            IPAddress iPServerAddress = IPAddress.Parse(Console.ReadLine());
            //IPAddress iPServerAddress = IPAddress.Parse("10.233.149.105");
            IPEndPoint serverEndPoint = new(iPServerAddress, 22222);

            Console.WriteLine("Enter your name:"); //intercepts the client and makes them write their name. Has no exception handling.
            clientName = Console.ReadLine();

            Console.WriteLine("Select your color (Red, Green, Blue, Yellow, White, Cyan, Magenta):"); //gives an example of possible colors
            clientColor = Enum.Parse<ConsoleColor>(Console.ReadLine(), true); 
            
            
            Socket sender = new(
                iPServerAddress.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);
            sender.Connect(serverEndPoint);
            Console.WriteLine("Connected to: " + serverEndPoint.ToString());
            
            while (sender.Connected) CreateMessage(sender);
            
        }

        void CreateMessage(Socket sender)
        {

            Console.ForegroundColor = clientColor; //the only way to make sure the server knows the client details is to send it to it.
            string msg = clientName + ":" + Console.ReadLine() + "<EOM>";
            
            
            
            byte[] byteArr = Encoding.Unicode.GetBytes(msg); //Encodes the message and defines what a ByteArr is.
            sender.Send(byteArr); //sends the message as a byte array to the server

            
            string? returnMsg = ClassLibrary1.Class1.GetMessage(sender); //I should make a new constructre that sends client details
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(returnMsg); //the server message
            
        }



       //* void SendClientDetails(Socket sender) something like this
        {
            // Combine client name and color into a string message
           // string clientDetails = $"{clientName}:{clientColor}";

            // Send client details to the server
            //byte[] detailsBytes = Encoding.Unicode.GetBytes(clientDetails);
            //sender.Send(detailsBytes);
        }
    
    }
}