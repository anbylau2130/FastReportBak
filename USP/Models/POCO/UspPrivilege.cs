using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP.Models.POCO
{
    public class UspPrivilege
    {
        public UspPrivilege(string menu, string parent, string name, string controllerClass, string controllerArea, string controllerName, string controllerAction, string actionParams)
        {
            Menu = menu;
            Parent = parent;
            Name = name;
            ControllerClass = controllerClass;
            ControllerArea = controllerArea;
            ControllerName = controllerName;
            ControllerAction = controllerAction;
            ActionParams = actionParams;
            if (ControllerArea.Length > 0)
            {
                Url = "~/" + ControllerArea + "/" + ControllerName + "/" + ControllerAction;
            }
            else
            {
                Url = "~/" + ControllerName + "/" + ControllerAction;
            }
        }
        public string Menu { get; set; }
        public string Parent { get; set; }
        public string Name { get; set; }
        public string ControllerClass { get; set; }
        public string ControllerArea { get; set; }
        public string ControllerName { get; set; }
        public string ControllerAction { get; set; }
        public string ActionParams { get; set; }
        public string Url { get; set; }
    }
}