namespace PasswordGenerator.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http;

    public class PasswordsController : ApiController
    {
        private readonly Random _random = new Random();

        public IEnumerable<string> GetAll()
        {
            var count = 10;
            var list = new List<string>(count);
            for (int i = 0; i < count; i++)
            {
                list.Add(Generate(10));
            }
            return list;
        }

        private string RandomSymbol()
        {
            var symbols = "~!@#$%^&*()_+-={}:;";
            return symbols[_random.Next(0, symbols.Length - 1)].ToString();
        }

        private bool RandomBool(double trueProbabilty = 0.5)
        {
            return _random.NextDouble() < trueProbabilty;
        }

        private string RandomWord(bool isRandCapital = false)
        {
            var w = Words[_random.Next(0, Words.Count - 1)];
            w = w.ToLower();
            if (isRandCapital)
            {
                var nw = new StringBuilder();
                for (int i = 0; i < w.Length; i++)
                {
                    nw.Append(RandomBool(0.2) ? char.ToUpper(w[i]) : w[i]);
                }
                w = nw.ToString();
            }
            return w;
        }

        private static List<string> Words
        {
            get
            {
                if (_words == null)
                {
                    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/words.txt");
                    var content = File.ReadAllText(path);
                    var words = content.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    _words = new List<string>(words);
                }
                return _words;
            }
        }
        private static List<string> _words;

        private string RandomNumber()
        {
            int n;
            while (true)
            {
                n = _random.Next(0, 99);
                if (n / 10 == n % 10)
                {
                    continue;
                }
                break;
            }
            return n.ToString();
        }

        private string Generate(int minLength = 10)
        {
            var pass = new StringBuilder();
            var odd = false;
            var wasNumber = false;
            var wasSymbol = false;
            while (pass.Length < minLength)
            {
                odd = !odd;
                if (odd)
                {
                    pass.Append(RandomWord(true));
                }
                else
                {
                    if (RandomBool())
                    {
                        pass.Append(RandomSymbol());
                        wasSymbol = true;
                    }
                    else
                    {
                        pass.Append(RandomNumber());
                        wasNumber = true;
                    }
                }
            }
            if (!wasNumber)
            {
                pass.Append(RandomNumber());
            }
            if (!wasSymbol)
            {
                pass.Append(RandomSymbol());
            }
            return pass.ToString();
        }
    }
}