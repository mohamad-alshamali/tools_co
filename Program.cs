using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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


       static void product(string[,] product, string name = "ALL")// method to display product info
        {
            Console.WriteLine("{0,13} {1,13} {2,13}", product[0, 0], product[0, 1], product[0, 2]);

            int rows = product.GetLength(0);// to get number of rows in the array
            if ((name == null)|| (name == "ALL"))// to display all products
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
            if ((name == "ALL")|| (name ==null))// to display all employs
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
      static  public void admin(string order, string[,] array)// admin method to handle admin operations
        {
            if (order == "A")// view employ data
            {

                Console.WriteLine(" ENTER EMPLOY NAME OR type ALL FOR ALL EMPLOY ");// prompt for employ name
                string NAME = Console.ReadLine();// read employ name

                employs(array, NAME);
            }


            else if (order == "B")// add new employ
            {
                Console.WriteLine("Enter new employ name ");
                string name = Console.ReadLine();
                Console.WriteLine("enter new employ password ");
                string password = Console.ReadLine();
                Console.WriteLine("enter new employ work ");
                string work = Console.ReadLine();
                Console.WriteLine("enter new employ statue ");
                string statue = Console.ReadLine();

                for (int i = 0; i < array.GetLength(0); i++)
                {
                    if (array[i, 1] == null)
                    {

                        array[i, 0] = "E00" + (i).ToString();
                        array[i, 1] = name;
                        array[i, 2] = "password";
                        array[i, 3] = work;
                        array[i, 4] = statue;
                        break;
                    }


                }








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


            }


            else if (order == "D")//add new product
            {
                Console.WriteLine(" enter product  symbol ");
                string symbol = Console.ReadLine();
                Console.WriteLine(" enter product name");
                string name = Console.ReadLine();
                Console.WriteLine(" enter product quantity");
                string quantity = Console.ReadLine();
                int row = array.GetLength(0);
                for (int i = 0; i < row; i++)
                
                    if (array[i, 0] == null)// find first empty row
                    {
                        array[i, 0] = symbol;
                        array[i, 1] = name;
                        array[i, 2] = quantity;
                        break;
                    }



                
            }



            else if (order == "E")// modify product quantity
            {

                Console.WriteLine("Enter Product  symbol");// prompt for product symbol
                string symbol = Console.ReadLine();// read product symbol
                Console.WriteLine("enter new quantity");// prompt for new quantity
                string quantity = Console.ReadLine();// read new quantity
                int rows = array.GetLength(0);
                for (int i = 0; i < rows; i++)
                    if (array[i, 0] == symbol)// find product by symbol
                    {

                        array[i, 2] = quantity;// modify quantity


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
                        Console.WriteLine(" {0,13}{1,13}{2,13}", array[0, 1], array[0, 2], array[i, 2]);
                        Console.WriteLine(" {0,13}{1,13}{2,13}", array[i, 1], array[i, 3], array[i, 3]);
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
        static void employ(string order, string[,] array,string[,] array2)// employ method to handle employ operations
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
                      String  quantity = array[i, 2]; // get current quantity 

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
            string[,] products = new string[10, 3];// product array

            string[,] employs = new string[10, 5];// employs array

            string[,] sall = new string[10, 4];// sall array




            string titl = "Tools Company Management System";// company title    


            products[0, 0] = "product-symbol";// product header
            products[0, 1] = " product-name";
            products[0, 2] = " quantity ";


            employs[0, 0] = "employ-symbol";// employs header
            employs[0, 1] = "employ-name";
            employs[0, 2] = "password";
            employs[0, 3] = "Profession";
            employs[0, 4] = "statue";
         
            sall[0, 0] = "action-symbol";// sall header
            sall[0, 1] = " employ-symbol ";
            sall[0, 2] = " product-symbol";
            sall[0, 3] = " quantity";

            products[1, 0] = "P001";// product data
            products[1, 1] = "Hammer";
            products[1, 2] = "50";
            products[2, 0] = "P002";
            products[2, 1] = "Screwdriver";
            products[2, 2] = "150";
            products[3, 0] = "P003";
            products[3, 1] = "Wrench";
            products[3, 2] = "75";
            products[4, 0] = "P004";
            products[4, 1] = "Drill";
            products[4, 2] = "30";
            products[5, 0] = "P005";
            products[5, 1] = "Saw";
            products[5, 2] = "20";
            products[6, 0] = "P006";
            products[6, 1] = "Pliers";
            products[6, 2] = "100";
            products[7, 0] = "P007";
            products[7, 1] = "Level";
            products[7, 2] = "40";
            employs[1, 0] = "E001";// employs data
            employs[1, 1] = "mohamad";
            employs[1, 2] = "123";
            employs[1, 3] = "admin";
            employs[1, 4] = "active";
            employs[2, 0] = "E002";
            employs[2, 1] = "ali";
            employs[2, 2] = "se354";
            employs[2, 3] = "employee";
            employs[2, 4] = "active";
            employs[3, 0] = "E003";
            employs[3, 1] = "KHALED";
            employs[3, 2] = "671K";
            employs[3, 3] = "Employ";
            employs[3, 4] = "aCTIVE";
            employs[4, 0] = "E004";
            employs[4, 1] = "SALEH";
            employs[4, 2] = "S123";
            employs[4, 3] = "EMPLOY";
            employs[4, 4] = "ACTIVE";
            employs[5, 0] = "E005";
            employs[5, 1] = "AHMED";
            employs[5, 2] = "A456";
            employs[5, 3] = "EMPLOY";
            employs[5, 4] = "ACTIVE";
            sall[1, 0] = "S001";// sall data
            sall[1, 1] = "E002";
            sall[1, 2] = "P001";
            sall[1, 3] = "5";
            sall[2, 0] = "S002";
            sall[2, 1] = "E002";
            sall[2, 2] = "P003";
            sall[2, 3] = "10";
            sall[3, 0] = "S003";
            sall[3, 1] = "E003";
            sall[3, 2] = "P002";
            sall[3, 3] = "15";
            sall[4, 0] = "S004";
            sall[4, 1] = "E004";
            sall[4, 2] = "P005";
            sall[4, 3] = "8";
            sall[5, 0] = "S005";
            sall[5, 1] = "E005";
            sall[5, 2] = "P004";
            sall[5, 3] = "12";




      


        switchcount:

            Console.WriteLine(" welcome to {0 },Please Select \n A-Admin\n B-Employee\n C-Exit", titl);//welcom massage
             
            string Name = Console.ReadLine();//select type of account
            if (Name == "A")
            {
            
                Console.WriteLine("type Your name");
                string name = Console.ReadLine();


                Console.WriteLine("enter password");
                string password = Console.ReadLine();


                for (int i = 1; i < employs.GetLength(0); i++)

                {


                    if (employs[i, 1] == name && employs[i, 2] != password)
                    {


                        Console.WriteLine("Invalid Name or Password");

                        goto switchcount;
                    }

                
                   


                    else if (employs[i, 1] == name && employs[i, 2] == password)// check for valid admin login
                    {
                        Console.WriteLine("Welcome+ " + name);

                        if (employs[i, 3] == "admin")
                        {
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
                                break;
                            }
                            else if ((order == "D") || (order == "E") || (order == "F"))// product operations
                            {

                                admin(order, products);

                                goto admin_list;
                            }
                            else if ((order == "G") || (order == "H"))// sall operations
                            {

                                admin(order, products);
                                goto admin_list;
                            }
                            else
                            {


                                admin(order, employs);// employ operations
                                goto admin_list;
                            }


                        }
                    }
                }
            }

            else if (Name == "B")


            {
            employ_list:
                Console.WriteLine("You have limited access select from the lise\n " +
                               "A-Execute a sale with verification of quantity availability\n" +
                               "B-View employee sales history\n" +
                               "C-Switch account and log in with another account\n" +
                               "D-EXITE");
                string order = Console.ReadLine();

                if ((order == "A")||(order=="B")) 
                {
                    employ(order, products,sall);
                    goto employ_list;
                    
                }
                else if (order == "C") 
                {
                    goto switchcount;


                }
                else if (order == "D")
                {
                    Console.WriteLine("Goodbye");
                   
                }














            }
            else if (Name == "C")
            {
                Console.WriteLine("Goodbye");
                
            }










            Console.ReadKey();






        }
    }
}
