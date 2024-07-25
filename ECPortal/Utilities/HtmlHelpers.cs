#nullable disable

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Pk.Com.Jazz.ECP.Utilities
{
    public static class HtmlHelpers
    {
        public static string DisplayShortNameFor<TModel, TValue>(this IHtmlHelper<IEnumerable<TModel>> t, Expression<Func<TModel, TValue>> exp)
        {
            CustomAttributeNamedArgument? DisplayName = null;
            if (exp.Body is MemberExpression prop)
            {
                var DisplayAttrib = (from c in prop.Member.GetCustomAttributesData()
                                     where c.AttributeType == typeof(DisplayAttribute)
                                     select c).FirstOrDefault();
                if (DisplayAttrib != null)
                {
                    DisplayName = DisplayAttrib.NamedArguments.FirstOrDefault(d => d.MemberName == "ShortName");
                }
            }
            return DisplayName != null ? DisplayName.Value.TypedValue.Value.ToString() : "";
        }

        public static string DisplayShortNameFor<TModel, TValue>(this IHtmlHelper<TModel> t, Expression<Func<TModel, TValue>> exp)
        {
            CustomAttributeNamedArgument? displayName = null;
            var prop = exp.Body as MemberExpression;
            if (prop != null)
            {
                var displayAttribData = prop.Member.GetCustomAttributesData()
                                                   .FirstOrDefault(c => c.AttributeType == typeof(DisplayAttribute));
                if (displayAttribData != null)
                {
                    var shortNameArg = displayAttribData.NamedArguments
                                                      .FirstOrDefault(d => d.MemberName == "ShortName");
                    if (shortNameArg != null)
                    {
                        displayName = shortNameArg;
                    }
                }
            }
            return displayName != null ? displayName.Value.TypedValue.Value?.ToString() : "";

        }

        public static string DisplayDescriptionFor<TModel, TValue>(this IHtmlHelper<IEnumerable<TModel>> t, Expression<Func<TModel, TValue>> exp)
        {
            CustomAttributeNamedArgument? displayName = null;
            var prop = exp.Body as MemberExpression;
            if (prop != null)
            {
                var displayAttribData = prop.Member.GetCustomAttributesData()
                                                   .FirstOrDefault(c => c.AttributeType == typeof(DisplayAttribute));
                if (displayAttribData != null)
                {
                    var descriptionArg = displayAttribData.NamedArguments
                                                        .FirstOrDefault(d => d.MemberName == "Description");
                    if (descriptionArg != null)
                    {
                        displayName = descriptionArg;
                    }
                }
            }
            return displayName != null ? displayName.Value.TypedValue.Value?.ToString() : "";

        }

        public static string DisplayDescriptionFor<TModel, TValue>(this IHtmlHelper<TModel> t, Expression<Func<TModel, TValue>> exp)
        {
            CustomAttributeNamedArgument? displayName = null;
            var prop = exp.Body as MemberExpression;
            if (prop != null)
            {
                var displayAttribData = prop.Member.GetCustomAttributesData()
                                                   .FirstOrDefault(c => c.AttributeType == typeof(DisplayAttribute));
                if (displayAttribData != null)
                {
                    var descriptionArg = displayAttribData.NamedArguments
                                                        .FirstOrDefault(d => d.MemberName == "Description");
                    if (descriptionArg != null)
                    {
                        displayName = descriptionArg;
                    }
                }
            }
            return displayName?.TypedValue.Value?.ToString() ?? "";

        }

        public static string DisplayPromptFor<TModel, TValue>(this IHtmlHelper<IEnumerable<TModel>> t, Expression<Func<TModel, TValue>> exp)
        {
            CustomAttributeNamedArgument? displayName = null;
            var prop = exp.Body as MemberExpression;
            if (prop != null)
            {
                var displayAttribData = prop.Member.GetCustomAttributesData()
                                                   .FirstOrDefault(c => c.AttributeType == typeof(DisplayAttribute));
                if (displayAttribData != null)
                {
                    var promptArg = displayAttribData.NamedArguments
                                                    .FirstOrDefault(d => d.MemberName == "Prompt");
                    if (promptArg != null)
                    {
                        displayName = promptArg;
                    }
                }
            }
            return displayName?.TypedValue.Value?.ToString() ?? "";

        }

        public static string DisplayPromptFor<TModel, TValue>(this IHtmlHelper<TModel> t, Expression<Func<TModel, TValue>> exp)
        {
            CustomAttributeNamedArgument? displayName = null;
            var prop = exp.Body as MemberExpression;
            if (prop != null)
            {
                var displayAttribData = prop.Member.GetCustomAttributesData()
                                                   .FirstOrDefault(c => c.AttributeType == typeof(DisplayAttribute));
                if (displayAttribData != null)
                {
                    var promptArg = displayAttribData.NamedArguments
                                                    .FirstOrDefault(d => d.MemberName == "Prompt");
                    if (promptArg != null)
                    {
                        displayName = promptArg;
                    }
                }
            }
            return displayName?.TypedValue.Value?.ToString() ?? "";

        }
    }
}
