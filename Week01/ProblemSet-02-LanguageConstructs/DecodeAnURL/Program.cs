using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecodeAnURL
{
    class Program
    {
        static string DecodeUrl(string input)
        {
            StringBuilder decoded = new StringBuilder();
            int lastChange = -1;

            for (int i = 0; i < input.Length - 2; i++)
            {
                if (input[i] == '%')
                {
                    switch (input.Substring(i + 1, 2))
                    {
                        case "20":
                            {
                                decoded.Append(" ");
                                lastChange = i;
                                i += 2;
                                break;
                            }
                        case "3A":
                            {
                                decoded.Append(":");
                                lastChange = i;
                                i += 2;
                                break;
                            }
                        case "3D":
                            {
                                decoded.Append("?");
                                lastChange = i;
                                i += 2;
                                break;
                            }
                        case "2F":
                            {
                                decoded.Append("/");
                                lastChange = i;
                                i += 2;
                                break;
                            }
                        default:
                            {
                                decoded.Append("%");
                            } break;
                    }
                }
                else 
                {
                    decoded.Append(input[i]);
                }
            }

            if (lastChange < input.Length - 4)
            {
                decoded.Append(input.Substring(input.Length - 2, 2));
            }
            else if (lastChange == input.Length - 4)
            {
                decoded.Append(input.Substring(input.Length - 1, 1));
            }
            
            return decoded.ToString();
        }

        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Console.WriteLine(DecodeUrl(input));
            Console.ReadKey();
        }
    }
}
