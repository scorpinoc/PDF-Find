using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ViewModel
{
    public abstract class ResourceSwitcher : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<CultureInfo> Cultures { get; }

        private CultureInfo Current { get; set; }
        
        public IEnumerable<CultureInfo> CultureList => Cultures;
        
        public CultureInfo CurrentCulture
        {
            get
            {
                return Current;
            }
            set
            {
                if (value == null || Equals(value, CurrentCulture))
                    return;
                Current = value;
                if (!CultureList.Contains(Current)) return;
              
                LoadResource();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }


        protected ResourceSwitcher()
        {
            Cultures = new List<CultureInfo>();
            if (Cultures.Any()) return;

            var manifestModuleName = System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name;
            var resourceName = Path.GetFileNameWithoutExtension(manifestModuleName) + ".resources.dll";

            foreach (var dir in Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory))
            {
                try
                {
                    var dirinfo = new DirectoryInfo(dir);
                    var culture = CultureInfo.GetCultureInfo(dirinfo.Name);

                    if (dirinfo.GetFiles(resourceName).Any())
                        Cultures.Add(culture);
                }
                catch (ArgumentException)
                {
                    //ignore
                }
            }
            CurrentCulture = CultureInfo.GetCultureInfo(Properties.Settings.Default.DefaultCulture);
        }

        protected abstract void LoadResource();
    }
}