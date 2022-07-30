using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace LoginTutorial.Helpers
{
    public static class ValidationHelper
    {
        

        public static bool IsFormValid(object model, VisualElement view)
        {
            try
            {
                bool ret = false;
                Device.BeginInvokeOnMainThread(() =>
                {
                    HideValidationFields(model, view);
                });

                var errors = new List<ValidationResult>();
                var context = new ValidationContext(model);
                bool isValid = Validator.TryValidateObject(model, context, errors, true);
                if (!isValid)
                {
                    
                    ShowValidationFields(errors, model, view);
                }
                ret = (errors.Count() == 0);
                return ret;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static void HideValidationFields
            (object model, VisualElement view, string validationLabelSuffix = "Error")
        {
            try
            {
                if (model == null) { return; }
                var properties = GetValidatablePropertyNames(model);
                foreach (var propertyName in properties)
                {
                    var errorControlName =
                    $"{propertyName.Replace(".", "_")}{validationLabelSuffix}";
                    var control = view.FindByName<Label>(errorControlName);
                    if (control != null)
                    {
                        control.IsVisible = false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void ShowValidationFields
        (List<ValidationResult> errors,
        object model, VisualElement view, string validationLabelSuffix = "Error")
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    try
                    {
                        if (model == null) { return; }
                        foreach (var error in errors)
                        {
                            var memberName = $"{model.GetType().Name}_{error.MemberNames.FirstOrDefault()}";
                            memberName = memberName.Replace(".", "_");
                            var errorControlName = $"{memberName}{validationLabelSuffix}";
                            var control = view.FindByName<Label>(errorControlName);
                            if (control != null)
                            {
                                control.Text = $"{error.ErrorMessage}{Environment.NewLine}";
                                control.IsVisible = true;
                            }
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static IEnumerable<string> GetValidatablePropertyNames(object model)
        {
            try
            {
                var validatableProperties = new List<string>();
                var properties = GetValidatableProperties(model);
                foreach (var propertyInfo in properties)
                {
                    var errorControlName = $"{propertyInfo.DeclaringType.Name}.{propertyInfo.Name}";
                    validatableProperties.Add(errorControlName);
                }
                return validatableProperties;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static List<PropertyInfo> GetValidatableProperties(object model)
        {
            try
            {
                var properties = model.GetType().GetProperties().Where(prop => prop.CanRead
                       && prop.GetCustomAttributes(typeof(ValidationAttribute), true).Any()
                       && prop.GetIndexParameters().Length == 0).ToList();
                return properties;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
