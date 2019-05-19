using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packet
{
    class CRC8
    {
        //overloaded constructor - get bytes from string
        public Form1 _hwnd;
        public byte[] polynomial = { 0x01, 0x25 };
        public byte hex01 = 0x01;
        public byte hex80 = 0x80;
        public byte[] _byteArray;
        public byte[] buffer = new byte[2];
        private Encoding asciiEncoder = Encoding.ASCII;
        public CRC8()
        {

        }

        public CRC8(String str, Form1 hwnd)
        {
            _hwnd = hwnd;
            // instantiate array 1 byte larger than string size
            _byteArray = new byte[str.Length + 1];

            // transfer string into byte array using Encoding object
            asciiEncoder.GetBytes(str, 0, str.Length, _byteArray, 0);

            //append a null char to the end of the array
            _byteArray[_byteArray.Length - 1] = 0;
        }

        //overloaded constructor - get bytes from char
        public CRC8(char[] characters, Form1 hwnd)
        {
            _hwnd = hwnd;
            //add 8 bytes for the crc and one byte zero'd out for the CRC
            _byteArray = new byte[characters.Length + 1];
            _byteArray = asciiEncoder.GetBytes(characters);
            _byteArray[_byteArray.Length - 1] = 0;
        }

        //overloaded constructor - get bytes from bytes
        public CRC8(byte[] bites, Form1 hwnd)
        {
            _hwnd = hwnd;

            _byteArray = new byte[bites.Length];

            int i = 0;
            foreach (byte B in bites)
            {
                _byteArray[i] = B;
                i++;
            }
        }

        public byte CHECK_CRC(byte crc)
        {
            byte crcCheck = new byte();
            crcCheck = Convert.ToByte(this.buffer[0] ^ crc);
            return crc;
        }
        public byte GET_CRC()
        {
            //outer loop - for each byte in the message starting @ 0
            for (int i = 0; i < (this._byteArray.Length * 8); i++)
            {
                //check if the carry bit in the buffer SHOULD BE SET to one
                if (Convert.ToBoolean(this.buffer[1] & hex80))
                {
                    //set the carry bit to one if necessary
                    //else, do nothing
                    this.buffer[0] = (byte)(this.buffer[0] | hex01);
                }

                //shift the buffer left, loose a bit.
                this.buffer[1] <<= 1;

                //check if a 1 or a zero is about to fall off the byteArray (message) and put into the buffer register
                if (Convert.ToBoolean(this._byteArray[0] & hex80))
                {
                    //put the 1 into the buffer if necessary
                    this.buffer[1] = (byte)(this.buffer[1] | hex01);
                }

                //shift left the first byte of the byte array
                this._byteArray[0] <<= 1;

                //first bit in each byte to the last bit in each byte, skip the first and last bytes

                //inner loop - for each byte in the message starting @ 1
                for (int bite = 1; bite < this._byteArray.Length; bite++)
                {
                    if (Convert.ToBoolean(this._byteArray[bite] & hex80))
                    {
                        this._byteArray[bite - 1] = (byte)(this._byteArray[bite - 1] | hex01);
                    }

                    this._byteArray[bite] <<= 1;
                }

                //check if the carry bit in the buffer IS SET to one
                if (Convert.ToBoolean(this.buffer[0] & hex01))
                {
                    //perform the exclusive or logic  w/ POLYNOMIAL on
                    //both upper and lower byte of the buffer
                    this.buffer[0] = Convert.ToByte(this.buffer[0] ^ polynomial[0]);
                    this.buffer[1] = Convert.ToByte(this.buffer[1] ^ polynomial[1]);
                }
            }

            //   String testStr = "";

            //for (int ind = 0; ind < 8; ind++)
            //{
            //    //crc8.buffer[1] is the CRC8
            //    //convert the CRC8 to a string
            //    if (Convert.ToBoolean(this.buffer[1] & hex80))
            //    {
            //        testStr += "1";
            //    }
            //    else
            //    {
            //        testStr += "0";
            //    }

            //    hex80 >>= 1;
            //}

            //MessageBox.Show(testStr);
            return buffer[1];
        }

    }
}

