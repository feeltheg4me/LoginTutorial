using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace LoginTutorial.Helpers
{
    public static class VisualTreeHelper
    {
        public static T GetParent<T>(this Element element) where T : Element
        {
            try
            {
                if (element is T)
                {
                    return element as T;
                }
                else
                {
                    if (element.Parent != null)
                    {
                        return element.Parent.GetParent<T>();
                    }

                    return default(T);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static IEnumerable<T> GetChildren<T>(this Element element) where T : Element
        {
            var properties = element.GetType().GetRuntimeProperties();

            var contentProperty = properties.FirstOrDefault(w => w.Name == "Content");
            if (contentProperty != null)
            {
                var content = contentProperty.GetValue(element) as Element;
                var visualElement = content as VisualElement;

                if (content != null && visualElement.IsVisible == true)
                {
                    if (content is T)
                    {
                        yield return content as T;
                    }
                    foreach (var child in content.GetChildren<T>())
                    {
                        yield return child;
                    }
                }
            }
            else
            {
                var childrenProperty = properties.FirstOrDefault(w => w.Name == "Children");
                if (childrenProperty != null)
                {
                    IEnumerable children = childrenProperty.GetValue(element) as IEnumerable;
                    foreach (var child in children)
                    {
                        var childElement = child as Element;
                        var childVisualEelement = childElement as VisualElement;
                        if (childElement != null && childVisualEelement.IsVisible == true)
                        {
                            if (childElement is T)
                            {
                                yield return childElement as T;
                            }
                            foreach (var childVisual in childElement.GetChildren<T>())
                            {
                                yield return childVisual;
                            }
                        }
                    }
                }
            }
        }

        public static IEnumerable<T> GetAllChildren<T>(this Element element) where T : Element
        {
            var properties = element.GetType().GetRuntimeProperties();

            var contentProperty = properties.FirstOrDefault(w => w.Name == "Content");
            if (contentProperty != null)
            {
                var content = contentProperty.GetValue(element) as Element;
                var visualElement = content as VisualElement;

                if (content != null)
                {
                    if (content is T)
                    {
                        yield return content as T;
                    }
                    foreach (var child in content.GetAllChildren<T>())
                    {
                        yield return child;
                    }
                }
            }
            else
            {
                var childrenProperty = properties.FirstOrDefault(w => w.Name == "Children");
                if (childrenProperty != null)
                {
                    IEnumerable children = childrenProperty.GetValue(element) as IEnumerable;
                    foreach (var child in children)
                    {
                        var childElement = child as Element;
                        var childVisualEelement = childElement as VisualElement;
                        if (childElement != null)
                        {
                            if (childElement is T)
                            {
                                yield return childElement as T;
                            }
                            foreach (var childVisual in childElement.GetAllChildren<T>())
                            {
                                yield return childVisual;
                            }
                        }
                    }
                }
            }
        }
    }
}