﻿using EDSCore;

namespace Hub.Dominio.ValueObjects
{
    public record CPF : ValueObject
    {

        private CPF() { }
        public CPF(string caracteres)
        {
            Texto = caracteres; 
        }

        public string Texto { get; private set; }
    }
}
