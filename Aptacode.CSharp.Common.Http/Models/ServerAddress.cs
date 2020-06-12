using System;
using System.Text;

namespace Aptacode.CSharp.Common.Http.Models
{
    /// <summary>
    ///     Represents a server address
    /// </summary>
    public class ServerAddress
    {
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Address))
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();

            if (Protocol != null)
            {
                stringBuilder.Append(Protocol.ToString());
                stringBuilder.Append("://");
            }

            stringBuilder.Append(Address);

            if (Port != null)
            {
                stringBuilder.Append(":");
                stringBuilder.Append(Port.ToString());
            }

            stringBuilder.Append("/");

            return stringBuilder.ToString();
        }

        #region Constructors

        public ServerAddress(string address, int? port) : this(null, address, port) { }

        public ServerAddress(Protocol? protocol, string address) : this(protocol, address, null) { }

        public ServerAddress(Protocol? protocol, string address, int? port)
        {
            Protocol = protocol;
            Address = address;
            Port = port;
        }

        #endregion

        #region Properties

        public Protocol? Protocol { get; set; }
        private string _address;

        public string Address
        {
            get => _address;
            set
            {
                if (value.EndsWith("/"))
                {
                    value = value.Remove(value.Length - 1, 1);
                }

                if (value.Contains("://"))
                {
                    var addressComponents = value.Split(new[] {"://"}, StringSplitOptions.None);
                    value = addressComponents.Length >= 1 ? addressComponents[1] : string.Empty;
                }

                _address = value;
            }
        }

        private int? _port;

        public int? Port
        {
            get => _port;
            set
            {
                if (value < 0 || value > 65535)
                {
                    _port = null;
                }
                else
                {
                    _port = value;
                }
            }
        }

        #endregion
    }
}