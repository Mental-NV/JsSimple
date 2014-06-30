using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasswordGenerator.Models
{
    public class Options
    {
        public Options()
        {
            IsIncludeNumbers = true;
            IsIncludeSymbols = true;
            IsRandomlyCapitalize = true;
        }

        public int MinLength
        {
            get 
            { 
                if (_minLength < 6)
                {
                    _minLength = 6;
                }
                return _minLength; 
            }
            set { _minLength = value; }
        }
        private int _minLength;

        public bool IsIncludeNumbers { get; set; }
        public bool IsIncludeSymbols { get; set; }
        public bool IsRandomlyCapitalize { get; set; }
    }
}