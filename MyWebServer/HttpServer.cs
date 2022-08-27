namespace MyWebServer
{
    using MyWebSurver.Http;
    using MyWebSurver.Routing;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;

        private readonly RoutingTable routingTable;

        public HttpServer(string ipAddress, int port, Action<IRoutingTable> routingTableConfig)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port; // 8080
            listener = new TcpListener(this.ipAddress, port);

            routingTableConfig(this.routingTable = new RoutingTable());
        }

        public HttpServer(int port, Action<IRoutingTable> routingTable)
            : this("127.0.0.1", port, routingTable)
        {
        }
        
        public HttpServer(Action<IRoutingTable> routingTable)
            : this(5000, routingTable)
        {
        }
       

        public async Task Start()
        {
            this.listener.Start();

            Console.WriteLine("Listening for requests...");

            while (true)
            {
                var connection = await this.listener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();

                var requestText = await this.ReadRequest(networkStream);

                Console.WriteLine(requestText);

                var request = HttpRequest.Parse(requestText);

                var response = this.routingTable.ExecuteRequest(request);

                await WriteResponse(networkStream, response);

                connection.Close();
            }
        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];

            var totalBytes = 0;

            var requestBuilder = new StringBuilder();

            do
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);

                totalBytes += bytesRead;

                if (bytesRead > 10 * 1024)
                {
                    throw new InvalidOperationException("Request is too large");
                }

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            while (networkStream.DataAvailable);

            return requestBuilder.ToString();
        }

        private async Task WriteResponse(NetworkStream networkStream, HttpResponse response)
        {
            var responseBites = Encoding.UTF8.GetBytes(response.ToString());

            await networkStream.WriteAsync(responseBites);
        }
    }
}