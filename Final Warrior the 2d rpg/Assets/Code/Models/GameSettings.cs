using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Models
{
    public class GameSettings
    {
        private Dictionary<string, int> _settings;

        public GameSettings()
        {
            _settings = new Dictionary<string, int>();
        }

        public void SetOrAdd(string key, int value)
        {
            if (!_settings.ContainsKey(key))
                _settings.Add(key, value);
            else
                _settings[key] = value;
        }

        public int GetOrAdd(string key)
        {
            AddKeyIfNotExists(key, 0);

            return _settings[key];
        }

        private void AddKeyIfNotExists(string key, int value)
        {
            if (!_settings.ContainsKey(key))
                _settings.Add(key, value);
        }
    }
}
