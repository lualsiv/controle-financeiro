﻿namespace ControleFinanceiro.DomainBase.Exceptions
{
    public class ModelValidationError : CoreError
    {
        public ModelValidationError(string key, string message) : base(key, message)
        {
        }
    }
}

