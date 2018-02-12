
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Demo_TheTravelingSalesperson
{
    /// <summary>
    /// MVC View class
    /// </summary>
    public class ConsoleView
    {
        #region FIELDS

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// default constructor to create the console view objects
        /// </summary>
        public ConsoleView()
        {
            InitializeConsole();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initialize all console settings
        /// </summary>
        private void InitializeConsole()
        {
            ConsoleUtil.WindowTitle = "Laughing Leaf Productions";
            ConsoleUtil.HeaderText = "The Traveling Salesperson Application";
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayMessage("Press any key to continue.");
            ConsoleKeyInfo response = Console.ReadKey();

            ConsoleUtil.DisplayMessage("");

            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the Exit prompt on a clean screen
        /// </summary>
        public void DisplayExitPrompt()
        {
            ConsoleUtil.DisplayReset();

            Console.CursorVisible = false;

            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Thank you for using the application. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }


        /// <summary>
        /// display the welcome screen
        /// </summary>
        public void DisplayWelcomeScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Written by Connor Hansen");
            ConsoleUtil.DisplayMessage("Northwestern Michigan College");
            ConsoleUtil.DisplayMessage("");

            sb.Clear();
            sb.AppendFormat("You are a traveling salesperson buying and selling widgets ");
            sb.AppendFormat("around the country. You will be prompted regarding which city ");
            sb.AppendFormat("you wish to travel to and will then be asked whether you wish to buy ");
            sb.AppendFormat("or sell widgets.");
            ConsoleUtil.DisplayMessage(sb.ToString());
            ConsoleUtil.DisplayMessage("");

            sb.Clear();
            sb.AppendFormat("Your first task will be to set up your account details.");
            ConsoleUtil.DisplayMessage(sb.ToString());

            DisplayContinuePrompt();
        }

        /// <summary>
        /// setup the new salesperson object with the initial data
        /// Note: To maintain the pattern of only the Controller changing the data this method should
        ///       return a Salesperson object with the initial data to the controller. For simplicity in 
        ///       this demo, the ConsoleView object is allowed to access the Salesperson object's properties.
        /// </summary>
        public Salesperson DisplaySetupAccount()
        {
            Salesperson salesperson = new Salesperson();
            Product product = new Product();

            ConsoleUtil.HeaderText = "Account Setup";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Setup your account now.");
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Enter your first name: ");
            salesperson.FirstName = Console.ReadLine();
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Enter your last name: ");
            salesperson.LastName = Console.ReadLine();
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Enter your account ID: ");
            salesperson.AccountID = Console.ReadLine();
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Enter your age: ");
            salesperson.Age = Convert.ToInt32(Console.ReadLine());
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Enter your gender(Male/Female/None): ");
            string myGender = Console.ReadLine();
            ConsoleUtil.DisplayMessage("");

            Salesperson.Gender gender;

            // compare user input to 'Gender' enum
            if (Enum.TryParse<Salesperson.Gender>(UppercaseFirst(myGender), out gender))
            {
                salesperson.gender = gender;
            }
            // default
            else
            {
                salesperson.gender = Salesperson.Gender.None;
            }

            // Display list of products from Product.cs
            foreach (string productTypeName in Enum.GetNames(typeof(Product.ProductType)))
            {
                //
                // do not display the "NONE" enum value
                //
                if (productTypeName != Product.ProductType.None.ToString())
                {
                    ConsoleUtil.DisplayMessage(productTypeName);
                }

            }

            //
            // get product type, if invalid entry, set type to "None"
            //
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayPromptMessage("Enter the product type: ");
            Product.ProductType productType;

            if (Enum.TryParse<Product.ProductType>(UppercaseFirst(Console.ReadLine()), out productType))
            {
                salesperson.CurrentStock.Type = productType;
            }
            else
            {
                salesperson.CurrentStock.Type = Product.ProductType.None;
            }

            //
            // get number of products in inventory
            //
            int numberOfUnits;

            if (ConsoleValidator.TryGetIntegerFromUser(0, 100, 3, "products", out numberOfUnits))
            {
                salesperson.CurrentStock.AddProducts(numberOfUnits);
            }
            else
            {
                ConsoleUtil.DisplayMessage("It appears you are having difficulty setting the number of products in your stock.");
                ConsoleUtil.DisplayMessage("By default, the number of products in your inventory are now set to zero.");
                salesperson.CurrentStock.AddProducts(0);
                DisplayContinuePrompt();
            }

            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Your account is setup");

            DisplayContinuePrompt();

            return salesperson;
        }

        /// <summary>
        /// display a closing screen when the user quits the application
        /// </summary>
        public void DisplayClosingScreen()
        {
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Thank you for using The Traveling Salesperson Application.");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// get the menu choice from the user
        /// </summary>
        public MenuOption DisplayGetUserMenuChoice()
        {
            MenuOption userMenuChoice = MenuOption.None;
            bool usingMenu = true;

            while (usingMenu)
            {
                //
                // set up display area
                //
                ConsoleUtil.HeaderText = "Menu";
                ConsoleUtil.DisplayReset();
                Console.CursorVisible = false;

                //
                // display the menu
                //
                ConsoleUtil.DisplayMessage("Please type the number of your menu choice.");
                ConsoleUtil.DisplayMessage("");
                Console.Write(
                    "\t" + "1. Travel" + Environment.NewLine +
                    "\t" + "2. Buy" + Environment.NewLine +
                    "\t" + "3. Sell" + Environment.NewLine +
                    "\t" + "4. Display Inventory" + Environment.NewLine +
                    "\t" + "5. Display Cities" + Environment.NewLine +
                    "\t" + "6. Display Account Info" + Environment.NewLine +
                    "\t" + "7. Save Account Info" + Environment.NewLine +
                    "\t" + "8. Load Account Info" + Environment.NewLine +
                    "\t" + "9. Edit Account Info" + Environment.NewLine +
                    "\t" + "E. Exit" + Environment.NewLine);

                //
                // get and process the user's response
                // note: ReadKey argument set to "true" disables the echoing of the key press
                //
                ConsoleKeyInfo userResponse = Console.ReadKey(true);
                switch (userResponse.KeyChar)
                {
                    case '1':
                        userMenuChoice = MenuOption.Travel;
                        usingMenu = false;
                        break;
                    case '2':
                        userMenuChoice = MenuOption.Buy;
                        usingMenu = false;
                        break;
                    case '3':
                        userMenuChoice = MenuOption.Sell;
                        usingMenu = false;
                        break;
                    case '4':
                        userMenuChoice = MenuOption.DisplayInventory;
                        usingMenu = false;
                        break;
                    case '5':
                        userMenuChoice = MenuOption.DisplayCities;
                        usingMenu = false;
                        break;
                    case '6':
                        userMenuChoice = MenuOption.DisplayAccountInfo;
                        usingMenu = false;
                        break;
                    case '7':
                        userMenuChoice = MenuOption.SaveAccountInfo;
                        usingMenu = false;
                        break;
                    case '8':
                        userMenuChoice = MenuOption.LoadAccountInfo;
                        usingMenu = false;
                        break;
                    case '9':
                        userMenuChoice = MenuOption.EditAccountInfo;
                        usingMenu = false;
                        break;
                    case 'E':
                    case 'e':
                        userMenuChoice = MenuOption.Exit;
                        usingMenu = false;
                        break;
                    default:
                        ConsoleUtil.DisplayMessage(
                            "It appears you have selected an incorrect choice." + Environment.NewLine +
                            "Press any key to continue or the ESC key to quit the application.");

                        userResponse = Console.ReadKey(true);
                        if (userResponse.Key == ConsoleKey.Escape)
                        {
                            usingMenu = false;
                        }
                        break;
                }
            }
            Console.CursorVisible = true;

            return userMenuChoice;
        }
        /// <summary>
        /// get the next city to travel to from the user
        /// </summary>
        /// <returns>string City</returns>
        public string DisplayGetNextCity()
        {
            ConsoleUtil.HeaderText = "Travel";
            ConsoleUtil.DisplayReset();

            string nextCity = "";

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayPromptMessage("Enter the name of the next city:");
            nextCity = Console.ReadLine();

            return nextCity;
        }

        /// <summary>
        /// display a list of the cities traveled
        /// </summary>
        public void DisplayCitiesTraveled(Salesperson salesperson)
        {
            ConsoleUtil.HeaderText = "Visited";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("You have traveled to the following cities.");
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("You have traveled to " + salesperson.CitiesVisited.Count() + " city(s).");
            ConsoleUtil.DisplayMessage("");

            foreach (string city in salesperson.CitiesVisited)
            {
                ConsoleUtil.DisplayMessage(city);
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the current account information
        /// </summary>
        public void DisplayAccountInfo(Salesperson salesperson)
        {
            ConsoleUtil.HeaderText = "Account Info";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("First Name: " + salesperson.FirstName);
            ConsoleUtil.DisplayMessage("Last Name: " + salesperson.LastName);
            ConsoleUtil.DisplayMessage("Account ID: " + salesperson.AccountID);
            ConsoleUtil.DisplayMessage("Age: " + salesperson.Age);
            ConsoleUtil.DisplayMessage("On Backorder: " + salesperson.OnBackorder);
            ConsoleUtil.DisplayMessage("Gender: " + salesperson.gender);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// changes string to lowercase with first letter uppercase
        /// adapted from: https://www.dotnetperls.com/uppercase-first-letter
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concatenation substring.
            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }

        // Display Salesperson Inventory
        public void DisplayInventory(Product product)
        {
            ConsoleUtil.HeaderText = "Inventory";
            ConsoleUtil.DisplayReset();

            int units = product.NumberOfUnits;
            Product.ProductType productType = product.Type;

            if (product.NumberOfUnits < 0)
            {
                Console.WriteLine("You are currently on backorder of the following product type: " + productType);
                Console.WriteLine("Number of products on backorder:  " + product.NumberOfUnits * -1);
            }
            else
            {
                Console.WriteLine("You currently have " + units + " products of type: " + productType);
            }
      
            DisplayContinuePrompt();
        }

        // Prompt user to enter number of units to buy
        public int DisplayGetNumberOfUnitsToBuy(Product product)
        {
            ConsoleUtil.HeaderText = "Buy";
            ConsoleUtil.DisplayReset();

            Console.WriteLine("Enter the number of units to buy: ");
            int unitsToBuy = Convert.ToInt32(Console.ReadLine());

            return unitsToBuy;
        }

        // Prompt user to enter number of units to sell
        public int DisplayGetNumberOfUnitsToSell(Product product)
        {
            ConsoleUtil.HeaderText = "Sell";
            ConsoleUtil.DisplayReset();

            Console.WriteLine("Enter the number of units to sell: ");
            int unitsToSell = Convert.ToInt32(Console.ReadLine());

            return unitsToSell;
        }

        // Display a backorder message
        public void DisplayBackorderMessage()
        {
            ConsoleUtil.HeaderText = "Alert";
            ConsoleUtil.DisplayReset();

            Console.WriteLine("This unit is on backorder!");
            Console.ReadLine();
        }

        // Prompt the user to edit their first name
        public string EditFirstName()
        {
            string firstName;

            ConsoleUtil.HeaderText = "Edit First Name";
            ConsoleUtil.DisplayReset();

            Console.WriteLine("Change first name to: ");
            firstName = Console.ReadLine();

            return firstName;
        }

        // Prompt the user to edit their Last Name
        public string EditLastName()
        {
            string lastName;

            ConsoleUtil.HeaderText = "Edit Last Name";
            ConsoleUtil.DisplayReset();

            Console.WriteLine("Change last name to: ");
            lastName = Console.ReadLine();

            return lastName;
        }

        // Prompt the user to edit their Account ID
        public string EditAccountID()
        {
            string accountID;

            ConsoleUtil.HeaderText = "Edit Account ID";
            ConsoleUtil.DisplayReset();

            Console.WriteLine("Change Account ID to: ");
            accountID = Console.ReadLine();

            return accountID;
        }

        // Prompt the user to edit their age
        public int EditAge()
        {
            int age;

            ConsoleUtil.HeaderText = "Edit Age";
            ConsoleUtil.DisplayReset();

            Console.WriteLine("Change age to: ");
            age = Convert.ToInt32(Console.ReadLine());

            return age;
        }

        // Prompt the user to edit their gender
        public Salesperson.Gender EditGender()
        {

            ConsoleUtil.HeaderText = "Edit Gender";
            ConsoleUtil.DisplayReset();

            Console.WriteLine("Change gender to: ");

            Salesperson.Gender gender;

            // compare user input to 'Gender' enum
            if (Enum.TryParse<Salesperson.Gender>(UppercaseFirst(Console.ReadLine()), out gender))
            {
                return gender;
            }
            // default
            else
            {
                return Salesperson.Gender.None;
            }
     
        }

        // Display Account Edit screen
        public MenuOption DisplayGetAccountInfoToEdit()
        {
            MenuOption userMenuChoice = MenuOption.None;
            
            bool usingMenu = true;

            while (usingMenu)
            {
                //
                // set up display area
                //
                ConsoleUtil.HeaderText = "Edit Account";
                ConsoleUtil.DisplayReset();
                Console.CursorVisible = false;

                //
                // display the menu
                //
                ConsoleUtil.DisplayMessage("Please type the number of the account info you would like to edit:");
                ConsoleUtil.DisplayMessage("");
                Console.Write(
                    "\t" + "1. First Name" + Environment.NewLine +
                    "\t" + "2. Last Name" + Environment.NewLine +
                    "\t" + "3. Account ID" + Environment.NewLine +
                    "\t" + "4. Age" + Environment.NewLine +
                    "\t" + "5. Gender" + Environment.NewLine +
                    "\t" + "E. Exit" + Environment.NewLine);

                //
                // get and process the user's response
                // note: ReadKey argument set to "true" disables the echoing of the key press
                //
                ConsoleKeyInfo userResponse = Console.ReadKey(true);
                switch (userResponse.KeyChar)
                {
                    case '1':
                        userMenuChoice = MenuOption.EditFirstName;
                        usingMenu = false;
                        break;
                    case '2':
                        userMenuChoice = MenuOption.EditLastName;
                        usingMenu = false;
                        break;
                    case '3':
                        userMenuChoice = MenuOption.EditAccountId;
                        usingMenu = false;
                        break;
                    case '4':
                        userMenuChoice = MenuOption.EditAge;
                        usingMenu = false;
                        break;
                    case '5':
                        userMenuChoice = MenuOption.EditGender;
                        usingMenu = false;
                        break;
                    case 'E':
                    case 'e':
                        usingMenu = false;
                        break;
                    default:
                        ConsoleUtil.DisplayMessage(
                            "It appears you have selected an incorrect choice." + Environment.NewLine +
                            "Press any key to continue or the ESC key to quit the application.");

                        userResponse = Console.ReadKey(true);
                        if (userResponse.Key == ConsoleKey.Escape)
                        {
                            usingMenu = false;
                        }
                        break;
                }
            }
            Console.CursorVisible = true;
            return userMenuChoice;
        }
        #endregion
    }
}
