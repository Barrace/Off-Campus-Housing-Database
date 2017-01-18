using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OffCampusHousingDatabase
{
    static class Globals
    {
        public static bool loggedOn = false;
        public static bool isManager = false;
        public static string email = "";

        //Keeps track of the functionality of the back button
        public static Stack<String> windowStack = new Stack<String>();

        public static void goBack()
        {
            Window window = new MainWindow();

            if (Globals.windowStack.Count != 0)
            {
                String[] tokens = Globals.windowStack.Pop().Split('|');

                switch (tokens[0])
                {
                    case "PropertyDescription":
                        window = new PropertyDescription(Convert.ToInt32(tokens[1]));
                        break;
                    case "UpdateProperty":
                        if (tokens.Length == 1)
                            window = new UpdateProperty();
                        else
                            window = new UpdateProperty(Convert.ToInt32(tokens[1]));
                        break;
                    case "UserWindow":
                        window = new UserWindow(tokens[1]);
                        break;
                }
            }

            //Code to return to created object
            App.Current.MainWindow = window;
            window.Show();
        }
    }
}
