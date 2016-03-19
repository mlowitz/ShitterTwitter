using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ShitterTwitter;
using ShitterTwitter.Common.Objects;
using ShitterTwitter.DAL;

namespace DatabaseManager
{
    class Program
    {
        static void Main(string[] args)
        {
            IDatabaseManeger maneger = new DatabaseManeger();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("************************************");
                Console.WriteLine("Shitter Twitter Menu:");
                Console.WriteLine("************************************");
                Console.WriteLine();
                Console.WriteLine("1) Add Message");
                Console.WriteLine("2) Read Message");
                Console.WriteLine("3) Delete Message");
                Console.WriteLine("4) Exit");
                Console.WriteLine(": ");
                var chocie = Console.ReadLine();
                var choiceint = int.Parse(chocie);

                switch (choiceint)
                {
                    case 1:
                        maneger.AddMessage(MakeMessage());
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("invalid");

                        break;
                }
            }
            

        }

        public static IShitterTwitterMessage MakeMessage()
        {
            IShitterTwitterMessage message = new ShitterTwitterMessage();

            Console.WriteLine("Enter Message: ");
            message.Message = Console.ReadLine();
            message.DateAdded = DateTime.Now.ToString("o");
            message.MessageType = 1;

            return message;
        }

        public static void ReadMessages()
        {
            
        }
    }
}
