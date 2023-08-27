using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReferigenatorSvc.Validator
{
    //https://stackoverflow.com/questions/36566836/asp-net-core-mvc-client-side-validation-for-custom-attribute
    //https://stackoverflow.com/questions/53035458/custom-client-side-validation-attribute-with-parameter-in-asp-net-core-using-icl
    //https://stackoverflow.com/questions/64045806/get-object-from-client-validation-attribute
    //IClientModelValidator
    //IClientValidatable
    public sealed class ComparaeValidatorAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string PropertyName;
        private readonly Type PropertyType;

        public ComparaeValidatorAttribute(string propertyName, Type T):base()
        {
            this.PropertyName = propertyName;
            this.PropertyType = T;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            var viewContext = context.ActionContext as ViewContext;
            var modelType = context.ModelMetadata.ContainerType;
            var instance = viewContext?.ViewData.Model;
            var model = instance?.GetType().Name == modelType.Name
                ? instance
                : instance?.GetType()?.GetProperties().First(x => x.PropertyType.Name == modelType.Name)
                    .GetValue(instance, null);
            var objectValue = modelType.GetProperty(this.PropertyName)?.GetValue(model, null);
            //if (this.PropertyType == typeof(float))
            //{
            //    var parsedValue = (float)objectValue;
            //   // MergeAttribute(context.Attributes, "data-val-ComparaeValidator-value", parsedValue.ToString());
            //}
            MergeAttribute(context.Attributes, "data-val-ComparaeValidator-value", $"{modelType.Name}_{this.PropertyName}");
            //MergeAttribute(context.Attributes, $"data-ComparaeValidator-id", $"{modelType.Name}_{this.PropertyName}");
            var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            MergeAttribute(context.Attributes, "data-val-ComparaeValidator", errorMessage);
           
        }



        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyTestedInfo = validationContext.ObjectType.GetProperty(this.PropertyName);
            if (propertyTestedInfo == null)
            {
                return new ValidationResult(string.Format("unknown property {0}", this.PropertyName));
            }

            var propertyTestedValue = propertyTestedInfo.GetValue(validationContext.ObjectInstance, null);


            if (this.PropertyType == typeof(float))
            {
                if (((float)value) <= (float)propertyTestedValue)
                {
                    return ValidationResult.Success;
                }
                
                return new ValidationResult(FormatErrorMessage($"{validationContext.DisplayName} Value shold not be greater than {PropertyName}"));
            }

            //// Compare values
            //if ((DateTime)value >= (DateTime)propertyTestedValue)
            //{
            //    if (this.allowEqualDates && value == propertyTestedValue)
            //    {
            //        return ValidationResult.Success;
            //    }
            //    else if ((DateTime)value > (DateTime)propertyTestedValue)
            //    {
            //        return ValidationResult.Success;
            //    }
            //}

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        //public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        //{
        //    var rule = new ModelClientValidationRule
        //    {
        //        ErrorMessage = this.ErrorMessageString,
        //        ValidationType = "compareValidator"
        //    };
        //    rule.ValidationParameters["propertytested"] = this.PropertyName;
        //    yield return rule;

        //    //var rule = new ModelClientValidationRule();
        //    //rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
        //    //rule.ValidationParameters.Add("chars", _chars);
        //    //rule.ValidationType = "exclude";

        //}




        private bool MergeAttribute(IDictionary<string, string> attributes,string key,string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
