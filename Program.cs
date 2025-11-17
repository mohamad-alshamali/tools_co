using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace tools_co
{







    internal class Program
    {
        public static void Write(
      string data,
      bool update,
      string path = @"C:\IPG203.txt",
      string newTextForLine = "",
      int lineToUpdate = 0)

        {
            if (!update)
            {
                // --- APPEND MODE ---
               
                using (var fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine(data);
                }
            }
            else
            {
                // --- UPDATE MODE ---
               
                var lines = File.ReadAllLines(path);

                // 2) Validate line number
                if (lineToUpdate < 1 || lineToUpdate > lines.Length)
                {
                    Console.WriteLine("Line number out of range.");
                    return;
                }

                // 3) Update in-memory array (convert to zero‐based)
                lines[lineToUpdate] = newTextForLine;

                // 4) Write them all back (this overwrites the file)
                File.WriteAllLines(path, lines);
            }
        }


        public static string Read(string symbol, string path = "C:\\IPG203.txt")
        {



            if (!File.Exists(path))
            {
                return "file not found";

            }
            else
            {


                string[] s = File.ReadAllLines(path).ToArray();
                int num = s.GetLength(0);
                if (symbol == "last")
                {
                    return num.ToString();


                }

                if (symbol == "all")
                {


                    for (int i = 0; i < num; i++)
                        if (s[i] != null)
                            return s[i];

                }
                if (symbol.StartsWith("#"))
                {
                    int n = int.Parse(symbol.Substring(1));


                    return s[n];

                }
                else { return "invalid"; }

            }
        }


        static void product(string[,] product, string name = "ALL")// method to display product info
        {
            Console.WriteLine("{0,13} {1,13} {2,13}", product[0, 0], product[0, 1], product[0, 2]);

            int rows = product.GetLength(0);// to get number of rows in the array
            if ((name == null) || (name == "ALL"))// to display all products
            {

                for (int I = 1; I < rows; I++)
                    if (product[I, 0] != null)// to avoid null values
                        Console.WriteLine("{0,13} {1,13} {2,13}", product[I, 0], product[I, 1], product[I, 2]);
            }
            else
            {
                for (int I = 0; I < rows; I++)// to display specific product
                    if (product[I, 0] == name)// check for product symbol

                        Console.WriteLine("{0,13},{1,13},{2,13}", product[I, 0], product[I, 1], product[I, 2]);// display product info
            }

        }
        static void employs(string[,] employs, string name = "ALL")// method to display employs info
        {
            Console.WriteLine(" {0,13} {1,13} {2,13} {3,13} {4,13}", employs[0, 0], employs[0, 1], employs[0, 2], employs[0, 3], employs[0, 4]);// display header

            int rows = employs.GetLength(0);
            if ((name == "ALL") || (name == null))// to display all employs
            {
                for (int I = 1; I < rows; I++)// start from 1 to skip header


                    if (employs[I, 0] != null)// to avoid null values
                        Console.WriteLine(" {0,13} {1,13} {2,13} {3,13} {4,13}", employs[I, 0], employs[I, 1], employs[I, 2], employs[I, 3], employs[I, 4]);

            }

            else

            {
                for (int I = 0; I < rows; I++)// to display specific employ
                    if (employs[I, 1] == name)// check for employ name

                        Console.WriteLine(" {0,13} {1,13} {2,13} {3,13} {4,13} ", employs[I, 0], employs[I, 1], employs[I, 2], employs[I, 3], employs[I, 4]);// display employ info
            }

        }
        static public void admin(string order, string[,] array)// admin method to handle admin operations
        {

            if (order == "A")// view employ data
            {

                Console.WriteLine(" ENTER EMPLOY NAME OR type ALL FOR ALL EMPLOY ");// prompt for employ name
                string NAME = Console.ReadLine();// read employ name

                employs(array, NAME);
            }


            else if (order == "B")// add new employ
            {

                string symbol = string.Format("E00{0}", Read("last", "C:\\employs.txt"));
                Console.WriteLine(symbol);
                Console.WriteLine("Enter new employ name ");
                string name = Console.ReadLine();
                Console.WriteLine("enter new employ password ");
                string password = Console.ReadLine();
                Console.WriteLine("enter new employ work ");
                string work = Console.ReadLine();
                Console.WriteLine("enter new employ statue ");
                string statue = Console.ReadLine();


                for (int i = 1; i < array.GetLength(0); i++)
                {
                    if (array[i, 1] == null)
                    {

                        array[i, 0] = "E00" + i.ToString();
                        array[i, 1] = name;
                        array[i, 2] = password;
                        array[i, 3] = work;
                        array[i, 4] = statue;

                        break;
                    }
                }

                Write(String.Format("{0,13} {1,13} {2,13} {3,13} {4,13}", symbol, name, password, work, statue), false, "C:\\employs.txt", "", 0);






            }

            else if (order == "C")// activate/deactivate employ
            {

                Console.WriteLine("Enter employ symbol");
                string symbol = Console.ReadLine();
                Console.WriteLine("enter new statue");
                string statue = Console.ReadLine();

                int rows = array.GetLength(0);
                for (int i = 0; i < rows; i++)
                    if (array[i, 0] == symbol)
                    {
                        array[i, 4] = statue;


                    }
                int r = int.Parse(symbol.Substring(1));


                Write("", true, "C:\\employs.txt", string.Format("{0,13} {1,13} {2,13} {3,13} {4,13}", symbol, array[r, 1], array[r, 2], array[r, 3], statue), r);


            }


            else if (order == "D")//add new product
            {
                string symbol = string.Format("P00{0}", Read("last", "C:\\products.txt"));
                Console.WriteLine(symbol);


                Console.WriteLine(" enter name");
                string name = Console.ReadLine();
                Console.WriteLine(" enter product quantity");
                string quantity = Console.ReadLine();


                int row = array.GetLength(0);
                for (int i = 0; i < row; i++)

                    if (array[i, 0] == null)// find first empty row
                    {
                        array[i, 0] = symbol;// generate product symbol

                        array[i, 1] = name;
                        array[i, 2] = quantity;



                        break;
                    }
                Write(string.Format("{0,13} {1,13} {2,13}", symbol, name, quantity), false, "C:\\products.txt");

            }



            else if (order == "E")// modify product quantity
            {

                Console.WriteLine("Enter Product  name");// prompt for product name
                string name = Console.ReadLine();// read product name
                Console.WriteLine("enter new quantity");// prompt for new quantity
                string quantity = Console.ReadLine();// read new quantity
                int rows = array.GetLength(0);
                for (int i = 0; i < rows; i++)
                    if (array[i, 1] == name)// find product by name
                    {

                        array[i, 2] = quantity;// modify quantity

                        string symbol = "P00" + i.ToString();
                        Write("", true, "C:\\products.txt", string.Format("{0,13} {1,13} {2,13} ", symbol, array[i, 1], array[i, 2], quantity), i);

                    }



            }
            else if (order == "F")
            {
                product(array);


            }


            else if (order == "G")// display sales for specific product

            {
                Console.WriteLine(" enter sall symbol");
                string symbol = Console.ReadLine();

                for (int i = 0; i < array.GetLength(0); i++)
                {
                    if (array[i, 0] == symbol)
                    {
                        Console.WriteLine(" {0,13}{1,13}{2,13}", array[0, 0], array[0, 1], array[0, 2], array[0, 3]);
                        Console.WriteLine(" {0,13}{1,13}{2,13}", array[i, 0], array[i, 1], array[i, 2], array[i, 3]);
                    }


                }
            }

            else if (order == "H")// display sales by specific employ

            {
                Console.WriteLine(" enter sall employ symbol");
                string symbol = Console.ReadLine();
                int rows = array.GetLength(0);

                Console.WriteLine("{0,13},{1,13},{2,13} {3,13}", array[0, 0], array[0, 1], array[0, 2], array[0, 3]);

                for (int i = 0; i < rows; i++)
                {


                    if (array[i, 1] == symbol)
                    {

                        Console.WriteLine("{0,13},{1,13},{2,13} {3,13}", array[i, 0], array[i, 1], array[i, 2], array[i, 3]);

                    }
                }

            }


        }
        static void employ(string order, string[,] array, string[,] array2)// employ method to handle employ operations
        {

            if (order == "A")

            {
                Console.WriteLine(" enter your employ symbol");// prompt for employ symbol
                string employ_symbol = Console.ReadLine();// read employ symbol
                Console.WriteLine("enter product symbol");// prompt for product symbol
                string symbol = Console.ReadLine();// read product symbol
                Console.WriteLine("Enter sall quantity");// prompt for sall quantity
                string sall_quantity = Console.ReadLine();// read sall quantity

                for (int i = 0; i < array.GetLength(0); i++)// loop through array
                {

                    if (array[i, 0] == symbol)// find product by symbol
                    {
                        String quantity = array[i, 2]; // get current quantity 

                        if (int.Parse(quantity) >= int.Parse(sall_quantity))// check if enough quantity is available
                        {
                            Console.WriteLine("processing");// display processing message
                            int def = int.Parse(quantity) - int.Parse(sall_quantity);// calculate remaining quantity

                            string re = def.ToString();// convert remaining quantity to string
                            array[i, 2] = re;// update quantity in array
                            for (int j = 0; j < array2.GetLength(0); j++)// loop through sall array
                            {
                                if (array2[j, 0] == null)// find first empty row
                                {
                                    array2[j, 0] = "S00" + (j).ToString();// generate sall symbol

                                    array2[j, 1] = employ_symbol;// set employ symbol
                                    array2[j, 2] = symbol;// set product symbol
                                    array2[j, 3] = sall_quantity;// set sall quantity
                                    break;
                                }


                                Write("", true, "C:\\products.txt", string.Format("{0,13} {1,13} {2,13} ", symbol, array[int.Parse(symbol.Substring(1)), 1], array[i, 2], re), i);

                                Console.WriteLine("sall  done remaining quantity is = {0} ", array[i, 2]);// display remaining quantity
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("there is't enough quantity");// display error message
                            break;
                        }


                    }
                }



            }
            if (order == "B")// display sall history
            {
                Console.WriteLine(" enter  employ symbol");// prompt for employ symbol
                string symbol = Console.ReadLine();// read employ symbol
                int rows = array2.GetLength(0);// get number of rows in sall array
                Console.WriteLine("{0,13},{1,13},{2,13} {3,13}", array2[0, 0], array2[0, 1], array2[0, 2], array2[0, 3]);// display sall header
                for (int i = 0; i < rows; i++)// loop through array
                {
                    if (array2[i, 1] == symbol)// find sall by employ symbol
                    {
                        Console.WriteLine("{0,13},{1,13},{2,13} {3,13}", array2[i, 0], array2[i, 1], array2[i, 2], array2[i, 3]);// display sall info
                    }




                }
            }
        }



        static public void Main()
        {
            string[,] products = new string[100, 3];// product array

            string[,] employs = new string[100, 5];// employs array

            string[,] sall = new string[100, 4];// sall array




            string titl = "Tools Company Management System";// company title    


            products[0, 0] = "product-symbol";// product header
            products[0, 1] = "product-name";
            products[0, 2] = "quantity";
        

            employs[0, 0] = "employ-symbol";// employs header
            employs[0, 1] = "employ-name";
            employs[0, 2] = "password";
            employs[0, 3] = "Profession";
            employs[0, 4] = "statue";
            sall[0, 0] = "action-symbol";// sall header
            sall[0, 1] = " employ-symbol ";
            sall[0, 2] = " product-symbol";
            sall[0, 3] = " quantity";
            employs[1, 0] = "E001";
            employs[1, 1] = "mohamad";
            employs[1, 2] = "123";
            employs[1, 3] = "admin";
            employs[1, 4] = "active";
            Write(String.Format("{0,13} {1,13} {2,13} {3,13} {4,13}", employs[0, 0] , employs[0, 1], employs[0, 2], employs[0, 3], employs[0, 4]), false, "C:\\employs.txt");
            Write(String.Format("{0,13} {1,13} {2,13} {3,13} {4,13}", employs[1, 0], employs[1, 1], employs[1, 2], employs[1, 3], employs[1, 4]), false, "C:\\employs.txt");
            Write(String.Format("{0,13} {1,13} {2,13}", products[1, 0], products[1, 1], products[1, 2]), false, "C:\\products.txt");
            Write(String.Format("{0,13} {1,13} {2,13} {3,13}", sall[1, 0], sall[1, 1], sall[1, 2], sall[1, 3]), false, "C:\\salls.txt");







            string[] e = File.ReadAllLines("C:\\employs.txt").ToArray();

            for (int i = 0; i < e.GetLength(0); i++)
            {
                employs[i, 0] = e[i].Substring(0, 13).TrimStart();
                employs[i, 1] = e[i].Substring(14, 13).TrimStart();
                employs[i, 2] = e[i].Substring(28, 13).TrimStart();
                employs[i, 3] = e[i].Substring(42, 13).TrimStart();
                employs[i, 4] = e[i].Substring(56, 13).TrimStart();
                Console.WriteLine("{0,13} {1,13} {2,13} {3,13}", employs[i, 0], employs[i, 1], employs[i, 2], employs[i, 3]);

            }
            
            string[] p =  File.ReadAllLines("C:\\products.txt").ToArray();
            for (int i = 0; i < p.GetLength(0); i++)
            {
                products[i, 0] = p[i].Substring(0, 13).TrimStart();
                products[i, 1] = p[i].Substring(14, 13).TrimStart();
                products[i, 2] = p[i].Substring(28, 13).TrimStart();

                Console.WriteLine("{0,13} {1,13} {2,13}", products[i, 0], products[i, 1], products[i, 2]);

            }

            string[] s = File.ReadAllLines("C:\\salls.txt").ToArray();

            for (int i = 0; i < s.GetLength(0); i++)
            {
                sall[i, 0] = s[i].Substring(0, 13).TrimStart();
                sall[i, 1] = s[i].Substring(14, 13).TrimStart();
                sall[i, 2] = s[i].Substring(28, 13).TrimStart();
                sall[i, 3] = s[i].Substring(42, 13).TrimStart();
                Console.WriteLine("{0,13} {1,13} {2,13} {3,13}", sall[i, 0], sall[i, 1], sall[i, 2], sall[i, 2]);

            }



        switchcount:

            Console.WriteLine(" welcome to {0 },Please Select \n A-Admin\n B-Employee\n C-Exit", titl);//welcom massage

            string Name = Console.ReadLine();//select type of account
            bool found = false;

            if (Name == "C")
            {
                Console.WriteLine("Goodbye");

                return;

            }
            else if ((Name == "A")||(Name == "B"))
            {

            
                Console.WriteLine("type Your name");
                string name = Console.ReadLine();
                for (int i = 0; i < employs.GetLength(0); i++)
                {

                    if (employs[i, 1] == name)
                    {
                        found = true;

                        Console.WriteLine("enter password");
                        string password = Console.ReadLine();

                        if (employs[i, 2] != password)
                        {


                            Console.WriteLine("wrong Password");

                            goto switchcount;
                        }






                        else if (employs[i, 3] == "admin" && employs[i, 2] == password)// check for valid admin login
                        {
                            Console.WriteLine("Login successful! Welcome+ " + name);


                        admin_list:
                            Console.WriteLine("You have full access  select from the liste \n" +
                                "A-View a list of employee account data\n" +
                                "B-Add a new employee account \n" +
                                "C- Activate/deactivate a specific account\n " +
                                "D- Add a new product\n" +
                                "E-Modify the supplied quantity of an existing product\n" +
                                "F Product list \n" +
                                "G- Display a list of sales made for a specific product\n" +
                                "H- Display a list of sales made by a specific employee\n" +
                                "I- Switch account and log in with another account\n" +
                                "J- Leave the program");



                            string order = Console.ReadLine();
                            if (order == "I") { goto switchcount; }// switch account
                            else if (order == "J")
                            {
                                Console.WriteLine("Goodbye");
                                return;
                            }
                            else if ((order == "D") || (order == "E") || (order == "F"))// product operations
                            {

                                admin(order, products);

                                goto admin_list;
                            }
                            else if ((order == "G") || (order == "H"))// sall operations
                            {

                                admin(order, sall);
                                goto admin_list;
                            }
                            else if ((order == "A") | (order == "B") | (order == "C"))
                            {


                                admin(order, employs);// employ operations
                                goto admin_list;
                            }
                            else
                            {
                                Console.WriteLine(" enter A,B,C,D,E,F,G,H,I or j ");

                                goto admin_list;
                            }


                        }
                        else if (employs[i, 3] != "admin" && employs[i, 2] == password)
                        {
                        employ_list:
                            Console.WriteLine("You have limited access select from the lise\n " +
                                           "A-Execute a sale with verification of quantity availability\n" +
                                           "B-View employee sales history\n" +
                                           "C-Switch account and log in with another account\n" +
                                           "D-EXITE");
                            string order = Console.ReadLine();

                            if ((order == "A") || (order == "B"))
                            {
                                employ(order, products, sall);
                                goto employ_list;

                            }
                            else if (order == "C")
                            {
                                goto switchcount;



                            }
                            else if (order == "D")
                            {

                                Console.WriteLine("Goodbye");
                                return;
                            }

                            else
                            {
                                Console.WriteLine("Please enter A, B, or C.");

                                goto switchcount;
                            }
                        }

                    }
                }

                if (!found)
                {
                    Console.WriteLine("Name does not exist");
                    goto switchcount;
                }

            }
            


            
           
        








            Console.ReadKey();



        


                
            
        }
    }
}
      

