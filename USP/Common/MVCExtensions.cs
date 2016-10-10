using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Newtonsoft.Json;
using USP.Models.POCO;

namespace USP.Common
{
    public static class MVCTreeExtensions
    {

        public static MvcHtmlString TreeListFor<TModel, TProperty>(
                        this HtmlHelper<TModel> htmlHelper,
                        Expression<Func<TModel, TProperty>> expression,
                        TreeOptions treeOptions,
                        object htmlAttributes
            )
        {
            TagBuilder tagBuilder = new TagBuilder("ul");
            tagBuilder.MergeAttribute("class", "easyui-tree");

           

            //htmlAttributes字典
            var htmlAttributesDic = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            if (treeOptions != null)
            {
                if (treeOptions.data != null && treeOptions.data.Count > 0)
                { //获取model的默认值
                    ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
                    if (metadata.Model!=null)
                    {
                        string defaultValue = metadata.Model.ToString();
                        treeOptions.data = SetDefaultValue(treeOptions.data, defaultValue);
                    }
                }
                tagBuilder.MergeAttribute("data-options", JsonConvert.SerializeObject(treeOptions,
                    Formatting.Indented,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            }
            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));

            if (htmlAttributesDic.Count > 0)
            {
                tagBuilder.MergeAttributes(htmlAttributesDic);
            }

            ////生成name项
            //tagBuilder.MergeAttribute("name", fullHtmlFieldName, true);
            //生成id
            tagBuilder.GenerateId(fullHtmlFieldName+"Tree");
            //增加验证信息
            //tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(ExpressionHelper.GetExpressionText(expression), metadata));

            //string inputHidden = string.Format(@"<input type='hidden' id='{0}' name='{0}' value='{1}'/>",
            //    fullHtmlFieldName, defaultValue);

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }

        public static List<TreeNode> SetDefaultValue(List<TreeNode> nodes, string defaultValue)
        {
            var result = nodes;
            for (int i = 0; i < result.Count; i++)
            {
                if (defaultValue.Split(',').Contains(result[i].id.ToString()))
                {
                    result[i].@checked = true;
                }
                if (result[i].children != null)
                {
                    SetDefaultValue(result[i].children, defaultValue);
                }
            }
            return result;
        }
    }
}
