using System;
using System.IO;
using System.Text;

namespace XorCmd
{
    class Program
    {
        /// <summary>
        /// Program entry point
        /// </summary>
        /// <param name="args">Arguments within the command line</param>
        static void Main(string[] args)
        {
            if (args.Length == 3) //Authorising only 3 arguments
            {
                if (File.Exists(args[0])) //Verifying if the file from the path given really exists
                    xorChar(args[0], Encoding.GetEncoding("iso-8859-15").GetBytes(args[1].ToCharArray()), new FileStream(args[2], FileMode.OpenOrCreate)); //XorKey is for now only Latin 9 encoded
                else
                    Console.WriteLine("The file to xor does not exists !");
            }
            else
            {
                if(args.Length==2 || args.Length>3)
                    Console.WriteLine("Not the right amount of arguments.");
                if (args.Length == 0|| args[0].Equals("-help")||args[0].Equals("--help")||args[0].Equals("/help") || args[0].Equals("-h") || args[0].Equals("/h")) //Deflaut an help messages
                {
                    Console.WriteLine("Syntax : xor.exe fileToXor xorKey fileXored");
                    Console.WriteLine("Beware the fileXored argument will create or overwrite the file.");
                }
            }
                
        }
        /// <summary>
        /// Xoring function
        /// </summary>
        /// <param name="file">Filepath to the file getting xored</param>
        /// <param name="xorKey">XorKey to xor the file with</param>
        /// <param name="xFile">Resulting file (xored)</param>
        static void xorChar(string file, byte[] xorKey, FileStream xFile)
        {
            byte xoredByte;
            int i = 0;

            try
            {
                byte[] readBuffer = File.ReadAllBytes(file);
                if (xorKey.Length > readBuffer.Length)
                {
                    Console.WriteLine("The Key cannot be bigger than the data to xor!!"); //If the user regardless the warning inputs a key longer than the file.
                    return;
                }
                foreach (byte b in readBuffer) //Xoring the file byte to byte with the key depending on the key length
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
