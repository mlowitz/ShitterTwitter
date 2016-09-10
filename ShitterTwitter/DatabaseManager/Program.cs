using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ShitterTwitter;
using ShitterTwitter.Common.DAL;
using ShitterTwitter.Common.MessagePublisher;
using ShitterTwitter.Common.Objects;
using ShitterTwitter.DAL;


namespace DatabaseManager
{
    class Program
    {
        public static IDatabaseManeger maneger;
        public static TwitterManeger Twitter;
        static void Main(string[] args)
        {
            maneger = new DatabaseManeger();
            Twitter = new TwitterManeger();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("************************************");
                Console.WriteLine("Shitter Twitter Menu:");
                Console.WriteLine("************************************");
                Console.WriteLine();
                Console.WriteLine("1) Add Message");
                Console.WriteLine("2) Read Messages");
                Console.WriteLine("3) Tweet it");
                Console.WriteLine("4) Delete Message");
                Console.WriteLine("5) Exit");
                Console.WriteLine(": ");
                var chocie = Console.ReadLine();
                int choiceint;
                if (int.TryParse(chocie, out choiceint))
                {



                
                    

                    switch (choiceint)
                    {
                        case 1:
                            maneger.AddMessage(MakeMessage());
                            break;
                        case 2:
                            var read = maneger.GetAllShitterMessages();
                            ReadMessages(read);
                            break;
                        case 3:
                            Tweetit();
                            break;
                        case 5:
                            return;
                        default:
                            Console.WriteLine("invalid");

                            break;
                    }
                }
            }


        }

        public static IShitterTwitterMessage MakeMessage()
        {
            IShitterTwitterMessage message = new ShitterTwitterMessage();

            Console.WriteLine("Enter Message: ");
            message.Message = Console.ReadLine();
           
            message.MessageType = 1;
            

            return message;
        }

        public static void Tweetit()
        {
            var message =  maneger.GetMessageToTweet();
            Twitter.sendTweet(message.Message);
            Console.WriteLine(message.Message);
            Console.WriteLine(message.DateLastUsed);
            Console.ReadKey();
        }

        public static void ReadMessages(List<IShitterTwitterMessage> messages )
        {
            foreach (IShitterTwitterMessage message in messages)
            {
                Console.WriteLine(message.Message);
            }
            Console.ReadKey();
        }
    }
}
