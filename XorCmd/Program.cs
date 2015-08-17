using System;
using System.IO;
using System.Text;

namespace XorCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                if (File.Exists(args[0]))
                    xorChar(args[0], Encoding.GetEncoding("iso-8859-15").GetBytes(args[1].ToCharArray()), new FileStream(args[2], FileMode.OpenOrCreate));
                else
                    Console.WriteLine("The file to xor does not exists !");
            }
            else
            {
                if(args.Length==2 || args.Length>3)
                    Console.WriteLine("Not the right amount of arguments.");
                if (args.Length == 0|| args[0].Equals("-help")||args[0].Equals("--help")||args[0].Equals("/help") || args[0].Equals("-h") || args[0].Equals("/h"))
                {
                    Console.WriteLine("Syntax : xor.exe fileToXor xorKey fileXored");
                    Console.WriteLine("Beware the fileXored argument will create or overwrite the file.");
                }
            }
                
        }

        static void xorChar(string file, byte[] xorKey, FileStream xFile)
        {
            byte xoredByte;
            int i = 0;

            try
            {
                byte[] readBuffer = File.ReadAllBytes(file);
                if (xorKey.Length > readBuffer.Length)
                {
                    Console.WriteLine("The Key cannot be bigger than the data to xor!!");
                    return;
                }
                foreach (byte b in readBuffer)
                {
                    xoredByte = (byte)(b ^ xorKey[i++]);
                    xFile.WriteByte(xoredByte);
                    if (i == xorKey.Length)
                        i = 0;
                }

                xFile.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
