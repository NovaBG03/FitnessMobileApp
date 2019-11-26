namespace MobileFitness.App.ViewModels
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Абстрактен клас за ViewModel
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Евент за промяна на свойство
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Създава нов ViewModel
        /// </summary>
        protected BaseViewModel()
        {

        }

        /// <summary>
        /// Известява, че стойността на свойството се е променила
        /// </summary>
        /// <param name="propertyName">име на свойството</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Изписва грешка на потребителя
        /// </summary>
        public Action<string> DisplayInvalidPrompt { get; set; }
    }
}
