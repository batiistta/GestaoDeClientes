using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.UI
{
    public class ScreenContext
    {
        public MainWindow mainWindow { get; set; }

        private static ScreenContext _screenContext;

        public static ScreenContext Instance
        {
            get
            {
                if (_screenContext == null)
                {
                    _screenContext = new ScreenContext();
                }

                return _screenContext;
            }
        }
    }
}
