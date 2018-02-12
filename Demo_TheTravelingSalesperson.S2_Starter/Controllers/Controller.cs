using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Demo_TheTravelingSalesperson
{
    /// <summary>
    /// MVC Controller class
    /// </summary>
    public class Controller
    {
        #region FIELDS

        private bool _usingApplication;

        //
        // declare ConsoleView and Salesperson objects for the Controller to use
        // Note: There is no need for a Salesperson or ConsoleView property given only the Controller 
        //       will access the ConsoleView object and will pass the Salesperson object to the ConsoleView.
        //
        private ConsoleView _consoleView;
        private Salesperson _salesperson;

        #endregion

        #region PROPERTIES


        #endregion
        
        #region CONSTRUCTORS

        public Controller()
        {
            InitializeController();

            //
            // instantiate a Salesperson object
            //
            _salesperson = new Salesperson();

            //
            // instantiate a ConsoleView object
            //
            _consoleView = new ConsoleView();

            //
            // begins running the application UI
            //
            ManageApplicationLoop();
        }

        #endregion
        
        #region METHODS

        /// <summary>
        /// initialize the controller 
        /// </summary>
        private void InitializeController()
        {
            _usingApplication = true;
        }

        /// <summary>
        /// method to manage the application setup and control loop
        /// </summary>
        private void ManageApplicationLoop()
        {
            MenuOption userMenuChoice;

            _consoleView.DisplayWelcomeScreen();

            //
            // setup initial salesperson account
            //
            //_salesperson = _consoleView.DisplaySetupAccount();

            //
            // application loop
            //
            while (_usingApplication)
            {

                //
                // get a menu choice from the ConsoleView object
                //
                userMenuChoice = _consoleView.DisplayGetUserMenuChoice();

                //
                // choose an action based on the user's menu choice
                //
                switch (userMenuChoice)
                {
                    case MenuOption.None:
                        break;
                    case MenuOption.SetupAccount:
                        _salesperson = _consoleView.DisplaySetupAccount();
                        break;
                    case MenuOption.Travel:
                        Travel();
                        break;
                    case MenuOption.DisplayCities:
                        DisplayCities();
                        break;
                    case MenuOption.DisplayAccountInfo:
                        DisplayAccountInfo();
                        break;
                    case MenuOption.Buy:
                        Buy();
                        break;
                    case MenuOption.Sell:
                        Sell();
                        break;
                    case MenuOption.DisplayInventory:
                        DisplayInventory();
                        break;
                    case MenuOption.SaveAccountInfo:
                        SaveAccountInfo(_salesperson);
                        break;
                    case MenuOption.LoadAccountInfo:
                        _salesperson = LoadAccountInfo();
                        break;
                    case MenuOption.EditAccountInfo:
                        EditAccountInfo();
                        break;
                    case MenuOption.Exit:
                        _usingApplication = false;
                        break;
                    default:
                        break;
                }
            }

            _consoleView.DisplayClosingScreen();

            //
            // close the application
            //
            Environment.Exit(1);
        }

        /// <summary>
        /// add the next city location to the list of cities
        /// </summary>
        private void Travel()
        {
            string nextCity = _consoleView.DisplayGetNextCity();

            //
            // do not add empty strings to list for city names
            //
            if (nextCity != "")
            {
                _salesperson.CitiesVisited.Add(nextCity);
            }
        }

        /// <summary>
        /// display all cities traveled to
        /// </summary>
        private void DisplayCities()
        {
            _consoleView.DisplayCitiesTraveled(_salesperson);
        }

        /// <summary>
        /// display account information
        /// </summary>
        private void DisplayAccountInfo()
        {
            _consoleView.DisplayAccountInfo(_salesperson);
        }
        
        private void DisplayInventory()
        {
            _consoleView.DisplayInventory(_salesperson.CurrentStock);
        }
       
        private void Buy()
        {
            int numberOfUnits = _consoleView.DisplayGetNumberOfUnitsToBuy(_salesperson.CurrentStock);
            _salesperson.CurrentStock.AddProducts(numberOfUnits);

            if (_salesperson.CurrentStock.NumberOfUnits > 0) 
            {
                _salesperson.OnBackorder = false; 
            }
        }

        private void Sell() 
        {
            int numberOfUnits = _consoleView.DisplayGetNumberOfUnitsToSell(_salesperson.CurrentStock);
            _salesperson.CurrentStock.SubtractProducts(numberOfUnits);

            if (_salesperson.CurrentStock.NumberOfUnits < 0) 
            {
                _consoleView.DisplayBackorderMessage();   
                _salesperson.OnBackorder = true;
            }
            else {
                return;
            }
        }

        public static void SaveAccountInfo(Salesperson salesperson) 
        {
            // instantiate an XmlSerializer object with the object class
            XmlSerializer serializer = new XmlSerializer(typeof(Salesperson));

            // instantiate a StreamWriter object with the data file loation
            StreamWriter sWriter = new StreamWriter("AccountInfo.xml");

            // write the serialized data to the xml file
            using (sWriter)
            {
                serializer.Serialize(sWriter, salesperson);
            }               
        }

        public static Salesperson LoadAccountInfo() 
        {
            Salesperson salesPerson = new Salesperson();

            // instantiate an XmlSerializer object with the object class
            XmlSerializer serializer = new XmlSerializer(typeof(Salesperson));

            // instantiate a StreamReader object with the data file location
            StreamReader sReader = new StreamReader("AccountInfo.xml");

            using (sReader)
            {
                Object xmlObject = serializer.Deserialize(sReader);
                salesPerson = (Salesperson)xmlObject;
            }

            return salesPerson;
        }

        private void EditAccountInfo() 
        {
            MenuOption userMenuChoice;
            userMenuChoice = _consoleView.DisplayGetAccountInfoToEdit();
            
            switch (userMenuChoice)
            {
                case MenuOption.EditFirstName:
                    _salesperson.FirstName = _consoleView.EditFirstName();
                    break; 
                case MenuOption.EditLastName:
                    _salesperson.LastName = _consoleView.EditLastName();
                    break;
                case MenuOption.EditAccountId:
                    _salesperson.AccountID = _consoleView.EditAccountID();
                    break;
                case MenuOption.EditAge:
                    _salesperson.Age = _consoleView.EditAge();
                    break;
                case MenuOption.EditGender:
                    //Salesperson.Gender = _consoleView.EditGender();
                    //Salesperson.Gender myGender = (Salesperson.Gender)_consoleView.EditGender();
                    _salesperson.gender = _consoleView.EditGender();
                    break;
                case MenuOption.Exit:
                    _usingApplication = false;
                    break;
                default:
                    break;
            }
            



            //_salesperson = _consoleView.DisplayEditAccountInfo(_salesperson);
            // get userMenuChoice from DisplayEditAccountInfo (rename this method to
            // editAccountMenuChoice or something

            //use switch case here
            //if (//usermenuchoice 1)
            //{
             //   _salesperson.FirstName = _consoleView.ChangeFirstName();
            //}

           // _salesperson.FirstName = _consoleView.ChangeFirstName();
           // _salesperson.LastName = _consoleView.ChangeLastName();
           // _salesperson.AccountID = _consoleView.ChangeAccountID();
        }            
        #endregion
    }
}
