using System.Collections.Generic;

namespace Demo_TheTravelingSalesperson
{
    /// <summary>
    /// Salesperson MVC Model class
    /// </summary>
    public class Salesperson
    {
        #region ENUMERABLES
        public enum Gender
        {
            None,
            Male,
            Female
        }
        #endregion

        #region FIELDS
        private string _firstName;
        private string _lastName;
        private string _accountID;
        private Product _currentStock;
        private List<string> _citiesVisited;
        // Build out the functionality for the three below
        private int _age;
        private bool _onBackorder;
        private Gender _gender;
  
        #endregion
        
        #region PROPERTIES

        public Gender gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public bool OnBackorder
        {
            get { return _onBackorder; }
            set { _onBackorder = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string AccountID
        {
            get { return _accountID; }
            set { _accountID = value; }
        }

        public Product CurrentStock
        {
            get { return _currentStock; }
            set { _currentStock = value; }
        }

        public List<string> CitiesVisited
        {
            get { return _citiesVisited; }
            set { _citiesVisited = value; }
        }

        #endregion
        
        #region CONSTRUCTORS

        public Salesperson()
        {
            _citiesVisited = new List<string>();
            _currentStock = new Product();
        }

        public Salesperson(string firstName, string lastName, string acountID)
        {
            _firstName = firstName;
            _lastName = lastName;
            _accountID = acountID;

            _citiesVisited = new List<string>();
            _currentStock = new Product();
        }

        #endregion
        
        #region METHODS



        #endregion
    }
}
