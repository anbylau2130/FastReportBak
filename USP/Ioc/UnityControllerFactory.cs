using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using USP.Utility;

namespace USP.Ioc
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        public IUnityContainer Container { get; private set; }
        public UnityControllerFactory(string configFile,string containerName = "")
        {
            try
            {
                UnityConfigurationSection unityConfigurationSection = null;
                var fileMap = new System.Configuration.ExeConfigurationFileMap { ExeConfigFilename = configFile };
                System.Configuration.Configuration configuration = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(fileMap, System.Configuration.ConfigurationUserLevel.None);
                if (!string.IsNullOrEmpty(containerName))
                {
                    unityConfigurationSection = (UnityConfigurationSection)configuration.GetSection(containerName);
                }
                else
                {
                    unityConfigurationSection = (UnityConfigurationSection)configuration.GetSection("unity");
                }
                if (null == unityConfigurationSection)
                {
                    throw new ConfigurationErrorsException("Cannot find <unity> configuration section");
                }
                Container = new UnityContainer();
                Container.LoadConfiguration(unityConfigurationSection);
            }
            catch(Exception ex)
            {
                LogUtil.Exception("IocLogger", ex);
            }
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            { 
                LogUtil.Debug("IocLogger", controllerType.ToString());
                Guard.ArgumentNotNull(controllerType, "controllerType");
                return (IController)this.Container.Resolve(controllerType);
            }
            catch (Exception ex)
            {
                LogUtil.Exception("IocLogger", ex);
                return null;
            }
        }
    }
}