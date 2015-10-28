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
        Dictionary<Keys, bool> registredKeys = new Dictionary<Keys, bool>();

        public void RegisterKey(Keys inputKey)
        {
            registredKeys.Add(inputKey, false);
        }
        public void RemoveKey(Keys inputKey)
        {
            registredKeys.Remove(inputKey);
        }
        public void SetKey(Keys inputKey,bool isDown)
        {
            registredKeys[inputKey] = isDown;
        }
        public bool KeyExists(Keys inputkey)
        {
            if (registredKeys.ContainsKey(inputkey))
                return true;
            else
                return false;
        }
        public bool GetKey(Keys inputKey)
        {
            if (!KeyExists(inputKey))
                RegisterKey(inputKey);

            return registredKeys[inputKey];
        }
    }
}
