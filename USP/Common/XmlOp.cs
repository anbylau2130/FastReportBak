using System;
using System.Xml;
using System.Web;

namespace USP.Common
{
    /// <summary>
    /// 必需用XPATH表达式来获取相应节点
    /// 关于xpath可以参见:
    /// </summary>
    public class XmlOp
    {
        #region 变量
        //// <summary>
        /// xml文件所在路径类型
        /// </summary>
        /// <remarks>xml文件所在路径类型</remarks>
        public enum enumXmlPathType
        {
            //// <summary>
            /// 绝对路径
            /// </summary>
            AbsolutePath,
            //// <summary>
            /// 虚拟路径
            /// </summary>
            VirtualPath
        }

        private string xmlFilePath;
        private enumXmlPathType xmlFilePathType;
        private XmlDocument xmlDoc = new XmlDocument();
        #endregion

        #region 属性
        /// <summary>
        /// 文件路径
        /// </summary>
        /// <remarks>文件路径</remarks>
        public string XmlFilePath
        {
            get
            {
                return this.xmlFilePath;
            }
            set
            {
                xmlFilePath = value;

            }
        }
        //// <summary>
        /// 文件路径类型
        /// </summary>
        public enumXmlPathType XmlFilePathTyp
        {
            set
            {
                xmlFilePathType = value;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public XmlOp()
        {
            this.xmlFilePathType = enumXmlPathType.VirtualPath;
        }
        //// <summary>
        /// 
        /// </summary>
        /// <param name="tempXmlFilePath"></param>
        public XmlOp(string tempXmlFilePath)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //

            this.xmlFilePathType = enumXmlPathType.VirtualPath;
            this.xmlFilePath = tempXmlFilePath;
            xmlDoc = GetXmlDocument();
            //xmlDoc.Load( xmlFilePath ) ;
        }

        //// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tempXmlFilePath">文件路径</param>
        /// <param name="tempXmlFilePathType">类型</param>
        public XmlOp(string tempXmlFilePath, enumXmlPathType tempXmlFilePathType)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.xmlFilePathType = tempXmlFilePathType;
            this.xmlFilePath = tempXmlFilePath;
            xmlDoc = GetXmlDocument();
        }
        #endregion

        #region 初始化
        ////<summary>
        ///获取XmlDocument实体类
        ///</summary>   
        /// <returns>指定的XML描述文件的一个xmldocument实例</returns>
        private XmlDocument GetXmlDocument()
        {
            XmlDocument doc = null;
            if (this.xmlFilePathType == enumXmlPathType.AbsolutePath)
            {
                doc = GetXmlDocumentFromFile(xmlFilePath);
            }
            else if (this.xmlFilePathType == enumXmlPathType.VirtualPath)
            {
                doc = GetXmlDocumentFromFile(HttpContext.Current.Server.MapPath(xmlFilePath));
            }
            return doc;
        }
        private XmlDocument GetXmlDocumentFromFile(string tempXmlFilePath)
        {
            string xmlFileFullPath = tempXmlFilePath;
            xmlDoc.Load(xmlFileFullPath);
            //定义事件处理
            xmlDoc.NodeChanged += new XmlNodeChangedEventHandler(this.nodeUpdateEvent);
            xmlDoc.NodeInserted += new XmlNodeChangedEventHandler(this.nodeInsertEvent);
            xmlDoc.NodeRemoved += new XmlNodeChangedEventHandler(this.nodeDeleteEvent);
            return xmlDoc;
        }
        #endregion

        #region 读取指定节点的指定属性值
        //// <summary>
        /// 功能:
        /// 读取指定节点的指定属性值   
        /// </summary>
        /// <param name="strNode">节点名称</param>
        /// <param name="strAttribute">此节点的属性</param>
        /// <returns></returns>
        public string GetXmlNodeAttributeValue(string strNode, string strAttribute)
        {
            string strReturn = "";
            try
            {
                //根据指定路径获取节点
                XmlNode xmlNode = xmlDoc.SelectSingleNode(strNode);
                if (!(xmlNode == null))
                {//获取节点的属性，并循环取出需要的属性值
                    XmlAttributeCollection xmlAttr = xmlNode.Attributes;

                    for (int i = 0; i < xmlAttr.Count; i++)
                    {
                        if (xmlAttr.Item(i).Name == strAttribute)
                        {
                            strReturn = xmlAttr.Item(i).Value;
                            break;
                        }
                    }
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return strReturn;
        }
        #endregion

        #region 读取指定节点的值
        //// <summary>
        /// 功能:
        /// 读取指定节点的值   
        /// </summary>
        /// <param name="strNode">节点名称</param>
        /// <returns></returns>
        public string GetXmlNodeValue(string strNode)
        {
            string strReturn = String.Empty;

            try
            {
                //根据路径获取节点
                XmlNode xmlNode = xmlDoc.SelectSingleNode(strNode);
                if (!(xmlNode == null))
                    strReturn = xmlNode.InnerText;
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return strReturn;
        }
        #endregion

        #region 设置节点值
        //// <summary>
        /// 功能:
        /// 设置节点值     
        /// </summary>
        /// <param name="strNode">节点的名称</param>
        /// <param name="newValue">节点值</param>
        public void SetXmlNodeValue(string xmlNodePath, string xmlNodeValue)
        {
            try
            {
                //可以批量为符合条件的节点进行付值
                XmlNodeList xmlNode = this.xmlDoc.SelectNodes(xmlNodePath);
                if (!(xmlNode == null))
                {
                    foreach (XmlNode xn in xmlNode)
                    {
                        xn.InnerText = xmlNodeValue;
                    }
                }

                /* 根据指定路径获取节点
                XmlNode xmlNode = xmlDoc.SelectSingleNode(xmlNodePath) ;         
                //设置节点值
                if (!(xmlNode==null))
                  xmlNode.InnerText = xmlNodeValue ;*/
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        #endregion

        #region 设置节点的属性值
        //// <summary>
        /// 功能:
        /// 设置节点的属性值   
        /// </summary>
        /// <param name="xmlNodePath">节点名称</param>
        /// <param name="xmlNodeAttribute">属性名称</param>
        /// <param name="xmlNodeAttributeValue">属性值</param>
        public void SetXmlNodeAttributeValue(string xmlNodePath, string xmlNodeAttribute, string xmlNodeAttributeValue)
        {
            try
            {
                //可以批量为符合条件的节点的属性付值
                XmlNodeList xmlNode = this.xmlDoc.SelectNodes(xmlNodePath);
                if (!(xmlNode == null))
                {
                    foreach (XmlNode xn in xmlNode)
                    {
                        XmlAttributeCollection xmlAttr = xn.Attributes;
                        for (int i = 0; i < xmlAttr.Count; i++)
                        {
                            if (xmlAttr.Item(i).Name == xmlNodeAttribute)
                            {
                                xmlAttr.Item(i).Value = xmlNodeAttributeValue;
                                break;
                            }
                        }
                    }
                }
                /*单个节点
                //根据指定路径获取节点
                XmlNode xmlNode = xmlDoc.SelectSingleNode(xmlNodePath) ;
                if (!(xmlNode==null))
                {//获取节点的属性，并循环取出需要的属性值
                  XmlAttributeCollection xmlAttr = xmlNode.Attributes ;
                  for(int i= ; i<xmlAttr.Count ; i++)
                  {
                      if ( xmlAttr.Item(i).Name == xmlNodeAttribute )
                      {
                        xmlAttr.Item(i).Value = xmlNodeAttributeValue;
                        break ;
                      }
                  }   
                }
                */
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        #endregion

        #region 添加
        //// <summary>
        /// 获取XML文件的根元素
        /// </summary>
        public XmlNode GetXmlRoot()
        {
            return xmlDoc.DocumentElement;
        }

        //// <summary>
        /// 在根节点下添加父节点
        /// </summary>
        public void AddParentNode(string parentNode)
        {
            try
            {
                XmlNode root = GetXmlRoot();
                XmlNode parentXmlNode = xmlDoc.CreateElement(parentNode);
                root.AppendChild(parentXmlNode);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }

        //// <summary>
        /// 向一个已经存在的父节点中插入一个子节点
        /// </summary>
        /// <param name="parentNodePath">父节点</param>
        /// <param name="childNodePath">字节点名称</param>
        public void AddChildNode(string parentNodePath, string childnodename)
        {
            try
            {
                XmlNode parentXmlNode = xmlDoc.SelectSingleNode(parentNodePath);
                if (!((parentXmlNode) == null))//如果此节点存在
                {
                    XmlNode childXmlNode = xmlDoc.CreateElement(childnodename);
                    parentXmlNode.AppendChild(childXmlNode);
                }
                else
                {//如果不存在就放父节点添加
                    //this.GetXmlRoot().AppendChild(childXmlNode);
                }

            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }

        //// <summary>
        /// 向一个节点添加属性
        /// </summary>
        /// <param name="NodePath">节点路径</param>
        /// <param name="NodeAttribute">属性名</param>
        public void AddAttribute(string NodePath, string NodeAttribute)
        {
            privateAddAttribute(NodePath, NodeAttribute, "");
        }
        //// <summary>
        /// 
        /// </summary>
        /// <param name="NodePath"></param>
        /// <param name="NodeAttribute"></param>
        /// <param name="NodeAttributeValue"></param>
        private void privateAddAttribute(string NodePath, string NodeAttribute, string NodeAttributeValue)
        {
            try
            {
                XmlNode nodePath = xmlDoc.SelectSingleNode(NodePath);
                if (!(nodePath == null))
                {
                    XmlAttribute nodeAttribute = this.xmlDoc.CreateAttribute(NodeAttribute);
                    nodeAttribute.Value = NodeAttributeValue;
                    nodePath.Attributes.Append(nodeAttribute);
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        //// <summary>
        /// 向一个节点添加属性,并付值
        /// </summary>
        /// <param name="NodePath">节点</param>
        /// <param name="NodeAttribute">属性名</param>
        /// <param name="NodeAttributeValue">属性值</param>
        public void AddAttribute(string NodePath, string NodeAttribute, string NodeAttributeValue)
        {
            privateAddAttribute(NodePath, NodeAttribute, NodeAttributeValue);
        }
        #endregion

        #region 删除
        //// <summary>
        /// 删除节点的一个属性
        /// </summary>
        /// <param name="NodePath">节点所在的xpath表达式</param>
        /// <param name="NodeAttribute">属性名</param>
        public void DeleteAttribute(string NodePath, string NodeAttribute)
        {
            XmlNodeList nodePath = this.xmlDoc.SelectNodes(NodePath);
            if (!(nodePath == null))
            {
                foreach (XmlNode tempxn in nodePath)
                {
                    XmlAttributeCollection xmlAttr = tempxn.Attributes;
                    for (int i = 0; i < xmlAttr.Count; i++)
                    {
                        if (xmlAttr.Item(i).Name == NodeAttribute)
                        {
                            tempxn.Attributes.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }

        //// <summary>
        /// 删除节点,当其属性值等于给定的值时
        /// </summary>
        /// <param name="NodePath">节点所在的xpath表达式</param>
        /// <param name="NodeAttribute">属性</param>
        /// <param name="NodeAttributeValue">值</param>
        public void DeleteAttribute(string NodePath, string NodeAttribute, string NodeAttributeValue)
        {
            XmlNodeList nodePath = this.xmlDoc.SelectNodes(NodePath);
            if (!(nodePath == null))
            {
                foreach (XmlNode tempxn in nodePath)
                {
                    XmlAttributeCollection xmlAttr = tempxn.Attributes;
                    for (int i = 0; i < xmlAttr.Count; i++)
                    {
                        if (xmlAttr.Item(i).Name == NodeAttribute && xmlAttr.Item(i).Value == NodeAttributeValue)
                        {
                            tempxn.Attributes.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }
        //// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="tempXmlNode"></param>
        /// <remarks></remarks>
        public void DeleteXmlNode(string tempXmlNode)
        {
            XmlNodeList nodePath = this.xmlDoc.SelectNodes(tempXmlNode);
            if (!(nodePath == null))
            {
                foreach (XmlNode xn in nodePath)
                {
                    xn.ParentNode.RemoveChild(xn);
                }
            }
        }

        #endregion

        #region XML文档事件
        //// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="args"></param>
        private void nodeInsertEvent(Object src, XmlNodeChangedEventArgs args)
        {
            //保存设置
            SaveXmlDocument();
        }
        //// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="args"></param>
        private void nodeDeleteEvent(Object src, XmlNodeChangedEventArgs args)
        {
            //保存设置
            SaveXmlDocument();
        }
        //// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="args"></param>
        private void nodeUpdateEvent(Object src, XmlNodeChangedEventArgs args)
        {
            //保存设置
            SaveXmlDocument();
        }
        #endregion

        #region 保存XML文件
        //// <summary>
        /// 功能: 
        /// 保存XML文件
        /// 
        /// </summary>
        public void SaveXmlDocument()
        {
            try
            {
                //保存设置的结果
                if (this.xmlFilePathType == enumXmlPathType.AbsolutePath)
                {
                    Savexml(xmlFilePath);
                }
                else if (this.xmlFilePathType == enumXmlPathType.VirtualPath)
                {
                    Savexml(HttpContext.Current.Server.MapPath(xmlFilePath));
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }

        //// <summary>
        /// 功能: 
        /// 保存XML文件   
        /// </summary>
        public void SaveXmlDocument(string tempXMLFilePath)
        {
            try
            {
                //保存设置的结果
                Savexml(tempXMLFilePath);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        //// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        private void Savexml(string filepath)
        {
            xmlDoc.Save(filepath);
        }

        #endregion

    }
}
