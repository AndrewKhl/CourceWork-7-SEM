using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class ObservableModel : INotifyPropertyChanged
    {
        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void NotifyPropertyChanged([CallerMemberName] string prop = null)
        {
            OnPropertyChanged(prop);
        }

        protected void NotifyPropertyChanged<T>(Expression<Func<T>> propertyName)
        {
            OnPropertyChanged(GetPropertyName(propertyName));
        }

        public string GetPropertyName<T>(Expression<Func<T>> propertyName)
        {
            if (!(propertyName.Body is MemberExpression body))
                throw new ArgumentException("'propertyExpression' should be a member expression");

            return body.Member.Name;
        }

        private void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }


    public class ValidationObservableModel : ObservableModel, IDataErrorInfo
    {
        #region Error handling and notifier stuff
        protected Dictionary<string, ValidationAttribute[]> Validators;
        public ValidationObservableModel()
        {
            Validators = GetType().GetProperties()
                        .Where(p => GetValidations(p).Length != 0)
                        .ToDictionary(p => p.Name, p => GetValidations(p));
        }

        protected ValidationAttribute[] GetValidations(PropertyInfo property)
        {
            return (ValidationAttribute[])property.GetCustomAttributes(typeof(ValidationAttribute), true);
        }

        public virtual string this[string columnName]
        {
            get
            {
                if (Validators.ContainsKey(columnName))
                {
                    var value = GetType().GetProperty(columnName).GetValue(this, null);
                    ValidationContext context = new ValidationContext(this) { MemberName = columnName };

                    var errors = Validators[columnName].Where(v =>
                    {
                        ValidationResult result = v.GetValidationResult(value, context);

                        if (result != null && result != ValidationResult.Success)
                        {
                            if ((v.ErrorMessage == null && !string.IsNullOrEmpty(result.ErrorMessage)))
                            {
                                v.ErrorMessage = result.ErrorMessage;
                            }
                        }

                        return result != ValidationResult.Success;
                    }).ToList();

                    var errorMessages = errors.Select(v => v.ErrorMessage).ToArray();
                    NotifyPropertyChanged(() => Error);
                    return string.Join(Environment.NewLine, errorMessages);
                }

                NotifyPropertyChanged(() => Error);
                return string.Empty;
            }
        }

        public virtual string Error
        {
            get
            {
                List<string> errorsMsg = new List<string>();
                foreach (var i in Validators)
                {
                    foreach (var v in i.Value)
                    {
                        if (v.GetValidationResult(GetType().GetProperty(i.Key).GetValue(this, null), new ValidationContext(this) { MemberName = i.Key }) != ValidationResult.Success)
                        {
                            errorsMsg.Add(v.ErrorMessage);
                        }
                    }
                }
                string result = string.Join(Environment.NewLine, errorsMsg);
                return result;
            }
        }

        public bool IsError
        {
            get { return !string.IsNullOrEmpty(Error); }
        }

        #endregion
    }
}
