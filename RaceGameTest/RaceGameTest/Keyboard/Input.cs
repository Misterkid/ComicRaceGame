using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace RaceGameTest.Keyboard
{
    class Input
    {
        //Register keys with a boolean for pressed or not
        Dictionary<Keys, bool> registredKeys = new Dictionary<Keys, bool>();

        //register a key
        public void RegisterKey(Keys inputKey)
        {
            registredKeys.Add(inputKey, false);
        }
        //remove a key
        public void RemoveKey(Keys inputKey)
        {
            registredKeys.Remove(inputKey);
        }
        //set key!
        public void SetKey(Keys inputKey,bool isDown)
        {
            registredKeys[inputKey] = isDown;
        }
        //check if key exists!
        public bool KeyExists(Keys inputkey)
        {
            if (registredKeys.ContainsKey(inputkey))
                return true;
            else
                return false;
        }
        //if get key is used and the input key is not in directory, add it and return the result for pressed or not.
        public bool GetKey(Keys inputKey)
        {
            if (!KeyExists(inputKey))
                RegisterKey(inputKey);

            return registredKeys[inputKey];
        }
    }
}
