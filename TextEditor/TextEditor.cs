using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;


namespace TextEditor
{
    public class TextEditor : ITextEditor
    {
        private Trie<BigList<char>> users;
        private Trie<Stack<string>> bin;
        public TextEditor()
        {
            this.users = new Trie<BigList<char>>();
            this.bin = new Trie<Stack<string>>();
        }

        public void Clear(string username)
        {
            if (this.users.Contains(username))
            {
                SaveOldValue(username);
                this.users.GetValue(username).RemoveRange(0, this.GetCount(username));
            }
        }

        public void Delete(string username, int startIndex, int length)
        {
            if (this.users.Contains(username))
            {
                SaveOldValue(username);
                this.users.GetValue(username).RemoveRange(startIndex, length);
            }
        }

        public void Insert(string username, int index, string str)
        {
            if (this.users.Contains(username))
            {
                SaveOldValue(username);
                this.users.GetValue(username).InsertRange(index, str);
            }
        }

        public int Length(string username)
        {
            return this.GetCount(username);
        }

        private int GetCount(string username)
        {
            return this.users.GetValue(username).Count;
        }

        public void Login(string username)
        {
            if (this.users.Contains(username))
            {
                return;
            }

            if (this.bin.Contains(username))
            {
                var userBin = this.bin.GetValue(username).Pop();

                this.users.Insert(username, new BigList<char>(userBin));
            }
            else
            {
                this.users.Insert(username, new BigList<char>());
                this.bin.Insert(username, new Stack<string>());
            }
        }

        // TO DO
        public void Logout(string username)
        {
            if (this.users.Contains(username))
            {
                SaveOldValue(username);
                this.users.Delete(username);
            }
        }

        public void Prepend(string username, string str)
        {
            if (this.users.Contains(username))
            {
                this.SaveOldValue(username);
                this.users.GetValue(username).AddRangeToFront(str);
            }
        }

        public string Print(string username)
        {
            if (!this.users.Contains(username))
            {
                return null;
            }

            return String.Join("", this.users.GetValue(username));
        }

        public void Substring(string username, int startIndex, int length)
        {
            if (this.users.Contains(username))
            {
                var count = this.GetCount(username);
                SaveOldValue(username);

                var newString = this.users.GetValue(username).GetRange(startIndex, length);
                this.users.GetValue(username).RemoveRange(0, count);
                users.GetValue(username).InsertRange(0, newString);

            }
        }

        public void Undo(string username)
        {
            if (this.users.Contains(username))
            {
                string oldValue = this.bin.GetValue(username).Pop();
                string toBeRemoved = String.Join("", this.users.GetValue(username));

                //this.bin.GetValue(username).Push(toBeRemoved);
                this.users.GetValue(username).RemoveRange(0, this.GetCount(username));
                this.users.GetValue(username).InsertRange(0, oldValue);
            }

        }

    

        public IEnumerable<string> Users(string prefix = "")
        {
            var users = this.users
                .GetByPrefix(prefix);
            return users;
        }

        public void End()
        {
            Environment.Exit(0);

        }


        private void SaveOldValue(string username)
        {
            var oldString = String.Join("", this.users.GetValue(username));
            this.bin.GetValue(username).Push(oldString);
        }


    }
}
