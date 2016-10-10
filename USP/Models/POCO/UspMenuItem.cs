using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP.Models.POCO
{
    public class UspMenuItem
    {
        public UspMenuItem(string icon,string name, string parent, string controllerClass, string controllerArea, string controllerName, string controllerAction, string actionParams)
        {
            Icon = icon;
            Name = name;
            Parent = parent;
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
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
        public string ControllerClass { get; set; }
        public string ControllerArea { get; set; }
        public string ControllerName { get; set; }
        public string ControllerAction { get; set; }
        public string ActionParams { get; set; }
        public string Url { get; set; } 
    }
}