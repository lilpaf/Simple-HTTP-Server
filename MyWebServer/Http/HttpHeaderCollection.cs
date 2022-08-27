namespace MyWebSurver.Http
{
    using System.Collections;

    public class HttpHeaderCollection : IEnumerable<HttpHeader>
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(string name, string value)
        {
            var header = new HttpHeader(name, value);

            this.headers.Add(name, header);
        }
        
        //public void Add(string name, string value)
        //=> this.headers[name] = new HttpHeader(name, value); 

        public IEnumerator<HttpHeader> GetEnumerator()
        => this.headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();

        public int Count => this.headers.Count;
    }
}
