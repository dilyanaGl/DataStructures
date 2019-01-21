using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace TextEditor
{
    public class User
    {
        private string username;
        private Trie<BigList<char>> text;
        private bool isLoggedIn;
        private Trie<Stack<string>> bin;

        public User(string username)
        {
            this.username = username;
            this.isLoggedIn = true;
            this.text = new Trie<BigList<char>>();
            this.bin = new Trie<Stack<string>>();
        }

        public void LogIn()
        {
            this.isLoggedIn = true;
        }
        public void LogOut()
        {
            this.isLoggedIn = false;
        }

        public void Save(string key)
        {
            
        }

        public void Back(string key)
        {
           // this.text.GetValue(key) = this.bin.Pop();
        }

        public bool IsLoggedIn { get => this.isLoggedIn; }
    }
}
