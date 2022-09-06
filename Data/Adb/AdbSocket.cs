namespace EveMagic.Data.Adb
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;

    public class AdbSocket : IDisposable
    {
        TcpClient _tcpClient;
        NetworkStream _tcpStream;
        Encoding _encoding = Encoding.ASCII;

        byte[] _buffer = new byte[65536];

        public string Address { get; set; }
        public int Port { get; set; }

        public AdbSocket(string adbServerHost, int adbServerPort)
        {
            _tcpClient = new TcpClient(adbServerHost, adbServerPort);
            _tcpStream = _tcpClient.GetStream();
            this.Address = adbServerHost;
            this.Port = adbServerPort;
        }

        public void Dispose()
        {
            if (_tcpClient != null)
            {
                _tcpClient.Close();
                _tcpClient = null;
            }

            if (_tcpStream != null)
            {
                _tcpStream.Close();
                _tcpStream = null;
            }
        }

        public void Write(byte[] data, int size)
        {
            _tcpStream.Write(data, 0, size);
        }

        public void Write(byte[] data)
        {
            Write(data, data.Length);
        }

        public void WriteString(string text)
        {
            var size = _encoding.GetBytes(text, 0, text.Length, _buffer, 0);
            Write(_buffer, size);
        }

        public void WriteInt32(int number)
        {
            var bytes = BitConverter.GetBytes(number);
            Write(bytes);
        }

        public string SendCommand(string command)
        {
            WriteString($"{command.Length:X04}");
            WriteString(command);

            // var response = ReadString(4);
            var response = ReadString(4);

            switch (response)
            {
                case "OKAY":
                    return response;
                case "FAIL":
                    var message = ReadHexString();
                    throw new AdbException(message);
                default:
                    throw new AdbInvalidResponseException(response);
            }
        }

        public string SendSyncCommand(string command, string parameter, bool readResponse = true)
        {
            WriteString(command);
            WriteInt32(parameter.Length);
            WriteString(parameter);

            if (!readResponse)
            {
                return null;
            }

            var response = ReadString(4);

            if (response.Equals("FAIL"))
            {
                var message = ReadSyncString();
                throw new AdbException(message);
            }

            return response;
        }

        public void Read(byte[] data, int size)
        {
            var total = 0;
            while (total < size)
            {
                var read = _tcpStream.Read(data, total, size - total);
                total += read;
            }
        }

        public byte[] Read(int size)
        {
            var bytes = new byte[size];

            Read(bytes, size);

            return bytes;
        }

        public string ReadHexString()
        {
            // get string length
            var length = ReadInt32Hex();

            // read and return string
            return ReadString(length);
        }

        public string ReadSyncString()
        {
            // get string length
            var length = ReadInt32();

            // read and return string
            return ReadString(length);
        }

        public string ReadString(int length)
        {
            // read string
            Read(_buffer, length);

            // convert to string and return
            return _encoding.GetString(_buffer, 0, length);
        }

        public int ReadInt32()
        {
            Read(_buffer, 4);
            return BitConverter.ToInt32(_buffer, 0);
        }

        public int ReadInt32Hex()
        {
            Read(_buffer, 4);

            var hex = _encoding.GetString(_buffer, 0, 4);

            return Convert.ToInt32(hex, 16);
        }

        public string[] ReadAllLines()
        {
            var lines = new List<string>();

            using (var reader = new StreamReader(_tcpStream, _encoding))
            {
                while (true)
                {
                    var line = reader.ReadLine();

                    if (null == line)
                    {
                        break;
                    }

                    lines.Add(line.Trim());
                }
            }

            return lines.ToArray();
        }
    }
}
