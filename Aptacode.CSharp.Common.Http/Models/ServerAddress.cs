using System;
using System.Text;

namespace Aptacode.CSharp.Common.Http.Models
{
    /// <summary>
    ///     Represents a server address
    /// </summary>
    public struct ServerAddress
    {
        #region Constructors

        public ServerAddress(string address, int? port) : this(null, address, port) { }

        public ServerAddress(Protocol? protocol, string address) : this(protocol, address, null) { }

        public ServerAddress(Protocol? protocol, string address, int? port)
        {
            Protocol = protocol;
            Address = CleanAddress(address);
            Port = CleanPort(port);
            _toString = ToString(Protocol, Address, Port);
        }

        public static string CleanAddress(string input)
        {
            if (input.EndsWith("/"))
            {
                input = input.Remove(input.Length - 1, 1);
            }

            if (input.Contains("://"))
            {
                var addressComponents = input.Split(new[] {"://"}, StringSplitOptions.None);
                input = addressComponents.Length >= 1 ? addressComponents[1] : string.Empty;
            }

            return input;
        }

        public static int? CleanPort(int? input)
        {
            if (input == null || input < 0 || input > 65535)
            {
                return null;
            }

            return input;
        }

        private static string ToString(Protocol? protocol, string address, int? port)
        {
            if (string.IsNullOrEmpty(address))
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();

            if (protocol != null)
            {
                stringBuilder.Append(protocol.ToString());
                stringBuilder.Append("://");
            }

            stringBuilder.Append(address);

            if (port != null)
            {
                stringBuilder.Append(":");
                stringBuilder.Append(port.ToString());
            }

            stringBuilder.Append("/");

            return stringBuilder.ToString();
        }

        #endregion

        #region Properties

        public Protocol? Protocol { get; }
        public string Address { get; }
        public int? Port { get; }

        private readonly string _toString;

        #endregion

        public override string ToString() => _toString;

        public ServerAddress Copy() => new ServerAddress(Protocol, Address, Port);
    }
}