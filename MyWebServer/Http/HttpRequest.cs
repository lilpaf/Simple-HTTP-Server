namespace MyWebSurver.Http
{
    using System.Web;

    public class HttpRequest
    {
        private static Dictionary<string, HttpSession> Sessions = new();

        private const string NewLine = "\r\n"; //Environment.NewLine

        public HttpMethod Method { get; private set; }

        public string Path { get; private set; }

        public IReadOnlyDictionary<string, string> Query { get; private set; }

        public IReadOnlyDictionary<string, HttpHeader> Headers { get; private set; }

        public IReadOnlyDictionary<string, HttpCookie> Cookies { get; private set; }

        public IReadOnlyDictionary<string, string> Form { get; private set; }

        public string Body { get; private set; }

        public HttpSession Session { get; private set; }

        public static HttpRequest Parse(string request)
        {
            var lines = request.Split(NewLine);

            var startLine = lines.First().Split(" ");

            var method = ParseMethod(startLine[0]);
            var url = startLine[1];

            var (path, query) = ParseUrl(url);

            var headers = ParseHeaders(lines.Skip(1));

            var cookies = ParseCookies(headers);

            var session = GetSession(cookies);

            var bodyLines = lines.Skip(headers.Count + 2).ToArray();

            var body = string.Join(NewLine, bodyLines);

            var form = ParseForm(headers, body);

            return new HttpRequest
            {
                Method = method,
                Path = path,
                Query = query,
                Headers = headers,
                Cookies = cookies,
                Session = session,
                Body = body,
                Form = form,
            };
        }

        private static (string, Dictionary<string, string>) ParseUrl(string url)
        {
            var urlParts = url.Split('?', 2);

            var path = urlParts[0];
            var query = urlParts.Length > 1
                ? ParseQuery(urlParts[1])
                : new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            return (path, query);
        }

        private static Dictionary<string, string> ParseQuery(string queryString)
        => HttpUtility.UrlDecode(queryString)
            .Split('&')
            .Select(part => part.Split('='))
            .Where(part => part.Length == 2)
            .ToDictionary(
                part => part[0], 
                part => part[1], 
                StringComparer.InvariantCultureIgnoreCase);


        private static Dictionary<string, HttpHeader> ParseHeaders(IEnumerable<string> headerLines)
        {
            var headerCollection = new Dictionary<string, HttpHeader>(StringComparer.InvariantCultureIgnoreCase);

            foreach (var headerLine in headerLines)
            {
                if (headerLine == String.Empty)
                {
                    break;
                }

                var headerParts = headerLine.Split(":", 2);

                if (headerParts.Length != 2)
                {
                    throw new InvalidOperationException("Request is not valid");
                }

                var headerName = headerParts[0];
                var headerValue = headerParts[1].Trim();

                var header = new HttpHeader(headerName, headerValue);

                headerCollection[headerName] = header;
            }

            return headerCollection;
        }

        private static HttpSession GetSession(Dictionary<string, HttpCookie> cookies)
        {
            var sessionId = cookies.ContainsKey(HttpSession.SessionCookieName)
                ? cookies[HttpSession.SessionCookieName].Value
                : Guid.NewGuid().ToString();

            if (!Sessions.ContainsKey(sessionId))
            {
                Sessions[sessionId] = new HttpSession(sessionId) 
                { 
                    IsNew = true 
                };
            }

            return Sessions[sessionId];
        }

        private static Dictionary<string, HttpCookie> ParseCookies(Dictionary<string, HttpHeader> headers)
        {
            var cookieCollection = new Dictionary<string, HttpCookie>(StringComparer.InvariantCultureIgnoreCase);

            if (headers.ContainsKey(HttpHeader.Cookie))
            {
                var cookieHeader = headers[HttpHeader.Cookie];

                cookieHeader
                   .Value
                   .Split(';')
                   .Select(c => c.Split('='))
                   .Select(cp => new
                   {
                       Name = cp[0].Trim(),
                       Value = cp[1].Trim()
                   })
                   .ToList()
                   .ForEach(c => cookieCollection[c.Name] = new HttpCookie(c.Name, c.Value));
            }

            return cookieCollection;
        }

        private static Dictionary<string, string> ParseForm(Dictionary<string, HttpHeader> headers, string body)
        {
            var result = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            if (headers.ContainsKey(HttpHeader.ContentType)
                && headers[HttpHeader.ContentType].Value == HttpContentType.FormUtlEncoded)
            {
                result = ParseQuery(body);
            }

            return result;
        }

        private static HttpMethod ParseMethod(string method)
        {
            try
            {
                return (HttpMethod)Enum.Parse(typeof(HttpMethod), method, true);
            }
            catch (Exception)
            {

                throw new InvalidOperationException($"Method '{method}' is not supported");
            }
        }
    }
}
